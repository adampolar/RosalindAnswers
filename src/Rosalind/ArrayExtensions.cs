using System.Collections.Generic;
using System;
using System.Linq;

namespace Rosalind
{
    public static class ArrayExtensions
    {
        public static int GetMaxIndex<V>(this V[] array) where V : IComparable
        {
            if (array.Length == 0)
            {
                return -1;
            }
            int indexOfCurrentMax = 0;
            for(int i = 1; i < array.Length; i++)
            {
                if(array[i].CompareTo(array[indexOfCurrentMax]) > 0)
                {
                    indexOfCurrentMax = i;
                }
            }
            return indexOfCurrentMax;

        }
        //On2 time 0n extra space
        public static Tuple<int, int, int> Determine3Sum(this int[] array)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < array.Length; i++)
            {
                if (!dict.Keys.Contains(array[i]))
                {
                    dict.Add(array[i], i);
                }
            }
            for (int j = 1; j < array.Length; j++)
            {
                for (int k = 0; k < j; k++)
                {
                    var relIndex = -1;
                    dict.TryGetValue(-(array[j] + array[k]), out relIndex);
                    //Try get value sets the out param back to 0, Cheers!!
                    if (array[relIndex] + array[j] + array[k] != 0) relIndex = -1;

                    if (relIndex != -1 && relIndex != j && relIndex != k)
                    {
                        var first = relIndex < k ? relIndex : k;
                        var second = relIndex < k ? k : relIndex < j ? relIndex : j;
                        var third = relIndex > j ? relIndex : j;
                        return new Tuple<int, int, int>(first + 1, second + 1, third + 1);
                    }
                }
            }
            return null;
        }

        //On3 time no extra space
        public static Tuple<int, int, int> Determine3Sum03(this int[] array)
        {
            for (int i = 2; i < array.Length; i++)
            {
                for (int j = 1; j < i; j++)
                {
                    for (int k = 0; k < j; k++)
                    {
                        if (array[i] + array[j] + array[k] == 0)
                        {
                            return new Tuple<int, int, int>(k + 1, j + 1, i + 1);
                        }
                    }
                }
            }
            return null;
        }

        public static Tuple<int, int> Determine2Sum(this int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (array[j] == -array[i])
                    {
                        return new Tuple<int, int>(j + 1, i + 1);
                    }
                }
            }
            return null;
        }

        public static int CountInversions(this int[] array)
        {
            var inversions = 0;
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[i])
                    {
                        inversions++;
                    }
                }
            }
            return inversions;
        }

        public static int[] MergeSort(this int[] arrayToSort)
        {
            if (arrayToSort.Length == 1) return arrayToSort;
            int halfWay = arrayToSort.Length / 2;
            return MergeTwoSortedArrays(
            MergeSort(arrayToSort.Take(halfWay).ToArray()),
            MergeSort(arrayToSort.Skip(halfWay).ToArray()));

        }

        //O(n) run  and O(n) space
        public static int[] MergeTwoSortedArrays(this int[] arrayOne, int[] arrayTwo)
        {
            int[] answer = new int[arrayOne.Length + arrayTwo.Length];
            var arrayOnePointer = 0;
            var arrayTwoPointer = 0;
            for (int i = 0; i < answer.Length; i++)
            {
                if (arrayOnePointer < arrayOne.Length &&
                (arrayTwoPointer >= arrayTwo.Length ||
                arrayOne[arrayOnePointer] < arrayTwo[arrayTwoPointer]))
                {
                    answer[i] = arrayOne[arrayOnePointer];
                    arrayOnePointer++;
                }
                else
                {
                    answer[i] = arrayTwo[arrayTwoPointer];
                    arrayTwoPointer++;
                }
            }
            return answer;
        }

    }
}