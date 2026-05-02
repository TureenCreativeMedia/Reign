using System;
using UnityEngine;

namespace Reign.Utility
{
    public static class ReignUtility
    {
        /// <summary>
        /// Convert a hex string to a Color32
        /// </summary>
        public static Color32 HexToColor32(string hex)
        {
            hex = hex.Replace("#", "");

            byte r = byte.Parse(hex[..2], System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            byte a = 255;

            if (hex.Length >= 8)
            {
                a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
            }

            return new Color32(r, g, b, a);
        }

        /// <summary>
        /// Convert a Color32 to a hex string
        /// </summary>
        public static string Color32ToHex(Color32 color, bool includeAlpha)
        {
            string h = $"#{color.r:X2}{color.g:X2}{color.b:X2}";
            if (includeAlpha) h += $"{color.a:X2}";

            return h;
        }

        /// <summary>
        /// Make a transform point towards a point with a speed (If speed <= 0, rotation will be instant. But if speed >= 0, 
        /// rotation will gradual so must be called repeatedly)
        /// </summary>
        public static void PointTowards(Transform pointer, Vector3 point, float speed = 0)
        {
            Vector3 direction = (point - pointer.position).normalized;
            Quaternion target = Quaternion.LookRotation(direction);

            if (speed <= 0)
            {
                pointer.rotation = target;
            }
            else
            {
                pointer.rotation = Quaternion.RotateTowards(pointer.rotation, target, Time.deltaTime / speed);
            }
        }

        /// <summary>
        /// Convert linear float to decibel.
        /// </summary>
        public static float FloatToDecibels(float lin)
        {
            if (lin > 0.0f)
            {
                // If linear float is greater than 0.0f, convert it to decibel
                return 20.0f * Mathf.Log10(lin);
            }
            else
            {
                // Otherwise, set it to the absolute minimum decibel value
                return -144.0f;
            }
        }

        /// <summary>
        /// Convert decibel to linear float.
        /// </summary>
        public static float DecibelsToFloat(float dB)
        {
            // Use the decibel to linear conversion, 10^db/20
            return Mathf.Pow(10.0f, dB / 20.0f);
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
        public static Vector3 Vector2ToVector3(Vector2 vec)
        {
            return new(vec.x, vec.y, 0);
        }

        /// <summary>
        /// Return new Vector2 replacing each co-ordinate with absolute
        /// </summary>
        public static Vector2 Vector2ToAbsolute(Vector2 vec)
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
        public static Vector3 Vector3ToAbsolute(Vector3 vec)
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
            if (number < 0) throw new ArgumentException($"Number ({number}) must be greater than or equal to zero.");
            return (number == 0) ? 1 : number * Factorial(number - 1);
        }
    }
}
