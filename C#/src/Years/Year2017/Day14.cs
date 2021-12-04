using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Xml.Serialization;
using Years.Utils;

namespace Years.Year2017
{
    public class Day14 : IDay
    {
        public int Day => 14;
        public int Year => 2017;
        
        public void ProblemOne()
        {
            var used = 0;
            for (int i = 0; i < 128; i++)
            {
                var bin = HexToBin(KnotHasher.Calculate(Input + "-" + i));
                used += bin.Count(i => i == '1');
            }
            Console.WriteLine(used);
        }

        public void ProblemTwo()
        {
            var memory = new int[128, 128];//row, col

            var binStrings = new List<string>();
            for (var i = 0; i < 128; i++)
            {
                var bin = HexToBin(KnotHasher.Calculate(Input + "-" + i));
                for (var j = 0; j < 128; j++)
                {
                    memory[i, j] = bin[j] == '1' ? -1 : 0;//Mark used bits as -1 instead of 1.
                }
            }

            int region = 1;
            for (var y = 0; y < 128; y++)
            {
                for (var x = 0; x < 128; x++)
                {
                    if (memory[y, x] == -1)
                    {
                        CreateRegion(region, x, y, memory);
                        region++;
                    }
                }
            }

            //var sb = new StringBuilder();
            //for (var y = 0; y < 128; y++)
            //{
            //    for (var x = 0; x < 128; x++)
            //    {
            //        sb.Append(memory[y, x].ToString().PadLeft(4, ' '));
            //        sb.Append(' ');
            //    }
            //    sb.Append("\n");
            //}
            //var test = sb.ToString();

            Console.WriteLine(region-1);
        }

        private List<(int x, int y)> _adjectentIndices = new List<(int x, int y)>()
        {
            (-1,  0),
            ( 1,  0),
            ( 0,  1),
            ( 0, -1),
        };


        private void CreateRegion(int region, int x, int y, int[,] disk)
        {
            var positions = new Stack<Vector2i>();
            positions.Push(new Vector2i(x, y));
            var visited = new List<Vector2i>();

            while (positions.Any())
            {
                var position = positions.Pop();
                visited.Add(position);

                disk[position.Y, position.X] = region;

                var nextMoves = AdjacentInBoundsPositions(position.X, position.Y, disk);
                nextMoves = nextMoves.Except(visited).ToList();
                var removals = new List<Vector2i>();
                foreach (var m in nextMoves)
                {
                    if (disk[m.Y, m.X] != -1)
                    {
                        removals.Add(m);
                    }
                }
                nextMoves = nextMoves.Except(removals).ToList();


                foreach (var n in nextMoves.Except(visited))
                {
                    positions.Push(n);
                }
            }
        }

        private List<Vector2i> AdjacentInBoundsPositions(int x, int y, int[,] disk)
        {
            var result = new List<Vector2i>();
            foreach (var i in _adjectentIndices)
            {
                var tempX = x + i.x;
                var tempY = y + i.y;

                //Check if we're in bounds
                if (tempY >= 0 && tempY < disk.GetLength(0) && tempX >= 0 && tempX < disk.GetLength(1))
                {
                    result.Add(new Vector2i(tempX, tempY));
                }
            }

            return result;
        }


        private string HexToBin(string input)
        {
            return string.Join(string.Empty, input.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
        }

       
        private const string Example = "flqrgnkx";
        private const string Input   = "oundnydw";
    }
}