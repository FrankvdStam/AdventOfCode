using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2022
{
    public class Day03 : BaseDay
    {
        public Day03() : base(2022, 3) 
        {
            _rucksacks = ParseInput(Input);
        }

        private List<(List<byte> left, List<byte> right, List<byte> all)> _rucksacks = new List<(List<byte> left, List<byte> right, List<byte> all)>();

        public override void ProblemOne()
        {
            var sum = 0;
            foreach(var rucksack in _rucksacks)
            {
                var inBoth = rucksack.left.First(i => rucksack.right.Contains(i));
                sum += inBoth;
            }
            Console.WriteLine(sum);
        }

        public override void ProblemTwo()
        {
            var sum = 0;
            for(int i = 0; i < _rucksacks.Count; i+=3)
            {
                var first = _rucksacks[i];
                var second = _rucksacks[i+1];
                var third = _rucksacks[i+2];

                sum += first.all.First(b => second.all.Contains(b) && third.all.Contains(b));
            }
            Console.WriteLine(sum);
        }


        private List<(List<byte>, List<byte>, List<byte>)> ParseInput(string input)
        {
            var a = Encoding.ASCII.GetBytes("a")[0];
            var A = Encoding.ASCII.GetBytes("A")[0];

            byte GetPriority(char c)
            {
                if (char.IsUpper(c))
                {
                    return (byte)(27 + (byte)c - A);
                }
                else
                {
                    return (byte)(1 + (byte)c - a);
                }
            }

            var rucksacks = new List<(List<byte>, List<byte>, List<byte>)>();
            foreach (var l in input.SplitNewLine())
            {
                var leftStr = l.Substring(0, l.Length / 2);
                var rightStr = l.Substring(l.Length / 2);

                var left = leftStr.Select(i => GetPriority(i)).ToList();
                var right = rightStr.Select(i => GetPriority(i)).ToList();

                var all = l.Select(i => GetPriority(i)).ToList();

                rucksacks.Add((left, right, all));
            }

            return rucksacks;
        }


        private const string Example = @"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw
";
    }
}