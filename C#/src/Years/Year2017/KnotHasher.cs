using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Years.Year2017
{
    public static class KnotHasher
    {
        public static string Calculate(string input)
        {
            //ascii sequence input
            var lengths = new List<int>();
            foreach (char c in input)
            {
                lengths.Add((int)c);
            }

            //Weird arbitrary requirement
            //add the following lengths to the end of the sequence: 17, 31, 73, 47, 23
            lengths.AddRange(new List<int>() { 17, 31, 73, 47, 23 });


            //Setup traversal variables
            var skipSize = 0;
            var numbers = new LinkedList<int>(Enumerable.Range(0, 256));
            LinkedListNode<int> currentNode = numbers.First;

            for (int loop = 0; loop < 64; loop++)
            {
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
            }

            //sparse to dense hash
            var node = numbers.First;
            var sb = new StringBuilder();
            while (node != null)
            {
                byte xored = 0;
                for (int i = 0; i < 16; i++)
                {
                    xored ^= (byte)node.Value;
                    node = node.Next;
                }
                sb.Append(xored.ToString("x").PadLeft(2, '0'));
            }
            if (sb.Length != 16)
            {

            }

            return sb.ToString();
        }
    }
}
