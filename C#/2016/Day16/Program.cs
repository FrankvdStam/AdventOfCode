using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16
{
    class Program
    {
        static void Main(string[] args)
        {
            ProblemOne(ParseInput("00101000101111010"), 35651584);
        }
        
        static void ProblemOne(int[] input, int inputLength)
        {
            while (input.Length < inputLength)
            {
                input = GenerateData(input);
            }

            //Trim excess
            input = input.Take(inputLength).ToArray();

            var checksum = CalcChecksum(input);
            
            StringBuilder builder = new StringBuilder();
            foreach (int i in checksum)
            {
                builder.Append(i);
            }
            string sumcheck = builder.ToString();
        }

        static int[] GenerateData(int[] input)
        {
            int[] result = new int[input.Length*2 + 1];

            //Copy input into result.
            for (int i = 0; i < input.Length; i++)
            {
                result[i] = input[i];
            }

            //Add 0 in the middle.
            result[input.Length] = 0;

            //Reverse input
            int index = input.Length + 1;
            for (int i = input.Length-1; i >= 0; i--)
            {
			    //Replace 0 -> 1 and 1-> 0
                result[index] = input[i] == 0 ? 1 : 0;
                index++;
            }
            return result;
        }

        static int[] CalcChecksum(int[] input)
        {
            List<int> result = new List<int>();
            
            while (true)
            {
                for (int i = 0; i+1 < input.Length; i+=2)
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

        
        static int[] ParseInput(string input)
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

    }
}
