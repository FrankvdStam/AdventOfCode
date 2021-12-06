using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2018
{
    public class Day09 : IDay
    {
        public int Day => 9;
        public int Year => 2018;

        public void ProblemOne()
        {
            ParseInput(Input, out int playersAmount, out int rounds);
            
            var players = new LinkedList<int>();
            var score = new Dictionary<int, long>();
            for (int i = 1; i <= playersAmount; i++)
            {
                score[i] = 0;
                players.AddLast(i);
            }
            var currentPlayer = players.First;
            var marbles = new LinkedList<int>();
            marbles.AddFirst(0);
            var currentMarble = marbles.First;

            for (int i = 1; i <= rounds; i++)
            {
                AddMarble(currentPlayer.Value, i, ref currentMarble, marbles, score);
                currentPlayer = currentPlayer.Next ?? players.First;
            }
            var winningScore = score.Values.Max();
            Console.WriteLine(winningScore);
        }

        public void ProblemTwo()
        {
            ParseInput(Input, out int playersAmount, out int rounds);
            rounds *= 100;

            var players = new LinkedList<int>();
            var score = new Dictionary<int, long>();
            for (int i = 1; i <= playersAmount; i++)
            {
                score[i] = 0;
                players.AddLast(i);
            }
            var currentPlayer = players.First;
            var marbles = new LinkedList<int>();
            marbles.AddFirst(0);
            var currentMarble = marbles.First;

            for (int i = 1; i <= rounds; i++)
            {
                AddMarble(currentPlayer.Value, i, ref currentMarble, marbles, score);
                currentPlayer = currentPlayer.Next ?? players.First;
            }
            var winningScore = score.Values.Max();
            Console.WriteLine(winningScore);
        }
        

        private void AddMarble(int playerId, int marbleValue, ref LinkedListNode<int> currentMarble, LinkedList<int> marbles, Dictionary<int, long> score)
        {
            if (marbleValue % 23 == 0)
            {
                score[playerId] += marbleValue;
                for (int i = 0; i < 7; i++)
                {
                    currentMarble = currentMarble.Previous ?? marbles.Last;
                }
                var removeMarble = currentMarble;
                score[playerId] += currentMarble.Value;
                currentMarble = currentMarble.Next;
                marbles.Remove(removeMarble);
            }
            else
            {
                currentMarble = currentMarble.Next ?? marbles.First;
                marbles.AddAfter(currentMarble, marbleValue);
                currentMarble = currentMarble.Next ?? marbles.First;
            }
        }

        private void ParseInput(string input, out int players, out int points)
        {
            var split = input.Split(' ');
            players = int.Parse(split[0]);
            points = int.Parse(split[6]);
        }
        
        private const string Input = @"464 players; last marble is worth 70918 points";
    }
}