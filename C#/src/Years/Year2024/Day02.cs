using System;
using System.Collections.Generic;
using System.Linq;
using Years.Utils;

namespace Years.Year2024
{
    public class Day02() : BaseDay(2024, 2)
    {
        private List<List<int>> _reports;

        public override void ProblemOne()
        {
            _reports = Input.SplitNewLine().Select(i => i.SplitWhitespace().Select(int.Parse).ToList()).ToList();
            var safeCount = _reports.Count(i => CountErrors(i) == 0);
            Console.WriteLine(safeCount);
        }

        //566 too high 530 too low
        public override void ProblemTwo()
        {
            var safeCount = _reports.Count(IsSafeWithTolerance);
            Console.WriteLine(safeCount);
        }

        public bool IsSafeWithTolerance(List<int> report)
        {
            //remove elements until version without errors is found
            for (int i = 0; i < report.Count; i++)
            {
                var clone = report.Clone();
                clone.RemoveAt(i);
                if (CountErrors(clone) == 0)
                {
                    return true;
                }
            }
            return false;
        }
    
        public int CountErrors(List<int> report)
        {
            var errors = 0;
            //Check if all numbers are increasing or decreasing
            var increasing = report.Zip(report.Skip(1), (a, b) => a.CompareTo(b) <= 0).All(b => b);
            var decreasing = report.Zip(report.Skip(1), (a, b) => b.CompareTo(a) <= 0).All(b => b);

            if (!increasing && !decreasing)
            {
                errors++;
            }

            //Check difference between numbers
            var previous = report[0];
            for (int i = 1; i < report.Count; i++)
            {
                var difference = report[i].Difference(previous);
                if (difference < 1 || difference > 3)
                {
                    errors++;
                }
                previous = report[i];
            }

            return errors;
        }

        private readonly string _example = @"7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9";
    }
}