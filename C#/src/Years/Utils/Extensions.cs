using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Years.Utils
{
    public static partial class Extensions
    {
        public static int Difference(this int a, int b)
        {
            if (a > b)
            {
                return a - b;
            }
            return b - a;
        }

        public static float Difference(this float a, float b)
        {
            if (a > b)
            {
                return a - b;
            }
            return b - a;
        }


        public static int Factorial(this int i)
        { 
            return Enumerable.Range(1, i).Aggregate(1, (p, item) => p * item);
        }

        public static bool IsPrime(this long number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (long)Math.Floor(Math.Sqrt(number));

            for (long i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }

        public static bool IsPrime(this int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }

        public static double DegreesToRadians(this double angle)
        {
            return (Math.PI / 180) * angle;
        }


        //https://stackoverflow.com/questions/311165/how-do-you-convert-a-byte-array-to-a-hexadecimal-string-and-vice-versa
        public static string ToHexString(this byte[] ba)
        {
            var hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static byte[] ToHexByteArray(this string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public static string HexStringToBinaryString(this string hex)
        {
            return string.Join(string.Empty, hex.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
        }


        public static string[] SplitNewLine(this string input)
        {
            if(input.Contains("\r\n"))
            {
                return input.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            }
            return input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        }

        public static string RemoveTrailingNewline(this string input)
        {
            if(input.EndsWith('\n'))
            {
                return input.Substring(0, input.Length - 1);
            }
            return input;
        }

        public static List<T> Clone<T>(this List<T> oldList)
        {
            var temp = JsonSerializer.Serialize(oldList);
            return JsonSerializer.Deserialize<List<T>>(temp);
        }

        public static Direction RotateLeft(this Direction direction)
        {
            //  /U\
            //  L R
            //  \D/
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Left;

                case Direction.Left:
                    return Direction.Down;

                case Direction.Down:
                    return Direction.Right;

                case Direction.Right:
                    return Direction.Up;
                
            }
            throw new Exception($"Unsupported ");
        }

        public static Direction RotateRight(this Direction direction)
        {
            //  /U\
            //  L R
            //  \D/
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Right;

                case Direction.Right:
                    return Direction.Down;

                case Direction.Down:
                    return Direction.Left;

                case Direction.Left:
                    return Direction.Up;

            }
            throw new Exception($"Unsupported ");
        }

        public static int ManhattanDistance(this Vector3 vector1, Vector3 vector2)
        {
            return (int)(Math.Abs(vector1.X - vector2.X) + Math.Abs(vector1.Y - vector2.Y) + Math.Abs(vector1.Z - vector2.Z));
        }


        public static void Draw(this List<Vector2i> vectors, char c)
        {
            int minx = vectors.Min(i => i.X);
            int maxx = vectors.Max(i => i.X);
            int width = maxx - minx;
            int miny = vectors.Min(i => i.Y);
            int maxy = vectors.Max(i => i.Y);
            int height = maxy - miny;

            int transformX = 0;
            if (minx < 0)
            {
                transformX = Math.Abs(minx);
            }

            int transformY = 0;
            if (miny < 0)
            {
                transformY = Math.Abs(miny);
            }

            //Console.SetWindowSize(width, height);

            foreach (Vector2i v in vectors)
            {
                Console.SetCursorPosition(v.X + transformX, v.Y + transformY);
                Console.Write(c);
            }
        }

        public static void Draw(this List<(Vector2i vec, char c)> vectors)
        {
            int minx = vectors.Min(i => i.vec.X);
            int maxx = vectors.Max(i => i.vec.X);
            int width = maxx - minx;
            int miny = vectors.Min(i => i.vec.Y);
            int maxy = vectors.Max(i => i.vec.Y);
            int height = maxy - miny;

            int transformX = 0;
            if (minx < 0)
            {
                transformX = Math.Abs(minx);
            }

            int transformY = 0;
            if (miny < 0)
            {
                transformY = Math.Abs(miny);
            }

            //Console.SetWindowSize(width, height);

            foreach (var v in vectors)
            {
                Console.SetCursorPosition(v.vec.X + transformX, v.vec.Y + transformY);
                Console.Write(v.c);
            }
        }



        public static string Reverse(this string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }


        public static List<long> SplitDigits(this long num)
        {
            List<long> digits = new List<long>();
            while (num > 0)
            {
                digits.Add(num % 10);
                num = num / 10;
            }

            digits.Reverse();
            return digits;
        }

        public static bool IsAllCaps(this string str)
        {
            return str.All(char.IsUpper);
        }

        #region IEnumerables ========================================================================================================

        public static IEnumerable<(int x, int y)> DiagonalIterator(int width, int height)
        {
            for (int k = 0; k <= width + height - 2; k++)
            {
                for (int j = 0; j <= k; j++)
                {
                    int i = k - j;
                    if (i < height && j < width)
                    {
                        yield return (i, j);
                    }
                }
            }
        }


        private static int AdditionFactorial(this int n)
        {
            return ((n * (n + 1)) / 2);
        }


        public enum AdjacentIteratorBehavior
        {
            None,
            IncludeDiagonal,
        }

        public static readonly Vector2i DirectionLeft  = new Vector2i(-1,  0);
        public static readonly Vector2i DirectionRight = new Vector2i( 1,  0);
        public static readonly Vector2i DirectionDown  = new Vector2i( 0,  1);
        public static readonly Vector2i DirectionUp    = new Vector2i( 0, -1);

        public static readonly List<Vector2i> AdjacentIndices = new List<Vector2i>()
        {
            new Vector2i(-1,  0), //left
            new Vector2i( 1,  0), //right
            new Vector2i( 0,  1), //down
            new Vector2i( 0, -1), //up
        };

        public static readonly List<Vector2i> DiagonalIndices = new List<Vector2i>()
        {
            new Vector2i(-1, -1), //left up
            new Vector2i(-1,  1), //left down
            new Vector2i( 1, -1), //right up
            new Vector2i( 1,  1), //right down
        };

        /// <summary>
        /// Iterates over a 2d array and returns the iterated element plus all adjacent elements that are within bounds of the array
        /// Returns the current element and it's position and a list of adjacent elements and their position
        /// </summary>
        public static IEnumerable<(Vector2i currentPosition, T currentElement, List<(Vector2i position, T element)> adjacentElements)> AdjacentIterator<T>(this T[,] array, AdjacentIteratorBehavior adjacentIteratorBehavior = AdjacentIteratorBehavior.None)
        {
            var width = array.GetLength(0);
            var height = array.GetLength(1);

            var indices = AdjacentIndices.Clone();
            if (adjacentIteratorBehavior == AdjacentIteratorBehavior.IncludeDiagonal)
            {
                indices.AddRange(DiagonalIndices);
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var currentPosition = new Vector2i(x, y);
                    var currentElement = array[x, y];
                    var adjacentElements = new List<(Vector2i position, T element)>();

                    foreach (var index in indices)
                    {
                        var adjacentPosition = currentPosition.Add(index);

                        if (adjacentPosition.X >= 0 &&
                            adjacentPosition.X < width && 
                            adjacentPosition.Y >= 0 &&
                            adjacentPosition.Y < height)
                        {
                            adjacentElements.Add((adjacentPosition, array[adjacentPosition.X, adjacentPosition.Y]));
                        }
                    }
                    yield return (currentPosition, currentElement, adjacentElements);
                }
            }
        }

        public static List<(Vector2i position, T element)> GetAdjacentElements<T>(this T[,] array, Vector2i position, AdjacentIteratorBehavior adjacentIteratorBehavior = AdjacentIteratorBehavior.None)
        {
            var width = array.GetLength(0);
            var height = array.GetLength(1);

            var indices = AdjacentIndices.Clone();
            if (adjacentIteratorBehavior == AdjacentIteratorBehavior.IncludeDiagonal)
            {
                indices.AddRange(DiagonalIndices);
            }

            var adjacentElements = new List<(Vector2i position, T element)>();
            foreach (var index in indices)
            {
                var adjacentPosition = position.Add(index);

                if (adjacentPosition.X >= 0 &&
                    adjacentPosition.X < width &&
                    adjacentPosition.Y >= 0 &&
                    adjacentPosition.Y < height)
                {
                    adjacentElements.Add((adjacentPosition, array[adjacentPosition.X, adjacentPosition.Y]));
                }
            }
            return adjacentElements;
        }


        public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> sequence)
        {
            if (sequence == null)
            {
                yield break;
            }

            var list = sequence.ToList();

            if (!list.Any())
            {
                yield return Enumerable.Empty<T>();
            }
            else
            {
                var startingElementIndex = 0;

                foreach (var startingElement in list)
                {
                    var remainingItems = list.AllExcept(startingElementIndex);

                    foreach (var permutationOfRemainder in remainingItems.Permute())
                    {
                        yield return startingElement.Concat(permutationOfRemainder);
                    }

                    startingElementIndex++;
                }
            }
        }


        public static IEnumerable<(T, T)> PermutePairs<T>(this List<T> sequence)
        {
            if (sequence == null || sequence.Count() < 2)
            {
                yield break;
            }

            for (int x = 0; x < sequence.Count(); x++)
            {
                for (int y = x + 1; y < sequence.Count(); y++)
                {
                    yield return (sequence[x], sequence[y]);
                }
            }
        }


        private static IEnumerable<T> Concat<T>(this T firstElement, IEnumerable<T> secondSequence)
        {
            yield return firstElement;
            if (secondSequence == null)
            {
                yield break;
            }

            foreach (var item in secondSequence)
            {
                yield return item;
            }
        }

        private static IEnumerable<T> AllExcept<T>(this IEnumerable<T> sequence, int indexToSkip)
        {
            if (sequence == null)
            {
                yield break;
            }

            var index = 0;

            foreach (var item in sequence.Where(item => index++ != indexToSkip))
            {
                yield return item;
            }
        }

        #endregion


        #region static md5 instance for entire application ========================================================================================================

        private static MD5 _md5;

        private static void InitMd5()
        {
            if (_md5 == null)
            {
                _md5 = MD5.Create();
            }
        }


        public static byte[] ComputeHash(byte[] buffer)
        {
            InitMd5();

            return _md5.ComputeHash(buffer);
        }

        public static byte[] ComputeHashFromUtf8String(string str)
        {
            InitMd5();

            return _md5.ComputeHash(Encoding.UTF8.GetBytes(str));
        }


        #endregion


        #region linked list ext ========================================================================================================

        public static int IndexOf<T>(this LinkedList<T> list, T item)
        {
            var count = 0;
            for (var node = list.First; node != null; node = node.Next, count++)
            {
                if (item.Equals(node.Value))
                    return count;
            }
            return -1;
        }

        public static LinkedListNode<T> NodeAt<T>(this LinkedList<T> list, int index)
        {
            int half = list.Count / 2;

            if (index <= half)
            {
                LinkedListNode<T> current = list.First;
                while (index > 0)
                {
                    current = current.Next;
                    index--;
                }
                return current;
            }
            else
            {
                index = list.Count - index;
                LinkedListNode<T> current = list.Last;
                while (index > 0)
                {
                    current = current.Previous;
                    index--;
                }
                return current;
            }
        }
        #endregion

        #region Array ========================================================================================================
        public static T[,] RotateRight<T>(this T[,] array)
        {
            int width = array.GetLength(0);
            int height = array.GetLength(1);
            var result = new T[height, width];

            for (var row = 0; row < height; row++)
            {
                for (var col = 0; col < width; col++)
                {
                    var x = height - (row + 1);
                    var y = col;

                    result[x, y] = array[col, row];
                }
            }

            return result;
        }

        public static T[,] RotateLeft<T>(this T[,] array)
        {
            int width = array.GetLength(0);
            int height = array.GetLength(1);
            var result = new T[height, width];

            for (var row = 0; row < height; row++)
            {
                for (var col = 0; col < width; col++)
                {
                    var x = row;
                    var y = width - (col + 1);
                           
                    result[x, y] = array[col, row];
                }
            }

            return result;
        }

        /// <summary>
        /// https://stackoverflow.com/questions/12446770/how-to-compare-multidimensional-arrays-in-c-sharp
        /// </summary>
        public static bool SequenceEquals<T>(this T[,] a, T[,] b) => a.Rank == b.Rank
                                                                     && Enumerable.Range(0, a.Rank).All(d => a.GetLength(d) == b.GetLength(d))
                                                                     && a.Cast<T>().SequenceEqual(b.Cast<T>());


        public static T[,] Flip<T>(this T[,] array)
        {
            int rows = array.GetLength(0);
            int columns = array.GetLength(1);
            var flipped = new T[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    flipped[i, j] = array[(rows - 1) - i, j];
                }
            }
            return flipped;
        }

        public static IEnumerable<T> Enumerate<T>(this T[,] array)
        {
            int rows = array.GetLength(0);
            int columns = array.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    yield return array[i, j];
                }
            }
        }
        #endregion
    }
}
