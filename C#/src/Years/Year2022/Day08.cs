using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2022
{
    //spaghetti code warning :)
    public class Day08 : BaseDay
    {
        public Day08() : base(2022, 8)
        {
            _grid = ParseInput(Input, out _width, out _height);
        }

        private readonly int[,] _grid;
        private readonly int _width;
        private readonly int _height;

        public override void ProblemOne()
        {
            var visible = 0;
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    foreach(var direction in Extensions.AdjacentIndices)
                    {
                        if(IsVisible(new Vector2i(x, y), direction, out _))
                        {
                            visible++;
                            break;
                        }
                    }
                }
            }
            Console.WriteLine(visible);
        }

        public override void ProblemTwo()
        {
            var max = 0;
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    var position = new Vector2i(x, y);
                    var score = 0;
                    //Edge case: distance should be 0 when the position is on the very edge
                    if (position.X == 0 || position.X == _width - 1 || position.Y == 0 || position.Y == _height - 1)
                    {
                        //score is 0, because we're on the edge. No need to even bother getting a score since it will be multiplied by 0
                        score = 0;
                    }
                    else
                    {
                        var distances = new List<int>();
                        foreach (var direction in Extensions.AdjacentIndices)
                        {
                            IsVisible(position, direction, out int distance);
                            distances.Add(distance);
                        }
                        score = distances.Aggregate((a, x) => a * x);
                    }

                    if(score > max)
                    {
                        max = score;
                    }
                }
            }
            Console.WriteLine(max);
        }

        private bool IsVisible(Vector2i position, Vector2i direction, out int distance)
        {
            var value = _grid[position.X, position.Y];

            distance = 0;
            position = position.Add(direction);
            while(position.X >= 0 && position.X < _width && position.Y >= 0 && position.Y < _height)
            {
                if (value <= _grid[position.X, position.Y])
                {
                    distance++;
                    return false;
                }
                distance++;
                position = position.Add(direction);
            }
            return true;
        }

        private int[,] ParseInput(string input, out int width, out int height)
        {
            var lines = input.SplitNewLine();
            width = lines[0].Length;
            height = lines.Length;

            var result = new int[width, height];
            for(int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    result[x, y] = int.Parse(lines[y][x].ToString());
                }
            }

            return result;
        }

        private const string Example = @"30373
25512
65332
33549
35390
";
    }
}