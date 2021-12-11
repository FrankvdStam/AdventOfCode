using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2021
{
    public class Day11 : IDay
    {
        public int Day => 11;
        public int Year => 2021;

        public void ProblemOne()
        {
            var map = ParseInput(Input);

            var count = 0; 
            for (var i = 0; i < 100; i++)
            {
                count += Step(map);
            }
            
            Console.WriteLine(count);
        }

        public void ProblemTwo()
        {
            var map = ParseInput(Input);
            var mapCount = map.Length;

            var count = 0;
            while (true)
            {
                var flashes = Step(map);
                count++;

                if (flashes == mapCount)
                {
                    Console.WriteLine(count);
                    return;
                }
            }
        }

        private int Step(int[,] map)
        {
            var flash = new Stack<Vector2i>();
            var visited = new List<Vector2i>();

            //Increment all once, keep track of the elements that go over 9
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    map[x, y]++;
                    if (map[x, y] > 9)
                    {
                        flash.Push(new Vector2i(x, y));
                        visited.Add(new Vector2i(x, y));
                    }
                }
            }

            //Flash, keep flashing adjacent nodes. Only allow nodes to flash once
            while (flash.Any())
            {
                var next = flash.Pop();
                var adjacentElements = map.GetAdjacentElements(next, Extensions.AdjacentIteratorBehavior.IncludeDiagonal);
                foreach (var adjacent in adjacentElements)
                {
                    map[adjacent.position.X, adjacent.position.Y]++;
                    if (map[adjacent.position.X, adjacent.position.Y] > 9 && !visited.Contains(adjacent.position))
                    {
                        flash.Push(adjacent.position);
                        visited.Add(adjacent.position);
                    }
                }
            }

            //Reset all nodes that flashed to 0
            foreach (var pos in visited)
            {
                map[pos.X, pos.Y] = 0;
            }

            return visited.Count;
        }

        


        private int[,] ParseInput(string input)
        {
            var lines = input.SplitNewLine();

            var map = new int[lines[0].Length, lines.Length];
            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[0].Length; x++)
                {
                    map[x, y] = int.Parse(lines[y][x].ToString());
                }
            }

            return map;
        }

        private const string Example = @"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526";

        private const string Input = @"7147713556
6167733555
5183482118
3885424521
7533644611
3877764863
7636874333
8687188533
7467115265
1626573134";
    }
}