using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    class Program
    {
        static void Main(string[] args)
        {
            ProblemOne(Input);
            //ProblemTwo();
        }

        static void ProblemOne(string input)
        {
            List<string> map = new List<string>();
            map.Add(input);

            while (map.Count < 400000)
            {
                map.Add(NextRow(map.Last()));
            }

            int count = 0;
            foreach (string s in map)
            {
                count += s.Count(i => i == '.');
            }
        }

        static string NextRow(string row)
        {
            string nextRow = "";

            for (int i = 0; i < row.Length; i++)
            {
                char left, center, right;


                if (i - 1 >= 0)
                {
                    left = row[i-1];
                }
                else
                {
                    left = '.';
                }

                center = row[i];

                if (i + 1 < row.Length)
                {
                    right = row[i + 1];
                }
                else
                {
                    right = '.';
                }

                //Its left and center tiles are traps, but its right tile is not.
                if (left == '^' && center == '^' && right == '.')
                {
                    nextRow += '^';
                    continue;
                }

                //Its center and right tiles are traps, but its left tile is not.
                if (left == '.' && center == '^' && right == '^')
                {
                    nextRow += '^';
                    continue;
                }

                //Only its left tile is a trap.
                if (left == '^' && center == '.' && right == '.')
                {
                    nextRow += '^';
                    continue;
                }

                //Only its right tile is a trap.
                if (left == '.' && center == '.' && right == '^')
                {
                    nextRow += '^';
                    continue;
                }

                nextRow += '.';
            }

            return nextRow;
        }

        static void ProblemTwo()
        {

        }
        
        private static string Input = @".^^^.^.^^^.^.......^^.^^^^.^^^^..^^^^^.^.^^^..^^.^.^^..^.^..^^...^.^^.^^^...^^.^.^^^..^^^^.....^....";
        private static string Example = ".^^.^.^^^^";

    }
}
