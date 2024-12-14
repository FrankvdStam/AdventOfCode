using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2024
{
    public class Day03 : BaseDay
    {
        public Day03() : base(2024, 3) {}

        public override void ProblemOne()
        {
            var memory = Input;

            var indices = memory.AllOccurencesOf("mul(");
            var parsed = new List<(int a, int b)>();

            foreach (var index in indices)
            {
                var comma = memory.IndexOf(',', index);
                var parenthesis = memory.IndexOf(')', index);

                if (comma == -1 || parenthesis == -1)
                {
                    continue;
                }

                string first;
                string second;
                try
                {
                    first = memory.Substring(index + 4, comma - (index + 4));
                    second = memory.Substring(comma + 1, parenthesis - (comma + 1));
                }
                catch
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(first) || string.IsNullOrWhiteSpace(second))
                {
                    continue;
                }

                if (!int.TryParse(first, out int a) || !int.TryParse(second, out int b))
                {
                    continue;
                }
                parsed.Add((a, b));
            }

            var result = parsed.Sum(i => i.a * i.b);
            Console.WriteLine(result);
        }

        public override void ProblemTwo()
        {

            var memory = Input;

            var indices = memory.AllOccurencesOf("mul(");
            indices.AddRange(memory.AllOccurencesOf("don't()"));
            indices.AddRange(memory.AllOccurencesOf("do()"));
            indices.Sort();

            var enabled = true;
            var parsed = new List<(int a, int b)>();

            foreach (var index in indices)
            {
                if (memory[index] == 'd')
                {
                    if (memory[index + 2] == 'n')
                    {
                        enabled = false;
                    }
                    else
                    {
                        enabled = true;
                    }
                    continue;
                }


                var comma = memory.IndexOf(',', index);
                var parenthesis = memory.IndexOf(')', index);

                if (comma == -1 || parenthesis == -1)
                {
                    continue;
                }

                string first;
                string second;
                try
                {
                    first = memory.Substring(index + 4, comma - (index + 4));
                    second = memory.Substring(comma + 1, parenthesis - (comma + 1));
                }
                catch
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(first) || string.IsNullOrWhiteSpace(second))
                {
                    continue;
                }

                if (!int.TryParse(first, out int a) || !int.TryParse(second, out int b))
                {
                    continue;
                }

                if (enabled)
                {
                    parsed.Add((a, b));
                }
            }

            var result = parsed.Sum(i => i.a * i.b);
            Console.WriteLine(result);
        }

        private readonly string _example = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
        private readonly string _example2 = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
    }
}