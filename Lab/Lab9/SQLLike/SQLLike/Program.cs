using System;

namespace SQLLike
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Adam".SQLike("A%a") == false);
            Console.WriteLine("Agnieszka".SQLike("A%a") == true);
            Console.WriteLine();

            Test(false, "A%a", "Adam");
            Test(true, "A%a", "Agnieszka");
            Test(false, "A%a", "agnieszka");
            Test(true, "%_a", "Agnieszka");
            Test(false, "%_a", "a");
            Test(true, "%", "alab");

            Console.WriteLine();

            Test(true, "", "");
            Test(false, "", "a");
            Test(false, "a", "");
            Test(false, "_", "");
            Test(true, "%", "");

            Console.WriteLine();

            Test(true, "_", "a");
            Test(false, "_", "aa");
            Test(true, "%", "a");
            Test(true, "%", "aa");
            Test(true, "_%", "a");
            Test(true, "_%", "aa");
            Test(true, "_%", "aaa");
            Test(false, "_%", "");
            Test(true, "%_", "a");
            Test(true, "%_", "aa");
            Test(true, "%_", "aaa");
            Test(false, "%_", "");

            Console.WriteLine();

            Test(true, "%%%ba%%ab", "baaabab");
            Test(false, "_%%ba%%ab", "baaabab");
            Test(false, "%_%ba%%ab", "baaabab");
            Test(true, "baaabab", "baaabab");
            Test(false, "baabab", "baaabab");
            Test(false, "baaabab", "baabab");
            Test(true, "%b%b", "baaabab");

            Console.WriteLine();


        }

        private static void Test(bool expected, string pattern, string text)
        {
            var result = text.SQLike(pattern);
            if (result != expected)
                Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"actual={result}, expected={expected} : pattern={pattern} | text={text}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public static class StringExtensions
    {
        public static bool SQLike(this string text, string pattern)
        {
            if (text == null
                    || pattern == null)
                throw new ArgumentNullException();

            if (text == pattern)
                return true;

            if (!pattern.Contains('%')
                    && text.Length != pattern.Length)
                return false;

            if (pattern == "%")
                return true;

            if (pattern == "_"
                    && text.Length == 1)
                return true;

            if (pattern.Contains("_")
                    && text.Length < 1)
                return false;

            if (!pattern.StartsWith('%')
                    && !pattern.StartsWith('_')
                    && pattern[0] != text[0])
                return false;
            if (!pattern.EndsWith('%')
                    && !pattern.EndsWith('_')
                    && pattern[pattern.Length - 1] != text[text.Length - 1])
                return false;

            var textIndex = 0;
            for (var patternIndex = 0; patternIndex < pattern.Length; patternIndex++)
            {
                try
                {
                    while (pattern[patternIndex] == '_')
                    {
                        if (textIndex > text.Length - 1)
                            return false;

                        patternIndex++;
                        textIndex++;
                        if (patternIndex >= pattern.Length
                                || textIndex >= text.Length)
                            return true;
                    }
                    if (pattern[patternIndex] == '%')
                    {
                        if (patternIndex + 1 < pattern.Length
                                && pattern[patternIndex + 1] == '%')
                            continue;

                        if (patternIndex == pattern.Length - 1)
                            return true;

                        if (patternIndex < pattern.Length - 1)
                            while (pattern[patternIndex + 1] == '_')
                            {
                                patternIndex++;
                                if (patternIndex >= pattern.Length - 1
                                        && textIndex >= textIndex - 1)
                                    return true;
                                if (textIndex > text.Length - 1
                                        && (pattern[patternIndex] == '%'
                                            || pattern[patternIndex] == '_')
                                        && patternIndex > pattern.Length - 1)
                                    return true;
                                textIndex++;
                            }

                        var others = pattern.Substring(patternIndex + 1);
                        if (!others.Contains('_')
                            && !others.Contains('%'))
                        {
                            var otherText = text.Substring(textIndex + 1);
                            if (otherText.Contains(others))
                                return true;
                        }

                        textIndex = text.IndexOf(pattern[patternIndex + 1], textIndex);
                        continue;
                    }

                    if (pattern[patternIndex] != text[textIndex])
                        return false;
                    textIndex++;
                }
                catch (IndexOutOfRangeException)
                {
                    return false;
                }
                catch (ArgumentOutOfRangeException)
                {
                    return false;
                }
            }

            return true;
        }
    }
}