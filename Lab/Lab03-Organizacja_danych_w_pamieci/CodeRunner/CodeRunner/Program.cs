using System;
using System.Text;

namespace CodeRunner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Node<int> head1 =
   new Node<int>(2,
     new Node<int>(5,
       new Node<int>(1)));
            PrintSingleLinkedList<int>(head1);
        }

        public class Node<T>
        {
            public T Data { get; set; }
            public Node<T> Next { get; set; }
            public Node(T data, Node<T> next = null)
            {
                Data = data; Next = next;
            }
            public override string ToString() => (this == null) ? "null" : $"{Data} -> ";
        }

        public static void PrintSingleLinkedList<T>(Node<T> head)
        {
            var result = "head -> ";
            if (head == null)
            {
                result += "null";
                Console.WriteLine(result);
                return;
            }
            result += head.ToString();
            var node = head.Next;

            while (true)
            {
                if (node.Next == null)
                {
                    result += "null";
                    break;
                }
                result += node.ToString();
                node = node.Next;
            }
            Console.WriteLine(result);
        }
    }
}
