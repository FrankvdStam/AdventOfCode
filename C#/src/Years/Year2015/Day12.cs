using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Years.Utils;

namespace Years.Year2015
{
    public class Day12 : BaseDay
    {
        public Day12() : base(2015, 12) {}

        public override void ProblemOne()
        {
            Console.WriteLine(Day12Func(Input.RemoveTrailingNewline()));
        }

        public override void ProblemTwo()
        {
            var input = Input.RemoveTrailingNewline();
            Console.WriteLine(Braces.IsMatch(input) ? Day12Part2Func(Braces.Replace(input, SumBraces)) : Day12Func(input));
        }

        private static readonly Regex _number = new Regex("-?\\d+", RegexOptions.Compiled);
        private static readonly Regex Braces = new Regex("\\{[^{}]*\\}", RegexOptions.Compiled);

        static int Day12Part2Func(string input) => Braces.IsMatch(input) ? Day12Part2Func(Braces.Replace(input, SumBraces)) : Day12Func(input);
        static int Day12Func(string input) => _number.Matches(input).Cast<Match>().Select(m1 => int.Parse(m1.Value)).Sum();

        static string SumBraces(Match m) => m.Value.Contains(":\"red") ? "0" : Day12Func(m.Value).ToString();
    }
}