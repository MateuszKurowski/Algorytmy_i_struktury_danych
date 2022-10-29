using System;

namespace INTEST_Enormous_Input_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var firstInputArray = Console.ReadLine().Split(' ');
            var n = int.Parse(firstInputArray[0]);
            var k = int.Parse(firstInputArray[1]);
            var result = 0;

            for (int i = 0; i < n; i++)
            {
                var t = int.Parse(Console.ReadLine());
                if (t % k == 0)
                    result++;
            }
            Console.WriteLine(result);
        }
    }
}