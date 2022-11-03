using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;

using static CodeRunner.Program;

namespace CodeRunner
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var head = CreateSingleLinkedList<int>(1, 1, 2, 2, 2, 5, 6);
            PrintSingleLinkedList<int>(head);
            RemoveAllDuplicatesFromSortedLinkedList<int>(ref head);
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

        public static void RemoveAllDuplicatesFromSortedLinkedList<T>(ref Node<T> head)
    where T : IEquatable<T>, IComparable<T>
        {
            if (head == null || head.Next == null)
                return;

            Node<T> node = null;
            while (head.Next != null && head.Data.CompareTo(head.Next.Data) == 0)
            {
                node = head.Next.Next;
                if (node == null)
                {
                    head = null;
                    return;
                }
                while (head.Data.CompareTo(node.Data) == 0)
                {
                    node = node.Next;
                }
                head = node;
            }
            node = head.Next;
            if (node == null)
                return;
            Node<T> lastNode = head;
            while (node.Next != null)
            {
                if (node.Data.CompareTo(node.Next.Data) == 0)
                {
                    while (node.Data.CompareTo(node.Next.Data) == 0)
                    {
                        node = node.Next;
                    }
                    lastNode.Next = node.Next;
                    node = node.Next;
                }
                else
                {
                    lastNode = node;
                    node = lastNode.Next;
                }
            }
        }

        public static void RemoveNodeAt<T>(int position, ref Node<T> head)
        {
            if (head == null || head.Next == null)
                return;
            if (position == 0)
            {
                head = head.Next;
                return;
            }

            var currentNode = head;
            Node<T> previousNode = null;

            for (int i = 0; i < position; i++)
            {
                previousNode = currentNode;
                currentNode = currentNode.Next;
            }
            if (currentNode == null)
            {
                previousNode.Next = null;
                return;
            }
            previousNode.Next = currentNode.Next;
        }

        public static void MoveLastNodeToFront<T>(ref Node<T> head)
        {
            if (head == null || head.Next == null)
                return;

            var currentNode = head;
            Node<T> previousNode = null;

            while(currentNode.Next != null)
            {
                previousNode = currentNode;
                currentNode = currentNode.Next;
            }
            previousNode.Next = null;
            currentNode.Next = head;
            head = currentNode;
        }

        public static Node<T> ReverseSingleLinkedList<T>(Node<T> head)
        {
            if (head == null)
                return null;

            Node<T> node = head;

            var index = 1;
            while(node.Next != null)
            {
                index++;
                node = node.Next;
            }
            var listOfData = new T[index];
            node = head;
            for (int i = 0; i < index; i++)
            {
                listOfData[i] = node.Data;
                node = node.Next;
            }

            node = head;
            for (int i = index - 1; i >= 0 ; i--)
            {
                node.Data = listOfData[i];
                if (node.Next != null)
                    node = node.Next;
            }
            return head;
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
