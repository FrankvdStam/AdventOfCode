using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    class Program10
    {

        static void Main(string[] args)
        {
            string num = "1321131112";
            // Part 1
            for (int i = 0; i < 40; i++)
            {
                //num.Dump();
                num = lookandsay(num);
            }
            // Part 2
            for (int i = 0; i < 10; i++)
            {
                //num.Dump();
                num = lookandsay(num);
            }


            var l = num.Length;
        }

        // Define other methods and classes here
        static string lookandsay(string number)
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

        static void Main2(string[] args)
        {
            ProblemOne("1321131112");
        }

        static void ProblemOne(string input)
        {
            for (int i = 0; i < 10; i++)
            {
                input = NextGeneration(input);
            }

            var result = input.Length;
        }

        static string NextGeneration(string input)
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

        static int DupplicateCount(char c, string input, int offset)
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
    }


    public class Day10 : IDay
    {
        public int Day => 10;
        public int Year => 2015;

        public void ProblemOne()
        {
        }

        public void ProblemTwo()
        {
        }
    }
}