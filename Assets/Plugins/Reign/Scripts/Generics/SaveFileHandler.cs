using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Reign.Systems;
using UnityEngine;

namespace Reign.Generics
{
    public sealed class SaveFileHandler
    {
        byte[] passCache => Encoding.ASCII.GetBytes(GameCertificates.SAVE_PASSWORD);
        byte[] saltCache => Encoding.UTF8.GetBytes(GameCertificates.SAVE_SALT.ToCharArray());

        byte[] Encrypt(string plaintext)
        {
            return (byte[])EncryptRoutine(plaintext).Current;
        }

        string Decrypt(byte[] cipher)
        {
            return (string)DecryptRoutine(cipher).Current;
        }

        public IEnumerator EncryptRoutine(string plaintext)
        {
            using Aes aes = Aes.Create();

            aes.BlockSize = 128;
            aes.KeySize = 64;

            using var key = new Rfc2898DeriveBytes(
                passCache,
                saltCache,
                GameCertificates.SAVE_ITERATIONS,
                HashAlgorithmName.SHA256
            );

            aes.Key = key.GetBytes(32);
            aes.GenerateIV();

            using MemoryStream memStream = new();
            memStream.Write(aes.IV, 0, aes.IV.Length);

            using (CryptoStream cryptoStream = new(memStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
            using (StreamWriter streamWriter = new(cryptoStream))
            {
                streamWriter.Write(plaintext);
            }

            yield return memStream.ToArray();
        }

        public IEnumerator DecryptRoutine(byte[] cipher)
        {
            using Aes aes = Aes.Create();

            aes.BlockSize = 128;
            aes.KeySize = 64;

            using var key = new Rfc2898DeriveBytes(
                passCache,
                saltCache,
                GameCertificates.SAVE_ITERATIONS,
                HashAlgorithmName.SHA256
            );

            aes.Key = key.GetBytes(32);

            byte[] processedIV = new byte[16];

            using MemoryStream memStream = new(cipher);

            memStream.Read(processedIV, 0, processedIV.Length);

            using CryptoStream cryptoStream = new(memStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
            using StreamReader streamReader = new(cryptoStream);

            yield return streamReader.ReadToEndAsync().ToString();
        }

        public IEnumerator Load()
        {
            var path = Path.Combine(Application.persistentDataPath, GameCertificates.SAVE_FILE_DIRECTORY);
            GameData gameDataLoaded = null;

            if (!File.Exists(path))
            {
                try
                {
                    byte[] fileBytes = File.ReadAllBytes(path);

                    string json;

                    if (GameCertificates.SAVE_ENCRYPT)
                    {
                        json = Decrypt(fileBytes);
                    }
                    else
                    {
                        json = Encoding.UTF8.GetString(fileBytes);
                    }

                    gameDataLoaded = JsonUtility.FromJson<GameData>(json);
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Could not load data: {e}");
                }
            }
            else
            {
                // Save new data
            }

            yield return gameDataLoaded;
        }
    }
}