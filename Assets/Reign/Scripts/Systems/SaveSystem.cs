using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace reign
{
    public struct ScreenResolution
    {
        public float float_Brightness;
        public int int_Width; 
        public int int_Height;
        public int int_Hz;
        public bool bool_Fullscreen;
        public bool bool_VSync;
        public ScreenResolution(float BRIGHTNESS, int WIDTH, int HEIGHT, bool FULLSCREEN, bool VSYNC, int HZ = -1)
        {
            float_Brightness = BRIGHTNESS;
            int_Width = WIDTH;
            int_Height = HEIGHT;
            bool_Fullscreen = FULLSCREEN;
            bool_VSync = VSYNC;
            int_Hz = HZ;
        }
    }

    [Serializable]
    public class KeyBinding
    {
        public string Key;
        public KeyCode[] KeyCodes;

        public KeyBinding(string KEY, KeyCode[] CODES)
        {
            Key = KEY;
            KeyCodes = CODES;
        }
    }

    [Serializable]
    public class PostProcessData
    {
        public float float_BloomIntensity;
        public float float_VignetteIntensity;
        public float float_ChromaticAberrationIntensity;
        public float float_LensDistortionIntensity;
        public float float_FilmGrainIntensity;
        public float float_MotionBlurIntensity;
        public bool bool_PostProcessingEnabled;
        public PostProcessData()
        {
            float_BloomIntensity = 0f;
            float_VignetteIntensity = 0f;
            float_ChromaticAberrationIntensity = 0f;
            float_LensDistortionIntensity = 0f;
            float_FilmGrainIntensity = 0f;
            float_MotionBlurIntensity = 0f;
            bool_PostProcessingEnabled = true;
        }

    }


    [Serializable]
    public class GameData
    {
        public ScreenResolution ScreenResolution_Resolution;
        public PostProcessData PostProcessData_PostProcessData;
        public List<KeyBinding> List_InputKeys = new List<KeyBinding>()
        {
            // Misc
            new("Any", new[]{KeyCode.None}),

            // Grouped
            new("Alphanumeric", new[]{KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4,
            KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9}),
            
            // Special Keys
            new("BackQuote", new[]{KeyCode.BackQuote}),
            new("F1", new[]{KeyCode.F1}),
            new("F2", new[]{KeyCode.F2}),
            new("F3", new[]{KeyCode.F3}),
            new("F4", new[]{KeyCode.F4}),
            new("F5", new[]{KeyCode.F5}),
            new("F6", new[]{KeyCode.F6}),
            new("F7", new[]{KeyCode.F7}),
            new("F8", new[]{KeyCode.F8}),
            new("F9", new[]{KeyCode.F9}),
            new("F10", new[]{KeyCode.F10}),
            new("F11", new[]{KeyCode.F11}),
            new("F12", new[]{KeyCode.F12}),
            
            // Common
            new("W", new[]{KeyCode.W}),
            new("A", new[]{KeyCode.A}),
            new("S", new[]{KeyCode.S}),
            new("D", new[]{KeyCode.D}),
            new("E", new[]{KeyCode.E}),
            new("F", new[]{KeyCode.F}),

            // Mouse
            new("LeftMouse", new[]{KeyCode.Mouse0}),
            new("MiddleMouse", new[]{KeyCode.Mouse2}),
            new("RightMouse", new[]{KeyCode.Mouse1}),
            new("BackSideMouse", new[]{KeyCode.Mouse4}),
            new("FrontSideMouse", new[]{KeyCode.Mouse5}),

            // External
            new("Screenshot", new[]{KeyCode.F2}),
        };
        public GameData()
        {
            ScreenResolution_Resolution = new() { float_Brightness = 1f, int_Width = 1280, int_Height = 720, bool_Fullscreen = false, bool_VSync = true, int_Hz = -1 };
            PostProcessData_PostProcessData = new();
            List_InputKeys = new List<KeyBinding>(List_InputKeys);
        }
    }

    public interface IDataHandler
    {
        void LoadData(GameData DATA);
        void SaveData(ref GameData DATA);
    }

    public class SaveSystem : BaseSystem
    {
        [SerializeField] string string_FileName = "save.reign";
        [SerializeField] bool bool_Encrypt = true;

        public static System.Action Action_AttemptLoad;
        public static System.Action Action_AttemptSave;

        protected SaveFileHandler SaveFileHandler_Handler;

        protected GameData GameData_Data;
        protected List<IDataHandler> List_DataHandlerObjects;

        private void OnEnable()
        {
            OriginSystem.Action_OnAwake += SaveSystemAwake;
            OriginSystem.Action_OnGameQuit += SaveGameData;
            Action_AttemptLoad += LoadGameData;
            Action_AttemptSave += SaveGameData;
        }
        private void OnDisable()
        {
            OriginSystem.Action_OnAwake -= SaveSystemAwake;
            OriginSystem.Action_OnGameQuit -= SaveGameData;
            Action_AttemptLoad -= LoadGameData;
            Action_AttemptSave -= SaveGameData;
        }
        protected List<IDataHandler> FindDataHandlerObjects()
        {
            IEnumerable<IDataHandler> IEnumerable_DataHandlerObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataHandler>();
            return new List<IDataHandler>(IEnumerable_DataHandlerObjects);
        }
        protected void InitializeDataHandlerObjects()
        {
            List_DataHandlerObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataHandler>().ToList();
        }

        protected void SaveSystemAwake()
        {
            SaveFileHandler_Handler = new(string_FileName, bool_Encrypt);
            InitializeDataHandlerObjects();
            LoadGameData();
        }
        public void CreateNewGameData()
        {
            GameData_Data = new GameData();
        }
        public void LoadGameData()
        {
            GameData_Data = SaveFileHandler_Handler.Load();
            if (GameData_Data == null)
            {
                CreateNewGameData();
            }

            foreach (IDataHandler HANDLER in List_DataHandlerObjects)
            {
                HANDLER.LoadData(GameData_Data);
            }

            Logger.Instance.Log(Logger.enum_LogIntensity.Log, "Game Data loaded successfully.");
        }
        public void SaveGameData()
        {
            foreach (IDataHandler HANDLER in List_DataHandlerObjects)
            {
                HANDLER.SaveData(ref GameData_Data);
            }

            SaveFileHandler_Handler.Save(GameData_Data);

            foreach (IDataHandler HANDLER in List_DataHandlerObjects)
            {
                HANDLER.LoadData(GameData_Data);
            }

            Logger.Instance.Log(Logger.enum_LogIntensity.Log, "Game Data saved.");
        }
    }
    public class SaveFileHandler
    {
        private bool bool_Encrypt;
        private string string_FileName;

        // Make this unique per project
        private const string string_PASSWORD = "reign_PASSWORD";
        private const string string_SALT = "reign_SALT";

        public SaveFileHandler(string FILENAME, bool ENCRYPT)
        {
            string_FileName = FILENAME;
            bool_Encrypt = ENCRYPT;
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
                    Logger.Instance.Log(Logger.enum_LogIntensity.Error, $"Could not load data: {e}");
                }
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
                Logger.Instance.Log(Logger.enum_LogIntensity.Error, $"Could not save data: {e}");
            }
        }

        // ------------------------------------ //
        // Secure AES encryption and decryption //
        // ------------------------------------ //
        private byte[] Encrypt(string PLAIN)
        {
            using Aes AES = Aes.Create();
            AES.KeySize = 256;
            AES.BlockSize = 128;

            using var key = new Rfc2898DeriveBytes(
                string_PASSWORD,
                Encoding.UTF8.GetBytes(string_SALT),
                100000,
                HashAlgorithmName.SHA256);

            AES.Key = key.GetBytes(32);
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

            using var key = new Rfc2898DeriveBytes(
                string_PASSWORD,
                Encoding.UTF8.GetBytes(string_SALT),
                100000,
                HashAlgorithmName.SHA256);

            AES.Key = key.GetBytes(32);

            using MemoryStream MEMSTREAM = new(CIPHER);

            byte[] IV = new byte[16];
            MEMSTREAM.Read(IV, 0, IV.Length);
            AES.IV = IV;

            using CryptoStream CRPSTREAM = new(MEMSTREAM, AES.CreateDecryptor(), CryptoStreamMode.Read);
            using StreamReader STREAMWRT = new(CRPSTREAM);

            return STREAMWRT.ReadToEnd();
        }
    }
}