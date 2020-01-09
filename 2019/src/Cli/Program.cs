using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib;

namespace Cli
{
    class Program
    {
        private static readonly List<IDay> Days = new List<IDay>()
        {
            new Lib.Day01.Day01(),
            new Lib.Day02.Day02(),
            new Lib.Day03.Day03(),
            new Lib.Day04.Day04(),
            new Lib.Day05.Day05(),
            new Lib.Day06.Day06(),
            new Lib.Day07.Day07(),
            new Lib.Day08.Day08(),
        };

        static void Main(string[] args)
        {
            int day = 5;

            Days[day-1].ProblemOne();
            Days[day-1].ProblemTwo();

        }

        
    }
}
