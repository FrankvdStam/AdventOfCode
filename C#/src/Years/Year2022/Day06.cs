using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2022
{
    public class Day06 : BaseDay
    {
        public Day06() : base(2022, 6) {}

        public override void ProblemOne()
        {
            Console.WriteLine(FindDifferentCharsIndex(Input.Replace("\n", string.Empty), 4) + 4);
        }

        public override void ProblemTwo()
        {
            Console.WriteLine(FindDifferentCharsIndex(Input.Replace("\n", string.Empty), 14) + 14);
        }

        private int FindDifferentCharsIndex(string input, int size)
        {
            if(input.Length < size)
            {
                throw new Exception();
            }

            for(int i = 0; i < input.Length - size; i++)
            {
                var count = new Dictionary<char, int>();
                for (int j = i; j < i + size; j++)
                {
                    if(!count.ContainsKey(input[j]))
                    {
                        count[input[j]] = 0;
                    }
                    count[input[j]]++;
                }

                if(count.All(i => i.Value == 1))
                {
                    return i;
                }
            }

            throw new Exception();
        }

        private const string Example1 = @"mjqjpqmgbljsphdztnvjfqwrcgsmlb";
        private const string Example5 = @"zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw";
    }
}