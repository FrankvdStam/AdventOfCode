using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2021
{
    public class Day17 : IDay
    {
        public int Day => 17;
        public int Year => 2021;

        public void ProblemOne()
        {
            var target = ParseInput(Input);
            var maxY = 0;

            for (var y = 0; y < 250; y++)
            {
                for (var x = 0; x < 250; x++)
                {
                    if (Simulate(new Vector2i(x, y), target, out int simMaxY))
                    {
                        if (simMaxY > maxY)
                        {
                            maxY = simMaxY;
                        }
                    }
                }
            }
            Console.WriteLine(maxY);
        }

        public void ProblemTwo()
        {
            var target = ParseInput(Input);
            var count = 0;

            for (var y = -250; y < 250; y++)
            {
                for (var x = -250; x < 250; x++)
                {
                    if (Simulate(new Vector2i(x, y), target, out int simMaxY))
                    {
                        count++;
                    }
                }
            }
            Console.WriteLine(count);
        }


        private bool Simulate(Vector2i velocity, (Vector2i first, Vector2i second) target, out int maxY)
        {
            var position = new Vector2i(0, 0);
            var steps = 0;
            maxY = 0;

            for (;;)
            {
                position = position.Add(velocity);
                if (position.Y > maxY)
                {
                    maxY = position.Y;
                }

                //Due to drag, the probe's x velocity changes by 1 toward the value 0; that is, it decreases by 1 if it is greater than 0, increases by 1 if it is less than 0, or does not change if it is already 0.
                if (velocity.X > 0)
                {
                    velocity.X -= 1;
                }

                if (velocity.X < 0)
                {
                    velocity.X += 1;
                }

                velocity.Y -= 1;

                //Check if target has been hit
                if (position.X >= target.first.X &&
                    position.X <= target.second.X &&
                    position.Y >= target.first.Y &&
                    position.Y <= target.second.Y)
                {
                    //hit!
                    return true;
                }

                steps++;
                if (steps > 200)
                {
                    return false;
                }
            }
        }


        public (Vector2i first, Vector2i second) ParseInput(string input)
        {
            var split = input.Split(' ');
            var x = split[2].Trim(',').TrimStart('x', '=');
            var y = split[3].TrimStart('y', '='); ;

            var splitX = x.Split("..");
            var splitY = y.Split("..");

            var first  = new Vector2i(int.Parse(splitX[0]), int.Parse(splitY[0]));
            var second = new Vector2i(int.Parse(splitX[1]), int.Parse(splitY[1]));

            return (first, second);
        }


        private const string Example = @"target area: x=20..30, y=-10..-5";
        private const string Input = @"target area: x=209..238, y=-86..-59";
    }
}