﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day24
{
    public class Vector2i : IEquatable<Vector2i>
    {
        public Vector2i Copy()
        {
            return new Vector2i(X, Y);
        }

        public Vector2i() { }

        public Vector2i(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public override bool Equals(object other)
        {
            return other is Vector2i vector2i && vector2i.X == X && vector2i.Y == Y;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }

        public bool Equals(Vector2i vector2i)
        {
            return vector2i.X == X && vector2i.Y == Y;
        }

        public static bool operator ==(Vector2i left, Vector2i right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Vector2i left, Vector2i right)
        {
            return !left.Equals(right);
        }

        public static Vector2i operator +(Vector2i left, Vector2i right)
        {
            return new Vector2i(left.X + right.X, left.Y + right.Y);
        }

        public int DistanceTo(Vector2i other)
        {
            return (int)Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
        }
    }
    

    class Program
    {
        static void Main(string[] args)
        {
            Maze2 m = new Maze2(Input);


            //ParseInput(Input, out List<Vector2i> walls, out List<Vector2i> points, out Vector2i startPosition);
            //ProblemOne(walls, points, startPosition);
            //ProblemTwo();
        }

        static void ProblemOne(List<Vector2i> walls, List<Vector2i> points, Vector2i startPosition)
        {
            Queue<List<Vector2i>> queue = new Queue<List<Vector2i>>();
            queue.Enqueue(new List<Vector2i>(){ startPosition });

            int steps = 0;

            while (queue.Any())
            {
                var states = queue.ToList();
                queue.Clear();

                Console.WriteLine($"{steps} : {states.Count}");

                foreach (var s in states)
                {
                    var possibleMoves = GetPossibleSteps(s.Last(), walls);
                    foreach (var vec in possibleMoves)
                    {
                        var newState = new List<Vector2i>(s);

                        //Skip this state if we are turning back, but allow turning back when we just reached a point.
                        if (!points.Contains(vec) && newState.Count > 2 && newState[newState.Count - 2] == vec)
                        {
                            continue;
                        }
                        
                        if (!points.Except(newState).Any())
                        {
                            //win?   
                        }

                        newState.Add(vec);
                        queue.Enqueue(newState);
                    }
                }
                steps++;
            }
        }

        static void ProblemTwo()
        {

        }

        static int[] directions =
        {
            -1,  0,
             1,  0,
             0,  1,
             0, -1,
        };

        static List<Vector2i> GetPossibleSteps(Vector2i position, List<Vector2i> walls)
        {
            List<Vector2i> steps = new List<Vector2i>();

            for (int i = 0; i + 1 < directions.Length; i += 2)
            {
                Vector2i potentialStep = new Vector2i(position.X + directions[i], position.Y + directions[i+1]);
                if (!walls.Contains(potentialStep))
                {
                    steps.Add(potentialStep);
                }
            }

            return steps;
        }


        static void ParseInput(string input, out List<Vector2i> walls, out List<Vector2i> points, out Vector2i startPosition)
        {
            walls = new List<Vector2i>();
            points = new List<Vector2i>();
            startPosition = new Vector2i();
            bool foundStart = false;

            var lines = input.Split(new string[] {"\r\n"}, StringSplitOptions.None);
            int y = 0;
            foreach (var line in lines)
            {
                for (int x = 0; x < line.Length; x++)
                {
                    switch (line[x])
                    {
                        case '#':
                            walls.Add(new Vector2i(x, y));
                            break;
                        case '.':
                            //Make sure we don't trigger the default route
                            break;
                        default:
                            var num = int.Parse(line[x].ToString());
                            if (num == 0)
                            {
                                startPosition = new Vector2i(x, y);
                                foundStart = true;
                            }
                            else
                            {
                                points.Add(new Vector2i(x, y));
                            }
                            break;
                    }
                }
                y++;
            }
            Debug.Assert(foundStart);
        }



        private static string Example = @"###########
#0.1.....2#
#.#######.#
#4.......3#
###########";

        private static string Input = @"#######################################################################################################################################################################################
#...........#.....#...........#.#.......#.....#.#...............#.....#.....#.......#.......#.......#.....................#.........#.....#...#3......#...#.#.............#.......#...#
#####.#.#.###.###.#####.#.#####.#.###.###.###.#.#.#.#.#.#.###.#.###.###.###.#.#######.#.#.#.###.###.#.#.#.#####.#.#.#####.###.#.#######.#############.#.#.#.#.#.#.###.#.#.#.#.###.#.#.#
#...#.#.#.#.....#.#...#...#.....#.....#.#...#.........#.....................#.....#...#...#.......#.....#.#.........#.#.#.#.#.......................#...#...#.#.#.#.....#.#.........#.#
#.#.#.#.#.#####.#.#.#.#.#.#.#.#.###.#.#.###.#.#.#.#.#.#########.#.###.###.#.#.###.###.#.#.#.#.###.#.#.#.#.#.#.#####.#.#.###.###.#######.#.#.#.###.#.###.#.#.#####.#####.#.#.###.#.#.#.#
#.#.#...#.....#.#.....#...#.#...#.#...#.......#...........#...#...#...#.#.....#.#...#...........#.#.......#...#...#.#.........#...#.......#.#...#.....#...#.......#.#.#.............#.#
#.#######.#.#.###.#.#.#####.#.#.#.#.###.#####.#.#.###.#.#.#.#.#.#.#.#.#.#.###.#.#.#.###.#######.#######.#.#.###.#.#.#.#.#.#.###.#.#.#.#.#.###.#.#####.#.#.#.#.#.#.#.#.#.###.#.#####.#.#
#.#...#.....#.......#.......#.#.......#...#.#.....#.#.#.#.#.#.......#...#.....#.#.#.#.#.........#...#...#...#.......#...#...#.....#.....#...#.........#...#.#...#...#...#.........#.#.#
#.#.#.#.###.###.#.#.#.###.###.#.#.###.#.#.#.#####.#.###.#.#####.#####.###.#.#.#.#.###.#.#.#.###.###.#.#.#.#.#.#####.#.###.#.#.###.#.#.#.#.###.###.#######.#.###.###.#.#######.#.#.#.#.#
#.#...#...#...............#...#...#...#.#.........#.#...#...#.#...#.#.....#...#.#...#.#.#...#.#.#.#.#.....#...#...#.......#.#...#.........#.#.....#.#...#.....#.#...#.#2#.....#.......#
#.#.#####.#########.#.###.#.#####.#.#.#.#######.#.###.#.#.#.#.###.#.#.###.#.###.#.#.#.#.#.###.###.#.#.#######.#.###.#.#.#.#.#.#.#.#####.###.#.###.#.#####.#.#.###.#.#.#.###.#.###.###.#
#...#...........#.....#.......#...#0..........#...#.....#.#...#.#...#.#.#...#.#.......#.........#...#.#...#.#.#.......#...#.#.........#.#.....................#.#.#.#.....#.#.....#...#
#.###.#.#.#.#.#.###.#.#.#.#.#.###.#.#.###.#.#.#.###.###.#.#####.#.#.#.#.###.#.#.###.#.#.###.###.#.#####.#.#.#.###.#.#######.#######.#.#.#.###.#.#.#.#.#.#######.#.#.#####.#.###.###.#.#
#...#...#...#.#...........#...#.#.....#...#...#.....#.#.............#...#.........#...#.....#...#.......#...#.#.............#.........#...#.#.....#.#.........#.............#...#.....#
#.#.#.###.#.###.#.#.#.###.#.###.#######.#.#.###.#.#.#.#.###.#.###.#.#.#.#.#####.#.#.#.#####.#.#####.#.#.#.#.###.###.#.###.###.###.#.#.#.###.#.#.###.#.#.###.#########.#####.#.#.#.#####
#.#...#.#.......#...#.....#.....#...#...#.....#...#...#.........#.....#.#.........#.....#.....#.........#...#.#.....#.#...#...#.#.#...#.#...#.........#.#...#.........#.#.......#.....#
#.#####.#.###.#.#.#.###.#.#.#.#.#.#.###.#.#.#.###.###.#.###.#.###.#.#.#.###.#.#####.###.#.#.#.#.#.#####.#.#.#.#.#.#.###.#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.###.#.#.#.#.#.#.#.#.###.#.#.#
#.....#...#...........#...#...........#.#.....#...#.....#...#.#.#.#...#.....#...#...#...............#...#.........#...#.....#...#.#...#.#...#.....#.....#.....#...#.#...#...#.#.......#
###.###.#.#.#####.###.#####.#.###.#.#.#####.#.###.#.###.#.###.#.#.#.#.###.#.###.#.#.#.#.###.#.###.#.#.#.#.#.#.###.#.#.#.###.###.#.#.###.#########.#.#####.###########.#.###.###.#####.#
#...#1#...#...#.......#...#...#.#...#...#...........#.#.......#.......#.#.........#.#...#...#...............#...#.....#.#...#.#.....#.........#...#.....#.#...#...#.....#.#.#.#...#...#
#.###.###.#.#.#.#####.#.#.#.#.#.#.#####.#####.#.#####.#.#.#.#.###.#.#.#.###.#.###.#.###.#.#.#####.#.#.#####.#.#.###.#.###.#.#.#.#.#.#.#####.#.#.#.#.###.#.#.###.#.#.#.#.#.#.#.###.#.#.#
#.....#.#.#.#.........#.#.#.....#.....#...#.....#...#.............#...#...#.....#...#.......#.......#.#.....#...#...#.#...#.....#.#...#.....#.#.#.#.#.#.#.......#...........#.#5#.....#
#.###.#.#.###.###.###.#.#####.#.#.###.###.#.###.#############.#######.#.###.#.###.#.#####.#.#.#.#####.#.#.#####.#.#.#.#.#.#######.#.#.###########.#.#.#.#.#.#.#.#.#.###.#.#.#.#.#.###.#
#.....#...#...................#.......#...#...#.#.....#.............#.....#...#.....#.......#.#.#.........#.........#.......#.......#...#.........#.#...#...#.....#.#.....#...#...#.#.#
#.###.###.###########.###.#####.#####.###.#.#.#.#.#.#.###.#.###.#.#######.#.#.#.#.#.#.###.#.#.#####.#.#.#.#######.#.###.#.#.###.#.#.#.###.#####.#####.#.#.#.#.###.#.###.###########.#.#
#.#.#.#...#...#.....#.....#.#.......#.....#.....#...........#.#...#.........#.#.#.#.....#.#.#...........#...#...#...#.......#...#.........#.....#.....#.....#.......#...#...#.#...#...#
#.#.#.#.#.#########.#.#.###.#.#.###.###.#.#.#.###.#####.#.#.#.#.#.#.#######.#.#.#######.#.###.#.###.###.#.###.#.#.#.#######.#######.#.#.#.#####.#.###.#.#####.#.###.#.#.###.#.#######.#
#.#...#...#.#...................#.#...#.#...#.......#.#...#.#...#.#.#.....#.#...#...#...#.#.............#.....#...........#.........#...#.#.#.....#.......#.......#.....#...#.#...#...#
###.#.#.#.#.#.#.###.#.###.#####.#.#####.#.#.###.#.#.#.#####.#.###.#####.#.#.#.#.#.#.#.###.#.#.#####.###.#.###.#####.#.#.#.#####.###.#.#.#.#.#.#######.###.#.#.#########.###.#.#######.#
#.....#...................#...#.#...#...#...........#.#.......#.#.....#.#.....#...........#.....#.........#.#...........#...#.....#...#.....#.............#.#.#.........#.#.#.#.......#
#.#.#.#.#.###.#.#.#.#.#########.###.#.#.#.#########.#.#.#.#.#.#.###########.#.#.###.###.###.#.#.#.#.#.###.#.###.#.###.#.###.###.#.###.#.#.#####.#.#.#######.#.#.###.###.#.#.#.###.#####
#...#.#.#....7#.#...#.#.........#.....#...#.#.......#.....#...........#.#...#.......#.#...#.#...#...........#...#.#...#.....#...#.#.#...........#...#.#...#...#4....#...#...#...#.....#
###.#.#.###.#####.#.#.#.#.#.#.#.#.#.#.###.#.###.#.###.#######.#.#.###.#.#####.#.#####.###.#.###.#.#.#.#.#####.###.#.###.#.#.#.#.###.#.#.###.#.#.###.#.###.#########.#.###.#.#.#.#.#.#.#
#.....#.#.#.......#.#.#.......#.#.#.............#...#.........#.......#.#.#...........#.....#.....#...#...#.#.....#...#.....#.#.#...#...#...#.....#...#...#.#...#...#.#.#.....#...#.#.#
#.###.#.#.#########.#.#.#.#######.#.#.#.#.###.###.#####.#.#.#.#.#.###.#.#.#.#.#####.#.#.###.#.#.#.#.###.###.###.#.#.#######.#.#.###.#####.#.#.###.#.#.#####.###.#.#.###.#.#########.###
#.......#...#...#...#...#.#...#...#.#.#...#.#...........#.........#...#.#.#.#.....#...#.....#.#.....#...#...#...#.#.#.#...#...#.#...#.....#...........#.#.........#.....#.#.#...#.....#
#####.###.#####.#.#####.#######.#.#.#.###.#.#######.###.#####.###.#.#.#.#.#.#####.#####.###.###.###.#.#.###.#.#.#.###.#.#.###.#.#.#####.#.###.#.#.#.###.#.#.#.#.#.###.###.#.#.###.#.#.#
#.....#.#.#.......#...#.#.#.....#.........#...#.....#6......#...#.#...........#.......#.............#...#...#...#...#.#...#...#...#.....#.........#.#...#.....#.#.#.......#...#.#.#...#
#######################################################################################################################################################################################";

    }
}
