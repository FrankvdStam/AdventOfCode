using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2024
{
    public class Day05 : BaseDay
    {
        public Day05() : base(2024, 5) {}

        private int _rewriteSum = 0;

        public override void ProblemOne()
        {
            var (pageOrder, updates) = Parse(Input);
            var sum = 0;
            _rewriteSum = 0;
            foreach (var update in updates)
            {
                if (IsUpdateCorrect(pageOrder, update))
                {
                    var middle = (update.Count-1) / 2;
                    sum += update[middle];
                    //Console.WriteLine(string.Join(',', update));
                }
                else
                {
                    var rewrite = RewriteUpdate(pageOrder, update);
                    if (!IsUpdateCorrect(pageOrder, rewrite))
                    {
                        throw new ArgumentException();
                    }
                    var middle = (rewrite.Count - 1) / 2;
                    _rewriteSum += rewrite[middle];
                }
            }
            Console.WriteLine(sum);
        }

        public override void ProblemTwo()
        {
            Console.WriteLine(_rewriteSum);
        }

        private void ToTree(List<(int x, int y)> pageOrder)
        {
            var dict = new Dictionary<int, TreeNode<int>>();
            foreach (var num in pageOrder)
            {
                if (!dict.ContainsKey(num.x))
                {
                    dict.Add(num.x, new TreeNode<int>(){Value = num.x});
                }
            }

            foreach (var num in pageOrder)
            {
                var parent = dict[num.x];
                var child = dict[num.y];
                if (!parent.Children.Contains(child))
                {
                    child.Parent = parent;
                    parent.Children.Add(child);
                }
            }

            
        }

        private List<int> RewriteUpdate(List<(int x, int y)> pageOrder, List<int> update)
        {
            var remaining = update.Clone();
            var result = new List<int>();
            while (remaining.Count > 0)
            {
                foreach (var num in remaining)
                {
                    var rules = pageOrder.Where(i => i.y == num).Select(i => i.x);
                    var shouldComeFirst = remaining.Where(i => rules.Contains(i));
                    if (!shouldComeFirst.Any())
                    {
                        remaining.Remove(num);
                        result.Add(num);
                        break;
                    }
                }
            }
            return result;
        }


        private bool IsUpdateCorrect(List<(int x, int y)> pageOrder, List<int> update)
        {
            var notAllowed = new List<int>();
            foreach (var num in update)
            {
                if (notAllowed.Contains(num))
                {
                    return false;
                }

                notAllowed.AddRange(pageOrder.Where(i => i.y == num).Select(i => i.x));
            }

            return true;
        }


        private (List<(int x, int y)>, List<List<int>>) Parse(string input)
        {
            var split = input.Split("\r\n\r\n");
            if (split.Length <= 1)
            {
                split = input.Split("\n\n");
            }

            var nums = split[0].SplitNewLine().SelectMany(i => i.Split('|')).Select(int.Parse).ToList();
            var pageOrder = new List<(int x, int y)>();
            for (int i = 0; i < nums.Count(); i+=2)
            { 
                pageOrder.Add((nums[i], nums[i + 1]));
            }

            var updates = new List<List<int>>();
            foreach (var line in split[1].SplitNewLine())
            {
                updates.Add(line.Replace(Environment.NewLine, "").Split(',').Select(int.Parse).ToList());
            }

            return (pageOrder, updates);
        }

        private readonly string _example = @"47|53
97|13
97|61
97|47
75|29
61|13
75|53
29|13
97|29
53|29
61|53
97|53
61|29
47|13
75|47
97|75
47|61
75|61
47|29
75|13
53|13

75,47,61,53,29
97,61,53,29,13
75,29,13
75,97,47,61,53
61,13,29
97,13,75,29,47";
    }
}