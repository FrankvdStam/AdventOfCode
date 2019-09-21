using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    //Could use a linkedlist but this makes things a little easier
    //public class DoubleLinkListNode
    //{
    //    public int Number;
    //    public DoubleLinkListNode Next;
    //    public DoubleLinkListNode Previous;
    //}

    public static class Ext
    {
        //TODO: make this actually circular
        public static T[] SubArrayCircular<T>(this T[] data, int index, int length)
        {
            //if (index > length)
            //{
            //    index = length % index;
            //}

            T[] result = new T[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = data[index + i < data.Length ? index + i : index + i - data.Length];
            }

            return result;
        }

        public static T[] ReplaceCircular<T>(this T[] data, T[] replacementData, int index)
        {
            //if (index > data.Length)
            //{
            //    index = data.Length % index;
            //}

            T[] result = new T[data.Length];
            Array.Copy(data, result, data.Length);

            for (int i = 0; i < replacementData.Length; i++)
            {
                int pos = index + i < result.Length ? index + i : index + i - result.Length;
                result[pos] = replacementData[i];
            }

            return result;
        }

        public static string GetFormattedArrayString<T>(this T[] data)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                if (i == 0)
                {
                    sb.Append($"[{data[i]},");
                    continue;
                }

                if (i + 1 < data.Length)
                {
                    sb.Append($"{data[i]},");
                }
                else
                {
                    sb.Append($"{data[i]}]");
                }
            }

            return sb.ToString();
        }
    }
    class Program
    {
        


        static void Main(string[] args)
        {
            var a1 = ProblemTwo("", 256);
            var a2 = ProblemTwo("AoC 2017", 256);
            var a3 = ProblemTwo("1,2,3", 256);
            var a4 = ProblemTwo("1,2,4", 256);



            //int[] lengths = {3, 4, 1, 5};

            int[] lengths = { 225,171,131,2,35,5,0,13,1,246,54,97,255,98,254,110 };
            int size = 256;

            int result = ProblemOne(lengths, size);

        }

        static int ProblemOne(int[] lengths, int size)
        {
            if (size < 2)
            {
                throw new Exception("Min size is 2");
            }

            int position = 0;
            int skipSize = 0;
            int[] numbers = new int[size];
            for (int i = 0; i < size; i++)
            {
                numbers[i] = i;
            }

            /*
            int[] test = new[] {0, 1, 2, 3, 4};
            var test2 = test.SubArrayCircular(0, 3);
            test2 = test2.Reverse().ToArray();
            var test3 = test.ReplaceCircular(test2, position);
            */

            //Console.WriteLine($"Starting with {numbers.GetFormattedArrayString()}");
            foreach (var length in lengths)
            {
                var temp = numbers.SubArrayCircular(position, length);
                temp = temp.Reverse().ToArray();
                numbers = numbers.ReplaceCircular(temp, position);

                position = GetNewPosition(position, length + skipSize, numbers.Length);
                skipSize++;
                //Console.WriteLine($"Numbers: {numbers.GetFormattedArrayString()}");
            }

            return numbers[0] * numbers[1];
        }


        static string ProblemTwo(string input, int size)
        {
            //Get length in bytes
            List<byte> lengths = new List<byte>();
            foreach (char c in input)
            {
                lengths.Add((byte)c);
            }

            //Add the specified bytes
            lengths.Add(17);
            lengths.Add(31);
            lengths.Add(73);
            lengths.Add(47);
            lengths.Add(23);

            //Setup state
            int position = 0;
            int skipSize = 0;
            byte[] numbers = new byte[size];
            for (int i = 0; i < size; i++)
            {
                numbers[i] = (byte)i;
            }

            //Run 64 times, preserving position and skip size
            for (int i = 0; i < 64; i++)
            {
                foreach (var length in lengths)
                {
                    var temp = numbers.SubArrayCircular(position, length);
                    temp = temp.Reverse().ToArray();
                    numbers = numbers.ReplaceCircular(temp, position);

                    position = GetNewPosition(position, length + skipSize, numbers.Length);
                    skipSize++;
                    //Console.WriteLine($"Numbers: {numbers.GetFormattedArrayString()}");
                }
            }

            //Get dense hash
            int hashSize = 16;
            byte[] hash = new byte[hashSize];
            for (int i = 0; i < hashSize; i++)
            {
                byte b = 0;
                for (int j = 0; j < hashSize; j++)
                {
                    b ^= numbers[(i * hashSize) + j];
                }
                hash[i] = b;
            }

            //Turn into hex string
            string hex = BitConverter.ToString(hash).Replace("-", string.Empty);
            return hex;
        }

        static int GetNewPosition(int position, int increment, int length)
        {
            int temp = (position + increment < length ? position + increment : position + increment - length);

            if ((position + increment < length ? position + increment : position + increment - length) > length)
            {
                var temp2 = (position + increment) % length;
                position = position % length;
            }

            return position + increment < length ? position + increment : position + increment - length;
        }




    }
}
