using System;
using System.IO;
using System.Text;

public class Test
{
    public static void Main()
    {
        var N = FastIO.ReadNonNegativeInt();
        var M = FastIO.ReadNonNegativeInt();
        //const int N = 4;
        //const int M = 3;

        var arrayOfStudents = new int[N][];
        //var arrayOfStudents = new int[N][]
        //{
        //new int[M]
        //{
        //    10, 20, 30
        //},
        //new int[M]
        //{
        //    40, 50, 60
        //},
        //new int[M]
        //{
        //    70, 80, 90
        //},
        //new int[M]
        //{
        //    100, 110, 120
        //},
        //
        //////
        //
        //new int[M]
        //{
        //    12, 16, 67, 43
        //},
        //new int[M]
        //{
        //    7, 17, 68, 48
        //},
        //new int[M]
        //{
        //    14, 15, 77, 54
        //},
        //
        ///////
        //
        //    new int[M]
        //    {
        //        100, 110, 120
        //    },
        //    new int[M]
        //    {
        //        70, 80, 90
        //    },
        //    new int[M]
        //    {
        //        40, 50, 60
        //    },
        //    new int[M]
        //    {
        //        10, 20, 30
        //    },
        //};

        for (int i = 0; i < N; i++)
        {
            var tempArray = new int[M];
            for (int j = 0; j < M; j++)
            {
                tempArray[j] = FastIO.ReadNonNegativeInt();
            }
            Array.Sort(tempArray);
            arrayOfStudents[i] = tempArray;
        }

        var indexes = new int[N];
        Array.Clear(indexes, 0, indexes.Length);
        var choosenStudents = new int[N];
        for (int i = 0; i < N; i++)
        {
            choosenStudents[i] = arrayOfStudents[i][0];
        }
        Array.Sort(choosenStudents);
        int min = choosenStudents[0];
        int max = choosenStudents[N - 1];
        int result = max - min;
        var nextIndex = 0;

        for (int i = 0; i < N * M - 3; i++)
        {
            for (int j = 0; j < choosenStudents.Length; j++)
            {
                if (arrayOfStudents[j][indexes[j]] == min)
                {
                    nextIndex = j;
                    indexes[nextIndex]++;
                    break;
                }
            }

            if (indexes[nextIndex] >= M)
                break;

            choosenStudents[Array.IndexOf(choosenStudents, min)] = arrayOfStudents[nextIndex][indexes[nextIndex]];
            Array.Sort(choosenStudents);

            min = choosenStudents[0];
            max = choosenStudents[N - 1];
            int tempResult = max - min;

            if (tempResult < result)
                result = tempResult;
        }
        FastIO.WriteNonNegativeInt(result);
        FastIO.Flush();
    }

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
}