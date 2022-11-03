using System;
using System.Net.Http.Headers;
using System.Text;

namespace CodeRunner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var head = CreateSingleLinkedList<int>(new int[] { 1, 2, 3, 4 });
            PrintSingleLinkedList<int>(head);
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

        public static Node<T> CreateSingleLinkedList<T>(params T[] arr)
        {
            if (arr == null || arr.Length < 1)
                return null;

            var head = new Node<T>(arr[0], null);
            Node<T> lastNode = null;

            for (int i = 1; i < arr.Length; i++)
            {
                var newNode = new Node<T>(arr[i], null);
                if (i == 1)
                    head.Next = newNode;
                else lastNode.Next = newNode;
                lastNode = newNode;
            }

            return head;
        }

        public static void AddAtEndOfSingleLinkedList<T>(T element, ref Node<T> head)
        {
            if (head == null)
            {
                var newHead = new Node<T>(element);
                head = newHead;
                return;
            }

            var node = head;
            while (node.Next != null)
            {
                node = node.Next;
            }
            var newNode = new Node<T> (element);
            node.Next = newNode;
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
                if (node == null)
                {
                    result += "null";
                    break;
                }
                result += node.ToString();
                if (node.Next == null)
                {
                    result += "null";
                    break;
                }
                node = node.Next;
            }
            Console.WriteLine(result);
        }
    }
}
