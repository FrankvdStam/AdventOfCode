using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2021
{
    public class Day21 : IDay
    {
        public int Day => 21;
        public int Year => 2021;


        private int _rolls;
        private int _rollValue = 1;

        private int DeterministicRoll()
        {
            _rolls++;

            var temp = _rollValue;
            _rollValue++;
            if (_rollValue > 100)
            {
                _rollValue = 1;
            }

            return temp;
        }


        private LinkedList<int> GetBoard()
        {
            var board = new LinkedList<int>();
            foreach(var num in Enumerable.Range(1,10))
            {
                board.AddLast(num);
            }
            return board;
        }

        public void ProblemOne()
        {
            var startPositions = ParseInput(Input);
            var board = GetBoard();

            //Setup players
            //var players = new List<LinkedListNode<int>>();
            var scores = new List<int>();
            foreach(var s in startPositions)
            {
                //players.Add(board.Find(s));
                scores.Add(0);
            }


            for(;;)
            {
                for (int i = 0; i < startPositions.Count; i++)
                {
                    //var player = players[i];

                    var currentPosition = startPositions[i];

                    //3 rolls
                    var roll = DeterministicRoll() + DeterministicRoll() + DeterministicRoll();

                    //Calculate next position
                    if(currentPosition + roll > 10)
                    {
                        currentPosition = (currentPosition + roll) % 10;
                        if(currentPosition == 0)
                        {
                            currentPosition = 10;
                        }
                    }
                    else
                    {
                        currentPosition = currentPosition + roll;
                    }


                    scores[i] += currentPosition;
                    startPositions[i] = currentPosition;

                    if (scores[i] >= 1000)
                    {
                        Console.WriteLine(_rolls * scores.Min());
                        return;
                    }




                    //while(roll > 0)
                    //{
                    //    player = player.Next ?? board.First;
                    //    roll--;
                    //}
                    //scores[i] += player.Value; ;
                    //players[i] = player;
                    //
                    //if(scores[i] >= 1000)
                    //{
                    //    Console.WriteLine(_rolls * scores.Min());
                    //    return;
                    //}
                }
            }
        }


        //Dont need all permutations of 123 - the end result will always be within 3-9.
        //
        //111 -> 3
        //
        //211 -> 4
        //121
        //112
        //
        //311 -> 5
        //131
        //113
        //
        //123 -> 6
        //132
        //231
        //213
        //312
        //321
        public void ProblemTwo()
        {
            var startPositions = ParseInput(Example);
            var board = GetBoard();

            //Setup players
            //var players = new List<LinkedListNode<int>>();
            var scores = new List<int>();
            foreach (var s in startPositions)
            {
                //players.Add(board.Find(s));
                scores.Add(0);
            }


            for (; ; )
            {
                for (int i = 0; i < startPositions.Count; i++)
                {
                    //var player = players[i];

                    var currentPosition = startPositions[i];

                    //3 rolls
                    var roll = DeterministicRoll() + DeterministicRoll() + DeterministicRoll();

                    //Calculate next position
                    if (currentPosition + roll > 10)
                    {
                        currentPosition = (currentPosition + roll) % 10;
                        if (currentPosition == 0)
                        {
                            currentPosition = 10;
                        }
                    }
                    else
                    {
                        currentPosition = currentPosition + roll;
                    }


                    scores[i] += currentPosition;
                    startPositions[i] = currentPosition;

                    if (scores[i] >= 21)
                    {
                        Console.WriteLine(_rolls);
                        return;
                    }




                    //while(roll > 0)
                    //{
                    //    player = player.Next ?? board.First;
                    //    roll--;
                    //}
                    //scores[i] += player.Value; ;
                    //players[i] = player;
                    //
                    //if(scores[i] >= 1000)
                    //{
                    //    Console.WriteLine(_rolls * scores.Min());
                    //    return;
                    //}
                }
            }
        }

        private List<int> ParseInput(string input)
        {
            var result = new List<int>();
            var lines = input.SplitNewLine();
            foreach(var l in lines)
            {
                var split = l.Split(' ');
                //result.Add(int.Parse(split[1]));
                result.Add(int.Parse(split[4]));
            }
            return result;
        }

        private const string Example = @"Player 1 starting position: 4
Player 2 starting position: 8";

        private const string Input = @"Player 1 starting position: 5
Player 2 starting position: 9";
    }
}