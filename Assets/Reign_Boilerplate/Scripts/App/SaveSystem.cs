using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace reign
{
    [Serializable]
    public class PlayerData
    {
        public Resolution u_ScreenResolution; // Screen resolution, by default what monitor it runs on.
    }

    public class SaveSystem : MonoBehaviour
	{
        public static Action a_OnSave;
        public static Action a_OnLoad;

        private static CipherMode u_EncryptionMode = CipherMode.ECB;

        // Change these
        private static byte[] key = Encoding.UTF8.GetBytes("9GolHfQ8uHS7FOhU");
        private static byte[] IV = Encoding.UTF8.GetBytes("6434u4wX0zcmPz1E");

        public static PlayerData u_PlayerData = new PlayerData();

        private static string s_Path;

        private void Awake()
        {
            s_Path = Path.Combine(Application.persistentDataPath, "savefile.save");
            Load();
        }

        private static byte[] Encrypt(string plainText)
        {
            using Aes aes = Aes.Create();
            aes.Mode = u_EncryptionMode;
            aes.Key = key;
            aes.IV = IV;

            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            return aes.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
        }

        private static string Decrypt(byte[] cipher)
        {
            using Aes aes = Aes.Create();
            aes.Mode = u_EncryptionMode;
            aes.Key = key;
            aes.IV = IV;

            byte[] decrypted = aes.CreateDecryptor().TransformFinalBlock(cipher, 0, cipher.Length);
            return Encoding.UTF8.GetString(decrypted);
        }

        public static void Save()
        {
            string json = JsonUtility.ToJson(u_PlayerData, true);
            byte[] encrypted = Encrypt(json);
            File.WriteAllBytes(s_Path, encrypted);

            a_OnSave?.Invoke();

            Debug.Log("Saved PlayerData");
        }

        public static void Load()
        {
            if (!File.Exists(s_Path))
            {
                Debug.Log("No save file found, creating new data");
                u_PlayerData = new PlayerData();
                Save();
                return;
            }

            try
            {
                byte[] encrypted = File.ReadAllBytes(s_Path);
                string json = Decrypt(encrypted);

                a_OnLoad?.Invoke();
                Complete(json);
                Debug.Log("Loaded PlayerData");
            }
            catch
            {
                Debug.LogWarning("Save file corrupted, creating new save");
                u_PlayerData = new PlayerData();
                a_OnLoad?.Invoke();
            }
        }

        private static void Complete(string json)
        {
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            // Add each data variable relevant to each saved
            // eg. u_PlayerData.variable = data.variable

            u_PlayerData.u_ScreenResolution = data.u_ScreenResolution;
        }
    }
}