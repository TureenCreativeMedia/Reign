using System.Collections.Generic;
using UnityEngine;

namespace reign
{
    public static class Time
    {
        public static float float_ReignTimeScale => 1.0f;
        public static float float_ReignDeltaTime => UnityEngine.Time.deltaTime * float_ReignTimeScale;

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
