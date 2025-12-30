using System;
using UnityEngine;

namespace reign.Extensions
{
    public class KeyGenerator
    {
        public static string GenerateKey(int length = 16)
        {
            string running = "";
            const string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            for (int i = 0; i < length; ++i)
            {
                running += characters[UnityEngine.Random.Range(0, characters.Length)];
            }
            return running;
        }
    }

    public class Time
    {
        public enum FormatType { MinutesSeconds, HoursMinutesSeconds };
        public static string Format(FormatType formatType, float time)
        {
            TimeSpan span = TimeSpan.FromSeconds(time);
            switch (formatType)
            {
                case FormatType.MinutesSeconds:
                    {
                        return string.Format("{0:00}:{1:00}", span.Minutes, span.Seconds);
                    }
                case FormatType.HoursMinutesSeconds:
                    {
                        return string.Format("{0:00}:{1:00}:{2:00}", span.Hours, span.Minutes, span.Seconds);
                    }
            }
            return "Time String Null Formatting Error";
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