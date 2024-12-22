using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using Years.Utils;

namespace Years.Year2024
{
    public class Day07 : BaseDay
    {
        public Day07() : base(2024, 7)
        {
            
        }

        public override void ProblemOne()
        {
            var numbers = Parse(Input);
            var max = numbers.Max(i => i.Item2.Count);

            var operators = new List<Operator>
            {
                Operator.Add,
                Operator.Multiply,
            }.PermuteAllCombinationsAndPermutations(max).Select(i => i.ToList()).ToList();

            long result = 0;
            foreach (var line in numbers)
            {
                var valid = false;

                foreach (var operatorWithLength in operators.Where(i => i.Count() == line.Item2.Count - 1))
                {
                    var num = line.Item2[0];

                    for (var i = 1; i < line.Item2.Count; i++)
                    {
                        var next = line.Item2[i];
                        var o = operatorWithLength[i - 1];
                        switch (o)
                        {
                            case Operator.Add:
                                num += next;
                                break;
                            case Operator.Multiply:
                                num *= next;
                                break;
                        }
                    }

                    if (num == line.Item1)
                    {
                        valid = true;
                        break;
                    }
                }

                if (valid)
                {
                    result += line.Item1;
                }
            }
            Console.WriteLine(result);
        }

        public override void ProblemTwo()
        {
            var numbers = Parse(Input);
            var max = numbers.Max(i => i.Item2.Count);

            var operators = new List<Operator>
            {
                Operator.Add,
                Operator.Multiply,
                Operator.Concat,
            }.PermuteAllCombinationsAndPermutations(max).Select(i => i.ToList()).ToList();

            long result = 0;
            foreach (var line in numbers)
            {
                var valid = false;

                foreach (var operatorWithLength in operators.Where(i => i.Count() == line.Item2.Count - 1))
                {
                    var num = line.Item2[0];

                    for (var i = 1; i < line.Item2.Count; i++)
                    {
                        var next = line.Item2[i];
                        var o = operatorWithLength[i - 1];
                        switch (o)
                        {
                            case Operator.Add:
                                num += next;
                                break;
                            case Operator.Multiply:
                                num *= next;
                                break;
                            case Operator.Concat:
                                var str = $"{num}{next}";
                                num = long.Parse(str);
                                break;
                        }
                    }

                    if (num == line.Item1)
                    {
                        valid = true;
                        break;
                    }
                }

                if (valid)
                {
                    result += line.Item1;
                }
            }
            Console.WriteLine(result);
        }

        private enum Operator
        {
            Multiply,
            Add,
            Concat,
        }


        private bool CanBeTrue(int result, List<int> numbers)
        {





            return false;
        }


        private List<(long, List<long>)> Parse(string input)
        {
            var result = new List<(long, List<long>)>();
            foreach (var line in input.SplitNewLine())
            {
                var colonSplit = line.Split(':');
                var split = colonSplit[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                
                var temp = new List<long>();
                foreach (var num in split)
                {
                    temp.Add(long.Parse(num));
                }
                result.Add((long.Parse(colonSplit[0]), temp));
            }

            return result;
        }

        private readonly string _example = @"190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20";
    }
}