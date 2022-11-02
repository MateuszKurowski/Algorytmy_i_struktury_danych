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
            var thirdLine = Console.ReadLine();

            var secondLineArray = Array.ConvertAll(secondLine.Split(' ', StringSplitOptions.RemoveEmptyEntries), s => Convert.ToInt32(s));
            var planetsWithAutobotsNo = Array.ConvertAll(thirdLine.Split(' ', StringSplitOptions.RemoveEmptyEntries), s => Convert.ToInt32(s));

            var P = secondLineArray[0];
            var M = secondLineArray[1];

            var firstIndex = 0;
            var seenPlanets = 0;
            var planetsToPossibleToSee = 0;
            var seenAutobots = 0;
            var autobotsToPossibleToSee = 0;

            for (int i = 0; i < P; i++)
            {
                if (autobotsToPossibleToSee + planetsWithAutobotsNo[i] <= M)
                {
                    autobotsToPossibleToSee += planetsWithAutobotsNo[i];
                    planetsToPossibleToSee++;
                }
                else
                {
                    while (autobotsToPossibleToSee + planetsWithAutobotsNo[i] > M)
                    {
                        autobotsToPossibleToSee -= planetsWithAutobotsNo[firstIndex];
                        planetsToPossibleToSee--;
                        firstIndex++;
                    }
                    autobotsToPossibleToSee += planetsWithAutobotsNo[i];
                    planetsToPossibleToSee++;
                }

                if (seenPlanets == 0
                    || planetsToPossibleToSee > seenPlanets)
                {
                    seenPlanets = planetsToPossibleToSee;
                    seenAutobots = autobotsToPossibleToSee;
                }
                else if (seenPlanets == planetsToPossibleToSee)
                {
                    seenAutobots = Math.Min(seenAutobots, autobotsToPossibleToSee);
                }
            }
            sb.Append(seenAutobots).
                Append(' ').
                Append(seenPlanets).
                AppendLine();
        }
        Console.WriteLine(sb.ToString());
        sb.Clear();
    }
}