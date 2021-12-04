using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2017
{
    public class Day10 : IDay
    {
        public int Day => 10;
        public int Year => 2017;

        public void ProblemOne()
        {
            //Parse input numbers
            var lengths = new List<int>();
            foreach (var num in Input.Split(','))
            {
                lengths.Add(int.Parse(num));
            }

            //Setup traversal variables
            var skipSize = 0;
            var numbers = new LinkedList<int>(Enumerable.Range(0, 256));
            LinkedListNode<int> currentNode = numbers.First;
            
            foreach (var length in lengths)
            {
                //Get the nodes to reverse, loop around
                var temp = new List<int>();
                var reverseStartNode = currentNode;
                for (int i = 0; i < length; i++)
                {
                    temp.Add(currentNode.Value);
                    currentNode = currentNode.Next ?? numbers.First;
                }

                //Apply reversal in-place
                temp.Reverse();
                foreach (var num in temp)
                {
                    reverseStartNode.Value = num;
                    reverseStartNode = reverseStartNode.Next ?? numbers.First;
                }

                //Finish up
                for (int i = 0; i < skipSize; i++)
                {
                    currentNode = currentNode.Next ?? numbers.First;
                }
                skipSize++;


                //Console.WriteLine($"{skipSize} {currentNode.Value} {string.Join(',', numbers)} {string.Join(',', temp)}");
            }
            Console.WriteLine(numbers.First.Value * numbers.First.Next.Value);
        }

        public void ProblemTwo()
        {
            var result = KnotHasher.Calculate(Input);
            Console.WriteLine(result);
        }


        private const string Example = "3,4,1,5";
        private readonly string Input = $"225,171,131,2,35,5,0,13,1,246,54,97,255,98,254,110";
    }
}