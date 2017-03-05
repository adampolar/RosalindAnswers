using System.Collections.Generic;
using System.Linq;
using System;

namespace Rosalind
{
    public class Graph<V>
    {
        private List<Node> _nodes;
        public List<Node> Nodes
        {
            get
            {
                if (_nodes == null) { _nodes = new List<Node>(); }
                return _nodes;
            }
        }

        public Graph(Node node)
        {

            Nodes.Add(node);
            node.AddAllChildrenToList(Nodes);
        }

        public int? FindMinimumDistanceBetweenNodes(V nodeOne, V nodeTwo)
        {
            var node = Nodes.First(n => nodeOne.Equals(n.Value));
            return node.DistanceTo(nodeTwo);
        }

        public class Node
        {
            public V Value { get; set; }
            public Node[] Neighbours { get; set; }

            public Node(V value, params Node[] nodes)
            {
                Value = value;
                Neighbours = nodes;
            }

            public int? DistanceTo(V otherNode)
            {
                Queue<Tuple<Node, List<Node>>> q = new Queue<Tuple<Node, List<Node>>>();

                var currentNode = new Tuple<Node, List<Node>>(this, new List<Node>());

                while (currentNode != null)
                {
                    foreach (Node child in currentNode.Item1.Neighbours)
                    {
                        if (child.Value.Equals(otherNode))
                        {
                            return currentNode.Item2.Count + 1;
                        }

                        //make sure we dont go round in circles
                        if (currentNode.Item2.Where(n => n.Value.Equals(child.Value)).Count() == 0)
                        {
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