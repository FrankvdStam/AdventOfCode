using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2022
{
    public class Day05 : BaseDay
    {
        public Day05() : base(2022, 5) 
        {
            (_state, _moves) = ParseInput(Input);
        }

        private readonly Dictionary<int, Stack<char>> _state = new();
        private readonly List<(int amount, int from, int to)> _moves = new();


        public override void ProblemOne()
        {
            var state = _state.ToDictionary(i => i.Key, i => new Stack<char>(new Stack<char>(i.Value))); //copy so that the state can be reused in p2

            foreach (var move in _moves)
            {
                for (int i = 0; i < move.amount; i++)
                {
                    if (state[move.from].Any())
                    {
                        var temp = state[move.from].Pop();
                        state[move.to].Push(temp);
                    }
                }
            }

            var sb = new StringBuilder();
            for (int i = 1; i <= state.Count; i++)
            {
                sb.Append(state[i].Pop());
            }
            Console.WriteLine(sb.ToString());
        }

        public override void ProblemTwo()
        {
            var state = _state.ToDictionary(i => i.Key, i => new Stack<char>(new Stack<char>(i.Value))); //copy so that the state can be reused in p2

            foreach (var move in _moves)
            {
                //hackity :)
                var moveList = new List<char>();
                for (int i = 0; i < move.amount; i++)
                {
                    if (state[move.from].Any())
                    {
                        var temp = state[move.from].Pop();
                        moveList.Add(temp);
                    }
                }

                //hackity :)
                moveList.Reverse();

                foreach (char c in moveList)
                {
                    state[move.to].Push(c);
                }
            }

            var sb = new StringBuilder();
            for (int i = 1; i <= state.Count; i++)
            {
                sb.Append(state[i].Pop());
            }
            Console.WriteLine(sb.ToString());
        }



        private static (Dictionary<int, Stack<char>> state, List<(int amount, int from, int to)> moves) ParseInput(string input)
        {
            var halves = input.Split("\n\n");

            var state = halves[0];
            var movements = halves[1];

            //Parse state
            var parsedState = new Dictionary<int, Stack<char>>();

            var stateLines = state.Split("\n");
            var len = stateLines[0].Length;
            //Each column is 3 characters for brackets + a letter, then only whitespaces on the inside.
            //That means we should be able to add 1 for the missing whitespace at the very end and then devide by 4
            var size = (len + 1) / 4;

            //Make sure all columns exist
            for (var column = 1; column <= size; column++)
            {
                parsedState[column] = new Stack<char>();
            }

            //start from the bottom, work upwards, remove "header"
            stateLines = stateLines.Take(stateLines.Length - 1).Reverse().ToArray();

            var height = 0;
            foreach (var line in stateLines)
            {
                for (var column = 1; column <= size; column++)
                {
                    //The character is always the 2nd character. Start at index 1 and incremented by 4.
                    var charIndex = ((column - 1) * 4) + 1;
                    char c = line[charIndex];
                    if (c != ' ')
                    {
                        parsedState[column].Push(c);
                    }
                }
                height++;
            }

            var parsedMoves = new List<(int amount, int from, int to)>();

            //Parse movements
            foreach (var line in movements.Split("\n", StringSplitOptions.RemoveEmptyEntries))
            {
                var split = line.Split(' ');
                parsedMoves.Add((
                    int.Parse(split[1]),
                    int.Parse(split[3]),
                    int.Parse(split[5])
                ));
            }

            return (parsedState, parsedMoves);
        }

        private const string Example = @"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2";
    }
}