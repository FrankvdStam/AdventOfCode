﻿using System;
using System.Collections.Generic;
using System.Linq;
using Years.Utils;
using Years.Year2019.IntCodeComputer;

namespace Years.Year2019
{
    public class Day11 : IDay
    {
        public int Day => 11;
        public int Year => 2019;

        private static readonly Dictionary<Direction, Vector2i> Moves = new Dictionary<Direction, Vector2i>()
        {
            {Direction.Up, new Vector2i(0, -1)},
            {Direction.Down, new Vector2i(0, 1)},
            {Direction.Left, new Vector2i(-1, 0)},
            {Direction.Right, new Vector2i(1, 0)},
        };


        private void RunComputerToInputHalt(Computer c)
        {
            var state = c.Step();
            while (true)
            {
                if (state == State.Halt || state == State.WaitingForInput)
                {
                    return;
                }
                state = c.Step();
            }
        }


        public void ProblemOne()
        {
            Dictionary<Vector2i, int> count = new Dictionary<Vector2i, int>();
            Dictionary<Vector2i, int> color = new Dictionary<Vector2i, int>();

            int GeColor(Vector2i p)
            {
                int c;
                if (!color.TryGetValue(p, out c))
                {
                    c = 0; //black
                    color[p] = c;
                }

                return c;
            }

            void IncrementCount(Vector2i p)
            {
                if (!count.ContainsKey(p))
                {
                    count[p] = 0;
                }

                count[p]++;
            }

            Vector2i position = new Vector2i();
            Direction direction = Direction.Up;

            Computer computer = new Computer(Program);
            computer.PrintDisassembly = false;
            computer.PrintOutput = false;
            int cycle = 0;
            while (computer.State != State.Halt)
            {
                try
                {

                    RunComputerToInputHalt(computer);

                    int currentColor = GeColor(position);
                    computer.Input.Add(currentColor); ;

                    RunComputerToInputHalt(computer);
                    long newColor = computer.Output[0];
                    computer.Output.RemoveAt(0);

                    RunComputerToInputHalt(computer);
                    long rotate = computer.Output[0];
                    computer.Output.RemoveAt(0);

                    color[position] = (int)newColor;
                    IncrementCount(position);

                    //Move
                    if (rotate == 0)
                    {
                        direction = direction.RotateLeft();
                    }
                    else
                    {
                        direction = direction.RotateRight();
                    }

                    position = position.Add(Moves[direction]);
                    cycle++;
                    //Console.WriteLine(cycle);
                }
                catch
                {
                    if (computer.State == State.Halt)
                    {
                        break;
                    }
                }
            }

            var panelsPaintedOnce = count.Count(i => i.Value > 0);
            Console.WriteLine(panelsPaintedOnce);
        }

