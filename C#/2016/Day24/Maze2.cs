using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day24
{

    public class GraphNode
    {
        public Vector2i Position;
        public List<(int Distance, GraphNode Neighbour)> Neighbours = new List<(int Distance, GraphNode Neighbour)>();
    }
    
    public class Maze2
    {
        public Maze2(string input)
        {
            FromString(input);
        }


        #region Parsing ========================================================================================================
        //Gameplan:
        //Use a flood algorithm to find nodes
        //keep track of the last node in the state to connect neighbours automatically and in one pass

        public class State
        {
            public GraphNode LastNode;
            public Vector2i Position;
        }

        private void FromString(string input)
        {
            List<GraphNode> nodes = new List<GraphNode>();
            List<(int Number, Vector2i Position)> pointsOfInterest = new List<(int, Vector2i)>();

            //Convert to char[,], easier to work with. Also deal with the numbers in the maze and remove them
            var lines = input.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            char[,] maze = new char[lines[0].Length, lines.Length];
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (char.IsDigit(lines[y][x]))
                    {
                        int num = int.Parse(lines[y][x].ToString());
                        pointsOfInterest.Add((num, new Vector2i(x, y)));
                        maze[x, y] = '.';
                    }
                    else
                    {
                        maze[x, y] = lines[y][x];
                    }
                }
            }


            List<(Vector2i Left, Vector2i Right)> graphLines = new List<(Vector2i Left, Vector2i Right)>();

            //Horizontally scan all lines
            Vector2i previousPosition = null;

            for (int y = 0; y < maze.GetLength(1); y++)
            {
                for (int x = 0; x < maze.GetLength(0); x++)
                {
                    Vector2i position = new Vector2i(x, y);

                    if (IsNode(position, maze))
                    {
                        if (previousPosition == null)
                        {
                            previousPosition = position;
                        }
                        else
                        {
                            graphLines.Add((start: previousPosition, pos: position));
                            //If we are not next to a wall, keep looking.
                            if (x + 1 < maze.GetLength(0) && maze[x+1, y] != '#')
                            {
                                position = previousPosition;
                            }
                            else
                            {
                                position = null;
                            }
                        }
                    }
                }
                previousPosition = null;
            }
        }

        private readonly int[] _directions =
        {
            -1, 0,
            1, 0,
            0, 1,
            0, -1,
        };

        private bool IsNode(Vector2i position, char[,] maze)
        {
            //Count the amount of .'s that neightboor us.
            int neighbours = 0;
            for (int i = 0; i + 1 < _directions.Length; i += 2)
            {
                if (maze[position.X + _directions[i], position.Y + _directions[i + 1]] == '.')
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
                    (maze[position.X + 1    , position.Y]     == '.' && maze[position.X - 1  , position.Y] == '.') ||
                    (maze[position.X        , position.Y + 1] == '.' && maze[position.X      , position.Y - 1] == '.'))
                {
                    //Set neighbours to zero so that the line will be ignored
                    neighbours = 0;
                }
            }

            
            if (neighbours == 0)
            {
                return false;
            }
            
            if (neighbours > 0)
            {
                return true;
            }
            return false;
        }
        #endregion


    }
}
