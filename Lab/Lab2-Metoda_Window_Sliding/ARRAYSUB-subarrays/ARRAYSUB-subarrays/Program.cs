using System;
using System.Text;

public class Test
{
    public static void Main()
    {
        var sb = new StringBuilder();

        var nString = Console.ReadLine();
        var arrString = Console.ReadLine();
        var kString = Console.ReadLine();

        var n = Convert.ToInt32(nString);
        var k = Convert.ToInt32(kString);
        var arr = Array.ConvertAll(arrString.Split(' ', StringSplitOptions.RemoveEmptyEntries), s => Convert.ToInt32(s));

        int maxNumber = 0;
        for (long i = 0; i < k; i++)
        {
            maxNumber = Math.Max(maxNumber, arr[i]);
        }
        sb.Append(maxNumber);
        sb.Append(' ');

        for (int i = k; i < n; i++)
        {
            if (arr[i - k] == maxNumber)
            {
                var x = i - k + 1;
                maxNumber = arr[x];
                for (int j = 0; j < k; j++)
                {
                    maxNumber = Math.Max(maxNumber, arr[x]);
                    x++;
                }
                sb.Append(maxNumber);
                sb.Append(' ');
            }
            else
            {
                maxNumber = Math.Max(maxNumber, arr[i]);
                sb.Append(maxNumber);
                sb.Append(' ');
            }
        }
        sb.Remove(sb.Length - 1, 1);
        Console.Write(sb.ToString());
    }
}