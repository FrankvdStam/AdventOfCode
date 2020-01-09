using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cli;
using Cli.Day01;

namespace Cli
{
    class Program
    {
        private static readonly List<IDay> Days = new List<IDay>()
        {
            new Day01.Day01(),
            new Day02.Day02(),
            new Day03.Day03(),
            new Day04.Day04(),
            new Day05.Day05(),
            new Day06.Day06(),
            new Day07.Day07(),
            new Day08.Day08(),
        };

        static void Main(string[] args)
        {
            int day = 2;

            Days[day-1].ProblemOne();
            Days[day-1].ProblemTwo();

        }

        
    }
}
