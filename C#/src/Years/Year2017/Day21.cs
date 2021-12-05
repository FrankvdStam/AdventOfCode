using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Years.Utils;

namespace Years.Year2017
{
    public class Day21 : IDay
    {
        public int Day => 21;
        public int Year => 2017;

        public void ProblemOne()
        {
            var rules = ParseInput(Input);
            var pattern = ParsePattern(StartPattern);
            
            for (int i = 0; i < 5; i++)
            {
                pattern = NextPattern(pattern, rules);
                //Print(pattern);
                //Console.ReadKey();
            }

            var result = pattern.Enumerate().Count(i => i == '#');
            Console.WriteLine(result);
        }


        public void ProblemTwo()
        {
            var rules = ParseInput(Input);
            var pattern = ParsePattern(StartPattern);
            
            for (int i = 0; i < 18; i++)
            {
                pattern = NextPattern(pattern, rules);
                //Print(pattern);
                //Console.ReadKey();
            }

            var result = pattern.Enumerate().Count(i => i == '#');
            Console.WriteLine(result);
        }



        private char[,] NextPattern(char[,] pattern, List<(char[,], char[,])> rules)
        {
            var size = pattern.GetLength(0);
            var chunkSize = size % 2 == 0 ? 2 : 3;
            var chunks = size / chunkSize;
            var newSize = (chunkSize + 1) * chunks;
            var result = new char[newSize, newSize];

            //Foreach split pattern
            for (var chunkY = 0; chunkY < chunks; chunkY++)
            {
                for (var chunkX = 0; chunkX < chunks; chunkX++)
                {
                    //Copy the pattern from the larger pattern into a smaller one
                    var currentPattern = new char[chunkSize, chunkSize];
                    for (var y = 0; y < chunkSize; y++)
                    {
                        for (var x = 0; x < chunkSize; x++)
                        {
                            currentPattern[x, y] = pattern[(chunkX * chunkSize) + x, (chunkY * chunkSize) + y];
                        }
                    }

                    //Find matching pattern to replace it with
                    var newPattern = rules.First(i => i.Item1.SequenceEquals(currentPattern)).Item2;

                    //Place new pattern in result pattern
                    for (var y = 0; y < newPattern.GetLength(1); y++)
                    {
                        for (var x = 0; x < newPattern.GetLength(0); x++)
                        {
                            result[(chunkX * newPattern.GetLength(0)) + x, (chunkY * newPattern.GetLength(1)) + y] = newPattern[x, y];
                        }
                    }
                }
            }

            return result;
        }



        private const string StartPattern = ".#./..#/###";


        

        private void Print(char[,] data)
        {
            Console.Clear();
            for (var y = 0; y < data.GetLength(1); y++)
            {
                for (var x = 0; x < data.GetLength(0); x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(data[x, y]);
                }
            }
        }



        private List<(char[,], char[,])> ParseInput(string input)
        {
            var result = new List<(char[,], char[,])>();
            var lines = input.SplitNewLine();
            foreach (var line in lines)
            {
                var split = line.Split(" => ");
                var rule1 = ParsePattern(split[0]);
                var rule2 = rule1.RotateRight();
                var rule3 = rule2.RotateRight();
                var rule4 = rule3.RotateRight();
                var rule5 = rule1.Flip();
                var rule6 = rule5.RotateRight();
                var rule7 = rule6.RotateRight();
                var rule8 = rule7.RotateRight();

                var pattern = ParsePattern(split[1]);
                
                result.Add((rule1, pattern));
                result.Add((rule2, pattern));
                result.Add((rule3, pattern));
                result.Add((rule4, pattern));
                result.Add((rule5, pattern));
                result.Add((rule6, pattern));
                result.Add((rule7, pattern));
                result.Add((rule8, pattern));
            }
            return result;
        }


        ///Takes an input like this: .#./..#/###
        private char[,] ParsePattern(string pattern)
        {
            var split = pattern.Split('/');
            var size = split.Length;

            var result = new char[size, size];
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    result[x, y] = split[y][x];
                }
            }
            return result;
        }


        private const string Example = @"../.# => ##./#../...
.#./..#/### => #..#/..../..../#..#";

        private const string Input = @"../.. => .##/#../..#
