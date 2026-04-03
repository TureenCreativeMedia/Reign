using Reign.Generic;
using Reign.Systems.App;
using Reign.Systems.Rendering;
using Reign.Systems.Screen;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Reign.Systems.Save
{
    [Serializable]
    public class GameData
    {
        public string PostProcessProfile_ID = "VolumeProfile";
        public ScreenSettings ScreenSettings_Data;
        public Dictionary<string, KeyCode[]> Dictionary_BindedInputs;
        public GameData()
        {
            ScreenSettings_Data = new ScreenSettings()
            {
                bool_Fullscreen = true,
                bool_VSync = false,
                Vector2_ScreenResolution = new(-1, -1),
                AntialiasingMode_Antialiasing = AntialiasingMode.None
            };
            Dictionary_BindedInputs = new Dictionary<string, KeyCode[]>()
            {
                // Add inputs here!
                {"Any", new KeyCode[]{KeyCode.None}}
            };
        }
    }
    public interface IDataHandler
    {
        void LoadData(GameData DATA);
        void SaveData(ref GameData DATA);
    }

    public class SaveSystem : Singleton<SaveSystem>
    {
        protected SaveFileHandler SaveFileHandler_Handler;
        protected GameData GameData_Data;
        protected List<IDataHandler> List_DataHandlerObjects;

        IEnumerator Start()
        {
            yield return new WaitUntil(() => InitialiseDataHandlerObjects());
            LoadGameData();
        }
        protected List<IDataHandler> FindDataHandlerObjects()
        {
            IEnumerable<IDataHandler> IEnumerable_DataHandlerObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataHandler>();
            return new List<IDataHandler>(IEnumerable_DataHandlerObjects);
        }
        protected bool InitialiseDataHandlerObjects()
        {
            List_DataHandlerObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataHandler>().ToList();
            return List_DataHandlerObjects != null;
        }
        protected void OnApplicationQuit()
        {
            if (AppSystem.Instance.AppData_Data.bool_SaveOnQuit)
            {
                SaveGameData();
            }
        }
        public void LoadGameData()
        {
            if (!AppSystem.Instance.AppData_Data.bool_SaveSystem)
            {
                return;
            }

            if (SaveFileHandler_Handler == null)
            {
                SaveFileHandler_Handler = new SaveFileHandler();
            }

            GameData_Data = SaveFileHandler_Handler.Load();

            if (GameData_Data == null)
            {
                Debug.LogWarning("SaveSystem: Game Data could not be found, creating new game data.");
                GameData_Data = new GameData();
            }
            else
            {
                Debug.Log("SaveSystem: Game Data loaded successfully.");
            }

            foreach (IDataHandler HANDLER in List_DataHandlerObjects)
            {
                if (HANDLER == null) continue;

                HANDLER?.LoadData(GameData_Data);
            }
        }
        public void SaveGameData()
        {
            if (!AppSystem.Instance.AppData_Data.bool_SaveSystem)
            {
                return;
            }

            if (GameData_Data == null)
            {
                Debug.LogWarning("SaveSystem: Game Data is null and could not be saved.");
                GameData_Data = new GameData();
            }

            foreach (IDataHandler HANDLER in List_DataHandlerObjects)
            {
                if (HANDLER == null) continue;

                HANDLER?.SaveData(ref GameData_Data);
            }

            SaveFileHandler_Handler.Save(GameData_Data);

            foreach (IDataHandler HANDLER in List_DataHandlerObjects)
            {
                if (HANDLER == null) continue;

                HANDLER?.LoadData(GameData_Data);
            }

            Debug.Log("SaveSystem: Game Data saved.");
        }
    }
    public class SaveFileHandler
    {
        private bool bool_Encrypt;
        private string string_FileName;

        public SaveFileHandler()
        {
            string_FileName = AppSystem.Instance.AppData_Data.string_SaveFileName;
            bool_Encrypt = AppSystem.Instance.AppData_Data.bool_EncryptSaves;
        }

        private byte[] Encrypt(string PLAIN)
        {
            using Aes AES = Aes.Create();
            AES.KeySize = 256;
            AES.BlockSize = 128;

            byte[] byte_Password = Encoding.ASCII.GetBytes(AppSystem.Instance.AppData_Data.string_Password);
            char[] char_Salt = AppSystem.Instance.AppData_Data.string_Salt.ToCharArray();

            using var KEY = new Rfc2898DeriveBytes(
                byte_Password,
                Encoding.UTF8.GetBytes(char_Salt),
                AppSystem.Instance.AppData_Data.int_EncryptionIterations,
                HashAlgorithmName.SHA256);

            AES.Key = KEY.GetBytes(32);
            AES.GenerateIV();

            using MemoryStream MEMSTREAM = new();

            MEMSTREAM.Write(AES.IV, 0, AES.IV.Length);

            using (CryptoStream CRPSTREAM = new(MEMSTREAM, AES.CreateEncryptor(), CryptoStreamMode.Write))
            using (StreamWriter STREAMWRT = new(CRPSTREAM))
            {
                STREAMWRT.Write(PLAIN);
            }

            return MEMSTREAM.ToArray();
        }
        private string Decrypt(byte[] CIPHER)
        {
            using Aes AES = Aes.Create();
            AES.KeySize = 256;
            AES.BlockSize = 128;

            byte[] byte_Password = Encoding.ASCII.GetBytes(AppSystem.Instance.AppData_Data.string_Password);
            char[] char_Salt = AppSystem.Instance.AppData_Data.string_Salt.ToCharArray();

            using var KEY = new Rfc2898DeriveBytes(
                byte_Password,
                Encoding.UTF8.GetBytes(char_Salt),
                AppSystem.Instance.AppData_Data.int_EncryptionIterations,
                HashAlgorithmName.SHA256);

            AES.Key = KEY.GetBytes(32);

            using MemoryStream MEMSTREAM = new(CIPHER);

            byte[] IV = new byte[16];
            MEMSTREAM.Read(IV, 0, IV.Length);
            AES.IV = IV;

            using CryptoStream CRPSTREAM = new(MEMSTREAM, AES.CreateDecryptor(), CryptoStreamMode.Read);
            using StreamReader STREAMWRT = new(CRPSTREAM);

            return STREAMWRT.ReadToEnd();
        }

        public GameData Load()
        {
            string string_FullPath = Path.Combine(Application.persistentDataPath, string_FileName);
            GameData GameData_Loaded = null;

            if (File.Exists(string_FullPath))
            {
                try
                {
                    byte[] byte_FileBytes = File.ReadAllBytes(string_FullPath);

                    string json;

                    if (bool_Encrypt)
                    {
                        json = Decrypt(byte_FileBytes);
                    }
                    else
                    {
                        json = Encoding.UTF8.GetString(byte_FileBytes);
                    }

                    return JsonUtility.FromJson<GameData>(json);
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"SaveSystem: Could not load data: {e}");
                }
            }
            else
            {
                Save(new GameData());
            }

            return GameData_Loaded;
        }
        public void Save(GameData DATA)
        {
            string string_FullPath = Path.Combine(Application.persistentDataPath, string_FileName);
            try
            {
                string string_Directory = Path.GetDirectoryName(string_FullPath);
                if (!string.IsNullOrEmpty(string_Directory))
                {
                    Directory.CreateDirectory(string_Directory);
                }

                string string_JSON = JsonUtility.ToJson(DATA, true);

                byte[] byte_FileBytes;

                if (bool_Encrypt)
                {
                    byte_FileBytes = Encrypt(string_JSON);
                }
                else
                {
                    byte_FileBytes = Encoding.UTF8.GetBytes(string_JSON);
                }
                File.WriteAllBytes(string_FullPath, byte_FileBytes);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"SaveSystem: Could not save data: {e}");
            }
        }
    }
}