using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day24
{
    /*
    public class GraphNode
    {
        public Vector2i Position;
        public List<(int Distance, GraphNode Neighbour)> Neighbours = new List<(int Distance, GraphNode Neighbour)>();
    }

    public class Maze
    {
        public Maze(string input)
        {
            FromString(input);
        }


        private int[] _directions =
        {
            -1, 0,
            1, 0,
            0, 1,
            0, -1,
        };



        #region Parsing input, building the graph ========================================================================================================

        
        private void FromString(string input)
        {
            List<GraphNode> nodes = new List<GraphNode>();

            var inputNoDigits = Regex.Replace(input, "[0-9]", ".");

            //Convert to char[,], easier to work with.
            var lines = inputNoDigits.Split(new string[] {"\r\n"}, StringSplitOptions.None);
            char[,] maze = new char[lines[0].Length, lines.Length];
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    maze[x, y] = lines[y][x];
                }
            }

            //Now look for endpoints, corners and split points.
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (maze[x, y] == '.')
                    {
                        //Count the amount of .'s that neightboor us.
                        int neighbours = 0;
                        for (int i = 0; i + 1 < _directions.Length; i += 2)
                        {
                            if (maze[x + _directions[i], y + _directions[i + 1]] == '.')
                            {
                                neighbours++;
                            }
                        }

                        //If there is a straight line or a corner, we will have 2 neighbours.
                        //I only want to add a point if there is a corner, not if there is a straight line.
                        //We can detect a straight line by comparing opposites.
                        if (neighbours == 2)
                        {
                            //Detect lines
                            if (
                                (maze[x + 1, y] == '.' && maze[x - 1, y] == '.') ||
                                (maze[x, y + 1] == '.' && maze[x, y - 1] == '.'))
                            {
                                //Set neighbours to zero so that the line will be ignored
                                neighbours = 0;
                            }

                        }


                        //means that we are in a line and we should not add any bend points.
                        if (neighbours == 0)
                        {

                        }

                        //Endpoint 1 neighbour is an endpoint, 2 is a corner or a straight.
                        if (neighbours > 0)
                        {
                            nodes.Add(new GraphNode() {Position = new Vector2i(x, y)});
                        }
                    }
                }
            }


            Console.Clear();

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    Vector2i v = new Vector2i(x, y);
                    if (nodes.Any(i => i.Position == v))
                    {
                        Console.Write('O');
                    }
                    else
                    {
                        Console.Write(maze[x, y]);
                    }
                }

                Console.Write('\n');
            }


            //Now find and connect all neightbours
            foreach (var node in nodes)
            {
                ConnectNeighbours(node, nodes, maze);
            }

            char[,] chars = new char[maze.GetLength(0), maze.GetLength(1)];

            foreach (var node in nodes)
            {
                chars[node.Position.X, node.Position.Y] = 'O';

                foreach (var neighbour in node.Neighbours)
                {
                    //The loops here only go from low to high, therefore we won't be drawing neighbours twice.
                    //left to right and up to down.
                    if (node.Position.X == neighbour.Neighbour.Position.X)
                    {
                        //differ in Y
                        for (int i = node.Position.Y + 1; i < neighbour.Neighbour.Position.Y; i++)
                        {
                            chars[node.Position.X, i] = '|';
                        }
                    }
                    else
                    {
                        //differ in Y
                        for (int i = node.Position.X + 1; i < neighbour.Neighbour.Position.X; i++)
                        {
                            chars[i, node.Position.Y] = '|';
                        }
                    }
                }
            }

            Console.Clear();
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    Console.Write(chars[x, y]);
                }
                Console.Write('\n');
            }
        }

        /// <summary>
        /// Find all first neighbours of a given node
        /// </summary>
        private void ConnectNeighbours(GraphNode node, List<GraphNode> nodes, char[,] maze)
        {
            List<GraphNode> neighbours = new List<GraphNode>();

            //No need to search depth or breadth first, we only have to search in straight lines.
            for (int i = 0; i + 1 < _directions.Length; i += 2)
            {
                Vector2i position = node.Position.Copy();
                Vector2i direction = new Vector2i(_directions[i], _directions[i+1]);
                position += direction;
                while (maze[position.X, position.Y] != '#' || nodes.Any(j => j.Position == position))
                {
                    position += direction;
                }

                //Find neighbour
                if (maze[position.X, position.Y] != '#')
                {
                    var neighbour = nodes.First(k => k.Position == position);
                    Debug.Assert(neighbour != null);
                    int distance = node.Position.DistanceTo(neighbour.Position);
                    node.Neighbours.Add((distance,neighbour));
                }
            }
        } 


        #endregion
    }*/
}
