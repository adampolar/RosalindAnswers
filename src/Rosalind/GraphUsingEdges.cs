using System.Collections.Generic;
using System.Linq;
using System;

namespace Rosalind
{
    public class GraphUsingEdges<V>
    {
        public Dictionary<V, List<Edge>> Edges { get; set; }

        public List<V> NodeValues
        {
            get
            {
                var nodeValues = Edges.Select(e => e.Key).ToList();
                var allNodes = new List<V>(nodeValues);
                nodeValues.ForEach(
                    (nodeValue) => allNodes.AddRange(Edges[nodeValue].Select(e => e.To.Value))
                    );

                return allNodes.Distinct().ToList();

            }
        }

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
                if (e.BothWays)
                {
                    Edge e2 = new Edge(e.To.Value, e.From.Value, e.Distance, e.BothWays);
                    if (!Edges.Keys.Contains(e2.From.Value))
                    {
                        Edges.Add(e2.From.Value, new List<Edge>() { e2 });
                    }
                    else
                    {
                        Edges[e2.From.Value].Add(e2);
                    }

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

        public V[] GetNodesConnectedTo(V nodeValue)
        {
            var node = new Node(nodeValue);
            return node.GetConnectedNodes(new Func<V, Edge[]>(a => GetNeighbours(a)));
        }

        public int GetConnectedComponentsCount()
        {
            HashSet<V[]> connectedGraphs = new HashSet<V[]>(new NodeArrayComparer());

            foreach (V node in Edges.Keys)
            {
                connectedGraphs.Add(GetNodesConnectedTo(node).OrderBy(n => n).ToArray());
            }

            return connectedGraphs.Distinct().Count();
        }

        public bool HasNegativeCycles()
        {
            var originalNode = Edges.First().Key;
            var costs = NodeValues
                .Where(e => !e.Equals(originalNode))
                .ToDictionary(e => e, e => new InfinityInt(true));

            Dictionary<V, V> predecessors = NodeValues
                .Where(e => !e.Equals(originalNode))
                .ToDictionary(e => e, e => default(V));


            costs.Add(originalNode, new InfinityInt(0));

            for (int i = 1; i <= NodeValues.Count + 1; i++)
            {
                foreach (KeyValuePair<V, List<Edge>> edges in Edges)
                {
                    foreach (Edge e in edges.Value)
                    {
                        if (!costs[e.From.Value].IsInfinity && (costs[e.To.Value].IsInfinity || costs[e.From.Value].Value + e.Distance < costs[e.To.Value].Value))
                        {
                            if (i == NodeValues.Count)
                            {
                                return true;
                            }
                            costs[e.To.Value] = new InfinityInt(costs[e.From.Value].Value + e.Distance);
                            predecessors[e.To.Value] = e.From.Value;
                        }
                    }
                }
            }

            return false;
        }

        private class InfinityInt
        {
            public bool IsInfinity { get; private set; }

            public int Value { get; private set; }

            public InfinityInt(int value)
            {
                Value = value;
            }

            public InfinityInt(bool isInfinity) 
            {
                IsInfinity = isInfinity;
            }
            public override bool Equals (object obj)
            {
                //
                // See the full list of guidelines at
                //   http://go.microsoft.com/fwlink/?LinkID=85237
                // and also the guidance for operator== at
                //   http://go.microsoft.com/fwlink/?LinkId=85238
                //
                
                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }
                
                return IsInfinity == IsInfinity && Value == Value;
            }
            
            // override object.GetHashCode
            public override int GetHashCode()
            {
                return IsInfinity ? IsInfinity.GetHashCode() : Value.GetHashCode();
            }

        }

        private class NodeArrayComparer : IEqualityComparer<V[]>
        {
            bool IEqualityComparer<V[]>.Equals(V[] x, V[] y)
            {
                if (x.Length != y.Length)
                {
                    return false;
                }

                for (int i = 0; i < x.Length; i++)
                {
                    if (!x[i].Equals(y[i]))
                    {
                        return false;
                    }
                }
                return true;
            }

            int IEqualityComparer<V[]>.GetHashCode(V[] obj)
            {
                return obj[0].GetHashCode();
            }
        }

        public class Edge
        {
            public Node From { get; set; }
            public Node To { get; set; }
            public bool BothWays { get; set; }
            public int Distance { get; set; }

            public Edge(V from, V to, int distance = 1, bool bothWays = false)
            {
                From = new Node(from);
                To = new Node(to);
                Distance = distance;
                BothWays = bothWays;
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

            internal V[] GetConnectedNodes(Func<V, Edge[]> neighbourGetter, HashSet<V> vertices = null)
            {
                if (vertices == null) vertices = new HashSet<V>();

                this.Neighbours = neighbourGetter(this.Value);

                vertices.Add(this.Value);

                foreach (Edge neighbour in this.Neighbours)
                {
                    if (!vertices.Contains(neighbour.To.Value))
                    {
                        new Node(neighbour.To.Value).GetConnectedNodes(neighbourGetter, vertices);
                    }
                }
                return vertices.ToArray();
            }
        }
    }
}