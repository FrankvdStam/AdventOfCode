using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cli.Day01
{
    public class Day01 : IDay
    {
        public int Day => 1;

        public Day01()
        {

        }


        public void ProblemOne()
        {

        }

        public void ProblemTwo()
        {

        }

        static void ParseInput(string input)
        {
            var lines = input.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                var bits = line.Split(' ');
            }
        }


        private static string Input = @"";
        private static string Example = @"";
    }
}