#./.. => .##/#../###
##/.. => ..#/#.#/#..
.#/#. => #../#../.#.
##/#. => .#./#../#..
##/## => .##/.../.##
.../.../... => #.#./###./####/#..#
#../.../... => .###/####/##../#.##
.#./.../... => ###./.###/#..#/#.##
##./.../... => ..../..../.#../##..
#.#/.../... => ...#/.##./..../##..
###/.../... => ##../##../##.#/..##
.#./#../... => .#../###./##../####
##./#../... => ####/##.#/..../..##
..#/#../... => ..#./####/...#/#.##
#.#/#../... => #.#./##../##../.##.
.##/#../... => ##../####/..#./...#
###/#../... => #..#/#.#./##.#/#.#.
.../.#./... => .#.#/..#./#.../....
#../.#./... => ##../..##/..##/.#..
.#./.#./... => ..../##../##../#.##
##./.#./... => ...#/##../#..#/.###
#.#/.#./... => ####/##.#/###./..##
###/.#./... => ..../...#/.###/.#..
.#./##./... => #.#./#..#/.##./.#.#
##./##./... => .###/#.../#..#/#.#.
..#/##./... => .###/####/..../#.##
#.#/##./... => ...#/.###/.###/.###
.##/##./... => ..##/..##/.###/##.#
###/##./... => ####/#..#/####/#.#.
.../#.#/... => #.##/..#./.###/#.#.
#../#.#/... => ####/##.#/##.#/....
.#./#.#/... => #.../...#/#.##/#..#
##./#.#/... => .#.#/##../##../....
#.#/#.#/... => ##.#/#.../##../.#..
###/#.#/... => ...#/###./.#.#/...#
.../###/... => .###/#.##/#.../###.
#../###/... => ..##/.#../.###/..#.
.#./###/... => ..../.##./#.##/#.##
##./###/... => .#.#/##.#/#.../#.#.
#.#/###/... => ..#./#.../#.#./.##.
###/###/... => ..##/.#.#/#..#/.##.
..#/.../#.. => ..##/.#../##.#/##..
#.#/.../#.. => ..#./..../#.../...#
.##/.../#.. => .##./..##/####/#...
###/.../#.. => #.##/..../##../#.##
.##/#../#.. => .###/...#/###./....
###/#../#.. => .#.#/#.#./#.##/..#.
..#/.#./#.. => ...#/..#./..##/.#.#
#.#/.#./#.. => #.../##.#/.###/#.#.
.##/.#./#.. => ###./####/#..#/##.#
###/.#./#.. => ..../..#./..../#...
.##/##./#.. => .#.#/.##./.#.#/#.##
###/##./#.. => ..../##../###./.#.#
#../..#/#.. => ...#/#.../#.##/.###
.#./..#/#.. => #..#/.#../###./#.#.
##./..#/#.. => #.#./..#./###./###.
#.#/..#/#.. => .#.#/##.#/##../####
.##/..#/#.. => ###./..../.#../...#
###/..#/#.. => #.#./.##./.#.#/#..#
#../#.#/#.. => #.#./##.#/.#../.###
.#./#.#/#.. => ##.#/#.#./#.../####
##./#.#/#.. => .#.#/#.../..#./#.##
..#/#.#/#.. => ##.#/.##./#.../.###
#.#/#.#/#.. => ..##/..../..../####
.##/#.#/#.. => ####/#.#./###./.#.#
###/#.#/#.. => #.##/..#./##../#...
#../.##/#.. => ..##/##.#/####/.#..
.#./.##/#.. => ..##/##../.#../..##
##./.##/#.. => ..##/.#.#/#..#/....
#.#/.##/#.. => #.../##../...#/.#.#
.##/.##/#.. => ##../...#/.###/.#.#
###/.##/#.. => ####/..#./.##./#.##
#../###/#.. => .#.#/##.#/#.#./#.#.
.#./###/#.. => .###/#..#/.#.#/###.
##./###/#.. => ##../.#../###./.#.#
..#/###/#.. => #.##/..../...#/..#.
#.#/###/#.. => #.../#..#/..../.#..
.##/###/#.. => ####/#..#/..#./.#.#
###/###/#.. => .##./##../.#../..#.
.#./#.#/.#. => #.#./.###/#.#./..##
##./#.#/.#. => .##./..../..##/##..
#.#/#.#/.#. => ...#/..../.#.#/..##
###/#.#/.#. => .#../####/#.#./#.##
.#./###/.#. => #..#/.#.#/#..#/#.#.
##./###/.#. => .#../##../#..#/..##
#.#/###/.#. => #.#./.##./##.#/.#.#
###/###/.#. => #.#./...#/..##/#...
#.#/..#/##. => ..#./..#./...#/#..#
###/..#/##. => #..#/###./..../##.#
.##/#.#/##. => #.##/.#.#/...#/..##
###/#.#/##. => #.##/...#/.##./.###
#.#/.##/##. => ..../##.#/..../...#
###/.##/##. => .###/#.../###./###.
.##/###/##. => #.../#.#./.###/..#.
###/###/##. => #.##/.#../..#./.#.#
#.#/.../#.# => .##./##../###./.###
###/.../#.# => ..##/...#/###./.#..
###/#../#.# => ##.#/..#./#.##/.#..
#.#/.#./#.# => .#../#.##/...#/###.
###/.#./#.# => ..#./..../####/####
###/##./#.# => ###./#..#/..../#..#
#.#/#.#/#.# => ##.#/###./..../#...
###/#.#/#.# => ##../.###/#..#/.#..
#.#/###/#.# => #.../###./.###/..#.
###/###/#.# => ..../.##./.#../###.
###/#.#/### => ##../#.../.###/#...
###/###/### => .###/###./#.##/..#.";
    }
}