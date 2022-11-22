
using Multizbior;

using System.IO;
using System.Numerics;

namespace MultiSet.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var data = new char[] { 'a', 'b', 'c', 'd', 'a', 'd' };
            var ms = new MultiSet<char>(data);
            var data2 = new char[] { 'b', 'c', 'c'};
            var ms2 = new MultiSet<char>(data2);
            var array = new char[5];
            ms2.CopyTo(array, 1);

            var ms3 = ms - ms2;

            //System.Console.WriteLine("Hello World!");

            //char[] znaki = new char[] { 'a', 'b', 'c', 'a', 'a', 'b', 'd' };
            //char[] znaki2 = new char[] { 'b', 'a', 'c', 'a', 'a', 'd', 'b' };
            //var mz = new MultiSet<char>(znaki);
            //var mz2 = new MultiSet<char>(znaki2);

            //var test = mz.MultiSetEquals(new char[] { 'a', 'f', 'g', 'd' });
            //var test2 = mz.MultiSetEquals(mz2);
            //var test3 = mz.MultiSetEquals(mz);

            //System.Console.WriteLine(mz.ToString());
            //System.Console.WriteLine(mz.ToStringExpanded());

            //var t = IMultiSet<int>.Empty;

            //foreach (var x in mz)
            //{

            //}
        }
    }
}
