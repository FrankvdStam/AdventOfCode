using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace Years.Utils
{
    public static partial class Extensions
    {
        public static int Factorial(this int i)
        { 
            return Enumerable.Range(1, i).Aggregate(1, (p, item) => p * item);
        }



        //https://stackoverflow.com/questions/311165/how-do-you-convert-a-byte-array-to-a-hexadecimal-string-and-vice-versa
        public static string ToHexString(this byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
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


        public static string[] SplitNewLine(this string input)
        {
            return input.Split(new string[] {"\r\n"}, StringSplitOptions.None);
        }

        public static List<T> Clone<T>(this List<T> oldList)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, oldList);
            stream.Position = 0;
            return (List<T>)formatter.Deserialize(stream);
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

        #region IEnumerables ========================================================================================================



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
    }
}
