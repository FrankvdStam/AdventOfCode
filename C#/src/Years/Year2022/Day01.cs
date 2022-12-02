using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2022
{
    public class Day01 : BaseDay
    {
        public Day01() : base(2022, 1) 
        {
            //Stopwatch s = new Stopwatch(); s.Start();
            //var altInput = File.ReadAllText(@"C:\Users\Frank\Desktop\aoc_2022_day01_large_input.txt");

            var current = 0;
            foreach (var l in (Input + "\n").Split("\n"))
            {
                if (!string.IsNullOrWhiteSpace(l))
                {
                    current += int.Parse(l);
                }
                else
                {
                    _scores.Add(current);
                    current = 0;
                }
            }
            //s.Stop();
            //Console.WriteLine($"parse input elapsed: {s.Elapsed}");
        }

        private readonly List<int> _scores = new List<int>();


        public override void ProblemOne()
        {
            Console.WriteLine(_scores.Max());
        }

        public override void ProblemTwo()
        {
            var sum = _scores.OrderByDescending(x => x).Take(3).Sum();
            Console.WriteLine(sum);
        }

        private const string Example = @"1000
2000
3000

4000

5000
6000

7000
8000
9000

10000";
    }
}