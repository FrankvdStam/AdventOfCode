using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2016
{
    public class Day17 : IDay
    {
        public int Day => 17;
        public int Year => 2016;

        public void ProblemOne()
        {
            List<string> winningPaths = new List<string>();

           
            var queue = new Queue<string>();

            int steps = 0;
            queue.Enqueue("");

            while (queue.Any())
            {
                steps++;
                //Handle entire queue each cycle to get actual steps
                var paths = queue.ToList();
                queue.Clear();

                foreach (var path in paths)
                {
                    var nextMoves = GetNextMoves(Input + path);

                    foreach (var move in nextMoves)
                    {
                        GetCoords(path + move, out int x, out int y);
                        if (x == 3 && y == 3)
                        {
                            //Reached the exit.
                            string result = path + move;
                            winningPaths.Add(result);
                        }
                        else
                        {
                            //only enqueue if we didn't finish
                            queue.Enqueue(path + move);
                        }
                    }
                }
            }

            _longest = winningPaths.Max(i => i.Length);
            Console.WriteLine(winningPaths.First());
        }

        private int _longest = 0;

        public void ProblemTwo()
        {
            Console.WriteLine(_longest);
        }










        private List<char> GetNextMoves(string input)
        {
            var result = new List<char>();

            int[] doorStatus = GetDoorStatus(input);

            for (int i = 0; i < doorStatus.Length; i++)
            {
                if (doorStatus[i] == 1)
                {
                    switch (i)
                    {
                        case 0:
                            if (IsInBounds(input + 'U'))
                            {
                                result.Add('U');
                            }
                            break;
                        case 1:
                            if (IsInBounds(input + 'D'))
                            {
                                result.Add('D');
                            }
                            break;
                        case 2:
                            if (IsInBounds(input + 'L'))
                            {
                                result.Add('L');
                            }
                            break;
                        case 3:
                            if (IsInBounds(input + 'R'))
                            {
                                result.Add('R');
                            }
                            break;
                    }
                }
            }
            return result;
        }


        private void GetCoords(string input, out int x, out int y)
        {
            int countU = input.Count(i => i == 'U');
            int countD = input.Count(i => i == 'D');
            int countL = input.Count(i => i == 'L');
            int countR = input.Count(i => i == 'R');

            x = countD - countU;
            y = countR - countL;
        }

        private bool IsInBounds(string input)
        {
            GetCoords(input, out int x, out int y);
            return x >= 0 && x < 4 && y >= 0 && y < 4;
        }

        private List<char> OpenChars = new List<char>()
        {
            'b',
            'c',
            'd',
            'e',
            'f',
        };

        private int[] GetDoorStatus(string input)
        {
            //Get the hash
            byte[] hash = Extensions.ComputeHashFromUtf8String(input);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                stringBuilder.Append(hash[i].ToString("X2"));
            }
            string hashString = stringBuilder.ToString().ToLower();

            int[] result = new int[4];

            for (int i = 0; i < result.Length; i++)
            {
                if (OpenChars.Contains(hashString[i]))
                {
                    result[i] = 1;
                }
            }
            return result;
        }


        private const string Input = "hhhxzeay";
    }
}