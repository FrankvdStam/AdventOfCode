using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01
{
    public class Point : IEquatable<Point>
    {
        public int X;
        public int Y;

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }
        
        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public bool Equals(Point other)
        {
            return other.X == X && other.Y == Y;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //ProblemOne(Input);
            ProblemTwo(Input);
        }

        static void ProblemOne(string input)
        {
            int x = 0;
            int y = 0;
            int currentDirection = 0;

            var lines = input.Split(new string[] { ", " }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                //Rotate
                if (line[0] == 'R')
                {
                    currentDirection++;
                    if (currentDirection >= 4)
                    {
                        currentDirection = 0;
                    }
                }
                else
                {
                    currentDirection--;
                    if (currentDirection <= -1)
                    {
                        currentDirection = 3;
                    }
                }

                int steps = int.Parse(line.Substring(1, line.Length - 1));

                //Move
                switch (currentDirection)
                {
                    //Up
                    case 0:
                        y += steps;
                        break;
                        //right
                    case 1:
                        x += steps;
                        break;
                    //down
                    case 2:
                        y -= steps;
                        break;
                    case 3:
                        x -= steps;
                        break;
                }
            }
            //Distance between x, y and 0.
            x = Math.Abs(x);
            y = Math.Abs(y);

            int distance = x + y;
        }
        

        static void ProblemTwo(string input)
        {
            List<Point> visitedLocations = new List<Point>();

            int x = 0;
            int y = 0;
            int prevX = 0;
            int prevY = 0;

            int currentDirection = 0;

            var lines = input.Split(new string[] { ", " }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                //Rotate
                if (line[0] == 'R')
                {
                    currentDirection++;
                    if (currentDirection >= 4)
                    {
                        currentDirection = 0;
                    }
                }
                else
                {
                    currentDirection--;
                    if (currentDirection <= -1)
                    {
                        currentDirection = 3;
                    }
                }

                int steps = int.Parse(line.Substring(1, line.Length - 1));
                for (int i = 0; i < steps; i++)
                {
                    switch (currentDirection)
                    {
                        //Up
                        case 0:
                            y += 1;
                            break;
                        //right
                        case 1:
                            x += 1;
                            break;
                        //down
                        case 2:
                            y -= 1;
                            break;
                        case 3:
                            x -= 1;
                            break;
                    }

                    Point p = new Point
                    {
                        X = x,
                        Y = y
                    };

                    if (visitedLocations.Contains(p))
                    {
                        int distance = Math.Abs(x) + Math.Abs(y);
                    }
                    visitedLocations.Add(p);
                }
            }
        }
        


        private static string Input = @"R2, L3, R2, R4, L2, L1, R2, R4, R1, L4, L5, R5, R5, R2, R2, R1, L2, L3, L2, L1, R3, L5, R187, R1, R4, L1, R5, L3, L4, R50, L4, R2, R70, L3, L2, R4, R3, R194, L3, L4, L4, L3, L4, R4, R5, L1, L5, L4, R1, L2, R4, L5, L3, R4, L5, L5, R5, R3, R5, L2, L4, R4, L1, R3, R1, L1, L2, R2, R2, L3, R3, R2, R5, R2, R5, L3, R2, L5, R1, R2, R2, L4, L5, L1, L4, R4, R3, R1, R2, L1, L2, R4, R5, L2, R3, L4, L5, L5, L4, R4, L2, R1, R1, L2, L3, L2, R2, L4, R3, R2, L1, L3, L2, L4, L4, R2, L3, L3, R2, L4, L3, R4, R3, L2, L1, L4, R4, R2, L4, L4, L5, L1, R2, L5, L2, L3, R2, L2";
        private static string Example = @"";
    }
}
