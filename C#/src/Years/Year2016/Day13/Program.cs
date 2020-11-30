using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Day13
{
    public class Vector2i : IEquatable<Vector2i>
    {
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
    }

    //public class Branch
    //{
    //    //Store previous steps so we never go back
    //    public List<Vector2i> Steps = new List<Vector2i>();
    //}

    class Program
    {
        static void Main(string[] args)
        {
            //ProblemOne(10, 10, 7, 7, 4);
            ProblemTwo(1350, 100, 100, 31, 39);

            //ProblemTwo();
        }


        static void ProblemTwo(int magicNumber, int width, int height, int targetX, int targetY)
        {
            List<Vector2i> uniqueLocations = new List<Vector2i>()
            {
                new Vector2i(1, 1)
            };

            var maze = GenerateMaze(magicNumber, width, height);
            PrintMaze(maze);

            //Setup the queue at the starting point
            Queue<List<Vector2i>> queue = new Queue<List<Vector2i>>();
            List<Vector2i> start = new List<Vector2i>()
            {
                new Vector2i(1, 1)
            };
            queue.Enqueue(start);
            int counter = 50;
            while (counter > 0 && queue.Any())
            {
                var dequeuedList = new List<List<Vector2i>>();
                //Empty entire queue to handle the whole step at once
                while (queue.Any())
                {
                    dequeuedList.Add(queue.Dequeue());
                }

                foreach (var steps in dequeuedList)
                {
                    //var steps = queue.Dequeue();
                    var position = steps.Last();
                    var possibleSteps = GetPosibleSteps(position, maze);

                    foreach (var step in possibleSteps)
                    {
                        if (!uniqueLocations.Contains(step))
                        {
                            uniqueLocations.Add(step);
                        }

                        if (!steps.Contains(step))
                        {
                            //Deep copy
                            var newSteps = new List<Vector2i>(steps);
                            newSteps.Add(step);

                            //PrintMaze(maze, newSteps);
                            //Console.SetCursorPosition(targetX, targetY);
                            //Console.Write('X');
                            

                            queue.Enqueue(newSteps);

                            //Thread.Sleep(50);
                        }
                    }
                }
                counter--;
            }

            int result = uniqueLocations.Count;
        }

        static void ProblemOne(int magicNumber, int width, int height, int targetX, int targetY)
        {
            var maze = GenerateMaze(magicNumber, width, height);
            PrintMaze(maze);

            //Setup the queue at the starting point
            Queue<List<Vector2i>> queue = new Queue<List<Vector2i>>();
            List<Vector2i> start = new List<Vector2i>()
            {
                new Vector2i(1, 1)
            };
            queue.Enqueue(start);

            while (queue.Any())
            {
                var steps = queue.Dequeue();
                var position = steps.Last();
                var possibleSteps = GetPosibleSteps(position, maze);

                foreach (var step in possibleSteps)
                {
                    if (!steps.Contains(step))
                    {
                        //Deep copy
                        var newSteps = new List<Vector2i>(steps);
                        newSteps.Add(step);

                        //PrintMaze(maze, newSteps);
                        //Console.SetCursorPosition(targetX, targetY);
                        //Console.Write('X');

                        if (step.X == targetX && step.Y == targetY)
                        {
                            //Done! -1 because the starting step doesn't count.
                            int result = newSteps.Count-1;
                            PrintMaze(maze, newSteps);
                        }

                        queue.Enqueue(newSteps);

                        //Thread.Sleep(50);
                    }
                }
            }
        }

        static List<Vector2i> GetPosibleSteps(Vector2i position, bool[,] maze)
        {
            var steps = new List<Vector2i>();

            int[] coords =
            {
                -1, 0,
                 1, 0,
                 0, 1,
                 0, -1,
            };

            int width = maze.GetLength(0);
            int height = maze.GetLength(1);

            for (int i = 0; i < coords.Length; i += 2)
            {
                int x = position.X + coords[i];
                int y = position.Y + coords[i + 1];

                //Check if coords are in bounds
                if (x >= 0 && x < width && y >= 0 && y < height && maze[x, y])
                {
                    steps.Add(new Vector2i(x, y));
                }
            }

            return steps;
        }

        static void ProblemTwo()
        {

        }

        #region Maze ========================================================================================================
        static void PrintMaze(bool[,] maze, List<Vector2i> steps = null)
        {
            Console.Clear();
            for (int x = 0; x < maze.GetLength(0); x++)
            {
                for (int y = 0; y < maze.GetLength(1); y++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(maze[x, y] ? '.' : '#');
                }
            }

            if (steps != null)
            {
                foreach (var step in steps)
                {
                    Console.SetCursorPosition(step.X, step.Y);
                    Console.Write('O');
                }
            }
        }


        static bool[,] GenerateMaze(int magicNumber, int width, int height)
        {
            var maze = new bool[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    maze[x, y] = IsEmpty(magicNumber, x, y);
                }
            }

            return maze;
        }

        static bool IsEmpty(int magicNumber, int x, int y)
        {
            int num = x * x + 3 * x + 2 * x * y + y + y * y + magicNumber;
            return HasEvenBitParity(num);
        }


        static bool HasEvenBitParity(int number)
        {
            int count = 0;
            while (number > 0)
            {
                count += number & 1;
                number >>= 1;
            }
            return count % 2 == 0;
        }
        #endregion
    }
}
