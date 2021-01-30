using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2016
{
    public class Day19 : IDay
    {
        public int Day => 19;
        public int Year => 2016;

        public void ProblemOne()
        {
            int winner = FindWinner(Input);
            Console.WriteLine(winner);


            //int size = 3014387;
            //while (true)
            //{
            //    int winner = 0; //= ProblemTwo(size);
            //    Console.WriteLine($"{size}: {winner}, {GetFormulaCrossPosition(size)}");
            //    size++;
            //}
        }

        public void ProblemTwo()
        {
            LinkedList<int> elfs = new LinkedList<int>();
            for (int i = 1; i <= Input; i++)
            {
                elfs.AddLast(i);
            }

            LinkedListNode<int> currentElf = elfs.First;
            while (elfs.Count > 1)
            {
                //Remove the elf 
                int deleteIndex = FindNextIndex(elfs.IndexOf(currentElf.Value), elfs.Count);
                var elf = elfs.NodeAt(deleteIndex);
                elfs.Remove(elf);

                //Move to next elf
                currentElf = currentElf.Next == null ? elfs.First : currentElf.Next;

                if (elfs.Count % 1000 == 0)
                {
                    Console.WriteLine(elfs.Count);
                }
            }

            int winner = elfs.First.Value;
        }


        private int GetFormulaCrossPosition(int n)
        {
            int pow = (int)Math.Floor(Math.Log(n) / Math.Log(3));
            int b = (int)Math.Pow(3, pow);
            if (n == b)
                return n;
            if (n - b <= b)
                return n - b;
            return 2 * n - 3 * b;
        }


        /*
        private void ProblemOne()
        {
            //for (int i = 1; i < 50; i++)
            //{
            //    Console.WriteLine($"{i} soldiers: {FindWinner(i)}");
            //}

            int winner = FindWinner(3014387);
        }

        private int ProblemTwo(int size)
        {

            LinkedList<int> elfs = new LinkedList<int>();
            for (int i = 1; i <= size; i++)
            {
                elfs.AddLast(i);
            }

            LinkedListNode<int> currentElf = elfs.First;
            while (elfs.Count > 1)
            {
                //Remove the elf 
                int deleteIndex = FindNextIndex(elfs.IndexOf(currentElf.Value), elfs.Count);
                var elf = elfs.NodeAt(deleteIndex);
                elfs.Remove(elf);

                //Move to next elf
                currentElf = currentElf.Next == null ? elfs.First : currentElf.Next;

                if (elfs.Count % 1000 == 0)
                {
                    Console.WriteLine(elfs.Count);
                }
            }

            int winner = elfs.First.Value;
            return winner;
        }*/

        private int FindNextIndex(int currentIndex, int count)
        {
            if (count % 2 != 0)
            {
                //By subtracting 1 in the case of odd length,
                //we force picking the left elf
                count++;
            }

            int next = count / 2 + currentIndex;

            if (next % count != next)
            {
                next -= count;
            }

            return next;
        }


        //https://en.wikipedia.org/wiki/Josephus_problem
        private int FindWinner(int soldiers)
        {
            int a = LargestPowerOf2(soldiers);
            int l = soldiers - a;

            return 2 * l + 1;
        }

        private int LargestPowerOf2(int n)
        {
            int result = 2;
            while (result <= n)
            {
                result *= 2;
            }
            return result / 2;
        }

        private const int Input = 3014387;
    }
}
