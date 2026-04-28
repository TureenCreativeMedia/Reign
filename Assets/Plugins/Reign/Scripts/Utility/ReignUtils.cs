using System;
using UnityEditor;
using UnityEngine;

namespace Reign.Utility
{
    public static class ReignUtils
    {
        /// <summary>
        /// Return the type of generic T
        /// </summary>
        public static Type GetType<T>()
        {
            return typeof(T);
        }

        /// <summary>
        /// Convert linear float to decibel.
        /// </summary>
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
        public static float DecibelsToFloat(float dB)
        {
            // Use the decibel to linear conversion, 10^db/20
            float lin = Mathf.Pow(10.0f, dB / 20.0f);
            return lin;
        }

        /// <summary>
        /// Compares the type of the given object to generic T
        /// </summary>
        public static bool IsType<T>(object obj)
        {
            return typeof(T) == obj.GetType();
        }

        /// <summary>
        /// Distance between two Vector3 points
        /// </summary>
        public static float Distance(Vector3 pointA, Vector3 pointB)
        {
            // Point A magnitude - Point B magnitude = distance
            return (pointA - pointB).magnitude;
        }

        /// <summary>
        /// Square distance between two Vector3 points
        /// </summary>
        public static float SquareDistance(Vector3 pointA, Vector3 pointB)
        {
            // Point A square magnitude - Point B square magnitude = square distance
            return (pointA - pointB).sqrMagnitude;
        }

        /// <summary>
        /// Converts a float array (minimum length 2) to a Vector3
        /// </summary>
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

            throw new ArgumentException("Float array needs a minimum of 2 values to convert to Vector3");
        }

        /// <summary>
        /// Converts a float array (minimum length 2) to a Vector2
        /// </summary>
        public static Vector3 FloatArrayToVector2(float[] array)
        {
            if (array.Length >= 2)
            {
                return new Vector3(array[0], array[1], 0);
            }

            throw new ArgumentException("Float array needs a minimum of 2 values to convert to Vector2");
        }

        /// <summary>
        /// Convert a Vector2 to a Vector3
        /// </summary>
        public static Vector3 ToVector3(Vector2 vec)
        {
            return new(vec.x, vec.y, 0);
        }

        /// <summary>
        /// Return new Vector2 replacing each co-ordinate with absolute
        /// </summary>
        public static Vector2 ToAbsolute(Vector2 vec)
        {
            return new Vector2
            (
                Mathf.Abs(vec.x),
                Mathf.Abs(vec.y)
            );
        }

        /// <summary>
        /// Return new Vector3 replacing each co-ordinate with absolute
        /// </summary>
        public static Vector3 ToAbsolute(Vector3 vec)
        {
            return new Vector3
            (
                Mathf.Abs(vec.x),
                Mathf.Abs(vec.y),
                Mathf.Abs(vec.z)
            );
        }

        /// <summary>
        /// Return the area of a rectangle with base and height
        /// </summary>
        public static double RectangleArea(double _base, double height)
        {
            return _base * height;
        }

        /// <summary>
        /// Return the area of a circle with radius
        /// </summary>
        public static double CircleArea(double radius)
        {
            // πr²
            return Mathf.PI * (radius * radius);
        }

        /// <summary>
        /// Return the area of a triangle with base and height
        /// </summary>
        public static double TriangleArea(double _base, double height)
        {
            return (_base * height) / 2;
        }

        /// <summary>
        /// Return the volume of a cube (side^3)
        /// </summary>
        public static double VolumeOfCube(double side)
        {
            return (side * side * side);
        }

        /// <summary>
        /// Return the volume of a cuboid
        /// </summary>
        public static double CuboidVolume(double width, double height, double depth)
        {
            return width * height * depth;
        }

        /// <summary>
        /// Return the volume of a sphere
        /// </summary>
        public static double SphereVolume(double radius)
        {
            // 4/3 * πr³
            return 4 / 3 * Mathf.PI * (radius * radius * radius);
        }

        /// <summary>
        /// Return the volume of a cone
        /// </summary>
        public static double ConeVolume(double radius, double height)
        {
            return 1 / 3 * height * Mathf.PI * (radius * radius);
        }

        /// <summary>
        /// Return factorial of a number
        /// </summary>
        public static long Factorial(int number)
        {
            if (number < 0) throw new ArgumentException($"Number ({number}) must be greater than zero.");
            return (number == 0) ? 1 : number * Factorial(number - 1);
        }
    }
}
