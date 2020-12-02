using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    public class Day17 : IDay
    {
        public int Day => 17;
        public int Year => 2015;

        public void ProblemOne()
        {
            var lines = Input.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            var list = new List<int>();

            foreach (var line in lines)
            {
                list.Add(int.Parse(line));
            }
            var result = Enumerable.Range(1, (1 << list.Count) - 1)
                .Select(index => list.Where((item, idx) => ((1 << idx) & index) != 0).ToList());
            //PART 1
            var combinationsSatysfying = result.Where(comb => comb.Sum() == 150);

            //PART 2
            var minCount = combinationsSatysfying.Min(comb => comb.Count());
            var minCombinations = combinationsSatysfying.Where(comb => comb.Count() == minCount);

            _partTwo = minCombinations.Count();

            Console.WriteLine(combinationsSatysfying.Count());
        }

        private int _partTwo;


        public void ProblemTwo()
        {
            Console.WriteLine(_partTwo);
        }



        private List<int> ParseInput(string input)
        {
            List<int> result = new List<int>();

            foreach (string line in input.Split(new string[] { "\r\n" }, StringSplitOptions.None))
            {
                result.Add(int.Parse(line));
            }

            return result;
        }


        private string Input = @"50
44
11
49
42
46
18
32
26
40
21
7
18
43
10
47
36
24
22
40";

    }
}