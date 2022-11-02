using System;

public class Test
{
    public static void Main()
    {
        var firstLine = Console.ReadLine();
        var secondLine = Console.ReadLine();

        var firstLineArray = Array.ConvertAll(firstLine.Split(' ', StringSplitOptions.RemoveEmptyEntries), s => Convert.ToInt32(s));
        var n = firstLineArray[0];
        var m = firstLineArray[1];
        var hotelsCost = Array.ConvertAll(secondLine.Split(' ', StringSplitOptions.RemoveEmptyEntries), s => Convert.ToInt32(s));

        var maxValue = 0;
        var firstElementIndex = 0;
        var currentValue = 0;

        for (int i = 0; i < n; i++)
        {
            if (hotelsCost[i] + currentValue <= m)
            {
                currentValue += hotelsCost[i];
            }
            else
            {
                while (hotelsCost[i] + currentValue > m)
                {
                    currentValue -= hotelsCost[firstElementIndex];
                    firstElementIndex++;
                }
                currentValue += hotelsCost[i];
            }
            if (currentValue > maxValue)
                maxValue = currentValue;
        }
        Console.WriteLine(maxValue);
    }
}