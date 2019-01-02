using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day12
{
    public static class Extention
    {
        public static string DottedSubString(this string input, int index, int length)
        {
            string substring = "";
            for(int i = index; i < index + length; i++)
            {
                if (i >= 0 && i < input.Length)
                {
                    substring += input[i];
                }
                else
                {
                    substring += ".";
                }
            }
            return substring;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ProblemOne(example, exampleRules, 50000000000);
            //ProblemOne(example, exampleRules, 20);
        }

        static void ProblemOne(string input, string ruleInput, long generations)
        {
            List<string> rules = ParseRules(ruleInput);

            long gen = 0;
            int zeroIndex = 0;

            Console.WriteLine(input);

            while (gen < generations)
            {
                input = NextGeneration(input, rules, ref zeroIndex);
                if(gen % 100000 == 0)
                {
                    Console.WriteLine(gen);
                }
                //Console.WriteLine(input);
                gen++;
            }
            Console.ReadKey();
        }

        static string NextGeneration(string input, List<string> rules, ref int zeroIndex)
        {
            string nextGeneration = "";
            
            for(int i = -2; i <= input.Length+2; i++)
            {
                string pattern = input.DottedSubString(i-2, 5);
                if (rules.Contains(pattern))
                {
                    nextGeneration += "#";
                }
                else
                {
                    nextGeneration += ".";
                }
            }

            return nextGeneration;
        }

        static List<string> ParseRules(string input)
        {
            var split = input.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
            var result = new List<string>();
            foreach (string s in split)
            {
                result.Add(s.Substring(0, 5));
            }
            return result;
        }

        static string actualInput = "......................##.#######...##.###...#..#.#.#..#.##.#.##....####..........#..#.######..####.#.#..###.##..##..#..#..............................";
        static int inputOffset = 20;
        static string inputRules = @"..#.# => #
.#... => #
##### => #
#.### => #
.##.. => #
#...# => #
####. => #
..### => #
##..# => #
###.# => #
#.##. => #
.##.# => #
##.## => #
...#. => #";

        static string example = "#..#.#..##......###...###";
        static int exampleOffset = 3;
        static string exampleRules = @"...## => #
..#.. => #
.#... => #
.#.#. => #
.#.## => #
.##.. => #
.#### => #
#.#.# => #
#.### => #
##.#. => #
##.## => #
###.. => #
###.# => #
####. => #";
    }
}
