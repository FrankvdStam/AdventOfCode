using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Years.Utils;

namespace Years.Year2024
{
    public class Day06 : BaseDay
    {
        public Day06() : base(2024, 6) {}

        public override void ProblemOne()
        {
            var (map, position) = Parse(Input);
            var direction = Extensions.DirectionUp;
            var visited = new List<Vector2i>();
            visited.Add(position);
            while (!TakeStep(map, ref position, ref direction))
            {
                if (!visited.Contains(position))
                {
                    visited.Add(position);
                }
                //Console.WriteLine(position);
            }
            Console.WriteLine(visited.Count);
        }

        //1304 too low
        public override void ProblemTwo()
        {
            var (map, position) = Parse(Input);
            var result = 0;
            
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    Console.WriteLine($"{x},{y}");
                    var originalChar = map[x, y];
                    if (originalChar == '#')
                    {
                        continue;
                    }
                    else
                    {
                        map[x, y] = '#';
                    }

                    if (FindLoop(map, position))
                    {
                        result++;
                    }

                    //restore original char
                    map[x, y] = originalChar;
                }
            }
            Console.WriteLine(result);
        }

        private bool FindLoop(char[,] map, Vector2i position)
        {
            var direction = Extensions.DirectionUp;
            var visited = new List<(Vector2i position, Vector2i direction)>();
            visited.Add((position, direction));

            while (!TakeStep(map, ref position, ref direction))
            {
                if (visited.Any(i => i.position == position && i.direction == direction))
                {
                    return true;//found loop
                }
                visited.Add((position, direction));
            }
            return false; //left maze without looping
        }


        private bool TakeStep(char[,] map, ref Vector2i position, ref Vector2i direction)
        {
            var nextPosition = position + direction;
            if (!map.IsInBound(nextPosition))
            {
                return true; //Exit map
            }

            var c = map[nextPosition.Y, nextPosition.X];
            if (c == '#')
            {
                if (direction == Extensions.DirectionUp)
                {
                    direction = Extensions.DirectionRight;
                }
                else if (direction == Extensions.DirectionRight)
                {
                    direction = Extensions.DirectionDown;
                }
                else if (direction == Extensions.DirectionDown)
                {
                    direction = Extensions.DirectionLeft;
                }
                else if (direction == Extensions.DirectionLeft)
                {
                    direction = Extensions.DirectionUp;
                }

                return false;
            }
            else if(c == '.')
            {
                position = nextPosition;
                return false;
            }

            throw new ArgumentException();
        }

        private (char[,], Vector2i) Parse(string input)
        {
            var lines = input.SplitNewLine();
            var result = new char[lines.Length, lines[0].Length];
            var position = new Vector2i();
            for (int y = 0; y < result.GetLength(0); y++)
            {
                for (int x = 0; x < result.GetLength(1); x++)
                {
                    result[y, x] = lines[y][x];
                    if (result[y, x] == '^')
                    {
                        result[y, x] = '.';
                        position.X = x;
                        position.Y = y;
                    }
                }
            }
            return (result, position);
        }

        private readonly string _example = @"....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#...";
    }
}