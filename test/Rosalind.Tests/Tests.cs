using Xunit;
using Rosalind;
using System;

namespace Tests
{
    public class Tests
    {
        [Fact]
        public void MergeTwoSortedArrays()
        {
            Assert.Equal(new int[] { -5, 2, 4, 10, 11, 12, 18 },
            new int[] { 2, 4, 10, 18 }.MergeTwoSortedArrays(
                new int[] { -5, 11, 12 }));
        }

        [Fact]
        public void MergeSortWorks()
        {
            Assert.Equal(
                new int[] { -20, -18, 1, 4, 4, 17, 19, 20, 20, 35 },
                new int[] { 20, 19, 35, -18, 17, -20, 20, 1, 4, 4 }.MergeSort());
        }

        [Fact]
        public void CountingInversionsWorks()
        {
            Assert.Equal(2,
                new int[] { -6, 1, 15, 8, 10 }.CountInversions());
        }
        
        [Fact]
        public void TestTwoSum()
        {
            Assert.Equal(null, new int[] { 2, -3, 4, 10, 5 }.Determine2Sum());

            var arr = new int[] { 8, 2, 4, -2, -8 };
            var ans = arr.Determine2Sum();
            Assert.NotNull(ans);
            Assert.Equal(arr[ans.Item1 - 1], -arr[ans.Item2 - 1]);
            
            Assert.Equal(null, new int[] { -5, 2, 3, 2, -4 }.Determine2Sum());

            arr = new int[] { 5, 4, -5, 6, 8 };
            ans = arr.Determine2Sum();
            Assert.NotNull(ans);
            Assert.Equal(arr[ans.Item1 - 1], -arr[ans.Item2 - 1]);
        }

        [Fact]
        public void TestThreeSum()
        {
            Assert.Equal(null, new int[] { 2, -3, 4, 10, 5 }.Determine3Sum());

            var arr = new int[] { 8, -6, 4, -2, -8};
            var ans = arr.Determine3Sum();
            Assert.NotNull(ans);
            Assert.Equal(0, arr[ans.Item1 - 1] + arr[ans.Item2 - 1] + arr[ans.Item3 - 1]);

            arr = new int[] { -5, 2, 3, 2, -4};
            ans = arr.Determine3Sum();
            Assert.NotNull(ans);
            Assert.Equal(0, arr[ans.Item1 - 1] + arr[ans.Item2 - 1] + arr[ans.Item3 - 1]);
            
            Assert.Equal(null, new int[] { -5, 2, 3, 2, -4 }.Determine2Sum());

            Assert.Equal(null, new int[] { 2, 4, -5, 6, 8 }.Determine3Sum());
        }
    }
}
