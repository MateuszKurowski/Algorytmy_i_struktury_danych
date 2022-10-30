using System;
using System.Linq;

namespace Efektywnosc_algorytmow_eksperyment
{
    internal static class Generators
    {
        public static int[] GenerateRandom(int size, int minVal, int maxVal)
        {
            var resultArray = new int[size];
            var random = new Random();
            for (int i = 0; i < size; i++)
            {
               resultArray[i] = random.Next(minVal, maxVal + 1);
            }
            return resultArray;
        }

        public static int[] GenerateSorted(int size, int minVal, int maxVal)
        {
            var resultArray = GenerateRandom(size, minVal, maxVal);
            Array.Sort(resultArray);
            return resultArray;
        }

        public static int[] GenerateReversed(int size, int minVal, int maxVal)
        {
            var resultArray = GenerateSorted(size, minVal, maxVal);
            Array.Reverse(resultArray);
            return resultArray;
        }

        public static int[] GenerateAlmostSorted(int size, int minVal, int maxVal, double percentOfArrayToChange = 8.0)
        {
            var resultArray = GenerateSorted(size, minVal, maxVal);
            var numbersToChange = Math.Round(resultArray.Length * (percentOfArrayToChange / 100));
            var random = new Random();
            var alreadyChangedIndexes = new int[(int)numbersToChange];

            for (int i = 0; i < numbersToChange; i++)
            {
                var index = random.Next(0, resultArray.Length + 1);
                while(alreadyChangedIndexes.Contains(index))
                {
                    index = random.Next(0, resultArray.Length + 1);
                }
                alreadyChangedIndexes[i] = index;

                var newNumber = random.Next(minVal, maxVal + 1);
                while (resultArray[index] == newNumber)
                {
                    newNumber = random.Next(minVal, maxVal + 1);
                }
                resultArray[index] = newNumber;
            }
            return resultArray;
        }
    }
}