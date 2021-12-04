using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2021
{
    public class Day02 : IDay
    {
        public int Day => 2;
        public int Year => 2021;

        public void ProblemOne()
        {
            var input = Parse(Input);
            int y = 0;
            int x = 0;

            foreach(var step in input)
            {
                switch (step.direction)
                {
                    case Direction.Right:
                        x += step.distance;
                        break;

                    case Direction.Up:
                        y -= step.distance;
                        break;

                    case Direction.Down:
                        y += step.distance;
                        break;
                }
            }

            var result = y * x;
            Console.WriteLine(result);
        }

        public void ProblemTwo()
        {
            var input = Parse(Input);
            int y = 0;
            int x = 0;
            int aim = 0;

            foreach (var step in input)
            {
                switch (step.direction)
                {
                    case Direction.Right:
                        x += step.distance;
                        y += aim * step.distance;
                        break;

                    case Direction.Up:
                        aim -= step.distance;
                        //y -= step.distance;
                        break;

                    case Direction.Down:
                        aim += step.distance;
                        //y += step.distance;
                        break;
                }
            }

            var result = y * x;
            Console.WriteLine(result);
        }

        private List<(Direction direction, int distance)> Parse(string input)
        {
            var result = new List<(Direction, int)>();
            var lines = input.SplitNewLine();
            foreach (var l in lines)
            {
                var bits = l.Split(' ');
                Direction d = Direction.Down;
                switch (bits[0])
                {
                    default:
                        throw new Exception();

                    case "forward":
                        d = Direction.Right;
                        break;

                    case "down":
                        d = Direction.Down;
                        break;

                    case "up":
                        d = Direction.Up;
                        break;
                }

                int num = int.Parse(bits[1]);
                result.Add((d, num));
            }

            return result;
        }

        private const string Example = @"forward 5
down 5
forward 8
up 3
down 8
forward 2";

        private const string Input = @"forward 6
forward 9
down 9
down 7
forward 8
down 4
forward 7
forward 3
up 5
down 7
down 3
down 2
down 1
down 2
down 1
forward 5
down 9
up 7
forward 6
forward 3
down 2
forward 2
down 4
down 3
forward 9
up 4
down 7
forward 5
down 4
down 4
forward 8
up 6
forward 4
forward 6
down 8
forward 6
up 9
down 6
down 8
forward 7
down 8
up 1
forward 5
up 8
forward 9
down 7
down 9
up 3
forward 9
forward 7
down 1
down 5
forward 6
up 3
down 2
forward 8
forward 5
forward 9
down 5
down 5
forward 7
forward 9
forward 8
forward 9
up 4
up 8
forward 2
up 6
forward 2
down 8
forward 4
forward 6
down 1
forward 3
down 7
down 8
forward 9
forward 5
down 8
forward 1
forward 8
down 8
up 1
down 9
forward 6
down 1
down 8
up 6
up 8
down 7
forward 8
down 3
forward 2
up 2
forward 1
forward 7
down 5
down 7
forward 5
down 7
forward 5
forward 4
up 8
down 5
forward 8
forward 6
up 4
up 1
down 1
down 8
down 1
up 1
down 4
down 4
up 3
down 6
forward 7
up 8
up 1
forward 7
forward 6
down 9
forward 3
down 9
down 5
forward 5
forward 1
forward 5
down 4
forward 4
up 5
down 8
down 2
forward 2
down 8
up 4
down 3
forward 6
up 7
forward 2
up 5
up 9
forward 6
down 1
down 1
up 4
down 7
up 2
forward 1
down 5
down 4
down 5
forward 4
down 3
up 8
forward 7
down 2
up 6
forward 4
forward 8
forward 8
up 7
down 2
down 7
down 9
forward 8
down 9
down 5
forward 8
forward 6
up 4
up 7
down 7
forward 1
up 2
forward 1
down 6
down 1
forward 9
forward 8
up 4
forward 9
up 7
forward 8
forward 1
down 6
up 3
forward 2
down 6
up 3
up 5
forward 6
up 5
down 9
forward 4
up 5
down 3
forward 5
forward 2
up 6
up 1
up 4
forward 9
down 5
up 2
forward 9
up 5
down 6
up 9
down 6
down 7
forward 6
forward 3
down 2
forward 3
down 1
down 9
down 2
down 2
forward 8
up 9
forward 4
forward 6
forward 5
forward 9
forward 4
up 6
down 8
down 8
down 3
forward 1
forward 7
up 3
forward 2
down 9
up 7
forward 5
down 1
forward 9
up 8
forward 7
forward 1
down 7
forward 8
down 7
down 6
forward 5
forward 7
up 9
up 4
forward 2
down 1
forward 3
down 5
forward 2
forward 2
forward 6
up 1
up 9
down 1
down 7
up 6
down 6
down 7
forward 3
forward 7
forward 9
forward 9
down 6
down 8
forward 9
up 5
forward 5
down 6
down 4
forward 1
up 9
forward 6
up 4
up 7
forward 5
down 7
down 6
forward 2
down 4
forward 2
down 1
forward 8
down 3
down 8
down 7
down 6
forward 3
forward 1
down 8
up 4
down 8
forward 7
down 2
forward 3
forward 2
forward 4
forward 2
forward 8
forward 2
up 1
down 6
down 9
down 3
forward 8
down 3
forward 8
forward 3
forward 5
down 6
forward 6
forward 2
down 7
forward 8
down 1
forward 7
forward 6
up 9
forward 2
up 4
up 6
forward 4
forward 5
forward 6
forward 7
down 4
forward 9
forward 1
down 8
down 5
down 9
up 5
forward 9
forward 6
forward 7
down 5
forward 9
down 4
down 8
forward 9
down 5
down 5
down 6
forward 8
forward 9
down 5
down 7
forward 8
forward 5
forward 9
up 4
down 3
down 3
forward 5
down 7
down 3
up 7
forward 6
up 7
down 9
forward 6
forward 8
up 8
down 9
down 6
up 3
forward 6
up 3
down 4
down 6
forward 5
down 5
forward 1
down 5
forward 2
up 3
up 9
down 9
up 1
down 3
down 4
forward 5
up 4
down 2
forward 1
forward 7
up 9
up 9
down 2
down 8
forward 5
down 4
up 2
forward 9
down 3
up 6
down 3
forward 1
down 7
down 7
down 7
forward 1
forward 6
down 5
up 7
forward 2
up 6
down 5
up 3
down 5
up 1
down 9
up 2
down 7
up 5
down 6
up 1
forward 5
down 2
down 1
up 2
forward 5
forward 8
up 7
up 5
forward 9
forward 9
forward 5
forward 8
down 4
forward 8
down 1
up 3
down 1
down 9
up 5
down 7
down 8
down 4
down 3
forward 6
down 5
down 7
forward 6
down 3
down 1
down 5
forward 4
up 8
down 6
forward 1
forward 1
down 4
down 8
forward 9
forward 3
forward 6
down 5
down 7
down 4
up 1
forward 2
forward 4
forward 8
up 2
up 9
up 3
down 8
forward 4
down 7
forward 7
down 5
up 3
up 6
down 5
forward 2
down 8
up 1
down 4
down 4
forward 1
down 6
down 1
up 2
forward 2
down 8
down 7
forward 5
forward 2
up 4
forward 6
down 4
down 6
down 2
up 5
down 5
forward 7
forward 2
down 2
down 7
forward 1
forward 1
forward 4
down 3
forward 3
up 4
up 6
down 3
forward 4
down 5
down 6
down 1
forward 3
up 3
down 5
down 5
down 5
forward 5
forward 5
forward 2
up 3
down 6
down 6
down 2
down 5
forward 3
forward 9
down 9
forward 6
up 4
down 8
up 7
forward 1
forward 4
forward 9
down 6
up 1
up 1
up 4
up 6
forward 7
down 5
forward 8
forward 7
up 1
up 6
down 7
down 8
down 4
down 8
up 8
down 7
up 1
forward 9
forward 6
forward 2
forward 7
down 9
down 9
down 7
forward 4
forward 8
forward 8
up 3
down 7
down 7
down 3
forward 5
forward 3
down 3
down 6
forward 2
down 6
up 7
forward 2
up 5
down 7
forward 4
down 7
forward 6
up 6
up 6
forward 6
forward 3
up 1
forward 2
forward 6
up 1
down 9
forward 8
forward 6
down 8
down 3
up 5
forward 7
forward 2
up 4
up 3
forward 4
down 3
down 5
down 5
down 2
down 7
up 2
down 2
up 7
down 1
forward 6
forward 5
forward 4
down 6
down 1
down 5
down 6
down 3
down 9
down 2
down 7
forward 7
up 7
down 3
down 3
forward 7
up 5
down 9
forward 9
down 2
up 4
up 9
up 9
down 1
up 6
down 1
forward 7
down 6
forward 9
down 9
down 4
down 1
forward 6
down 2
forward 6
up 8
forward 1
up 1
up 5
down 6
down 8
down 8
forward 5
forward 4
forward 3
up 8
up 9
down 4
forward 4
forward 4
down 9
forward 9
down 7
forward 7
up 4
forward 2
down 8
forward 1
down 7
forward 5
forward 1
down 8
up 1
up 1
down 2
down 4
down 7
down 3
forward 7
down 3
up 3
forward 8
up 6
forward 7
forward 6
down 1
down 8
forward 7
up 1
forward 2
down 6
down 1
forward 4
forward 2
down 9
up 8
down 9
down 2
down 3
down 5
down 3
down 8
down 1
down 1
up 5
forward 5
down 2
down 5
down 5
up 4
up 5
up 6
up 7
down 5
forward 4
up 6
up 3
down 5
forward 9
down 5
down 3
down 8
forward 6
forward 4
forward 4
forward 4
forward 9
forward 3
up 3
forward 7
down 6
down 7
down 9
down 5
up 3
forward 9
forward 7
forward 2
down 4
up 4
down 7
down 9
down 6
down 8
forward 4
forward 9
down 4
forward 9
forward 3
forward 9
down 8
forward 6
down 6
up 8
forward 9
forward 5
forward 6
down 5
up 1
forward 2
up 5
up 8
down 7
down 9
forward 6
forward 7
forward 2
down 8
down 5
forward 5
down 6
forward 2
forward 7
down 6
up 5
down 4
down 7
up 9
up 4
down 1
forward 4
up 6
forward 2
forward 8
forward 9
down 5
forward 1
forward 1
down 6
up 9
forward 7
down 9
forward 4
forward 2
forward 6
forward 6
forward 7
up 6
down 8
up 8
down 7
forward 6
up 2
forward 6
down 1
down 5
down 6
up 8
forward 5
up 5
forward 1
down 9
down 1
up 4
down 2
up 8
up 2
down 3
up 4
down 3
up 5
down 4
down 2
up 9
forward 2
forward 8
down 5
down 3
forward 4
forward 3
forward 3
down 6
down 8
forward 6
down 1
forward 6
down 8
down 7
down 7
forward 6
forward 5
up 8
forward 7
up 6
down 7
forward 7
down 8
down 8
forward 7
up 4
forward 4
forward 1
up 1
down 5
up 6
up 6
up 7
down 1
down 2
up 3
down 3
forward 6
down 7
down 9
down 1
forward 2
forward 6
down 9
up 2
forward 6
up 7
forward 5
down 6
down 5
forward 2
down 9
forward 8
up 7
down 9
down 7
forward 8
forward 7
down 6
down 7
forward 1
down 9
up 6
up 8
down 4
down 2
down 4
down 2
down 5
forward 9
down 3
forward 3
forward 1
down 2
down 4
down 1
up 3
up 7
down 5
down 8
forward 6
up 6
down 2
down 3
forward 3
up 4
up 3
down 6
down 3
forward 4
forward 7
forward 3
up 7
forward 3
down 8
forward 7
down 3
forward 8
forward 2
forward 4
down 5
forward 3
down 8
up 2
forward 5
up 7
forward 2
down 2
down 5
up 1
up 7
down 4
up 9
down 4
forward 3
forward 1
up 9
forward 1
forward 3
forward 1
forward 5
forward 2
forward 6
up 6
down 1
forward 1
forward 6
forward 6
forward 5
down 6
forward 8
down 4
forward 3
forward 4
forward 1
forward 9
down 4
forward 5
down 2
down 3
forward 7
forward 2
forward 1
forward 5
down 9
up 3
up 7
forward 3
forward 2
down 2
up 7
forward 9
up 4
up 2
up 1
down 5
down 4
forward 2
down 6
down 5
down 6
forward 6
down 9
down 1
forward 2
forward 7
forward 4
down 5
down 6
down 2
forward 7
down 3
forward 5";
    }
}