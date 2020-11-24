using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    public class Ingredient
    {
        public int Capacity;
        public int Durability;
        public int Flavor;
        public int Texture;
        public int Calories;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Reddit r = new Reddit(@"Sprinkles: capacity 5, durability -1, flavor 0, texture 0, calories 5
PeanutButter: capacity -1, durability 3, flavor 0, texture 0, calories 1
Frosting: capacity 0, durability -1, flavor 4, texture 0, calories 6
Sugar: capacity -1, durability 0, flavor 0, texture 2, calories 8");





            //var ingredients = ParseInput(example);
            //Dictionary<string, int> spoons = new Dictionary<string, int>()
            //{
            //    {"Butterscotch" , 44},
            //    {"Cinnamon"     , 56},
            //};
            //int score = GetScore(ingredients, spoons);
        }

        public static int GetScore(Dictionary<string, Ingredient> ingredients, Dictionary<string, int> spoons)
        {
            Ingredient totals = new Ingredient();
            foreach (var pair in spoons)
            {
                totals.Capacity     += pair.Value * ingredients[pair.Key].Capacity;
                totals.Durability   += pair.Value * ingredients[pair.Key].Durability;
                totals.Flavor       += pair.Value * ingredients[pair.Key].Flavor;
                totals.Texture      += pair.Value * ingredients[pair.Key].Texture;
            }

            totals.Capacity     = totals.Capacity   < 0 ? 0 : totals.Capacity;
            totals.Durability   = totals.Durability < 0 ? 0 : totals.Durability;
            totals.Flavor       = totals.Flavor     < 0 ? 0 : totals.Flavor;
            totals.Texture      = totals.Texture    < 0 ? 0 : totals.Texture;
            
            return totals.Capacity * totals.Durability * totals.Flavor * totals.Texture;
        }

        public static Dictionary<string, Ingredient> ParseInput(string input)
        {
            var result = new Dictionary<string, Ingredient>();
            var lines = input.Split(new string[] {"\r\n"}, StringSplitOptions.None);
            foreach (var line in lines)
            {
                string rep = line.Replace(':', ' ').Replace(",", "");
                var bits = rep.Split(' ');
                Ingredient i = new Ingredient();
                i.Capacity      = int.Parse( bits[3]   );
                i.Durability    = int.Parse( bits[5]   );
                i.Flavor        = int.Parse( bits[7]   );
                i.Texture       = int.Parse( bits[9]   );
                i.Calories      = int.Parse( bits[11]  );
                result[bits[0]] = i;
            }
            return result;
        }

        private static string example = @"Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8
Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3";
    }
}
