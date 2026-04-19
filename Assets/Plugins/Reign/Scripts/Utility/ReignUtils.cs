using UnityEngine;

namespace Reign.Utility
{
    public static class ReignUtils
    {
        public static float FloatToDecibels(float lin)
        {
            float dB = 0.0f;

            if (lin != 0)
            {
                dB = 20.0f * Mathf.Log10(lin);
            }
            else
            {
                dB = -144.0f;
            }

            return dB;
        }

        public static float DecibelsToFloat(float dB)
        {
            float lin = Mathf.Pow(10.0f, dB / 20.0f);
            return lin;
        }
    }
}
