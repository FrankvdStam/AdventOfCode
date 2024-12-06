using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2024
{
    public class Day01 : BaseDay
    {
        public Day01() : base(2024, 1) {}

        private List<int> _left;
        private List<int> _right;

        public override void ProblemOne()
        {
            var nums = Input.SplitNewLine().SelectMany(i => i.Split(" ", StringSplitOptions.RemoveEmptyEntries)).Select(int.Parse).ToList();
            _left = nums.Where((value, index) => index % 2 == 0).OrderBy(i => i).ToList();
            _right = nums.Where((value, index) => index % 2 != 0).OrderBy(i => i).ToList();

            var distance = 0;
            for (int i = 0; i < _left.Count; i++)
            {
                distance += _left[i].Difference(_right[i]);
            }
            Console.WriteLine(distance);
        }

        public override void ProblemTwo()
        {
            var total = 0;
            foreach (var num in _left)
            {
                total += num * _right.Count(i => i == num);
            }
            Console.WriteLine(total);
        }

#pragma warning disable CS0414
        private readonly string _example = "3   4\r\n4   3\r\n2   5\r\n1   3\r\n3   9\r\n3   3";
#pragma warning restore CS0414
    }
}