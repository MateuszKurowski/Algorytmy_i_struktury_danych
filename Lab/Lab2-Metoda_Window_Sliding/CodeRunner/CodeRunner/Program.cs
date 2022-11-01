using System;

namespace CodeRunner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[] { 1, 4, 2, 10, 23, 3, 1, 0, 20 };
            var n = 9;
            var k = 4;
            Console.WriteLine(MaxSum(arr, n, k));
        }

        static int MaxSum(int[] arr, int n, int k)
        {
            if (k > n)
                return -1;

            var result = 0;

            for (int i = 0; i < k; i++)
            {
                result += arr[i];
            }

            var sum = result;
            for (int i = k; i < n; i++)
            {
                sum += arr[i] - arr[i - k];

                if (sum > result)
                    result = sum;
            }

            return result;
        }
    }
}