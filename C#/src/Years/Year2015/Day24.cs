using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    class Program24
    {
        static void Main(string[] args)
        {
        }

        public void MakeGroups(List<int> input)
        {
            //Assembly is set to check for arithmetic over/underflow.
            int groupSize = input.Sum() / 3;
            Stack<int> stack = new Stack<int>(input);
            List<List<int>> groups = new List<List<int>>();

            while (stack.Any())
            {
                //List<int> potentialGroup =
                    int number = stack.Pop();

            }



            for (int x = 0; x < input.Count; x++)
            {
                //Start at x: don't look back and create duplicate groups
                for (int y = x; y < input.Count; y++)
                {

                }
            }


        }


        static List<int> _testInput = new List<int>()
        {
            1, 2, 3, 4, 5, 7, 8, 9, 10, 11
        };
    }

    public class Day24 : IDay
    {
        public int Day => 24;
        public int Year => 2015;

        public void ProblemOne()
        {
        }

        public void ProblemTwo()
        {
        }
    }
}