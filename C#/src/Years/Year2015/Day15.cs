using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    



    public class Day15 : BaseDay
    {
        public Day15() : base(2015, 15) { }

        private int _maxWithCalories = 0;

        public override void ProblemOne()
        {
            var ingredients = ParseIngredients(Input);

            int max = 0;
            //Find all numbers (a,b,c,d) that will add up to 100
            for (int s1 = 0; s1 < 100; s1++)
            {
                for (int s2 = s1+1; s2 < 100; s2++)
                {
                    for (int s3 = s2 + 1; s3 < 100; s3++)
                    {
                        int a = s1;
                        int b = s2 - s1;
                        int c = s3 - s2;
                        int d = 100 - a - b - c;

                        //Calculate this cookie's score
                        int capacity      = ingredients[0].Capacity   * a + ingredients[1].Capacity   * b + ingredients[2].Capacity   * c + ingredients[3].Capacity   * d;
                        int durability    = ingredients[0].Durability * a + ingredients[1].Durability * b + ingredients[2].Durability * c + ingredients[3].Durability * d;
                        int flavor        = ingredients[0].Flavor     * a + ingredients[1].Flavor     * b + ingredients[2].Flavor     * c + ingredients[3].Flavor     * d;
                        int texture       = ingredients[0].Texture    * a + ingredients[1].Texture    * b + ingredients[2].Texture    * c + ingredients[3].Texture    * d;
                        int calories      = ingredients[0].Calories   * a + ingredients[1].Calories   * b + ingredients[2].Calories   * c + ingredients[3].Calories   * d;

                        capacity = capacity > 0 ? capacity : 0;
                        durability = durability > 0 ? durability : 0;
                        flavor = flavor > 0 ? flavor : 0;
                        texture = texture > 0 ? texture : 0;

                        int score = capacity * durability * flavor * texture;
                        if (score > max)
                        {
                            max = score;
                        }

                        //Precalculating pt2
                        if (calories == 500)
                        {
                            if (score > _maxWithCalories)
                            {
                                _maxWithCalories = score;
                            }
                        }
                    }
                }
            }
            Console.WriteLine(max);
        }

        public override void ProblemTwo()
        {
            Console.WriteLine(_maxWithCalories);
        }


        private List<Ingredient> ParseIngredients(string input)
        {
            var ingredients = new List<Ingredient>();
            var lines = input.SplitNewLine();
            foreach (var line in lines)
            {
                ingredients.Add(Ingredient.FromString(line));
            }

            return ingredients;
        }


        //Sprinkles: capacity 5, durability -1, flavor 0, texture 0, calories 5
        class Ingredient
        {
            public int Capacity;
            public int Durability;
            public int Flavor;
            public int Texture;
            public int Calories;





            public override string ToString()
            {
                return $"{Capacity} {Durability} {Flavor} {Texture} {Calories}";
            }


            public static Ingredient FromString(string str)
            {
                var split = str.Split(new string[] { " " }, StringSplitOptions.None);
                
                return new Ingredient()
                {
                    Capacity    = int.Parse(split[2].Trim(',')),
                    Durability  = int.Parse(split[4].Trim(',')),
                    Flavor      = int.Parse(split[6].Trim(',')),
                    Texture     = int.Parse(split[8].Trim(',')),
                    Calories    = int.Parse(split[10]),
                };
            }
        }
    }
}