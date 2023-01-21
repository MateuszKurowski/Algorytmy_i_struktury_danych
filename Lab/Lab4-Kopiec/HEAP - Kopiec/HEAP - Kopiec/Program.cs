using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

internal class Test
{
    private static void Main()
    {
        var t = FastIO.ReadNonNegativeInt();
        StringBuilder sb = new();

        for (var i = 0; i < t; i++)
        {
            var input = FastIO.ReadASCIIString().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var mainJobs = FastIO.ReadASCIIString().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var n = int.Parse(input[0]);
            var m = int.Parse(input[1]);
            var time = 0;
            List<int> jobsList = new(mainJobs);

            while (jobsList.Count > 0)
            {
                var job = jobsList[0];
                jobsList.RemoveAt(0);
                if (jobsList.Any(x => x > job))
                {
                    jobsList.Add(job);
                }
                else
                {
                    time++;
                    if (m == 0)
                    {
                        break;
                    }
                }

                m--;

                if (m < 0)
                    m = jobsList.Count - 1;
            }

            _ = sb.AppendLine(time.ToString());
        }

        FastIO.WriteASCIIString(sb.ToString());
    }
}

#region FastIO

public static class FastIO
{
    public static void Flush()
    {
        _outputStream.Write(_outputBuffer, 0, _outputBufferSize);
        _outputBufferSize = 0;
        _outputStream.Flush();
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
        StringBuilder sb = new(initialLength);
        _ = sb.Append((char)asciiChar);
        while (true)
        {
            asciiChar = ReadByte();
            if (asciiChar < SPACE_CHAR)
            {
                break;
            }

            _ = sb.Append((char)asciiChar);
        }

        return sb.ToString();
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

        var isNegative = digit == MINUS_SIGN;
        if (isNegative)
        {
            digit = ReadByte();
        }

        // Build up the integer from its digits, until we run into whitespace or the null byte.
        var result = isNegative ? -(digit - ZERO) : (digit - ZERO);
        while (true)
        {
            digit = ReadByte();
            if (digit < ZERO)
            {
                break;
            }

            result = (result * 10) + (isNegative ? -(digit - ZERO) : (digit - ZERO));
        }

        return result;
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
        var result = digit - ZERO;
        while (true)
        {
            digit = ReadByte();
            if (digit < ZERO)
            {
                break;
            }

            result = (result * 10) + (digit - ZERO);
        }

        return result;
    }

    public static void WriteASCIIString(string asciiString) => Console.Write(asciiString);

    public static void WriteInt(int value)
    {
        var isNegative = value < 0;

        var digitCount = 0;
        do
        {
            var digit = isNegative ? -(value % 10) : (value % 10);
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

    public static void WriteLineASCIIString(string asciiString) => Console.WriteLine(asciiString);

    public static void WriteNonNegativeInt(int value)
    {
        var digitCount = 0;
        do
        {
            var digit = value % 10;
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

    private const int INPUT_BUFFER_LIMIT = 8192;
    private const byte MINUS_SIGN = (byte)'-';
    private const byte NEWLINE = (byte)'\n';
    private const byte NULL = (byte)'\0';
    private const int OUTPUT_BUFFER_LIMIT = 8192;
    private const byte SPACE_CHAR = (byte)' ';
    private const byte ZERO = (byte)'0';
    private static readonly byte[] _digitsBuffer = new byte[11];
    private static readonly byte[] _inputBuffer = new byte[INPUT_BUFFER_LIMIT];
    private static readonly Stream _inputStream = Console.OpenStandardInput();
    private static readonly byte[] _outputBuffer = new byte[OUTPUT_BUFFER_LIMIT];
    private static readonly Stream _outputStream = Console.OpenStandardOutput();
    private static int _inputBufferIndex = 0;
    private static int _inputBufferSize = 0;
    private static int _outputBufferSize = 0;

    private static byte ReadByte()
    {
        if (_inputBufferIndex == _inputBufferSize)
        {
            _inputBufferIndex = 0;
            _inputBufferSize = _inputStream.Read(_inputBuffer, 0, INPUT_BUFFER_LIMIT);
            if (_inputBufferSize == 0)
            {
                return NULL; // All input has been read.
            }
        }

        return _inputBuffer[_inputBufferIndex++];
    }
}

#endregion FastIO