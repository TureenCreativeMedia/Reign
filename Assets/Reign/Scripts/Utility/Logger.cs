using System.Collections.Generic;
using UnityEngine;

namespace reign
{
    public class Logger : Singleton<Logger>
    {
        private bool bool_Log;
        public enum enum_LogIntensity 
        {
            Log,
            Warning,
            Error
        }
        private readonly Dictionary<enum_LogIntensity, string> Dictionary_LogColors = new()
        {
            {enum_LogIntensity.Log, "#53E107"},
            {enum_LogIntensity.Warning, "#F5A207" },
            {enum_LogIntensity.Error, "#F5073F" }
        };
        public void Log(enum_LogIntensity INTENSITY, string MESSAGE, bool USECOLORS = true)
        {
            if (bool_Log != App.Instance.AppData_App.bool_DoLogging) { bool_Log = App.Instance.AppData_App.bool_DoLogging; }
            if (!bool_Log) return;

            if(USECOLORS && Dictionary_LogColors.TryGetValue(INTENSITY, out string string_TextColor))
            {
                Debug.Log($"<color={string_TextColor}>{INTENSITY}: {MESSAGE}</color>");
            }
            else
            {
                Debug.Log($"{INTENSITY}: {MESSAGE}");
            }
        }
    }
}
