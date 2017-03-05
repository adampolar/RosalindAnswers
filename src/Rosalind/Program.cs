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
            var fileName = "/home/adam/Downloads/rosalind_hea.txt";
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
