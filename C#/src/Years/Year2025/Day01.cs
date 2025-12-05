using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Years.Utils;

namespace Years.Year2025
{
    public class Day01 : BaseDay
    {
        public Day01() : base(2025, 1) 
        {
            Nums = ParseInput(Example);
        }

        private List<int> Nums = new List<int>();

        private string Example = @"L68
L30
R48
L5
R60
L55
L1
L99
R14
L82";

        

        public override void ProblemOne()
        {
            var dial = 50;
            var passedZero = 0;
            foreach (var num in Nums)
            {
                var remainder = num % 100;

                dial += remainder;
                
                if(dial < 0)
                {
                    dial += 100;
                }

                if(dial > 99)
                {
                    dial -= 100;
                }

                if(dial == 0)
                {
                    passedZero += 1;
                }
            }
            Console.WriteLine(passedZero);
        }

        public override void ProblemTwo()
        {
            var dial = 50;
            var passedZero = 0;
            foreach (var num in Nums)
            {
                passedZero += Math.Abs(num) / 100;
                var remainder = num % 100;

                dial += remainder;

                if (dial < 0)
                {
                    passedZero += 1;
                    dial += 100;
                }

                if (dial > 99)
                {
                    passedZero += 1;
                    dial -= 100;
                }
            }
            Console.WriteLine(passedZero);
        }

        //2217 too low

        private List<int> ParseInput(string input)
        {
            return input
                .SplitNewLine()
                .Select(i => i[0] == 'L' ? -int.Parse(i.Substring(1)) : int.Parse(i.Substring(1)))
                .ToList();
        }
    }
}
