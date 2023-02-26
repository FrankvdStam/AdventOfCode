using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2022
{
    public class Day12 : BaseDay
    {
        public Day12() : base(2022, 12)
        {
            _map = ParseInput(Input, out _start, out _best);
        }

        private readonly Vector2i _start;
        private readonly Vector2i _best;
        private readonly int[,] _map;

        public override void ProblemOne()
        {
        }

        public override void ProblemTwo()
        {
        }

        private int[,] ParseInput(string input, out Vector2i start, out Vector2i best)
        {
            var split = input.SplitNewLine();
            var result = new int[split.Length, split[0].Length];
            start = new Vector2i();
            best = new Vector2i();
            
            for (int y = 0; y < split.Length; y++)
            {
                for (int x = 0; x < split[0].Length; x++)
                {
                    var elevation = 0;
                    switch (split[y][x])
                    {
                        case 'S':
                            start = new Vector2i(x, y);
                            elevation = 0;
                            break;
                        case 'E':
                            best = new Vector2i(x, y);
                            elevation = 'z' - 'a';
                            break;
                        default:
                            elevation = split[y][x] - 'a';
                            break;
                    }
                    result[y, x] = elevation;
                }
            }

            return result;
        }
    }
}