using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2022
{
    public class Day04 : BaseDay
    {
        public Day04() : base(2022, 4) 
        {
            foreach(var l in Input.SplitNewLine())
            {
                var halves = l.Split(',');

                var leftStr = halves[0].Split('-');
                var rightStr = halves[1].Split('-');

                var left  = new Vector2i(int.Parse(leftStr[0]), int.Parse(leftStr[1]));
                var right = new Vector2i(int.Parse(rightStr[0]), int.Parse(rightStr[1]));

                _assignments.Add((left, right));
            }
        }

        private readonly List<(Vector2i left, Vector2i right)> _assignments = new List<(Vector2i left, Vector2i right)>();

        public override void ProblemOne()
        {
            var result = _assignments.Count(pair => 
                //Left contains right
                (pair.left.X <= pair.right.X && pair.left.Y >= pair.right.Y) ||

                //Right contains left
                (pair.right.X <= pair.left.X && pair.right.Y >= pair.left.Y)
                );
            Console.WriteLine(result);
        }

        public override void ProblemTwo()
        {
            //Return true if num is in bound of vec
            bool IsInBound(Vector2i vec, int num) => vec.X <= num && vec.Y >= num;

            var result = _assignments.Count(pair =>
                IsInBound(pair.left , pair.right.X) ||
                IsInBound(pair.left , pair.right.Y) ||
                IsInBound(pair.right, pair.left.X) ||
                IsInBound(pair.right, pair.left.Y)
            );

            Console.WriteLine(result);
        }

        private const string Example = @"2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8
";
    }
}