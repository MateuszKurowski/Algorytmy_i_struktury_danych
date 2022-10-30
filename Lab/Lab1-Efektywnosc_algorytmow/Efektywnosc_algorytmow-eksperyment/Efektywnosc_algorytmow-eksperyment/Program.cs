using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Timers;

namespace Efektywnosc_algorytmow_eksperyment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var manager = new TryManager();

            // Small
            manager.GenerateNewCase(Constants.Arrays.SmallArray, Constants.SortType.Random, 0, 100);
            manager.GenerateNewCase(Constants.Arrays.SmallArray, Constants.SortType.Sorted, 0, 100);
            manager.GenerateNewCase(Constants.Arrays.SmallArray, Constants.SortType.Reversed, 0, 100);
            manager.GenerateNewCase(Constants.Arrays.SmallArray, Constants.SortType.AlmostSorted, 0, 100);
            manager.GenerateNewCase(Constants.Arrays.SmallArray, Constants.SortType.FewUnique, 1, 10);

            // Medium
            manager.GenerateNewCase(Constants.Arrays.MediumArray, Constants.SortType.Random, 0, 100);
            manager.GenerateNewCase(Constants.Arrays.MediumArray, Constants.SortType.Sorted, 0, 100);
            manager.GenerateNewCase(Constants.Arrays.MediumArray, Constants.SortType.Reversed, 0, 100);
            manager.GenerateNewCase(Constants.Arrays.MediumArray, Constants.SortType.AlmostSorted, 0, 100);
            manager.GenerateNewCase(Constants.Arrays.MediumArray, Constants.SortType.FewUnique, 1, 10);

            // Large
            manager.GenerateNewCase(Constants.Arrays.LargeArray, Constants.SortType.Random, 0, 100);
            manager.GenerateNewCase(Constants.Arrays.LargeArray, Constants.SortType.Sorted, 0, 100);
            manager.GenerateNewCase(Constants.Arrays.LargeArray, Constants.SortType.Reversed, 0, 100);
            manager.GenerateNewCase(Constants.Arrays.LargeArray, Constants.SortType.AlmostSorted, 0, 100);
            manager.GenerateNewCase(Constants.Arrays.LargeArray, Constants.SortType.FewUnique, 1, 10);

            Console.WriteLine(manager.ToString());
        }
    }
}