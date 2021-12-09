using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Years.Utils;

namespace Years.Year2018
{
    public class Day12 : IDay
    {
        public int Day => 12;
        public int Year => 2018;

        public void ProblemOne()
        {
            ParseInput(Input, out string state, out List<(string, char)> rules);
            
            
            var zeroIndex = 0;
            var previousScore = 0;
            
            for (int i = 0; i < 20; i++)
            {
                state = NextGeneration(state, rules, ref zeroIndex);


                var score = Score(state, zeroIndex);
                var diff = score - previousScore;
                previousScore = score;
                //Console.WriteLine($"{i+1} {score} {diff} {state}");
            }

            var count = Score(state, zeroIndex);
            Console.WriteLine(count);
        }

        public void ProblemTwo()
        {
            ParseInput(Input, out string state, out List<(string, char)> rules);



            //After 100 generations, the score delta is consistent. All we need to do is calculate the first 100 generations, get its final score and delta
            //Then add (5B-100) * delta to the score
            
            var zeroIndex = 0;
            var previousScore = 0;
            var delta = 0;
            var deltas = new Dictionary<int, int>();

            for (int i = 0; i < 100; i++)
            {
                state = NextGeneration(state, rules, ref zeroIndex);
                var score = Score(state, zeroIndex);
                delta = score - previousScore;
                previousScore = score;
                //Console.WriteLine(i + " - " + delta);
            }

            var finalScore = previousScore + ((50000000000 - 100) * delta);
            Console.WriteLine(finalScore);
        }



        private int Score(string state, int zeroIndex)
        {
            var score = 0;
            for (int i = 0; i < state.Length; i++)
            {
                if (state[i] == '#')
                {
                    score += (i - zeroIndex);
                }
            }
            return score;
        }


        private string NextGeneration(string input, List<(string, char)> rules, ref int zeroIndex)
        {

            var sb = new StringBuilder();
            for (int i = -2; i <= input.Length + 2; i++)
            {
                string pattern = DottedSubString(input, i - 2, 5);

                if (rules.Any(i => i.Item1 == pattern))
                {
                    var rule = rules.First(i => i.Item1 == pattern);
                    sb.Append(rule.Item2);
                }
                else
                {
                    sb.Append('.');
                }
            }

            //Trim start, fix zero index
            zeroIndex += 2;
            if (sb[0] == '.')
            {
                sb.Remove(0, 1);
                zeroIndex--;
            }
            if (sb[0] == '.')
            {
                sb.Remove(0, 1);
                zeroIndex--;
            }

            //Trim end
            if (sb[sb.Length - 1] == '.')
            {
                sb.Remove(sb.Length - 1, 1);
            }
            if (sb[sb.Length - 1] == '.')
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString();
        }




        private string DottedSubString(string input, int index, int length)
        {
            var sb = new StringBuilder();
            for (int i = index; i < index + length; i++)
            {
                if (i >= 0 && i < input.Length)
                {
                    sb.Append(input[i]);
                }
                else
                {
                    sb.Append('.');
                }
            }
            return sb.ToString();
        }





        private void ParseInput(string input, out string initialState, out List<(string, char)> rules)
        {
            var lines = input.SplitNewLine();

            initialState = lines[0].Substring("initial state: ".Length);
           
            rules = new List<(string, char)>();
            for (int i = 2; i < lines.Length; i++)
            {
                rules.Add((lines[i].Substring(0, 5), lines[i][9]));
            }
        }


        private const string Example = @"initial state: #..#.#..##......###...###

...## => #
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

        private const string Input =
            @"initial state: ..##.#######...##.###...#..#.#.#..#.##.#.##....####..........#..#.######..####.#.#..###.##..##..#..#

#..#. => .
..#.. => .
..#.# => #
##.#. => .
.#... => #
#.... => .
##### => #
.#.## => .
#.#.. => .
#.### => #
.##.. => #
##... => .
#...# => #
####. => #
#.#.# => .
#..## => .
.#### => .
...## => .
..### => #
.#..# => .
##..# => #
.#.#. => .
..##. => .
###.. => .
###.# => #
#.##. => #
..... => .
.##.# => #
....# => .
##.## => #
...#. => #
.###. => .";
    }
}