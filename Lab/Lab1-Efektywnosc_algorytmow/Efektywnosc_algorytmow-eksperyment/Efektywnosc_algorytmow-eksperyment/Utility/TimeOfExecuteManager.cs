using System;
using System.Diagnostics;
using System.Linq;

using static Efektywnosc_algorytmow_eksperyment.Constants;


namespace Efektywnosc_algorytmow_eksperyment
{
    internal class TimeOfExecuteManager
    {
        public double AverageTime { get; set; }
        public double StandardDeviation { get; set; }
        private readonly double[] _times;

        public TimeOfExecuteManager(SortAlgorithm sortAlgorithm, int[] array, int numberOfExecutions = 10)
        {
            _times = new double[numberOfExecutions];
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < numberOfExecutions; i++)
            {
                switch(sortAlgorithm)
                {
                    case SortAlgorithm.InsertionSort:
                        stopwatch.Start();
                        InsertionSort.Sort(array);
                        stopwatch.Stop();
                        _times[i] = stopwatch.Elapsed.TotalMilliseconds;
                        stopwatch.Reset();
                        break;
                    case SortAlgorithm.QuickSortClassicial:
                        stopwatch.Start();
                        QuickSortClassical.Sort(array, 0, array.Length - 1);
                        stopwatch.Stop();
                        _times[i] = stopwatch.Elapsed.TotalMilliseconds;
                        stopwatch.Reset();
                        break;
                    case SortAlgorithm.MergeSort:
                        stopwatch.Start();
                        MergeSort.Sort(array, 0, array.Length - 1);
                        stopwatch.Stop();
                        _times[i] = stopwatch.Elapsed.TotalMilliseconds;
                        stopwatch.Reset();
                        break;
                    case SortAlgorithm.QuickSort:
                        stopwatch.Start();
                        Array.Sort(array);
                        stopwatch.Stop();
                        _times[i] = stopwatch.Elapsed.TotalMilliseconds;
                        stopwatch.Reset();
                        break;
                }
            }
            Calculate();
        }

        private void Calculate()
        {
            AverageTime = Math.Round(_times.Average(), 4);
            StandardDeviation = Math.Round(Math.Sqrt(_times.Average(v => Math.Pow(v - AverageTime, 2))), 6);
        }
    }
}