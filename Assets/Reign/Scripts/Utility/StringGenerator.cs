using UnityEngine;

namespace Reign
{
    public class StringGenerator : MonoBehaviour
    {
        public string Generate(string CIPHER = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789$£&^*()#+-_", int LENGTH = 24)
        {
            string string_Running = "";
            for (int i = 0; i < LENGTH; ++i)
            {
                string_Running += CIPHER[i];
            }
            return string_Running;
        }
    }
}
