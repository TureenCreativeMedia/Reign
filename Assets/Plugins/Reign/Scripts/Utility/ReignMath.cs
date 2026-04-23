using System;
using UnityEngine;

namespace Reign.Utility
{
    public static class ReignMath
    {
        #region Math Functions

        public static float SquareRoot(float number)
        {
            if (number < 0.0f) return float.NaN;
            if (number == 0.0f) return 0.0f;

            float guess = number;
            float ceil = 1.0f;

            while (Abs(guess - ceil) > 0.0001f)
            {
                guess = (guess + ceil) / 2.0f;
                ceil = number / guess;
            }
            return ceil;
        }

        public static float Abs(float number)
        {
            return number < 0.0f ? number * -1.0f : number;
        }

        public static float Distance(Vector3 pointA, Vector3 pointB)
        {
            return pointA.magnitude - pointB.magnitude;
        }

        #endregion

        #region Vector

        public static Vector3 ToVector3<T>(T val)
        {
            switch (val)
            {
                case int[] integerArray:
                    {
                        if (integerArray.Length >= 3)
                        {
                            return new Vector3(integerArray[0], integerArray[1], integerArray[2]);
                        }
                        else if (integerArray.Length == 2)
                        {
                            return new Vector3(integerArray[0], integerArray[1], 0);
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                    }
                case float[] floatArray:
                    {
                        if (floatArray.Length >= 3)
                        {
                            return new Vector3(floatArray[0], floatArray[1], floatArray[2]);
                        }
                        else if (floatArray.Length == 2)
                        {
                            return new Vector3(floatArray[0], floatArray[1], 0);
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                    }
                case double[] doubleArray:
                    {
                        if (doubleArray.Length >= 3)
                        {
                            return new Vector3((float)doubleArray[0], (float)doubleArray[1], (float)doubleArray[2]);
                        }
                        else if (doubleArray.Length == 2)
                        {
                            return new Vector3((float)doubleArray[0], (float)doubleArray[1], 0);
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                    }
                case decimal[] decimalArray:
                    {
                        if (decimalArray.Length >= 3)
                        {
                            return new Vector3((float)decimalArray[0], (float)decimalArray[1], (float)decimalArray[2]);
                        }
                        else if (decimalArray.Length == 2)
                        {
                            return new Vector3((float)decimalArray[0], (float)decimalArray[1], 0);
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                    }
                default:
                    {
                        // Ignore any other type
                        throw new InvalidCastException();
                    }
            }
        }

        #endregion
    }
}
