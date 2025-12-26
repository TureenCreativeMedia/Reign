using UnityEngine;

namespace reign.Extensions
{
    public class KeyGenerator : MonoBehaviour
    {
        public static string GenerateKey(int length = 16)
        {
            string running = "";
            const string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            for (int i = 0; i < length; ++i)
            {
                running += characters[Random.Range(0, characters.Length)];
            }
            return running;
        }
    }

    public class Misc : MonoBehaviour
    {
        public static void ToggleScriptArray(MonoBehaviour[] scripts, bool enabled)
        {
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = enabled;
            }
        }
    }
}