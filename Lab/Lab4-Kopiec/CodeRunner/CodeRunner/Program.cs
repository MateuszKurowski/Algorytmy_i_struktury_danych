using System;
using System.Collections.Generic;

namespace CodeRunner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Test1();
            WaitForRespond();
            Test2();
            WaitForRespond();
            Test3();
            WaitForRespond();
            Test4();
            WaitForRespond();
            Test5();
            WaitForRespond();
        }

        static void Test1()
        {
            var heap = new Heap<int>(new int[] { 2, 1, 6, 7, 1, 3 }, HeapOptions.MaxHeap);
            int[] tab = heap.ToArray();
            Console.WriteLine(string.Join(' ', tab));
            Console.WriteLine("Element szczytowy: " + heap.Top());
            Console.WriteLine("Liczba elementów: " + heap.Count);
            heap.Clear();
            Console.WriteLine("Liczba elementów po czyszczeniu: " + heap.Count);
            try
            {
                Console.WriteLine(heap.Top());
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Wyjątek przechwycony");
            }


            PrintCorrectAnswer("7 6 3 1 1 2\r\nElement szczytowy: 7\r\nLiczba elementów: 6\r\nLiczba elementów po czyszczeniu: 0\r\nWyjątek przechwycony");
        }

        static void Test2()
        {
            // MinHeap, tworzenie kopca
            var heap = new Heap<int>(new int[] { 2, 1, 6, 7, 1, 3 }, HeapOptions.MinHeap);
            int[] tab = heap.ToArray();
            Console.WriteLine(string.Join(' ', tab));
            Console.WriteLine(heap.Top());
            heap.Clear();
            try
            {
                Console.WriteLine(heap.Delete());
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Wyjątek przechwycony");
            }


            PrintCorrectAnswer("1 1 3 7 2 6\r\n1\r\nWyjątek przechwycony");
        }

        static void Test3()
        {
            // dodawanie elementu do kopca
            var heap = new Heap<int>(new int[] { 2, 3, 6, 7, 8, 3 }, HeapOptions.MinHeap);
            Console.WriteLine(string.Join(' ', heap.ToArray()));
            heap.Insert(5);
            Console.WriteLine(string.Join(' ', heap.ToArray()));
            heap.Insert(1);
            Console.WriteLine(string.Join(' ', heap.ToArray()));


            PrintCorrectAnswer("2 3 3 7 8 6\r\n2 3 3 7 8 6 5\r\n1 2 3 3 8 6 5 7");
        }

        static void Test4()
        {
            // usuwanie elementu z kopca
            var heap = new Heap<char>(new char[] { 'd', 'f', 'g', 'o', 'c', 'h' }, HeapOptions.MaxHeap);
            Console.WriteLine(string.Join(' ', heap.ToArray()));
            Console.WriteLine(heap.Delete());
            Console.WriteLine(string.Join(' ', heap.ToArray()));


            PrintCorrectAnswer("o g h d c f\r\no\r\nh g f d c");
        }

        static void Test5()
        {
            // IEnumerable<T>
            var heap = new Heap<int>(new List<int> { 2, 1, 6, 7, 1, 3 }, HeapOptions.MaxHeap);
            foreach (var x in heap)
                Console.Write(x);


            PrintCorrectAnswer("763112");
        }

        static void WaitForRespond()
        {
            Console.ReadKey();
            Console.Clear();
        }

        static void PrintCorrectAnswer(string answer)
        {
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Poprawne odpowiedzi: ");
            Console.WriteLine();
            Console.WriteLine(answer);
        }
    }
}
