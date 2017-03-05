using Xunit;
using Rosalind;

namespace Tests
{
    public class BinaryTreeAndHeapTests
    {
        [Fact]
        public void TestReverseBreadthFirstSearch()
        {
            var tree = new BinaryTree<int>(1,
                new BinaryTree<int>(2,
                    new BinaryTree<int>(4),
                    new BinaryTree<int>(5,
                        new BinaryTree<int>(8),
                        new BinaryTree<int>(9))),
                new BinaryTree<int>(3,
                    new BinaryTree<int>(6,
                        new BinaryTree<int>(10),
                        new BinaryTree<int>(11)),
                    new BinaryTree<int>(7)));

            Assert.Equal(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, tree.ReverseBreadthFirstOrdering());

        }

        [Fact]
        public void TestHeapConstruction()
        {
            Heap<int> heap = new Heap<int>(new int[] { 1, 3, 5, 7, 2 });
            Assert.Equal(new int[] { 7, 5, 1, 3, 2 }, 
                new Heap<int>(new int[] { 1, 3, 5, 7, 2 }).ReverseBreadthFirstOrdering());
        }
        [Fact]
        public void TestTriangleNumberGenerator()
        {
            Assert.Equal(1, Arithmetic.GetLargestTriangleNumberLessThan(1));

            Assert.Equal(3, Arithmetic.GetLargestTriangleNumberLessThan(3));

            Assert.Equal(15, Arithmetic.GetLargestTriangleNumberLessThan(20));
        }

    }
}