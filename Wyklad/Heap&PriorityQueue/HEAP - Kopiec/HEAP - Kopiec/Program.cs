using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

internal class Test
{
    static void Main()
    {
        var t = FastIO.ReadNonNegativeInt();
        //var t = 1;
        var sb = new StringBuilder();
        for (int i = 0; i < t; i++)
        {
            var n = FastIO.ReadNonNegativeInt();
            var heap = new Heap();
            for (int j = 0; j < n; j++)
            {
                heap.Insert(FastIO.ReadInt());
            }
            //var heap = new Heap<int>(new List<int>() { 3, -14, -3, 15, 13, -5, 6, -8, -11, 1 });

            var m = FastIO.ReadNonNegativeInt();
            //var m = 6;
            //var tasks = new List<string>() { "P", "E", "E", "P", "E", "E" };
            for (int j = 0; j < m; j++)
            {
                var task = FastIO.ReadASCIIString();
                //var task = tasks[j];
                switch (task)
                {
                    case "P":
                        foreach (var item in heap)
                        {
                            sb.Append(item);
                            sb.Append(' ');
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sb.AppendLine();
                        break;

                    case "E":
                        sb.Append(heap.Delete());
                        sb.AppendLine();
                        break;
                }
            }
        }
        Console.WriteLine(sb.ToString());
    }
}

#region Kopiec
public class Heap : IEnumerable<int>
{
    private List<int> list;

    public Heap()
    {
        list = new List<int>();
    }

    public int Count => list.Count;

    private void Swap(ref int item1, ref int item2)
    {
        var temp = item1;
        item1 = item2;
        item2 = temp;
    }

    private int Parent(int key) => (key - 1) / 2;

    private int Left(int key) => 2 * key + 1;

    private int Right(int key) => 2 * key + 2;

    public void Insert(int x)
    {
        list.Add(x);
        var index = list.LastIndexOf(x);

        while (index != 0 && list[index].CompareTo(list[Parent(index)]) < 0)
        {
            var item = list[index];
            var parent = list[Parent(index)];
            Swap(ref item,
                 ref parent);
            index = Parent(index);
        }
    }

    public int Delete()
    {
        var elementToDelete = Top();
        var lastElement = list[list.Count - 1];

        Swap(ref elementToDelete, ref lastElement);
        list.RemoveAt(list.Count - 1);

        MinHeapify(0);

        return elementToDelete;
    }

    public void MinHeapify(int key)
    {
        int l = Left(key);
        int r = Right(key);

        int smallest = key;
        if (l < Count - 1 &&
            list[l] < list[smallest])
        {
            smallest = l;
        }
        if (r < Count - 1 &&
            list[r] < list[smallest])
        {
            smallest = r;
        }
        if (l == r && l == smallest)
            smallest = l;

        if (smallest != key)
        {
            var item1 = list[key];
            var item2 = list[Parent(smallest)];
            Swap(ref item1,
                 ref item2);
            MinHeapify(smallest);
        }
    }

    public int Top() => list.Count > 0 ? list[0] : throw new InvalidOperationException("Heap is empty.");

