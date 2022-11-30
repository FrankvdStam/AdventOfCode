using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2018
{
    public class Day14 : BaseDay
    {
        public Day14() : base(2018, 14) { }

        public override void ProblemOne()
        {
            var recipes = new List<int>() { 3, 7 };
            var elf1 = 0;
            var elf2 = 1;

            var target = int.Parse(Input);

            while (recipes.Count < target + 10)
            {
                var score1 = recipes[elf1];
                var score2 = recipes[elf2];

                var newRecipes = GetDigits(score1 + score2);
                recipes.AddRange(newRecipes);
               
                elf1 = (elf1 + score1 + 1) % recipes.Count;
                elf2 = (elf2 + score2 + 1) % recipes.Count;

                var index1 = recipes.Count - 2;
                var index2 = recipes.Count - 1;

                //Console.WriteLine(Format(recipes, elf1, elf2));
            }
            Console.WriteLine(string.Join("", recipes.TakeLast(10)));
        }


        public override void ProblemTwo()
        {
            //Day14Part2();

            var recipes = new List<int>() { 3, 7 };
            var elf1 = 0;
            var elf2 = 1;

            var target = Input.Replace("\n", "");

            while (true)
            {
                var score1 = recipes[elf1];
                var score2 = recipes[elf2];

                var newRecipes = GetDigits(score1 + score2);
                recipes.AddRange(newRecipes);

                elf1 = (elf1 + score1 + 1) % recipes.Count;
                elf2 = (elf2 + score2 + 1) % recipes.Count;

                var index1 = recipes.Count - 2;
                var index2 = recipes.Count - 1;


                //Bit of a gimmicky string match. Probably very slow compared to comparing digits
                //Take 1 extra, for situations where 2 new recipes are added
                var lastAsString = string.Join("", recipes.TakeLast(target.Length + 1));

                if (lastAsString.Length < target.Length)
                {
                    continue;
                }

                var first = lastAsString.Substring(0, target.Length);
                var second = lastAsString.Substring(1);

                if(first == target)
                {
                    Console.WriteLine(recipes.Count - target.Length - 1);
                    return;
                }

                if (second == target)
                {
                    Console.WriteLine(recipes.Count - target.Length - 1);
                    return;
                }
            }
        }

        private string Format(List<int> recipes, int elf1, int elf2)
        {
            var sb = new StringBuilder();
            for(int i = 0; i < recipes.Count; i++)
            {
                if(i == elf1)
                {
                    sb.Append($"({recipes[i]})");
                }
                else if  (i == elf2)
                {
                    sb.Append($"[{recipes[i]}]");
                }
                else
                {
                    sb.Append($" {recipes[i]} ");
                }
            }
            return sb.ToString();
        }

        private List<int> GetDigits(int num)
        {
            if(num == 0)
            {
                return new List<int>() { 0 };
            }

            var result = new List<int>();
            while (num > 0)
            {
                result.Add(num % 10);
                num = num / 10;
            }
            result.Reverse();
            return result;
        }
    }
}