using System;
using System.Linq;
using System.IO;

namespace Rosalind
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var fileName = "/home/adam/Downloads/rosalind_inv.txt";
            var answer = "";
            using (var r = new StreamReader(new FileStream(fileName, FileMode.Open)))
            {
                r.ReadLine();
                var a = r.ReadLine();
                answer = string.Join(" ", CountInversions(
                    a.Split(' ').Select(c => int.Parse(c)).ToArray()));
            }
            using (var w = new StreamWriter(new FileStream(fileName + "ans", FileMode.OpenOrCreate)))
            {
                w.Write(answer);
                w.Flush();
            }
        }

        public static int CountInversions(int[] array)
        {
            var inversions = 0;
            for(int i = 0; i < array.Length; i++)
            {
                for(int j = i + 1; j < array.Length; j++)
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
