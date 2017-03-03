using Xunit;
using Rosalind;

namespace Tests
{
    public class Tests
    {
        [Fact]
        public void MergeTwoSortedArrays()
        {
            Assert.Equal(new int[] { -5, 2, 4, 10, 11, 12, 18 },
            Program.MergeTwoSortedArrays(
                new int[] { 2, 4, 10, 18 },
                new int[] { -5, 11, 12 }));
        }

        [Fact]
        public void MergeSortWorks()
        {
            Assert.Equal(
                new int[] { -20, -18, 1, 4, 4, 17, 19, 20, 20, 35 },
                Program.MergeSort(new int[] { 20, 19, 35, -18, 17, -20, 20, 1, 4, 4 }));
        }

        [Fact]
        public void CountingInversionsWorks()
        {
            Assert.Equal(2,
                Program.CountInversions(new int[] { -6, 1, 15, 8, 10 }));
        }
    }
}
