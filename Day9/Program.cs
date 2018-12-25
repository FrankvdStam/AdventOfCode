using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day9
{
    public static class Extensions
    {
        public static IEnumerable<T> Slice<T>(this IEnumerable<T> source, int startIndex, int endIndex)
        {
            return source.Skip(startIndex).Take(endIndex);
        }

        public static LinkedListNode<T> NodeAt<T>(this LinkedList<T> source, int index)
        {
            LinkedListNode<T> current = source.First;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            return current;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ProblemOne();
            //ProblemTwo();
        }

        static void ProblemOne()
        {
            int playersAmount = 464;
            int rounds = 70918 * 100;

            LinkedList<int> players = new LinkedList<int>();
            Dictionary<int, int> score = new Dictionary<int, int>();
            for(int i = 1; i <= playersAmount; i++)
            {
                score[i] = 0;
                players.AddLast(i);
            }
            LinkedListNode<int> currentPlayer = players.First;
            LinkedList<int> marbles = new LinkedList<int>();
            marbles.AddFirst(0);
            LinkedListNode<int> currentMarble = marbles.First;

            for(int i = 1; i <= rounds; i++)
            {
                if(i % 100000 == 0)
                {
                    Console.WriteLine(i);
                }
                AddMarble(currentPlayer.Value, i, ref currentMarble, marbles, score);
                currentPlayer = currentPlayer.Next ?? players.First;
            }
            var winningScore = score.Values.Max();
        }

        

        static void AddMarble(int playerId, int marbleValue, ref LinkedListNode<int> currentMarble, LinkedList<int> marbles, Dictionary<int, int> score)
        {
            if(marbleValue % 23 == 0)
            {
                score[playerId] += marbleValue;
                for(int i = 0; i < 7; i++)
                {
                    currentMarble = currentMarble.Previous ?? marbles.Last;
                }
                int removeValue = currentMarble.Value;
                score[playerId] += removeValue;
                currentMarble = currentMarble.Next;
                marbles.Remove(removeValue);
            }
            else
            {
                currentMarble = currentMarble.Next ?? marbles.First;
                marbles.AddAfter(currentMarble, marbleValue);
                currentMarble = currentMarble.Next ?? marbles.First;
            }
        }

        static void ProblemTwo(){
        }

        static void ParseInput(string input)
        {

        }

        static string example = @"";
        static string input = @"";
    }
}
