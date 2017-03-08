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
            var fileName = "/home/adam/Downloads/rosalind_cc.txt";

            int answer;
            
            using (var r = new StreamReader(new FileStream(fileName, FileMode.Open)))
            {

                var a = r.ReadLine();
                int nodeCount = int.Parse(a.Split(' ')[0]);
                a = r.ReadLine();

                List<GraphUsingEdges<int>.Edge> edges = new List<GraphUsingEdges<int>.Edge>();
                
                while (a != null)
                {
                    int[] edge = a.Split(' ').Select(n => int.Parse(n)).ToArray();
                    edges.Add(new GraphUsingEdges<int>.Edge(edge[0], edge[1], bothWays:true));
                    a = r.ReadLine();
                }
                GraphUsingEdges<int> graph = new GraphUsingEdges<int>(edges);

                int unconnectedNodes = 0;
                for(int i = 1; i <= nodeCount; i++ )
                {
                    if(!graph.Edges.Keys.Contains(i)) unconnectedNodes++;
                }

                answer = graph.GetConnectedComponentsCount() + unconnectedNodes;

            }


            using (var w = new StreamWriter(new FileStream(fileName + "ans", FileMode.OpenOrCreate)))
            {
                w.Write(answer);
                w.Flush();
            }
        }


    }
}
