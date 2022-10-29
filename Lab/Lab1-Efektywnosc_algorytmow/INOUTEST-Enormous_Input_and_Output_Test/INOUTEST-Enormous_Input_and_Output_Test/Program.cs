using System;

namespace INOUTEST_Enormous_Input_and_Output_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var result = string.Empty;

            for (int i = 0; i < n; i++)
            {
                var numbers = Console.ReadLine().Split(' ');
                var a = int.Parse(numbers[0]);
                var b = int.Parse(numbers[1]);
                result += (a * b) + Environment.NewLine;
            }
            Console.WriteLine(result);
        }
    }
}