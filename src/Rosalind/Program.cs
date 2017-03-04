﻿using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace Rosalind
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var fileName = "/home/adam/Downloads/rosalind_3sum.txt";
            var answer = new List<string>();
            using (var r = new StreamReader(new FileStream(fileName, FileMode.Open)))
            {
                r.ReadLine();
                var a = r.ReadLine();
                while (!String.IsNullOrEmpty(a))
                {
                    var ans = Determine3Sum(a.Split(' ').Select(c => int.Parse(c)).ToArray());
                    answer.Add(ans != null ? (ans.Item1 + " " + ans.Item2 + " " + ans.Item3) : (-1).ToString());
                    a = r.ReadLine();
                }
            }
            using (var w = new StreamWriter(new FileStream(fileName + "ans", FileMode.OpenOrCreate)))
            {
                foreach (var a in answer)
                {
                    w.WriteLine(a);
                }
                w.Flush();
            }
        }

        //On2 time 0n extra space
        public static Tuple<int, int, int> Determine3Sum(int[] array)
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
                    if(array[relIndex] + array[j] + array[k] != 0) relIndex = -1;
                    
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
        public static Tuple<int, int, int> Determine3Sum03(int[] array)
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

        public static Tuple<int, int> Determine2Sum(int[] array)
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

        public static int CountInversions(int[] array)
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

        public static int[] MergeSort(int[] arrayToSort)
        {
            if (arrayToSort.Length == 1) return arrayToSort;
            int halfWay = arrayToSort.Length / 2;
            return MergeTwoSortedArrays(
            MergeSort(arrayToSort.Take(halfWay).ToArray()),
            MergeSort(arrayToSort.Skip(halfWay).ToArray()));

        }

        //O(n) run  and O(n) space
        public static int[] MergeTwoSortedArrays(int[] arrayOne, int[] arrayTwo)
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
