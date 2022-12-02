using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    public class Day10 : BaseDay
    {
        public Day10() : base(2015, 10)
        {
            string input = Input.Replace("\n", "");
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


        public override void ProblemOne()
        {
            Console.WriteLine(_one);
        }

        public override void ProblemTwo()
        {
            Console.WriteLine(_two);
        }


        private int _one;
        private int _two;


        private string lookandsay(string number)
        {
            var result = new StringBuilder();

            var repeat = number[0];
            number = number.Substring(1, number.Length - 1) + " ";
            var times = 1;

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
            var result = "";
            for (int i = 0; i < input.Length; /*Don't increment i here.*/)
            {
                var duplicate = DupplicateCount(input[i], input, i);
                result += duplicate.ToString() + input[i].ToString();
                i += duplicate;
            }
            return result;
        }

        private int DupplicateCount(char c, string input, int offset)
        {
            var count = 0;
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
    }
}