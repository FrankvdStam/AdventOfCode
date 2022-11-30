using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    public class Day01 : BaseDay
    {
        public Day01() : base(2015, 01) {}

        public override void ProblemOne()
        {
            int floor = 0;
            foreach (char c in Input)
            {
                if (c == '(')
                {
                    floor++;
                }
                else
                {
                    floor--;
                }
            }
            Console.WriteLine(floor);
        }

        public override void ProblemTwo()
        {
            int floor = 0;
            int position = 1;
            foreach (char c in Input)
            {
                if (c == '(')
                {
                    floor++;
                }
                else
                {
                    floor--;
                }

                if (floor == -1)
                {
                    Console.WriteLine(position);
                    return;
                }

                position++;
            }
            Console.WriteLine("Didn't reach basement");
        }
    }
}