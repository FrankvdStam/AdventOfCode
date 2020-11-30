using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2019
{
    public class Day10 : IDay
    {
        public int Day => 10;
        public int Year => 2019;
        public void ProblemOne()
        {
            var asteroids = ParseMap(Example1, out int width, out int height);
            RenderMap(asteroids, width, height);
            Console.SetCursorPosition(width, height);
            Console.WriteLine("\n\nFound this map");
            Console.ReadKey();
            Console.Clear();

            var edges = GetOuterEdgeCoords(width, height);
            RenderMap(edges, width, height);
            Console.SetCursorPosition(width, height);
            Console.WriteLine("\n\nFound these edges");
            Console.ReadKey();
            Console.Clear();

            Dictionary<Vector2i, int> results = new Dictionary<Vector2i, int>();
            foreach (var asteroid in asteroids)
            {
                int count = CountVisibleAsteroids(asteroid, asteroids, edges);
                results.Add(asteroid, count);
                Console.SetCursorPosition(asteroid.X, asteroid.Y);
                Console.Write(count);
                Console.ReadKey();
            }
        }

        

        public void ProblemTwo()
        {
        }



        public int CountVisibleAsteroids(Vector2i position, List<Vector2i> asteroids, List<Vector2i> edges)
        {
            List<Vector2i> matchedAsteroids = new List<Vector2i>();
            int count = 0;
            foreach (var edge in edges)
            {
                if (edge != position)
                {
                    var line = position.PlotLine(edge);
                    foreach (var step in line)//:)
                    {
                        if (step != position && asteroids.Contains(step))
                        {
                            //Match!
                            //Increment the count - we just hit an asteroid. Everything else on this line is being blocked by this asteroid tho.
                            //Must break out of this loop and continue with the next edge coordinate.
                            //Only increment the count if this is the first time running into this asteroid.
                            if (!matchedAsteroids.Contains(step))
                            {
                                matchedAsteroids.Add(step);
                                count++;
                            }
                            break;
                        }
                    }
                }
            }

            return count;
        }



        public List<Vector2i> GetOuterEdgeCoords(int width, int height)
        {
            List<Vector2i> edges = new List<Vector2i>();

            //Get upper edge
            for (int x = 0; x < width; x++)
            {
                edges.Add(new Vector2i(x, 0));
            }

            //Get right edge (minus upper one previously included)
            for (int y = 1; y < height; y++)
            {
                edges.Add(new Vector2i(width-1, y));
            }

            //Get lower edge (minus right one previously included)
            for (int x = width - 2; x > -1; x--)
            {
                edges.Add(new Vector2i(x, height-1));
            }

            //Get left edge (minus lower one previously included and highest previously included in first loop)
            for (int y = height - 2; y > 0; y--)
            {
                edges.Add(new Vector2i(0, y));
            }

            return edges;
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

       // .7..7
       // .....
       // 67775
       // ....7
       // ...87
    }
}
