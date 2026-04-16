using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEditor.Compilation;

namespace Reign.Generics
{
    public sealed class SaveFileHandler
    {
        public byte[] Encrypt(string plaintext)
        {
            return (byte[])EncryptRoutine(plaintext).Current;
        }

        private IEnumerator EncryptRoutine(string plaintext)
        {
            using Aes aes = Aes.Create();

            aes.BlockSize = 128;
            aes.KeySize = 64;

            byte[] password = Encoding.ASCII.GetBytes(GameCertificates.SAVE_PASSWORD);
            byte[] salt = Encoding.UTF8.GetBytes(GameCertificates.SAVE_SALT.ToCharArray());

            using var key = new Rfc2898DeriveBytes(
                password,
                salt,
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
    }
}