        public void ProblemTwo()
        {
            Dictionary<Vector2i, int> count = new Dictionary<Vector2i, int>();
            Dictionary<Vector2i, int> color = new Dictionary<Vector2i, int>();
            color[new Vector2i(0, 0)] = 1;

            int GeColor(Vector2i p)
            {
                int c;
                if (!color.TryGetValue(p, out c))
                {
                    c = 0; //black
                    color[p] = c;
                }

                return c;
            }

            void IncrementCount(Vector2i p)
            {
                if (!count.ContainsKey(p))
                {
                    count[p] = 0;
                }

                count[p]++;
            }

            Vector2i position = new Vector2i();
            Direction direction = Direction.Up;

            Computer computer = new Computer(Program);
            computer.PrintDisassembly = false;
            computer.PrintOutput = false;
            int cycle = 0;
            while (computer.State != State.Halt)
            {
                try
                {
                    RunComputerToInputHalt(computer);

                    int currentColor = GeColor(position);
                    computer.Input.Add(currentColor);

                    RunComputerToInputHalt(computer);
                    long newColor = computer.Output[0];
                    computer.Output.RemoveAt(0);

                    RunComputerToInputHalt(computer);
                    long rotate = computer.Output[0];
                    computer.Output.RemoveAt(0);

                    color[position] = (int)newColor;
                    IncrementCount(position);

                    //Move
                    if (rotate == 0)
                    {
                        direction = direction.RotateLeft();
                    }
                    else
                    {
                        direction = direction.RotateRight();
                    }

                    position = position.Add(Moves[direction]);
                    cycle++;
                    //Console.WriteLine(cycle);
                }
                catch
                {
                    if (computer.State == State.Halt)
                    {
                        break;
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            //var panelsPaintedOnce = count.Count(i => i.Value > 0);
            Render(color);
        }

        private void Render(Dictionary<Vector2i, int> colors)
        {
            var x = Console.CursorLeft;
            var y = Console.CursorTop;

            //Console.Clear();
            foreach (var c in colors)
            {
                Console.SetCursorPosition(c.Key.X + x, c.Key.Y + y);
                Console.BackgroundColor = c.Value == 0 ? ConsoleColor.Black : ConsoleColor.White;
                Console.Write(' ');
            }
            Console.WriteLine();
            Console.WriteLine();
        }



        private const string Program =
            @"3,8,1005,8,320,1106,0,11,0,0,0,104,1,104,0,3,8,1002,8,-1,10,101,1,10,10,4,10,1008,8,1,10,4,10,102,1,8,29,2,1005,1,10,1006,0,11,3,8,1002,8,-1,10,101,1,10,10,4,10,108,0,8,10,4,10,102,1,8,57,1,8,15,10,1006,0,79,1,6,3,10,3,8,102,-1,8,10,101,1,10,10,4,10,108,0,8,10,4,10,101,0,8,90,2,103,18,10,1006,0,3,2,105,14,10,3,8,102,-1,8,10,1001,10,1,10,4,10,108,0,8,10,4,10,101,0,8,123,2,9,2,10,3,8,102,-1,8,10,1001,10,1,10,4,10,1008,8,1,10,4,10,1001,8,0,150,1,2,2,10,2,1009,6,10,1,1006,12,10,1006,0,81,3,8,102,-1,8,10,1001,10,1,10,4,10,1008,8,1,10,4,10,102,1,8,187,3,8,102,-1,8,10,1001,10,1,10,4,10,1008,8,0,10,4,10,101,0,8,209,3,8,1002,8,-1,10,1001,10,1,10,4,10,1008,8,1,10,4,10,101,0,8,231,1,1008,11,10,1,1001,4,10,2,1104,18,10,3,8,102,-1,8,10,1001,10,1,10,4,10,108,1,8,10,4,10,1001,8,0,264,1,8,14,10,1006,0,36,3,8,1002,8,-1,10,1001,10,1,10,4,10,108,0,8,10,4,10,101,0,8,293,1006,0,80,1006,0,68,101,1,9,9,1007,9,960,10,1005,10,15,99,109,642,104,0,104,1,21102,1,846914232732,1,21102,1,337,0,1105,1,441,21102,1,387512115980,1,21101,348,0,0,1106,0,441,3,10,104,0,104,1,3,10,104,0,104,0,3,10,104,0,104,1,3,10,104,0,104,1,3,10,104,0,104,0,3,10,104,0,104,1,21102,209533824219,1,1,21102,1,395,0,1106,0,441,21101,0,21477985303,1,21102,406,1,0,1106,0,441,3,10,104,0,104,0,3,10,104,0,104,0,21101,868494234468,0,1,21101,429,0,0,1106,0,441,21102,838429471080,1,1,21102,1,440,0,1106,0,441,99,109,2,21201,-1,0,1,21101,0,40,2,21102,472,1,3,21101,0,462,0,1106,0,505,109,-2,2106,0,0,0,1,0,0,1,109,2,3,10,204,-1,1001,467,468,483,4,0,1001,467,1,467,108,4,467,10,1006,10,499,1102,1,0,467,109,-2,2106,0,0,0,109,4,2101,0,-1,504,1207,-3,0,10,1006,10,522,21101,0,0,-3,21202,-3,1,1,22101,0,-2,2,21102,1,1,3,21102,541,1,0,1106,0,546,109,-4,2105,1,0,109,5,1207,-3,1,10,1006,10,569,2207,-4,-2,10,1006,10,569,22102,1,-4,-4,1105,1,637,22102,1,-4,1,21201,-3,-1,2,21202,-2,2,3,21102,588,1,0,1105,1,546,22101,0,1,-4,21102,1,1,-1,2207,-4,-2,10,1006,10,607,21101,0,0,-1,22202,-2,-1,-2,2107,0,-3,10,1006,10,629,21201,-1,0,1,21102,629,1,0,105,1,504,21202,-2,-1,-2,22201,-4,-2,-4,109,-5,2105,1,0";
    }
}
