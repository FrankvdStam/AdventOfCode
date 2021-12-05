using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;
using System.Linq;

namespace Years.Year2017
{
    public class Day25 : IDay
    {
        private struct State
        {
            public char Label;
            public int  _0Write;
            public bool _0MoveRight;
            public char _0NextState;

            public int  _1Write;
            public bool _1MoveRight;
            public char _1NextState;
        }

        public int Day => 25;
        public int Year => 2017;

        public void ProblemOne()
        {
            var states = ParseInput(Input, out char startState, out int steps);
            var currentState = states[startState];
            var tape = new Dictionary<int, int>();
            int position = 0;
            for (int i = 0; i < steps; i++)
            {
                //Read value
                if (!tape.TryGetValue(position, out int value))
                {
                    value = 0;
                }

                //Setup execution
                var write = 0;
                var moveRight = false;
                var nextState = 'A';

                if (value == 1)
                {
                    write     = currentState._1Write;
                    moveRight = currentState._1MoveRight;
                    nextState = currentState._1NextState;
                }
                else
                {
                    write     = currentState._0Write;
                    moveRight = currentState._0MoveRight;
                    nextState = currentState._0NextState;
                }
                
                //Execute
                tape[position] = write;
                if (moveRight)
                {
                    position++;
                }
                else
                {
                    position--;
                }
                currentState = states[nextState];
            }

            var result = tape.Count(i => i.Value == 1);
            Console.WriteLine(result);
        }

        public void ProblemTwo()
        {
        }


        private Dictionary<char, State> ParseInput(string input, out char startState, out int steps)
        {
            var lines = input.SplitNewLine();
            startState = lines[0].Split(' ')[3][0];
            steps = int.Parse(lines[1].Split(' ')[5]);

            var result = new Dictionary<char, State>();
            for (int i = 3; i < lines.Length; i += 10)
            {
                var state = new State();
                state.Label = lines[i + 0].Split(' ')[2][0];
                //If value is 0:
                state._0Write     = lines[i + 2].Split(' ', StringSplitOptions.RemoveEmptyEntries)[4][0] == '1' ? 1 : 0;
                state._0MoveRight = lines[i + 3].Split(' ', StringSplitOptions.RemoveEmptyEntries)[6] == "right.";
                state._0NextState = lines[i + 4].Split(' ', StringSplitOptions.RemoveEmptyEntries)[4][0];

                //if value is 1:
                state._1Write     = lines[i + 6].Split(' ', StringSplitOptions.RemoveEmptyEntries)[4][0] == '1' ? 1 : 0;
                state._1MoveRight = lines[i + 7].Split(' ', StringSplitOptions.RemoveEmptyEntries)[6] == "right.";
                state._1NextState = lines[i + 8].Split(' ', StringSplitOptions.RemoveEmptyEntries)[4][0];
                result[state.Label] = state;
            }
            return result;
        }


        private const string Example = @"Begin in state A.
Perform a diagnostic checksum after 6 steps.

In state A:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the right.
    - Continue with state B.
  If the current value is 1:
    - Write the value 0.
    - Move one slot to the left.
    - Continue with state B.

In state B:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the left.
    - Continue with state A.
  If the current value is 1:
    - Write the value 1.
    - Move one slot to the right.
    - Continue with state A.";

        private const string Input = @"Begin in state A.
Perform a diagnostic checksum after 12656374 steps.

In state A:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the right.
    - Continue with state B.
  If the current value is 1:
    - Write the value 0.
    - Move one slot to the left.
    - Continue with state C.

In state B:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the left.
    - Continue with state A.
  If the current value is 1:
    - Write the value 1.
    - Move one slot to the left.
    - Continue with state D.

In state C:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the right.
    - Continue with state D.
  If the current value is 1:
    - Write the value 0.
    - Move one slot to the right.
    - Continue with state C.

In state D:
  If the current value is 0:
    - Write the value 0.
    - Move one slot to the left.
    - Continue with state B.
  If the current value is 1:
    - Write the value 0.
    - Move one slot to the right.
    - Continue with state E.

In state E:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the right.
    - Continue with state C.
  If the current value is 1:
    - Write the value 1.
    - Move one slot to the left.
    - Continue with state F.

In state F:
  If the current value is 0:
    - Write the value 1.
    - Move one slot to the left.
    - Continue with state E.
  If the current value is 1:
    - Write the value 1.
    - Move one slot to the right.
    - Continue with state A.";
    }
}