using System.Collections.Generic;
using System.Linq;
using System;

namespace Rosalind
{
    public class GraphUsingEdges<V>
    {
        public Dictionary<V, List<V>> Edges { get; set; }

        public GraphUsingEdges(List<Edge> edges)
        {
            Edges = new Dictionary<V, List<V>>();

            foreach (Edge e in edges)
            {
                if (!Edges.Keys.Contains(e.From.Value))
                {
                    Edges.Add(e.From.Value, new List<V>() { e.To.Value });
                }
                else
                {
                    Edges[e.From.Value].Add(e.To.Value);
                }
            }
        }

        public int? FindMinimumDistanceBetweenNodes(V nodeOne, V nodeTwo)
        {
            var node = new Node(nodeOne);
            return node.DistanceTo(nodeTwo, new Func<V, V[]>(a => GetNeighbours(a))); ///mmmm lexical closure
        }

        public V[] GetNeighbours(V value)
        {
            List<V> ret;
            return Edges.TryGetValue(value, out ret)? ret.ToArray(): new V[]{};
        }

        public class Edge
        {
            public Node From { get; set; }
            public Node To { get; set; }

            public Edge(V from, V to)
            {
                From = new Node(from);
                To = new Node(to);
            }
        }

        public class Node
        {
            public V Value { get; set; }
            public Node[] Neighbours
            {
                get; set;
            }

            public Node(V value)
            {
                Value = value;
            }

            public int? DistanceTo(V otherNode, Func<V, V[]> neighbourGetter)
            {
                Queue<Tuple<Node, List<Node>>> q = new Queue<Tuple<Node, List<Node>>>();
                Dictionary<V, bool> visited = new Dictionary<V, bool>();

                var currentNode = new Tuple<Node, List<Node>>(this, new List<Node>());

                while (currentNode != null)
                {
                    //get all neighbours
                    currentNode.Item1.Neighbours = neighbourGetter(currentNode.Item1.Value).Select(a => new Node(a)).ToArray();
                    foreach (Node child in currentNode.Item1.Neighbours)
                    {
                        if (child.Value.Equals(otherNode))
                        {
                            return currentNode.Item2.Count + 1;
                        }

                        //make sure we dont go round in circles
                        var visitedBool = false;
                        visited.TryGetValue(child.Value, out visitedBool);
                        if (!visitedBool)//(currentNode.Item2.Where(n => n.Value.Equals(child.Value)).Count() == 0)
                        {
                            visited.Add(child.Value, true);
                            var routeCopy = new List<Node>(currentNode.Item2);
                            routeCopy.Add(child);
                            q.Enqueue(new Tuple<Node, List<Node>>(child, routeCopy));
                        }
                    }

                    currentNode = q.Count > 0 ? q.Dequeue() : null;
                }

                return null;
            }

            internal void AddAllChildrenToList(List<Node> nodes)
            {
                foreach (var child in Neighbours)
                {
                    if (nodes.Where(n => n.Value.Equals(child.Value)).Count() == 0)
                    {
                        nodes.Add(child);
                        child.AddAllChildrenToList(nodes);
                    }
                }

            }
        }
    }
}