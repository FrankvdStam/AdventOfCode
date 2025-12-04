using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2024
{
    public class Day08 : BaseDay
    {
        public Day08() : base(2024, 8) {}

        public override void ProblemOne()
        {
            var antennas = Parse(_example2, out Vector2i upperbound);
            var lookup = antennas.Select(i => i.pos).ToList();

            var result = new List<Vector2i>();
            //Only add result if not an antenna, in bounds and not duplicate
            void AddResult(Vector2i v)
            {
                //if (lookup.Contains(v))
                //{
                //    return;
                //}

                if (!v.IsInBound(new Vector2i(0, 0), upperbound))
                {
                    return;
                }

                if (result.Contains(v))
                {
                    return;
                }

                result.Add(v);
            }

            //Group by freq
            var freqGroup = antennas.GroupBy(i => i.freq).Select(i => i.ToList()).ToList();
            foreach (var freq in freqGroup)
            {
                //permute each pair per freq
                var pairs = freq.PermutePairs();
                foreach (var pair in pairs)
                {
                    var pos1 = pair.Item1.pos;
                    var pos2 = pair.Item2.pos;


                    var res = GetTarget(8, 4);
                    var rasdes = GetTarget(4, 8);

                    int GetTarget(int first, int second)
                    {
                        if (first == second)
                        {
                            return 0;
                        }
                        else if (first > second)
                        {
                            return first - second;
                        }
                        else // if (first < second)
                        {
                            return second - first;
                        }
                    }

                    var first = new Vector2i(
                        GetTarget(pos1.X, pos2.X),
                        GetTarget(pos1.Y, pos2.Y)
                    );

                    var second = new Vector2i(
                        GetTarget(pos2.X, pos1.X),
                        GetTarget(pos2.Y, pos1.Y)
                    );

                    AddResult(first);
                    AddResult(second);
                }
            }

            Console.WriteLine(_example2);
            foreach (var item in result)
            {
                Console.SetCursorPosition(item.X, item.Y);
                Console.Write('#');
            }
        }

        public override void ProblemTwo()
        {
        }

        private List<(char freq, Vector2i pos)> Parse(string input, out Vector2i upperbound)
        {
            var split = input.SplitNewLine();
            var result = new List<(char, Vector2i)>();
            upperbound = new Vector2i(split[0].Length, split.Length);

            for (var y = 0; y < split.Length; y++)
            {
                for (var x = 0; x < split[y].Length; x++)
                {
                    if (split[y][x] != '.')
                    {
                        result.Add((split[y][x], new Vector2i(x, y)));
                    }
                }
            }

            return result;
        }

        private readonly string _example2 = @"..........
..........
..........
....a.....
........a.
.....a....
..........
..........
..........
..........";

        private readonly string _example1 = @"..........
..........
..........
....a.....
..........
.....a....
..........
..........
..........
..........";


        private readonly string _example = @"............
........0...
.....0......
.......0....
....0.......
......A.....
............
............
........A...
.........A..
............
............";
    }
}