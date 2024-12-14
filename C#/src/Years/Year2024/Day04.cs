using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Years.Utils;

namespace Years.Year2024
{
    public class Day04 : BaseDay
    {
        private readonly List<Vector2i> _directions;
        private readonly List<char[,]> _masks;

        public Day04() : base(2024, 4)
        {
            _directions = Extensions.AdjacentIndices.Clone();
            _directions.AddRange(Extensions.DiagonalIndices);

            _masks = new List<char[,]>();
            _masks.Add(_baseMask);
            var rotated90 = _baseMask.RotateRight();
            _masks.Add(rotated90);
            var rotated180 = rotated90.RotateRight();
            _masks.Add(rotated180);
            var rotated270 = rotated180.RotateRight();
            _masks.Add(rotated270);
        }

        public override void ProblemOne()
        {
            var map = Parse(Input);
            var result = CountXmas(map);
            Console.WriteLine(result);
        }

        public override void ProblemTwo()
        {
            var map = Parse(Input);
            var result = CountCrossMas(map);
            Console.WriteLine(result);
        }

        private char[,] Parse(string input)
        {
            var split = input.SplitNewLine();
            var height = split.Length;
            var width = split[0].Length;

            var result = new char[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    result[y, x] = split[y][x];
                }
            }

            return result;
        }

        #region part 2

        private readonly char[,] _baseMask =
        {
            { 'M', ' ', 'S' },
            { ' ', 'A', ' ' },
            { 'M', ' ', 'S' },
        };

        private int CountCrossMas(char[,] map)
        {
            var count = 0;
            for (int y = 1; y < map.GetLength(0) - 1; y++)
            {
                for (int x = 1; x < map.GetLength(1) - 1; x++)
                {
                    var position = new Vector2i(x, y);
                    foreach (var mask in _masks)
                    {
                        if (CheckMask(map, position, mask))
                        {
                            count++;
                            //Console.WriteLine(position);
                        }
                    }
                }
            }

            return count;
        }

        private bool CheckMask(char[,] map, Vector2i position, char[,] mask)
        {
            //middle pos is not a direction, check it first
            if (map[position.Y, position.X] != mask[1, 1])
            {
                return false;
            }

            foreach (var direction in _directions)
            {
                

                var maskPos = new Vector2i(1, 1) + direction;
                var maskChar = mask[maskPos.Y, maskPos.X];
                if (maskChar != ' ')
                {
                    var mapPos = position + direction;

                    //bounds check
                    if (
                        position.X >= 0 && position.X < map.GetLength(1) &&
                        position.Y >= 0 && position.Y < map.GetLength(0)
                    )
                    {
                        var mapChar = map[mapPos.Y, mapPos.X];
                        if (mapChar != maskChar)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }


        #endregion

        #region part 1


        private int CountXmas(char[,] map)
        {
            var count = 0;
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    count += FindXmasAtPosition(map, new Vector2i(x, y));
                }
            }

            return count;
        }

        private int FindXmasAtPosition(char[,] map, Vector2i position)
        {
            if (map[position.Y, position.X] != 'X')
            {
                return 0;
            }

            var count = 0;
            foreach (var direction in _directions)
            {
                if (
                    CheckDirection(map, position + direction, 'M') &&
                    CheckDirection(map, position + direction + direction, 'A') &&
                    CheckDirection(map, position + direction + direction + direction, 'S')
                    )
                {
                    //Console.WriteLine(position + " " + direction);
                    count++;
                }

            }

            return count;
        }


        /// <summary>
        /// look for char c in given direction 
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="c"></param>
        private bool CheckDirection(char[,] map, Vector2i position, char c)
        {
            //Bounds check
            if (
                position.X >= 0 && position.X < map.GetLength(1) &&
                position.Y >= 0 && position.Y < map.GetLength(0)
                )
            {
                return map[position.Y, position.X] == c;
            }
            return false;
        }


        #endregion

        private readonly string _example = @"..X...
.SAMX.
.A..A.
XMAS.S
.X....";

        private readonly string _example2 = @"MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX";
    }
}