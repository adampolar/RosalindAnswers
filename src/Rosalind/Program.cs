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
            var fileName = "/home/adam/Downloads/rosalind_nwc (3).txt";



            var ans = new List<int>();

            using (var r = new StreamReader(new FileStream(fileName, FileMode.Open)))
            {

                var a = r.ReadLine();

                List<GraphUsingEdges<int>.Edge> edges = new List<GraphUsingEdges<int>.Edge>();

                while (a != null)
                {
                    if ((string.Empty == a || a.Split(' ').Length == 2) && edges.Count > 0)
                    {
                        ans.Add(new GraphUsingEdges<int>(edges).HasNegativeCycles() ? 1 : -1);
                        edges = new List<GraphUsingEdges<int>.Edge>();
                    }
                    else if (a.Split(' ').Length == 3)
                    {
                        var e = a.Split(' ').Select(i => int.Parse(i)).ToArray();
                        edges.Add(new GraphUsingEdges<int>.Edge(e[0], e[1], e[2]));
                    }

                    a = r.ReadLine();
                }

                ans.Add(new GraphUsingEdges<int>(edges).HasNegativeCycles() ? 1 : -1);

            }


            using (var w = new StreamWriter(new FileStream(fileName + "ans", FileMode.OpenOrCreate)))
            {
                w.Write(String.Join(" ", ans));
                w.Flush();
            }
        }


    }
}
