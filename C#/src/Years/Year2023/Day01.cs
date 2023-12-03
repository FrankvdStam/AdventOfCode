using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2023
{
    public class Day01 : BaseDay
    {
        public Day01() : base(2023, 1) {}


        private const string Example = @"1abc2
pqr3stu8vwx
a1b2c3d4e5f
treb7uchet";

        private const string Example2 = @"two1nine
eightwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen";

        public override void ProblemOne()
        {
            var res = Input.SplitNewLine().Select(s => int.Parse($"{s.First(char.IsDigit)}{s.Last(char.IsDigit)}")).Sum();
            Console.WriteLine(res);
        }


        private readonly Dictionary<string, int> _numbers = new Dictionary<string, int>()
        {
            { "1", 1 },
            { "2", 2 },
            { "3", 3 },
            { "4", 4 },
            { "5", 5 },
            { "6", 6 },
            { "7", 7 },
            { "8", 8 },
            { "9", 9 },

            { "one",   1 },
            { "two",   2 },
            { "three", 3 },
            { "four",  4 },
            { "five",  5 },
            { "six",   6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine",  9 },
        };

        public override void ProblemTwo()
        {
            var input = Input;

            var sum = 0;
            foreach (var line in input.SplitNewLine())
            {
                var digits = new List<(int Index, int value)>();
                foreach (var num in _numbers)
                {
                    foreach (var occurrence in line.AllOccurencesOf(num.Key))
                    {
                        digits.Add((occurrence, num.Value));
                    }
                }
                var first = digits.MinBy(i => i.Index).value;
                var last = digits.MaxBy(i => i.Index).value;
                var result = int.Parse($"{first}{last}");
                //Console.WriteLine(result);
                sum += result;
            }
            Console.WriteLine(sum);
        }
    }
}