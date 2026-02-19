using System.Collections.Generic;
using UnityEngine;

namespace reign
{
    public class Logger : Singleton<Logger>
    {
        public enum enum_LogIntensity 
        {
            Log,
            Warning,
            Error
        }
        private readonly Dictionary<enum_LogIntensity, string> Dictionary_LogColors = new()
        {
            {enum_LogIntensity.Log, "#53e107"},
            {enum_LogIntensity.Warning, "#F5A207" },
            {enum_LogIntensity.Error, "#F5073F" }
        };
        public void Log(enum_LogIntensity INTENSITY, string MESSAGE)
        {
            Dictionary_LogColors.TryGetValue(INTENSITY, out string string_TextColor);
            Debug.Log($"<color={string_TextColor}>{MESSAGE}</color>");
        }
    }
}
