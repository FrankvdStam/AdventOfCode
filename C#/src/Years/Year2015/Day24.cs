using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    public class Day24 : IDay
    {
        public int Day => 24;
        public int Year => 2015;

        public void ProblemOne()
        {
            var input = Input.Split(new string[] { "\r\n" }, StringSplitOptions.None).Select(i => long.Parse(i)).ToList();
            var groupSize = input.Sum() / 3;

            //Find all the possible groups
            List<List<long>> groups = Sum(input, groupSize);

            //Find smallest groups and their entanglement, keep track of the smallest entanglement
            long minCount = groups.Min(i => i.Count);
            var smallest = groups.Where(i => i.Count == minCount).ToList();

            long min = long.MaxValue;
            foreach (var group in smallest)
            {
                long quantumEntanglement = group.Aggregate((a, b) => a * b);
                if (quantumEntanglement < min)
                {
                    min = quantumEntanglement;
                }
            }
            Console.WriteLine(min);
        }

        public void ProblemTwo()
        {
            var input = Input.Split(new string[] { "\r\n" }, StringSplitOptions.None).Select(i => long.Parse(i)).ToList();
            var groupSize = input.Sum() / 4;

            //Find all the possible groups
            List<List<long>> groups = Sum(input, groupSize);

            //Find smallest groups and their entanglement, keep track of the smallest entanglement
            long minCount = groups.Min(i => i.Count);
            var smallest = groups.Where(i => i.Count == minCount).ToList();

            long min = long.MaxValue;
            foreach (var group in smallest)
            {
                long quantumEntanglement = group.Aggregate((a, b) => a * b);
                if (quantumEntanglement < min)
                {
                    min = quantumEntanglement;
                }
            }
            Console.WriteLine(min);
        }

        //https://stackoverflow.com/questions/4632322/finding-all-possible-combinations-of-numbers-to-reach-a-given-sum
        private void RecursiveSum(List<long> numbers, long target, List<long> partial, ref List<List<long>> results)
        {
            long sum = partial.Sum();
            if (sum == target)
            {
                results.Add(partial);
                //if (results.Count() % 10000 == 0)
                //{
                //    Console.WriteLine(results.Count);
                //}
            }

            if (sum >= target)
            {
                return;
            }
                
            for (int i = 0; i < numbers.Count(); i++)
            {
                List<long> remaining = new List<long>();
                for (int j = i + 1; j < numbers.Count(); j++)
                {
                    remaining.Add(numbers[j]);
                }

                List<long> newPartial = new List<long>(partial);
                newPartial.Add(numbers[i]);
                RecursiveSum(remaining, target, newPartial, ref results);
            }
        }
        private List<List<long>> Sum(List<long> numbers, long target)
        {
            List<List<long>> results = new List<List<long>>();
            RecursiveSum(numbers, target, new List<long>(), ref results);
            return results;
        }




        private const string Example = @"1
2
3
4
5
7
8
9
10
11";


        private const string Input = @"1
2
3
7
11
13
17
19
23
31
37
41
43
47
53
59
61
67
71
73
79
83
89
97
101
103
107
109
113";

    }
}