using System.Collections.Generic;
using System.Linq;
using System;

namespace Rosalind
{
    public class GraphUsingEdges<V>
    {
        public Dictionary<V, List<Edge>> Edges { get; set; }

        public GraphUsingEdges(List<Edge> edges)
        {
            Edges = new Dictionary<V, List<Edge>>();

            foreach (Edge e in edges)
            {
                if (!Edges.Keys.Contains(e.From.Value))
                {
                    Edges.Add(e.From.Value, new List<Edge>() { e });
                }
                else
                {
                    Edges[e.From.Value].Add(e);
                }
            }
        }

        public int? FindMinimumDistanceBetweenNodes(V nodeOne, V nodeTwo)
        {
            var node = new Node(nodeOne);
            return node.DistanceTo(nodeTwo, new Func<V, Edge[]>(a => GetNeighbours(a))); ///mmmm lexical closure
        }

        public Edge[] GetNeighbours(V value)
        {
            List<Edge> ret;
            return Edges.TryGetValue(value, out ret) ? ret.ToArray() : new Edge[] { };
        }

        public class Edge
        {
            public Node From { get; set; }
            public Node To { get; set; }
            public int Distance { get; set; }

            public Edge(V from, V to, int distance = 1)
            {
                From = new Node(from);
                To = new Node(to);
                Distance = distance;
            }
        }

        public class Node
        {
            public V Value { get; set; }
            public Edge[] Neighbours
            {
                get; set;
            }

            public Node(V value)
            {
                Value = value;
            }

            public int? DistanceTo(V otherNode, Func<V, Edge[]> neighbourGetter)
            {
                Queue<Tuple<Node, int>> q = new Queue<Tuple<Node, int>>();
                Dictionary<V, int?> visited = new Dictionary<V, int?>();

                var currentNode = new Tuple<Node, int>(this, 0);

                while (currentNode != null)
                {
                    //get all neighbours
                    currentNode.Item1.Neighbours = neighbourGetter(currentNode.Item1.Value).ToArray();
                    foreach (Edge edge in currentNode.Item1.Neighbours)
                    {
                        //if (edge.To.Value.Equals(otherNode))
                        //{
                        //    return currentNode.Item2 + edge.Distance;
                       // }

                        //make sure we dont go round in circles
                        int? distance = null;
                        visited.TryGetValue(edge.To.Value, out distance);
                        if (!distance.HasValue)
                        {
                            visited.Add(edge.To.Value, currentNode.Item2 + edge.Distance);
                            q.Enqueue(new Tuple<Node, int>(edge.To, currentNode.Item2 + edge.Distance));
                        }
                        else if (distance.Value > currentNode.Item2 + edge.Distance)
                        {
                            visited[edge.To.Value] = currentNode.Item2 + edge.Distance;
                            q.Enqueue(new Tuple<Node, int>(edge.To, currentNode.Item2 + edge.Distance));
                        }

                        
                    }

                    currentNode = q.Count > 0 ? q.Dequeue() : null;
                }
                int? distanceAnswer = null;

                visited.TryGetValue(otherNode, out distanceAnswer);

                return distanceAnswer;
            }
        }
    }
}