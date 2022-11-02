using System;
using System.Text;

public class Test
{
    public static void Main()
    {
        var sb = new StringBuilder();

        var firstLine = Console.ReadLine();
        var T = Convert.ToInt32(firstLine);

        for (int t = 0; t < T; t++)
        {
            var secondLine = Console.ReadLine();

            var secondLineArray = Array.ConvertAll(secondLine.Split(' ', StringSplitOptions.RemoveEmptyEntries), s => Convert.ToInt32(s));
            var At = Convert.ToInt32(secondLineArray[0]);
            var Bt = Convert.ToInt32(secondLineArray[1]);

            var thirdLine = Console.ReadLine();
            var peopleOnStation = Array.ConvertAll(thirdLine.Split(' ', StringSplitOptions.RemoveEmptyEntries), s => Convert.ToInt32(s));

            var seenPeople = 0;
            var possibleToSeePeople = 0;
            var firstIndex = 0;
            var seenStation = 0;
            var possibleStation = 0;

            for (int i = 0; i < At; i++)
            {
                if (possibleToSeePeople + peopleOnStation[i] <= Bt)
                {
                    possibleToSeePeople += peopleOnStation[i];
                    possibleStation++;
                }
                else
                {
                    while (possibleToSeePeople + peopleOnStation[i] > Bt)
                    {
                        possibleToSeePeople -= peopleOnStation[firstIndex];
                        firstIndex++;
                        possibleStation--;
                    }
                    possibleToSeePeople += peopleOnStation[i];
                    possibleStation++;
                }
                if (seenStation == 0
                    || seenStation < possibleStation
                    )
                {
                    seenPeople = possibleToSeePeople;
                    seenStation = possibleStation;
                }
                else if (seenStation == possibleStation)
                { seenPeople = Math.Min(seenPeople, possibleToSeePeople); }
            }
            sb.Append(seenPeople);
            sb.Append(' ');
            sb.Append(seenStation);
            sb.Append(Environment.NewLine);
            Console.WriteLine(sb.ToString());
            sb.Clear();
        }
    }
}