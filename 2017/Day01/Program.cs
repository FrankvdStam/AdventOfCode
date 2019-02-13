using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            ParseInput(Example);
            ProblemOne();
            //ProblemTwo();
        }

        static void ProblemOne()
        {

        }

        static void ProblemTwo()
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
