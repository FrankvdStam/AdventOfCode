using System;
using System.Collections.Generic;
using System.Linq;
using Years.Utils;

namespace Years.Year2019
{
    public class Day14 : IDay
    {
        public class Recipe
        {
            public string Item;
            public int ItemQty;

            public Dictionary<string, int> Ingredients = new Dictionary<string, int>();
        }


        public class Component
        {
            public Component() { }

            public Component(string name, int qty)
            {
                Name = name;
                Qty = qty;
            }

            public string Name;
            public int Qty;
        }



        public int Day => 14;
        public int Year => 2019;
        public void ProblemOne()
        {
            var recipes = ParseInput(Example1);
            Dictionary<string, int> items = new Dictionary<string, int>();
            Dictionary<string, int> itemsUsed = new Dictionary<string, int>();

            Stack<Recipe> stack = new Stack<Recipe>();
            stack.Push(recipes["FUEL"]);

            while (stack.Any())
            {
                var item = stack.Pop();
                items.Add(item.Item, item.ItemQty);

                foreach (var ingredient in item.Ingredients)
                {
                    int requested = ingredient.Value;
                    //See if there are existing items left, otherwise create it
                    if (items.TryGetValue(ingredient.Key, out int quantity))
                    {
                        int use = requested > quantity ? quantity : quantity - requested;
                        items[ingredient.Key] -= use;

                    }
                }

                //Now actually make the damn thing

            }
        }

        public void ProblemTwo()
        {
        }

        //Well, scanf woulda been nice here
        public Recipe ParseLine(string line)
        {
            Dictionary<string, int> ingredients = new Dictionary<string, int>();
            var ingredientString = line.Substring(0, line.IndexOf('='));
            int amountOfIngredients = ingredientString.Count(i => i == ',') + 1;
            string resultItemName = "";
            int resultItemQty = 0;

            for (int i = 0; i < amountOfIngredients; i++)
            {
                int index = 0;
                while (char.IsDigit(ingredientString[index]))
                {
                    index++;
                }

                string num = ingredientString.Substring(0, index);
                int qty = int.Parse(num);
                index++;
                int nameStartIndex = index;

                while (index < ingredientString.Length && ingredientString[index] != ' ' && ingredientString[index] != ',')
                {
                    index++;
                }
                string name = ingredientString.Substring(nameStartIndex, index - nameStartIndex);
                ingredients.Add(name, qty);

                if (i + 1 < amountOfIngredients)
                {
                    ingredientString = ingredientString.Substring(index + 2, ingredientString.Length - index - 3);
                }

                var resultStr = line.Substring(line.IndexOf('>') + 2);
                index = 0;
                while (char.IsDigit(ingredientString[index]))
                {
                    index++;
                }
                num = resultStr.Substring(0, index);
                resultItemQty = int.Parse(num);
                index++;
                resultItemName = resultStr.Substring(index);
            }

            Console.WriteLine($"Found {resultItemName}: {resultItemQty}");
            foreach (var pair in ingredients)
            {
                Console.WriteLine($"Needs {pair.Key}: {pair.Value}");
            }

            Recipe r = new Recipe();
            r.Item = resultItemName;
            r.ItemQty = resultItemQty;
            r.Ingredients = ingredients;
            return r;
        }


        public Dictionary<string, Recipe> ParseInput(string input)
        {
            Dictionary<string, Recipe> lookup = new Dictionary<string, Recipe>();

            var lines = input.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var recipe = ParseLine(line);
                lookup.Add(recipe.Item, recipe);
            }

            if (lookup.Count(i => i.Value.Item == "FUEL") != 1)
            {
                throw new Exception();
            }

            return lookup;
        }


        private const string Example1 = @"10 ORE => 10 A
1 ORE => 1 B
7 A, 1 B => 1 C
7 A, 1 C => 1 D
7 A, 1 D => 1 E
7 A, 1 E => 1 FUEL";
    }
}
