using System;
using UnityEditor;
using UnityEngine;

namespace Reign.Utility
{
    public static class ReignUtils
    {
        /// <summary>
        /// Convert linear float to decibel.
        /// </summary>
        /// <param name="lin"></param>
        /// <returns>float</returns>
        public static float FloatToDecibels(float lin)
        {
            float dB;

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

        /// <summary>
        /// Convert decibel to linear float.
        /// </summary>
        /// <param name="lin"></param>
        /// <returns>float</returns>
        public static float DecibelsToFloat(float dB)
        {
            float lin = Mathf.Pow(10.0f, dB / 20.0f);
            return lin;
        }

        /// <summary>
        /// Check if the type of the given object the same as the generic T
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>bool</returns>
        public static bool IsType<T>(object obj)
        {
            return typeof(T) == obj.GetType();
        }

        /// <summary>
        /// Distance between two Vector3 points
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <returns>float</returns>
        public static float Distance(Vector3 pointA, Vector3 pointB)
        {
            return pointA.magnitude - pointB.magnitude;
        }

        public static Vector3? FloatArrayToVector3(float[] array)
        {
            if (array.Length >= 3)
            {
                return new Vector3(array[0], array[1], array[2]);
            }
            else if (array.Length == 2)
            {
                return new Vector3(array[0], array[1], 0);
            }
            return null;
        }

        public static Vector3 Vector2ToVector3(Vector2 vec)
        {
            return new(vec.x, vec.y, 0);
        }
    }
}
