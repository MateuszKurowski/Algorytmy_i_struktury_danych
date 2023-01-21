using System;

namespace SABBIRGAME
{
    public class Program
    {
        private static void Main()
        {
            var numberOfCases = int.Parse(Console.ReadLine());
            for (var i = 0; i < numberOfCases; i++)
            {
                var minHP = default(long);
                var currentHP = default(long);
                var numberOfLevels = int.Parse(Console.ReadLine());
                var HPsForLevel = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                for (var j = 0; j < numberOfLevels; j++)
                {
                    currentHP += long.Parse(HPsForLevel[j]);
                    if (currentHP < 1)
                    {
                        minHP += 1 - currentHP;
                        currentHP = 1;
                    }
                }
                Console.WriteLine(minHP);
            }
        }
    }
}