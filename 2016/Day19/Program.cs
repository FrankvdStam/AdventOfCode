using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    
    class Program
    {
        static void Main(string[] args)
        {
            int size = 3014387;
            while (true)
            {
                int winner = 0; //= ProblemTwo(size);
                Console.WriteLine($"{size}: {winner}, {GetFormulaCrossPosition(size)}");
                size++;
            }

            //ProblemTwo();
        }

        static int GetFormulaCrossPosition(int n)
        {
            int pow = (int)Math.Floor(Math.Log(n) / Math.Log(3));
            int b = (int)Math.Pow(3, pow);
            if (n == b)
                return n;
            if (n - b <= b)
                return n - b;
            return 2 * n - 3 * b;
        }

        static void ProblemOne()
        {
            //for (int i = 1; i < 50; i++)
            //{
            //    Console.WriteLine($"{i} soldiers: {FindWinner(i)}");
            //}

            int winner = FindWinner(3014387);
        }

        static int ProblemTwo(int size)
        {

            LinkedList<int> elfs =  new LinkedList<int>();
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
        }

        static int FindNextIndex(int currentIndex, int count)
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
        static int FindWinner(int soldiers)
        {
            int a = LargestPowerOf2(soldiers);
            int l = soldiers - a;
            
            return 2*l + 1;
        }

        static int LargestPowerOf2(int n)
        {
            int result = 2;
            while (result <= n)
            {
                result *= 2;
            }
            return result/2;
        }
    }

    public static class LinkedListExt
    {
        public static int IndexOf<T>(this LinkedList<T> list, T item)
        {
            var count = 0;
            for (var node = list.First; node != null; node = node.Next, count++)
            {
                if (item.Equals(node.Value))
                    return count;
            }
            return -1;
        }

        public static LinkedListNode<T> NodeAt<T>(this LinkedList<T> list, int index)
        {
            int half = list.Count / 2;

            if (index <= half)
            {
                LinkedListNode<T> current = list.First;
                while (index > 0)
                {
                    current = current.Next;
                    index--;
                }
                return current;
            }
            else
            {
                index = list.Count - index;
                LinkedListNode<T> current = list.Last;
                while (index > 0)
                {
                    current = current.Previous;
                    index--;
                }
                return current;
            }

            
        }
    }
}
