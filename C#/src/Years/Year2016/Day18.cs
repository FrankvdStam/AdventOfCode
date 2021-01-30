using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2016
{
    public class Day18 : IDay
    {
        public int Day => 18;
        public int Year => 2016;

        public void ProblemOne()
        {
            int rows = CalcRows(Input, 40);
            Console.WriteLine(rows);
        }

        public void ProblemTwo()
        {
            int rows = CalcRows(Input, 400000);
            Console.WriteLine(rows);
        }



        private int CalcRows(string input, int rows)
        {
            List<string> map = new List<string>();
            map.Add(input);

            while (map.Count < rows)
            {
                map.Add(NextRow(map.Last()));
            }

            int count = 0;
            foreach (string s in map)
            {
                count += s.Count(i => i == '.');
            }

            return count;
        }


        private string NextRow(string row)
        {
            string nextRow = "";

            for (int i = 0; i < row.Length; i++)
            {
                char left, center, right;


                if (i - 1 >= 0)
                {
                    left = row[i - 1];
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


        private const string Input = @".^^^.^.^^^.^.......^^.^^^^.^^^^..^^^^^.^.^^^..^^.^.^^..^.^..^^...^.^^.^^^...^^.^.^^^..^^^^.....^....";
        private const string Example = ".^^.^.^^^^";

    }
}