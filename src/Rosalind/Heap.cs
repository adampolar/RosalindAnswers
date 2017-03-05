using System;
using System.Linq;
using System.Collections.Generic;

namespace Rosalind{
    public class Heap<V> : BinaryTree<V> where V : IComparable
    {
        public Heap(V[] arr)
        {
            List<V> list = arr.OrderBy(a => a).Reverse().ToList();
            this.Value = list[0]; 
            list.RemoveAt(0);
            if(list.Count == 0)
            {
                return;
            }
            //we need to set up the heap so that both sides have equal "rows"
            var leftHandSideNumber = Arithmetic.GetLargestTriangleNumberLessThan(list.Count/2);
            var remainder = list.Count() - 2 * leftHandSideNumber;
            var leftHand2 = Arithmetic.GetLargestTriangleNumberLessThan(leftHandSideNumber + remainder);
            if(leftHand2 > leftHandSideNumber)
            {
                leftHandSideNumber = leftHand2;
            }

            if(leftHandSideNumber == -1)
            {
                return;
            }
            
            var leftHand = list.Take(leftHandSideNumber).ToArray();
            var rightHand = list.Skip(leftHandSideNumber).ToArray();
            if (leftHand.Length > 0) this.ChildOne = new Heap<V>(leftHand);
            if(rightHand.Length > 0) this.ChildTwo = new Heap<V>(rightHand);
        }
    }
}