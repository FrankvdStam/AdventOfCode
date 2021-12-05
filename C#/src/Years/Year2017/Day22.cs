using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2017
{
    public class Day22 : IDay
    {
        private enum NodeState
        {
            Clean,
            Weakened,
            Infected,
            Flagged,
        }

        private NodeState CycleNodeState(NodeState n)
        {
            switch (n)
            {
                case NodeState.Clean   : return NodeState.Weakened;   
                case NodeState.Weakened: return NodeState.Infected;
                case NodeState.Infected: return NodeState.Flagged;
                case NodeState.Flagged : return NodeState.Clean;
                default                : throw new Exception();
            }
        }



        public int Day => 22;
        public int Year => 2017;

        public void ProblemOne()
        {
            var nodes = ParseInput(Input);
            var position = new Vector2i(0, 0);
            var direction = Direction.Up;
            var infectionCount = 0;

            for (var burst = 0; burst < 10000; burst++)
            {
                if(!nodes.TryGetValue(position, out bool currentNodeInfected))
                {
                    currentNodeInfected = false;
                }

                if (currentNodeInfected)
                {
                    direction = direction.RotateRight();
                    nodes[position] = false;//clean
                }
                else
                {
                    direction = direction.RotateLeft();
                    nodes[position] = true;//infect
                    infectionCount++;
                }

                //Step forward
                position = position.Add(_vectorLookup[direction]);
            }
            Console.WriteLine(infectionCount);
        }

        public void ProblemTwo()
        {
            var temp = ParseInput(Input);
            var nodes = new Dictionary<Vector2i, NodeState>();
            foreach (var t in temp)
            {
                nodes[t.Key] = t.Value ? NodeState.Infected : NodeState.Clean;
            }


            var position = new Vector2i(0, 0);
            var direction = Direction.Up;
            var infectionCount = 0;

            for (var burst = 0; burst < 10000000; burst++)
            {
                if (!nodes.TryGetValue(position, out NodeState currentNodeState))
                {
                    currentNodeState = NodeState.Clean;
                }

                switch (currentNodeState)
                {
                    case NodeState.Clean:
                        direction = direction.RotateLeft();
                        break;

                    case NodeState.Weakened:
                        break;

                    case NodeState.Infected:
                        direction = direction.RotateRight();
                        break;

                    case NodeState.Flagged:
                        direction = direction.RotateRight().RotateRight();
                        break;
                }

                //Infect
                currentNodeState = CycleNodeState(currentNodeState);
                if (currentNodeState == NodeState.Infected)
                {
                    infectionCount++;
                }
                nodes[position] = currentNodeState;


                //Step forward
                position = position.Add(_vectorLookup[direction]);
            }
            Console.WriteLine(infectionCount);
        }


        private static Dictionary<Direction, Vector2i> _vectorLookup = new Dictionary<Direction, Vector2i>()
        {
            { Direction.Left , new Vector2i(-1,  0)},
            { Direction.Right, new Vector2i( 1,  0)},
            { Direction.Down , new Vector2i( 0,  1)},
            { Direction.Up   , new Vector2i( 0, -1)},
        };

        

        private Dictionary<Vector2i, bool> ParseInput(string input)
        {
            var result = new Dictionary<Vector2i, bool>();
            var lines = input.SplitNewLine();
            var width = lines[0].Length;
            var height = lines.Length;
            var offsetX = width / 2;
            var offsetY = height / 2;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    result[new Vector2i(x-offsetX, y-offsetY)] = lines[y][x] == '#';
                }
            }

            return result;
        }

        private const string Example = @"..#
#..
...";

        private const string Input = @"...##.#.#.####...###.....
..#..##.#...#.##.##.#..#.
.#.#.#.###....#...###....
.#....#..####.....##.#..#
##.#.#.#.#..#..#.....###.
#...##....##.##.#.##.##..
.....###..###.###...#####
######.####..#.#......##.
#..###.####..####........
#..######.##....####...##
...#.##.#...#.#.#.#..##.#
####.###..#####.....####.
#.#.#....#.####...####...
##...#..##.##....#...#...
......##..##..#..#..####.
.##..##.##..####..##....#
.#..#..##.#..##..#...#...
#.#.##.....##..##.#####..
##.#.......#....#..###.#.
##...#...#....###..#.#.#.
#....##...#.#.#.##..#..##
#..#....#####.....#.##.#.
.#...#..#..###....###..#.
..##.###.#.#.....###.....
#.#.#.#.#.##.##...##.##.#";
    }
}