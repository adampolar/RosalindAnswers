﻿using Xunit;
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
        
        [Fact]
        public void TestTwoSum()
        {
            Assert.Equal(null, Program.Determine2Sum(new int[] { 2, -3, 4, 10, 5 }));

            var arr = new int[] { 8, 2, 4, -2, -8 };
            var ans = Program.Determine2Sum(arr);
            Assert.NotNull(ans);
            Assert.Equal(arr[ans.Item1 - 1], -arr[ans.Item2 - 1]);
            
            Assert.Equal(null, Program.Determine2Sum(new int[] { -5, 2, 3, 2, -4 }));

            arr = new int[] { 5, 4, -5, 6, 8 };
            ans = Program.Determine2Sum(arr);
            Assert.NotNull(ans);
            Assert.Equal(arr[ans.Item1 - 1], -arr[ans.Item2 - 1]);
        }

        [Fact]
        public void TestThreeSum()
        {
            Assert.Equal(null, Program.Determine3Sum(new int[] { 2, -3, 4, 10, 5 }));

            var arr = new int[] { 8, -6, 4, -2, -8};
            var ans = Program.Determine3Sum(arr);
            Assert.NotNull(ans);
            Assert.Equal(0, arr[ans.Item1 - 1] + arr[ans.Item2 - 1] + arr[ans.Item3 - 1]);

            arr = new int[] { -5, 2, 3, 2, -4};
            ans = Program.Determine3Sum(arr);
            Assert.NotNull(ans);
            Assert.Equal(0, arr[ans.Item1 - 1] + arr[ans.Item2 - 1] + arr[ans.Item3 - 1]);
            
            Assert.Equal(null, Program.Determine2Sum(new int[] { -5, 2, 3, 2, -4 }));

            Assert.Equal(null, Program.Determine3Sum(new int[] { 2, 4, -5, 6, 8 }));
        }
    }
}
