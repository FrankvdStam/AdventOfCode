using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Years.Utils;

namespace Years.Year2015
{
    //By reddit user recursive
    //https://www.reddit.com/r/adventofcode/comments/3vw32y/day_8_solutions/
    public class Day08 : BaseDay
    {
        public Day08() : base(2015, 08) { }


        public override void ProblemOne()
        {
            var lines = Input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            int totalCode = lines.Sum(l => l.Length);
            int totalCharacters = lines.Sum(CharacterCount);
            

            Console.WriteLine(totalCode - totalCharacters);
        }

        public override void ProblemTwo()
        {
            var lines = Input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            int totalCode = lines.Sum(l => l.Length);
            int totalEncoded = lines.Sum(EncodedStringCount);
            Console.WriteLine(totalEncoded - totalCode);
        }

        private int CharacterCount(string arg) => Regex.Match(arg, @"^""(\\x..|\\.|.)*""$").Groups[1].Captures.Count;
        private int EncodedStringCount(string arg) => 2 + arg.Sum(CharsToEncode);
        private int CharsToEncode(char c) => c == '\\' || c == '\"' ? 2 : 1;

    }
}
