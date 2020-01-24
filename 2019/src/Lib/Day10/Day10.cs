using System;
using System.Collections.Generic;
using System.Text;
using Lib.Shared;

namespace Lib.Day10
{
    public class Day10 : IDay
    {
        public int Day => 10;

        public void ProblemOne()
        {

            var asteroids = ParseMap(Example1, out int width, out int height);
            RenderMap(asteroids, width, height);
        }

        

        public void ProblemTwo()
        {
        }

        public List<Vector2i> ParseMap(string map, out int width, out int height)
        {
            List<Vector2i> asteroids = new List<Vector2i>();

            var split = map.Split(new string[]{"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            height = split.Length;
            width = split[0].Length;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (split[y][x] == '#')
                    {
                        asteroids.Add(new Vector2i(x, y));
                    }
                }
            }

            return asteroids;
        }
        

        public static void RenderMap(List<Vector2i> map, int width, int height)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write('.');
                }
                Console.WriteLine();
            }

            foreach (var vec in map)
            {
                Console.SetCursorPosition(vec.X, vec.Y);
                Console.Write('#');
            }
        }

        private const string Example1 = @".#..#
.....
#####
....#
...##";
    }
}
