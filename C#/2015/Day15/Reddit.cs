using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    //https://www.reddit.com/r/adventofcode/comments/3wwj84/day_15_solutions/
    //by cjx3m
    public class Reddit
    {
        public Reddit(string input)
        {
            var lines = input.Split(new string[] {"\r\n"}, StringSplitOptions.None);
            List<CookiesIngredients> ingredients = lines.Select(l => new CookiesIngredients(l)).ToList();
            int[] possibleQuantities = new int[ingredients.Count];
            Dictionary<string, int> possibleOutcomes = new Dictionary<string, int>();
            do
            {
                for (int i = 0; i < ingredients.Count; i++)
                {
                    possibleQuantities[i]++;
                    if (possibleQuantities[i] > 100)
                        possibleQuantities[i] = 0;
                    else
                        break;
                }

                if (possibleQuantities.Sum() != 100)
                    continue;

                var combinationKey = String.Join("\t", possibleQuantities);
                if (possibleOutcomes.ContainsKey(combinationKey))
                    break;
                int outCome = GetCombinationOutCome(ingredients, possibleQuantities);
                if (outCome < 0)
                {
                    combinationKey += "!";
                    outCome *= -1;
                }
                possibleOutcomes.Add(combinationKey, outCome);
            } while (true);
            var bestCombination = possibleOutcomes.OrderByDescending(c => c.Value).First();
            Console.WriteLine(String.Format("Best combination is"));
            Console.WriteLine(String.Format("{0}", String.Join("\t", ingredients.Select(i => i.Ingredient))));
            Console.WriteLine(String.Format("{0} \t Total: {1}", bestCombination.Key, bestCombination.Value));
            Console.ReadKey();

            bestCombination = possibleOutcomes.Where(c => c.Key.EndsWith("!")).OrderByDescending(c => c.Value).First();
            Console.WriteLine(String.Format("Best combination with the lowest calories"));
            Console.WriteLine(String.Format("{0}", String.Join("\t", ingredients.Select(i => i.Ingredient))));
            Console.WriteLine(String.Format("{0} \t Total: {1}", bestCombination.Key, bestCombination.Value));
            Console.ReadKey();
        }

        private int GetCombinationOutCome(List<CookiesIngredients> ingredients, int[] possibleQuantities)
        {
            int capacity = 0;
            int durability = 0;
            int flavor = 0;
            int texture = 0;
            int calories = 0;

            for (int i = 0; i < possibleQuantities.Length; i++)
            {
                capacity += ingredients[i].Capacity * possibleQuantities[i];
                durability += ingredients[i].Durability * possibleQuantities[i];
                flavor += ingredients[i].Flavor * possibleQuantities[i];
                texture += ingredients[i].Texture * possibleQuantities[i];
                calories += ingredients[i].Calories * possibleQuantities[i];
            }
            if (calories == 500)
                return (Math.Max(0, capacity) * Math.Max(0, durability) * Math.Max(0, flavor) * Math.Max(0, texture)) * -1;
            return Math.Max(0, capacity) * Math.Max(0, durability) * Math.Max(0, flavor) * Math.Max(0, texture);
        }
    }

    
}

internal class CookiesIngredients
{
    private string ingredient;
    private int capacity;
    private int durability;
    private int flavor;
    private int texture;
    private int calories;

    public string Ingredient { get { return ingredient; } }
    public int Capacity { get { return capacity; } }
    public int Durability { get { return durability; } }
    public int Flavor { get { return flavor; } }
    public int Texture { get { return texture; } }
    public int Calories { get { return calories; } }

    public CookiesIngredients(string input)
    {
        ingredient = input.Split(':')[0];
        var data = input.Split(':')[1].Split(',');
        capacity = Convert.ToInt32(data[0].Trim().Split(' ')[1]);
        durability = Convert.ToInt32(data[1].Trim().Split(' ')[1]);
        flavor = Convert.ToInt32(data[2].Trim().Split(' ')[1]);
        texture = Convert.ToInt32(data[3].Trim().Split(' ')[1]);
        calories = Convert.ToInt32(data[4].Trim().Split(' ')[1]);
    }
}