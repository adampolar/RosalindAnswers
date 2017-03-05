using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace Rosalind
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var fileName = "/home/adam/Downloads/rosalind_hea (2).txt";
            var answer = new List<string>();
            using (var r = new StreamReader(new FileStream(fileName, FileMode.Open)))
            {
                r.ReadLine();
                var a = r.ReadLine();
                while (!String.IsNullOrEmpty(a))
                {
                    var ans = new Heap<int>(
                        a.Split(' ').Select(b => int.Parse(b)).ToArray())
                        .ReverseBreadthFirstOrdering();
                    answer.Add(String.Join(" ", ans));
                    a = r.ReadLine();
                }
            }

            int[] heapOrder = answer[0].Split(' ').Select(a => int.Parse(a)).ToArray();
            for (int i = 2; i <= heapOrder.Length - 1; i = i + 2)
            {
                if (heapOrder[i - 1] > heapOrder[i / 2 - 1])
                {
                    Console.WriteLine(i);
                    break;
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


    }
}
