using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    class Program
    {
        static void Main(string[] args)
        {
            ProblemOne("hhhxzeay");
            //ProblemTwo();
        }

        private static MD5 md5;

        static void ProblemOne(string input)
        {
            List<string> winningPaths = new List<string>();

            using (md5 = MD5.Create())
            {
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
                        var nextMoves = GetNextMoves(input + path);

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
            }

            var winningLngth = winningPaths.Max(i => i.Length);
        }

        static void ProblemTwo()
        {

        }


        static List<char> GetNextMoves(string input)
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


        static void GetCoords(string input, out int x, out int y)
        {
            int countU = input.Count(i => i == 'U');
            int countD = input.Count(i => i == 'D');
            int countL = input.Count(i => i == 'L');
            int countR = input.Count(i => i == 'R');

            x = countD - countU;
            y = countR - countL;
        }

        static bool IsInBounds(string input)
        {
            GetCoords(input, out int x, out int y);
            return x >= 0 && x < 4 && y >= 0 && y < 4;
        }

        static List<char> OpenChars = new List<char>()
        {
            'b',
            'c',
            'd',
            'e',
            'f',
        };

        static int[] GetDoorStatus(string input)
        {
            //Get the hash
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
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
    }
}
