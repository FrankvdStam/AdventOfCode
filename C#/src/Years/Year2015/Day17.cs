using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    public class Day17 : BaseDay
    {
        public Day17() : base(2015, 17) { }

        public override void ProblemOne()
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


        public override void ProblemTwo()
        {
            Console.WriteLine(_partTwo);
        }



        private List<int> ParseInput(string input)
        {
            List<int> result = new List<int>();

            foreach (string line in input.SplitNewLine())
            {
                result.Add(int.Parse(line));
            }

            return result;
        }
    }
}