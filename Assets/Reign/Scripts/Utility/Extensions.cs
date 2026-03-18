using System.Collections.Generic;
using UnityEngine;

namespace reign
{
    public static class Time
    {
        public static float float_UnscaledDeltaTime => UnityEngine.Time.unscaledDeltaTime;
        public static float float_DeltaTime => UnityEngine.Time.deltaTime;

        /// <summary>
        /// Convert time from seconds to MM:SS format
        /// </summary>
        public static string ConvertToMMSS(float SECONDS)
        {
            SECONDS = Mathf.FloorToInt(SECONDS);
            return $"{SECONDS / 60}:{SECONDS % 60:00}";
        }
    }
}
