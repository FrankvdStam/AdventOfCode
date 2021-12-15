using System;
using System.Collections.Generic;
using System.Linq;
using Years.Utils;

namespace Years.Year2016
{
    public enum MazePart
    {
        Empty,
        Wall,
        Digit,
    }

    public class Maze
    {
        public Maze(string input)
        {
            var lines = input.SplitNewLine();
            _mazeWidth = lines[0].Length;
            _mazeHeight = lines.Length;

            _maze = new MazePart[_mazeWidth, _mazeHeight];

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    char c = lines[y][x];
                    if (char.IsDigit(c))
                    {
                        Points.Add((new Vector2i(x, y), int.Parse(c.ToString())));
                        _maze[x, y] = MazePart.Digit;
                    }
                    else if (c == '#')
                    {
                        _maze[x, y] = MazePart.Wall;
                    }
                    else
                    {
                        _maze[x, y] = MazePart.Empty;
                    }
                }
            }
        }

        public List<(Vector2i Position, int Digit)> Points = new List<(Vector2i, int)>();
        private MazePart[,] _maze;
        private int _mazeWidth;
        private int _mazeHeight;

        public MazePart GetMazePart(int x, int y)
        {
            return _maze[x, y];
        }

        /// <summary>
        /// Find all possible moves from a given position
        /// </summary>
        public List<Vector2i> GetMoves(Vector2i position)
        {
            if (_maze[position.X, position.Y] == MazePart.Wall)
            {
                return new List<Vector2i>(0);
            }

            var result = new List<Vector2i>();

            if (position.X + 1 < _mazeWidth && _maze[position.X + 1, position.Y] != MazePart.Wall)
            {
                result.Add(new Vector2i(position.X + 1, position.Y));
            }

            if (position.X - 1 >= 0 && _maze[position.X - 1, position.Y] != MazePart.Wall)
            {
                result.Add(new Vector2i(position.X - 1, position.Y));
            }

            if (position.Y + 1 < _mazeWidth && _maze[position.X, position.Y + 1] != MazePart.Wall)
            {
                result.Add(new Vector2i(position.X, position.Y + 1));
            }

            if (position.Y - 1 >= 0 && _maze[position.X, position.Y - 1] != MazePart.Wall)
            {
                result.Add(new Vector2i(position.X, position.Y - 1));
            }
            return result;
        }
    }




    public class Day24 : IDay
    {
        public int Day => 24;
        public int Year => 2016;

        
        public void ProblemOne()
        {
            var maze = new Maze(Input);
            var graph = ParseGraph(maze);

            int min = int.MaxValue;
            var route = new List<Graph<int>>();
            //1 7 0 6 3 2 4 5
            var permutedGraph = graph.Permute();
            foreach (var permutation in permutedGraph)
            {
                try
                {
                    //Start position has to be 0
                    var list = permutation.ToList();
                    if (list[0].Object != 0)
                    {
                        continue;
                    }



                    int distance = 0;
                    for (int i = 0; i < graph.Count - 1; i++)
                    {
                        //Find the distance between the 2 current nodes, add it to current total
                        var firstNode = list[i];
                        var secondNode = list[i + 1];
                        //Can throw an exception when the order of nodes is invalid. Instead of going through all possible permutations, should optimize to only relevant permutations
                        distance += firstNode.Nodes.Single(i => i.Node == secondNode).Distance;
                    }

                    if (distance < min)
                    {
                        min = distance;
                        route = list;
                    }
                }
                catch { }
            }
            Console.WriteLine(min);
        }

        public void ProblemTwo()
        {
            var maze = new Maze(Input);
            var graph = ParseGraph(maze);

            int min = int.MaxValue;
            var route = new List<Graph<int>>();
            //1 7 0 6 3 2 4 5
            var permutedGraph = graph.Permute();
            foreach (var permutation in permutedGraph)
            {
                try
                {
                    //Start position has to be 0
                    var list = permutation.ToList();
                    if (list[0].Object != 0)
                    {
                        continue;
                    }



                    int distance = 0;
                    for (int i = 0; i < graph.Count - 1; i++)
                    {
                        //Find the distance between the 2 current nodes, add it to current total
                        var firstNode = list[i];
                        var secondNode = list[i + 1];
                        //Can throw an exception when the order of nodes is invalid. Instead of going through all possible permutations, should optimize to only relevant permutations
                        distance += firstNode.Nodes.Single(i => i.Node == secondNode).Distance;
                    }

                    //Final step: move back to zero
                    var lastNode = list[graph.Count - 1];
                    var zeroNode = list.Single(i => i.Object == 0);

                    distance += lastNode.Nodes.Single(i => i.Node == zeroNode).Distance;
                    
                    if (distance < min)
                    {
                        min = distance;
                        route = list;
                    }
                }
                catch { }
            }




            Console.WriteLine(min);
        }



        private List<Graph<int>> ParseGraph(Maze maze)
        {
            //Initialize all nodes of the graph
            var graph = new List<Graph<int>>();
            maze.Points.ForEach(i => graph.Add(new Graph<int>(i.Digit)));

            //Visit each point of interest and map it out on the graph
            foreach (var node in graph)
            {
                FindNodesFloodFill(maze, node, graph);
            }

            return graph;
        }

        /// <summary>
        /// Finds and builds node connections by doing breadth first search/flood fill
        /// </summary>
        private void FindNodesFloodFill(Maze maze, Graph<int> currentNode, List<Graph<int>> graphNodes)
        {
            //Starting at point, find all paths that connect to another node
            var stack = new List<Vector2i>();
            var visited = new List<Vector2i>();
            
            //Setup initial set of moves
            var startPoint = maze.Points.Single(i => i.Digit == currentNode.Object);
            visited.Add(startPoint.Position);

            var moves = maze.GetMoves(startPoint.Position);
            stack.AddRange(moves);

            int distance = 1;
            while (stack.Any())
            {
                var newMoves = new List<Vector2i>();
                foreach (var p in stack)
                {
                    //Don't go in circles
                    if (!visited.Contains(p))
                    {
                        visited.Add(p);

                        switch (maze.GetMazePart(p.X, p.Y))
                        {
                            //Completed a path.
                            case MazePart.Digit:
                                var foundDigit = maze.Points.Single(i => i.Position.X == p.X && i.Position.Y == p.Y).Digit;

                                //Create connection one-way, the other way will automatically be create later, when the other node is processed as starting point
                                var node = graphNodes.Single(i => i.Object == foundDigit);
                                currentNode.Nodes.Add((distance, node));
                                break;

                            //Empty space - check for moves
                            case MazePart.Empty:
                                newMoves.AddRange(maze.GetMoves(p).Except(visited));
                                break;

                            //Wall - dead end. Do nothing
                            case MazePart.Wall:
                                break;
                        }
                    }
                }
                stack.Clear();
                stack.AddRange(newMoves);
                distance++;
            }
        }


#pragma warning disable CS0414
        private string Example = @"###########
#0.1.....2#
#.#######.#
#4.......3#
###########";

        private string Example2 = @"###########
#0.1.....2#
#.#######.#
#4.......3#
#######.###
#5........#
###########";



        private string Input = @"#######################################################################################################################################################################################
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