using System;
using System.Collections.Generic;

public static class PhoneList
{
    public static bool Solve(int phoneNumberCount, string[] phoneNumbers)
    {
        var PhoneList = new PhoneNode();
        for (var i = 0; i < phoneNumberCount; ++i)
        {
            if (PhoneList.Add(phoneNumbers[i]))
                return false;
        }
        return true;
    }
}

public static class Program
{
    private static void Main()
    {
        var phoneNumbers = new string[10000];
        var rem = int.Parse(Console.ReadLine());
        while (rem > 0)
        {
            var phoneNumberCount = int.Parse(Console.ReadLine());

            for (var i = 0; i < phoneNumberCount; ++i)
            {
                phoneNumbers[i] = Console.ReadLine();
            }

            Console.WriteLine(
                PhoneList.Solve(phoneNumberCount, phoneNumbers) ? "YES" : "NO");

            rem--;
        }
    }
}

public class PhoneNode
{
    public bool Add(string word)
    {
        var isPrefixedByAWord = false;
        var isPrefixOfAWord = false;

        var currentNode = _root;
        Node nextNode = null;
        var index = 0;

        while (index < word.Length
                && currentNode.Children.TryGetValue(word[index], out nextNode))
        {
            currentNode = nextNode;
            index++;

            if (currentNode.IsAWordEnd)
                isPrefixedByAWord = true;
        }

        if (index == word.Length)
        {
            if (currentNode.Children.Count != 0)
                isPrefixOfAWord = true;
        }
        else
        {
            while (index < word.Length)
            {
                nextNode = new Node(word[index]);
                currentNode.Children.Add(word[index], nextNode);
                currentNode = nextNode;
                ++index;
            }
        }
        currentNode.IsAWordEnd = true;

        return isPrefixedByAWord
                    || isPrefixOfAWord;
    }

    private readonly Node _root = new Node((char)0);

    private class Node
    {
        public Dictionary<char, Node> Children { get; } = new Dictionary<char, Node>();

        public bool IsAWordEnd { get; set; }

        public char Value { get; }

        public Node(char value)
        {
            Value = value;
        }
    }
}