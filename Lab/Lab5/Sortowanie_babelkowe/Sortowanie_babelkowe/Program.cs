using System;
using System.Collections.Generic;
using System.Linq;

namespace Sortowanie_babelkowe
{
    public class Program
    {
        public static void Main()
        {
            var firstInput = Console.ReadLine();
            var secondInput = Console.ReadLine();

            var array = firstInput.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                            .Select(x => int.Parse(x))
                                            .ToList();
            var rangeArray = secondInput.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                                .Select(x => int.Parse(x))
                                                .ToList();
            if (array.Count < 1
                    || rangeArray.Count < 1)
                return;

            var firstIndex = rangeArray[0];
            var lastIndex = rangeArray[1];

            for (var i = firstIndex; i <= lastIndex; i++)
            {
                if (i - 1 < 0) continue;
                if (array[i] <= array[i - 1])
                    continue;

                var currentIndex = i;
                while (array[currentIndex] > array[currentIndex - 1])
                {
                    if (currentIndex - 1 < firstIndex)
                        break;
                    SwapWithPreviousIndex(array, currentIndex);
                    currentIndex--;
                    if (currentIndex <= 0) break;
                }
            }

            Console.WriteLine(string.Join(' ', array));
        }

        private static void SwapWithNextIndex(List<int> array, int index)
        {
            if (index + 1 > array.Count + 1) return;
            var temp = array[index];
            array[index] = array[index + 1];
            array[index + 1] = temp;
        }

        private static void SwapWithPreviousIndex(List<int> array, int index)
        {
            if (index - 1 > array.Count - 1) return;
            var temp = array[index];
            array[index] = array[index - 1];
            array[index - 1] = temp;
        }
    }
}