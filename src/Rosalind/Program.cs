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
            var fileName = "/home/adam/Downloads/rosalind_bfs.txt";
            var answer = new List<string>();
            List<GraphUsingEdges<int>.Edge> edges = new List<GraphUsingEdges<int>.Edge>();
            var indexNo = 0;
            using (var r = new StreamReader(new FileStream(fileName, FileMode.Open)))
            {
                indexNo = int.Parse(r.ReadLine().Split(' ')[0]);
                var a = r.ReadLine();


                while (!String.IsNullOrEmpty(a))
                {
                    int[] edge = a.Split(' ').Select(b => int.Parse(b)).ToArray();
                    edges.Add(new GraphUsingEdges<int>.Edge(edge[0], edge[1]));
                    a = r.ReadLine();
                }
            }

            GraphUsingEdges<int> g = new GraphUsingEdges<int>(edges);

            using (var w = new StreamWriter(new FileStream(fileName + "ans", FileMode.OpenOrCreate)))
            {
                w.Write(0 + " ");
                for (int i = 2; i <= indexNo; i++)
                {
                    var a = g.FindMinimumDistanceBetweenNodes(1, i);
                    w.Write((a == null ? -1 : a) + " ");
                }
                w.Flush();
            }
        }


    }
}
