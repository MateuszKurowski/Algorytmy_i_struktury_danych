using System;
using System.Text;

using static Efektywnosc_algorytmow_eksperyment.Constants;

namespace Efektywnosc_algorytmow_eksperyment
{
    internal class TryManager
    {
        public int Cases { get; private set; }
        private StringBuilder _sb;

        public TryManager()
        {
            _sb = new StringBuilder();
            Cases = 0;
        }

        public void GenerateNewCase(Arrays arrayType, SortType sortType, int minValue, int maxValue)
        {
            Cases++;
            var array = GenerateArray(arrayType, sortType, minValue, maxValue);
            var insertionSortTimes = new TimeOfExecuteManager(SortAlgorithm.InsertionSort, array);
            var mergeSortTimes = new TimeOfExecuteManager(SortAlgorithm.MergeSort, array);
            var quickSortClassicalTimes = new TimeOfExecuteManager(SortAlgorithm.QuickSortClassicial, array);
            var quickSortTimes = new TimeOfExecuteManager(SortAlgorithm.QuickSort, array);

            _sb.Append($"Przypadek {Cases}: próba {(arrayType == Arrays.SmallArray ? "mała" : arrayType == Arrays.MediumArray ? "średnia" : "duża")} (n = {(arrayType == Arrays.SmallArray ? "10" : arrayType == Arrays.MediumArray ? "1000" : "100000")}), {(sortType == SortType.Random ? "random" : sortType == SortType.Sorted ? "sorted" : sortType == SortType.Reversed ? "reversed" : sortType == SortType.AlmostSorted ? "almost sorted" : "few unique")}{Environment.NewLine}");
            _sb.Append($"* InsertionSort: t = {insertionSortTimes.AverageTime} +/- {insertionSortTimes.StandardDeviation}{Environment.NewLine}");
            _sb.Append($"* MergeSort: t = {mergeSortTimes.AverageTime} +/- {mergeSortTimes.StandardDeviation}{Environment.NewLine}");
            _sb.Append($"* QuickSortClassical: t = {quickSortClassicalTimes.AverageTime} +/- {quickSortClassicalTimes.StandardDeviation}{Environment.NewLine}");
            _sb.Append($"* QuickSort: t = {quickSortTimes.AverageTime} +/- {quickSortTimes.StandardDeviation}{Environment.NewLine}");
            _sb.Append(Environment.NewLine);
        }

        public override string ToString()
        {
            Console.OutputEncoding = Encoding.UTF8;
            return _sb.ToString();
        }

        private int[] GenerateArray(Arrays arrayType, SortType sortType, int minValue, int maxValue)
        {
            switch(sortType)
            {
                case SortType.Random:
                    {
                        if (arrayType == Arrays.SmallArray)
                            return Generators.GenerateRandom(10, minValue, maxValue);
                        else if (arrayType == Arrays.MediumArray)
                            return Generators.GenerateRandom(1000, minValue, maxValue);
                        else
                            return Generators.GenerateRandom(100000, minValue, maxValue);
                    }
                case SortType.Sorted:
                    {
                        if (arrayType == Arrays.SmallArray)
                            return Generators.GenerateSorted(10, minValue, maxValue);
                        else if (arrayType == Arrays.MediumArray)
                            return Generators.GenerateSorted(1000, minValue, maxValue);
                        else
                            return Generators.GenerateSorted(100000, minValue, maxValue);
                    }
                case SortType.Reversed:
                    {
                        if (arrayType == Arrays.SmallArray)
                            return Generators.GenerateReversed(10, minValue, maxValue);
                        else if (arrayType == Arrays.MediumArray)
                            return Generators.GenerateReversed(1000, minValue, maxValue);
                        else
                            return Generators.GenerateReversed(100000, minValue, maxValue);
                    }
                case SortType.AlmostSorted:
                    {
                        if (arrayType == Arrays.SmallArray)
                            return Generators.GenerateAlmostSorted(10, minValue, maxValue);
                        else if (arrayType == Arrays.MediumArray)
                            return Generators.GenerateAlmostSorted(1000, minValue, maxValue);
                        else
                            return Generators.GenerateAlmostSorted(100000, minValue, maxValue);
                    };
                case SortType.FewUnique:
                    {
                        if (arrayType == Arrays.SmallArray)
                            return Generators.GenerateRandom(10, 1, 10);
                        else if (arrayType == Arrays.MediumArray)
                            return Generators.GenerateRandom(1000, 1, 10);
                        else
                            return Generators.GenerateRandom(100000, 1, 10);
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}