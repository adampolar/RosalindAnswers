using System.Collections.Generic;

namespace Rosalind
{
    public class BinaryTree<V>
    {
        public BinaryTree(V value)
        {
            this.Value = value;
        }

        protected BinaryTree(){}

        public BinaryTree(V value, BinaryTree<V> childOne, BinaryTree<V> childTwo) : this(value)
        {
            ChildOne = childOne;
            ChildTwo = childTwo;            
        }

        public V Value { get; set; }
        public BinaryTree<V> ChildOne { get; set; }
        public BinaryTree<V> ChildTwo { get; set; }

        public V[] ReverseBreadthFirstOrdering()
        {
            List<V> answer = new List<V>();
            Queue<BinaryTree<V>> ordering = new Queue<BinaryTree<V>>();
            var currentNode = this;
            while(currentNode != null)
            {
                answer.Add(currentNode.Value);
                if(currentNode.ChildOne != null) ordering.Enqueue(currentNode.ChildOne);
                if(currentNode.ChildTwo != null) ordering.Enqueue(currentNode.ChildTwo);
                currentNode = ordering.Count > 0 ? ordering.Dequeue() : null;                
            }
            return answer.ToArray();
        }
    }

}