    public IEnumerator<int> GetEnumerator()
    {
        foreach (var item in list)
        {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
#endregion

#region FastIO
public static class FastIO
{
    private const byte NULL = (byte)'\0';
    private const byte NEWLINE = (byte)'\n';
    private const byte MINUS_SIGN = (byte)'-';
    private const byte SPACE_CHAR = (byte)' ';
    private const byte ZERO = (byte)'0';
    private const int INPUT_BUFFER_LIMIT = 8192;
    private const int OUTPUT_BUFFER_LIMIT = 8192;

    private static readonly Stream _inputStream = Console.OpenStandardInput();
    private static readonly byte[] _inputBuffer = new byte[INPUT_BUFFER_LIMIT];
    private static int _inputBufferSize = 0;
    private static int _inputBufferIndex = 0;

    private static readonly Stream _outputStream = Console.OpenStandardOutput();
    private static readonly byte[] _outputBuffer = new byte[OUTPUT_BUFFER_LIMIT];
    private static readonly byte[] _digitsBuffer = new byte[11];
    private static int _outputBufferSize = 0;

    private static byte ReadByte()
    {
        if (_inputBufferIndex == _inputBufferSize)
        {
            _inputBufferIndex = 0;
            _inputBufferSize = _inputStream.Read(_inputBuffer, 0, INPUT_BUFFER_LIMIT);
            if (_inputBufferSize == 0)
                return NULL; // All input has been read.
        }

        return _inputBuffer[_inputBufferIndex++];
    }

    public static string ReadASCIIString(int initialLength = 1)
    {
        byte asciiChar;
        // Consume and discard whitespace characters (their ASCII codes are all < MINUS_SIGN).
        do
        {
            asciiChar = ReadByte();
        }
        while (asciiChar < SPACE_CHAR);

        // Build up the string from its chars, until we run into whitespace or the null byte.
        StringBuilder sb = new StringBuilder(initialLength);
        sb.Append((char)asciiChar);
        while (true)
        {
            asciiChar = ReadByte();
            if (asciiChar < SPACE_CHAR) break;
            sb.Append((char)asciiChar);
        }

        return sb.ToString();
    }

    public static void WriteASCIIString(string asciiString)
    {
        Console.Write(asciiString);
    }
    public static void WriteLineASCIIString(string asciiString)
    {
        Console.WriteLine(asciiString);
    }

    public static int ReadNonNegativeInt()
    {
        byte digit;

        // Consume and discard whitespace characters (their ASCII codes are all < MINUS_SIGN).
        do
        {
            digit = ReadByte();
        }
        while (digit < MINUS_SIGN);

        // Build up the integer from its digits, until we run into whitespace or the null byte.
        int result = (digit - ZERO);
        while (true)
        {
            digit = ReadByte();
            if (digit < ZERO) break;
            result = result * 10 + (digit - ZERO);
        }

        return result;
    }

    public static int ReadInt()
    {
        // Consume and discard whitespace characters (their ASCII codes are all < MINUS_SIGN).
        byte digit;
        do
        {
            digit = ReadByte();
        }
        while (digit < MINUS_SIGN);

        bool isNegative = (digit == MINUS_SIGN);
        if (isNegative)
        {
            digit = ReadByte();
        }

        // Build up the integer from its digits, until we run into whitespace or the null byte.
        int result = isNegative ? -(digit - ZERO) : (digit - ZERO);
        while (true)
        {
            digit = ReadByte();
            if (digit < ZERO) break;
            result = result * 10 + (isNegative ? -(digit - ZERO) : (digit - ZERO));
        }

        return result;
    }

    public static void WriteNonNegativeInt(int value)
    {
        int digitCount = 0;
        do
        {
            int digit = value % 10;
            _digitsBuffer[digitCount++] = (byte)(digit + ZERO);
            value /= 10;
        } while (value > 0);

        if (_outputBufferSize + digitCount > OUTPUT_BUFFER_LIMIT)
        {
            _outputStream.Write(_outputBuffer, 0, _outputBufferSize);
            _outputBufferSize = 0;
        }

        while (digitCount > 0)
        {
            _outputBuffer[_outputBufferSize++] = _digitsBuffer[--digitCount];
        }
    }

    public static void WriteInt(int value)
    {
        bool isNegative = value < 0;

        int digitCount = 0;
        do
        {
            int digit = isNegative ? -(value % 10) : (value % 10);
            _digitsBuffer[digitCount++] = (byte)(digit + ZERO);
            value /= 10;
        } while (value != 0);

        if (isNegative)
        {
            _digitsBuffer[digitCount++] = MINUS_SIGN;
        }

        if (_outputBufferSize + digitCount > OUTPUT_BUFFER_LIMIT)
        {
            _outputStream.Write(_outputBuffer, 0, _outputBufferSize);
            _outputBufferSize = 0;
        }

        while (digitCount > 0)
        {
            _outputBuffer[_outputBufferSize++] = _digitsBuffer[--digitCount];
        }
    }

    public static void WriteLine()
    {
        if (_outputBufferSize == OUTPUT_BUFFER_LIMIT) // else _outputBufferSize < OUTPUT_BUFFER_LIMIT.
        {
            _outputStream.Write(_outputBuffer, 0, _outputBufferSize);
            _outputBufferSize = 0;
        }

        _outputBuffer[_outputBufferSize++] = NEWLINE;
    }

    public static void Flush()
    {
        _outputStream.Write(_outputBuffer, 0, _outputBufferSize);
        _outputBufferSize = 0;
        _outputStream.Flush();
    }
}
#endregion