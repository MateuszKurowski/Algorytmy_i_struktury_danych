using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efektywnosc_algorytmow_eksperyment
{
    internal class QuickSortClassical
    {
        private static void Swap(int[] arr, int i, int j)
        {
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        private static int Partition(int[] arr, int low, int high)
        {

            var pivot = arr[high];
            var i = (low - 1);

            for (var j = low; j <= high - 1; j++)
            {
                if (arr[j] < pivot)
                {

                    i++;
                    Swap(arr, i, j);
                }
            }
            Swap(arr, i + 1, high);
            return (i + 1);
        }

        public static void Sort(int[] array, int low, int high)
        {
            if (low < high)
            {
                var pi = Partition(array, low, high);
                Sort(array, low, pi - 1);
                Sort(array, pi + 1, high);
            }
        }
    }
}