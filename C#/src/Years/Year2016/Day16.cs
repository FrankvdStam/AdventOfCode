using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2016
{
    public class Day16 : IDay
    {
        public int Day => 16;
        public int Year => 2016;

        public void ProblemOne()
        {
            string checksum = CalculateFullChecksum(Input, 272);
            Console.WriteLine(checksum);
        }

        public void ProblemTwo()
        {
            string checksum = CalculateFullChecksum(Input, 35651584);
            Console.WriteLine(checksum);
        }


        private string CalculateFullChecksum(string input, int discLength)
        {
            int[] bits = ParseInput(input);


            while (bits.Length < discLength)
            {
                bits = GenerateData(bits);
            }

            //Trim excess
            bits = bits.Take(discLength).ToArray();

            var checksum = CalcChecksum(bits);

            StringBuilder builder = new StringBuilder();
            foreach (int i in checksum)
            {
                builder.Append(i);
            }
            return builder.ToString();
        }



        private int[] GenerateData(int[] input)
        {
            int[] result = new int[input.Length * 2 + 1];

            //Copy input into result.
            for (int i = 0; i < input.Length; i++)
            {
                result[i] = input[i];
            }

            //Add 0 in the middle.
            result[input.Length] = 0;

            //Reverse input
            int index = input.Length + 1;
            for (int i = input.Length - 1; i >= 0; i--)
            {
                //Replace 0 -> 1 and 1-> 0
                result[index] = input[i] == 0 ? 1 : 0;
                index++;
            }
            return result;
        }

        private int[] CalcChecksum(int[] input)
        {
            List<int> result = new List<int>();

            while (true)
            {
                for (int i = 0; i + 1 < input.Length; i += 2)
                {
                    if (input[i] == input[i + 1])
                    {
                        result.Add(1);
                    }
                    else
                    {
                        result.Add(0);
                    }
                }

                //Find odd length 
                if (result.Count % 2 != 0)
                {
                    return result.ToArray();
                }
                else
                {
                    input = result.ToArray();
                    result.Clear();
                }
            }
        }

        private int[] ParseInput(string input)
        {
            int[] result = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '1')
                {
                    result[i] = 1;
                }
            }
            return result;
        }


        private const string Input = "00101000101111010";

        //"00101000101111010"), 35651584
    }
}