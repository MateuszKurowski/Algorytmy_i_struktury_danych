namespace Efektywnosc_algorytmow_eksperyment
{
    internal static class MergeSort
    {
        public static int[] Sort(int[] array, int l, int r)
        {
            if (l < r)
            {
                var m = l + (r - l) / 2;
                Sort(array, l, m);
                Sort(array, m + 1, r);
                Merge(array, l, m, r);
            }
            return new int[1];
        }

        private static void Merge(int[] array, int l, int m, int r)
        {
            var n1 = m - l + 1;
            var n2 = r - m;
            var L = new int[n1];
            var R = new int[n2];
            int i, j;

            for (i = 0; i < n1; ++i)
                L[i] = array[l + i];
            for (j = 0; j < n2; ++j)
                R[j] = array[m + 1 + j];

            i = 0;
            j = 0;

            int k = l;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    array[k] = L[i];
                    i++;
                }
                else
                {
                    array[k] = R[j];
                    j++;
                }
                k++;
            }

            while (i < n1)
            {
                array[k] = L[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                array[k] = R[j];
                j++;
                k++;
            }
        }
    }
}