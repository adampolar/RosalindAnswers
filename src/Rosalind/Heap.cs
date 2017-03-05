using System;
using System.Linq;
using System.Collections.Generic;

namespace Rosalind
{
    public class Heap<V> : BinaryTree<V> where V : IComparable
    {
        public Heap(V[] arr)
        {
            List<V> list = arr.ToList();
            var index = arr.GetMaxIndex();
            
            this.Value = list[index];
            list.RemoveAt(index);

            if (list.Count == 0)
            {
                return;
            }
            //we need to set up the heap so that both sides have equal "rows"
            var leftHandSideNumber = Arithmetic.GetLargestBinaryTreeNumberLessThan(list.Count / 2);
            var remainder = list.Count() - 2 * leftHandSideNumber;
            var leftHand2 = Arithmetic.GetLargestBinaryTreeNumberLessThan(leftHandSideNumber + remainder);
            if (leftHand2 > leftHandSideNumber)
            {
                leftHandSideNumber = leftHand2;
            }
            else
            {
                leftHandSideNumber += remainder;
            }

            if (leftHandSideNumber == -1)
            {
                return;
            }

            var leftHand = list.Take(leftHandSideNumber).ToArray();
            var rightHand = list.Skip(leftHandSideNumber).ToArray();
            if (leftHand.Length > 0) this.ChildOne = new Heap<V>(leftHand);
            if (rightHand.Length > 0) this.ChildTwo = new Heap<V>(rightHand);
        }
    }
}