using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    public class Day10 : IDay
    {
        public int Day => 10;
        public int Year => 2015;


        public void ProblemOne()
        {
            SolveAndCache();
            Console.WriteLine(_one);
        }

        public void ProblemTwo()
        {
            Console.WriteLine(_two);
        }


        private int _one;
        private int _two;

        private void SolveAndCache()
        {
            string input = Input;
            for (int i = 0; i < 50; i++)
            {
                input = lookandsay(input);
                if (i == 39)
                {
                    _one = input.Length;
                }
            }
            _two = input.Length;
        }


        string lookandsay(string number)
        {
            StringBuilder result = new StringBuilder();

            char repeat = number[0];
            number = number.Substring(1, number.Length - 1) + " ";
            int times = 1;

            foreach (char actual in number)
            {
                if (actual != repeat)
                {
                    result.Append(Convert.ToString(times) + repeat);
                    times = 1;
                    repeat = actual;
                }
                else
                {
                    times += 1;
                }
            }
            return result.ToString();
        }


        private string NextGeneration(string input)
        {
            string result = "";
            for (int i = 0; i < input.Length; /*Don't increment i here.*/)
            {
                int duplicate = DupplicateCount(input[i], input, i);
                result += duplicate.ToString() + input[i].ToString();
                i += duplicate;
            }
            return result;
        }

        private int DupplicateCount(char c, string input, int offset)
        {
            int count = 0;
            for (int i = offset; i < input.Length; i++)
            {
                if (input[i] == c)
                {
                    count++;
                }
                else
                {
                    return count;
                }
            }
            return count;
        }

        private const string Input = "1321131112";
    }
}