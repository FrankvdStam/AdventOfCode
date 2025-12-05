using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Years.Utils;

namespace Years.Year2025
{
    public class Day03 : BaseDay
    {
        public Day03() : base(2025, 3) 
        {
            IdRanges = ParseInput(Example);
        }

        private List<(long left, long right)> IdRanges = new();

        private string Example = @"11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";
               
        public override void ProblemOne()
        {
            long id = 0;
            foreach (var range in IdRanges)
            {
                for (var i = range.left; i <= range.right; i++)
                {
                    if(!IsValidId(i.ToString()))
                    {
                        id += i;
                    }
                }
            }
            Console.WriteLine(id);
        }

        public override void ProblemTwo()
        {
            long id = 0;
            foreach (var range in IdRanges)
            {
                for (var i = range.left; i <= range.right; i++)
                {
                    if (!IsValidPart2(i.ToString()))
                    {
                        id += i;
                    }
                }
            }
            Console.WriteLine(id);
        }



        public static bool IsValidId(string num)
        {
            //odd numbers are always valid IDs
            if(num.Length % 2 != 0)
            {
                return true;
            }

            var half = num.Length / 2;
            var left = num.Substring(0, half);
            var right = num.Substring(half, half);

            return left != right;
        }

        public static bool IsValidPart2(string num)
        {
            //check each length substring, at most HALF (rounded down by implicit int behavior) the size of the full string
            for (int substrLength = 1; substrLength <= num.Length / 2; substrLength++)
            {
                //check if this substring length fits x times in the total size; if not, its not possible to have a repeating pattern
                if(num.Length % substrLength != 0)
                {
                    continue;
                }

                var repeatCount = (num.Length / substrLength) - 1;

                var pattern = num.Substring(0, substrLength);

                var breakEarly = false;
                for(int i = 0; i < repeatCount; i++)
                {
                    var current = num.Substring(substrLength * (i + 1), substrLength);
                    if (pattern != current)
                    {
                        breakEarly = true;
                        break; //don't have to check the rest of the sequence
                    }
                }

                if(!breakEarly)
                {
                    return false;
                } 
            }
            return true;
        }

        public static bool IsValidIdOverengineered(string num)
        {
            //check each length substring, at most HALF (rounded down by implicit int behavior) the size of the full string
            for(int substrLength = 1; substrLength <= num.Length/2; substrLength++)
            {

                //No point checking in the right half for positions either
                for(int substrPosition = 0;  substrPosition <= num.Length / 2; substrPosition++)
                {
                    var substring = num.Substring(substrPosition, substrLength);
                    var subStrCount = num.Count(substring);
                    if(subStrCount > 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        private List<(long left, long right)> ParseInput(string input)
        {
            return input
                .Split(',')
                .Select(i => (long.Parse(i.Split('-')[0]), long.Parse(i.Split('-')[1])))
                .ToList();
        }
    }
}
