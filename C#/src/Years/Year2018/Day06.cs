using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2018
{
    public class Day06 : IDay
    {
        public int Day => 6;
        public int Year => 2018;

        public void ProblemOne()
        {
            List<(int id, Vector2i coord)> temp = ParseInput(Input);
            var coords = OptimizeOrigin(temp).ToList();

            int maxX = coords.Max(i => i.coord.X);
            int maxY = coords.Max(i => i.coord.Y);
            int[,] data = new int[maxX + 1, maxY + 1];

            foreach (var x in Enumerable.Range(0, maxX))
            {
                foreach (var y in Enumerable.Range(0, maxY))
                {
                    var a = new Vector2i() { X = x, Y = y };
                    var distances = coords.Select(c => (c.id, c.coord.ManhattanDistance(a))).OrderBy(i => i.Item2).ToList();
                    if (distances[0].Item2 < distances[1].Item2)
                    {
                        // Owner
                        data[x, y] = distances[0].Item1;
                    }
                    else if (distances[0].Item2 == distances[1].Item2)
                    {
                        // Tie
                        data[x, y] = -1;
                    }
                }
            }

            //Find edges
            List<int> edgeIds = new List<int>();
            foreach (var x in Enumerable.Range(0, maxX))
            {
                foreach (var y in Enumerable.Range(0, maxY))
                {
                    if ((x == 0 || x == maxX) && (y == 0 || y == maxY))
                    {
                        if (data[x, y] > 0 && !edgeIds.Contains(data[x, y]))
                        {
                            edgeIds.Add(data[x, y]);
                        }
                    }
                }
            }

            //count areas of non-edges
            int[] areas = new int[coords.Count];
            foreach (var x in Enumerable.Range(0, maxX))
            {
                foreach (var y in Enumerable.Range(0, maxY))
                {
                    if (!edgeIds.Contains(data[x, y]) && data[x, y] > 0)
                    {
                        areas[data[x, y] - 1]++;
                    }
                }
            }

            var largest = areas.Zip(Enumerable.Range(1, coords.Count + 1), (area, index) => (index, area)).OrderByDescending(i => i.area).First();
            Console.WriteLine(largest.area);
        }

        public void ProblemTwo()
        {
            List<(int id, Vector2i coord)> temp = ParseInput(Input);
            var coords = OptimizeOrigin(temp).ToList();

            int maxX = coords.Max(i => i.coord.X);
            int maxY = coords.Max(i => i.coord.Y);
            int[,] data = new int[maxX + 1, maxY + 1];

            int area = 0;
            foreach (var x in Enumerable.Range(0, maxX))
            {
                foreach (var y in Enumerable.Range(0, maxY))
                {
                    var a = new Vector2i() { X = x, Y = y };
                    var distances = coords.Select(c => c.coord.ManhattanDistance(a)).Sum();
                    if (distances < 10000)
                    {
                        area++;
                    }
                }
            }

            Console.WriteLine(area);
        }
        
        private IEnumerable<(int id, Vector2i coord)> OptimizeOrigin(List<(int id, Vector2i coord)> coords)
        {
            Vector2i correction = new Vector2i() { X = coords.Min(i => i.coord.X), Y = coords.Min(i => i.coord.Y) };
            foreach (var tup in coords)
            {
                var c = tup.coord.Sub(correction);
                yield return (tup.id, c);
            }
        }

        private List<(int id, Vector2i coord)> ParseInput(string input)
        {
            var split = (input.Split(new string[] { "\r\n" }, StringSplitOptions.None)).ToList();
            var result = new List<(int id, Vector2i coord)>();
            int idCounter = 1;
            foreach (string i in split)
            {
                result.Add((idCounter, new Vector2i() { X = int.Parse(i.Substring(0, i.IndexOf(","))), Y = int.Parse(i.Substring(i.IndexOf(" ") + 1)) }));
                idCounter++;
            }
            return result;
        }

        private const string Example = @"1, 1
1, 6
8, 3
3, 4
5, 5
8, 9";

        private const string Input = @"311, 74
240, 84
54, 241
99, 336
53, 244
269, 353
175, 75
119, 271
267, 301
251, 248
216, 259
327, 50
120, 248
56, 162
42, 278
309, 269
176, 74
305, 86
93, 359
311, 189
85, 111
255, 106
286, 108
233, 228
105, 211
66, 256
213, 291
67, 53
308, 190
320, 131
254, 179
338, 44
88, 70
296, 113
278, 75
92, 316
274, 92
147, 121
71, 181
113, 268
317, 53
188, 180
42, 267
251, 98
278, 85
268, 266
334, 337
74, 69
102, 227
194, 239";
    }
}