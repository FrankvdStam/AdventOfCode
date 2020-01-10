using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Lib.Shared;

namespace Lib.Day09
{
    public class Day09 : IDay
    {
        public int Day => 9;

        public void ProblemOne()
        {
            IntCodeComputer computer = new IntCodeComputer("109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99");
            computer.Run();
            var result = computer.ToString();
        }
        public void ProblemTwo()
        {
        }
    }
}
