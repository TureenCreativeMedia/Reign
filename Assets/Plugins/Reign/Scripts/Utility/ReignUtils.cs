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

            if (lin > 0.0f)
            {
                // If linear float is greater than 0.0f, convert it to decibel
                dB = 20.0f * Mathf.Log10(lin);
            }
            else
            {
                // Otherwise, set it to the absolute minimum decibel value
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
            // Use the decibel to linear conversion, 10^db/20
            float lin = Mathf.Pow(10.0f, dB / 20.0f);
            return lin;
        }

        /// <summary>
        /// Compares the type of the given object to generic T
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
            // Point A magnitude - Point B magnitude = distance
            return (pointA - pointB).magnitude;
        }

        /// <summary>
        /// Square distance between two Vector3 points
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <returns>float</returns>
        public static float SquareDistance(Vector3 pointA, Vector3 pointB)
        {
            // Point A square magnitude - Point B square magnitude = square distance
            return (pointA - pointB).sqrMagnitude;
        }

        /// <summary>
        /// Converts a float array (minimum length 2) to a Vector3
        /// </summary>
        /// <param name="array"></param>
        /// <returns>Vector3</returns>
        public static Vector3 FloatArrayToVector3(float[] array)
        {
            if (array.Length >= 3)
            {
                return new Vector3(array[0], array[1], array[2]);
            }
            else if (array.Length == 2)
            {
                return new Vector3(array[0], array[1], 0);
            }

            throw new ArgumentException("Float array needs a minimum of 2 values");
        }

        /// <summary>
        /// Convert a Vector2 to a Vector3
        /// </summary>
        /// <param name="vec"></param>
        /// <returns>Vector3</returns>
        public static Vector3 Vector2ToVector3(Vector2 vec)
        {
            return new(vec.x, vec.y, 0);
        }
    }
}
