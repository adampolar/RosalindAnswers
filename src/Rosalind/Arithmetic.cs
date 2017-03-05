using System;

namespace Rosalind
{
    public class Arithmetic
    {
        public static int GetLargestTriangleNumberLessThan(int thisNumber)
        {
            if (thisNumber < 1)
            {
                return -1;
            }
            var lastAnswer = 1;
            var answer = 3;
            for (int i = 2; true; i++)
            {
                if (answer > thisNumber)
                {
                    return lastAnswer;
                }
                lastAnswer = answer;
                answer = answer + (int)Math.Pow(2, i);
            }

        }
    }
}