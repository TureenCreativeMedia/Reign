using System;
using UnityEngine;

namespace Reign.Utility
{
    public static class SortingAlgorithm
    {
        /// <summary>
        /// Use QuickSort algorithm to organise an array of type T 
        /// </summary>
        public static T[] QuickSort<T>(T[] array) where T : IComparable<T>
        {
            // Return is less than 1 element (sorted)
            if (array.Length <= 1)
            {
                return array;
            }

            // Sort with helper
            return QuickSort(array, 0, array.Length - 1);
        }

        private static T[] QuickSort<T>(T[] array, int left, int right) where T : IComparable<T>
        {
            // Left index is greater than right, so stop recursion
            if (left >= right)
            {
                return array;
            }

            int pivotIndex = Partition(array, left, right); // Partition around
            QuickSort(array, left, pivotIndex - 1);         // Recursive sort left subarray (less than pivot)
            QuickSort(array, pivotIndex + 1, right);        // Recursively sort right subarray (greater than pivot)

            return array;
        }

        private static int Partition<T>(T[] array, int left, int right) where T : IComparable<T>
        {
            T pivot = array[right]; // Select pivot
            int i = left - 1;       // Initialise index for smaller element

            // Iterate over subarray
            for (int j = left; j < right; j++)
            {
                // Current element is less or equal to pivot
                if (array[j].CompareTo(pivot) <= 0)
                {
                    i++; // Increment smaller index
                    Swap(array, i, j); // Swap elements
                }
            }

            // Swap pivot with element at right index
            Swap(array, i + 1, right);
            return i + 1;
        }

        private static void Swap<T>(T[] array, int i, int j)
        {
            // Swap elements at indices in array
            (array[j], array[i]) = (array[i], array[j]);
        }
    }
}
