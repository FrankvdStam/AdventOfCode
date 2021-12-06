using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Years.Utils;

namespace Years.Year2021
{
    public class Day06 : IDay
    {
        public int Day => 6;
        public int Year => 2021;

        public void ProblemOne()
        {
            var fish = ParseInput(Input);

            for (var day = 0; day < 80; day++)
            {
                fish = Tick(fish);
            }
            Console.WriteLine(fish.Count);
        }

        public void ProblemTwo()
        {
            //Each fish is born on a day of the cycle. Keep track of the amount of new fish born on each cycle + total fish
            var fish = ParseInput(Input);
            var total = (long)fish.Count;

            var population = new Dictionary<int, long>();
            for (int i = 0; i <= 6; i++)
            {
                population[i] = fish.Count(j => j == i);
            }
            population[7] = 0;
            population[8] = 0;

            for (var day = 0; day < 256; day++)
            {
                var newFish = population[0];
                total += newFish;
                for (int i = 0; i < population.Count-1; i++)
                {
                    population[i] = population[i + 1];
                }
                population[6] += newFish;
                population[8] = newFish;
            }
            Console.WriteLine(total);
        }


        private List<int> Tick(List<int> fish)
        {
            //List might grow - cash current size
            var size = fish.Count;
            for (int i = 0; i < size; i++)
            {
                fish[i]--;
                if (fish[i] == -1)
                {
                    fish[i] = 6;
                    fish.Add(8);
                }
            }
            return fish;
         }

        private List<int> ParseInput(string input)
        {
            var result = new List<int>();
            foreach (var l in input.Split(','))
            {
                result.Add(int.Parse(l));
            }
            return result;
        }

        private const string Example = @"3,4,3,1,2";

        private const string Input = @"3,4,3,1,2,1,5,1,1,1,1,4,1,2,1,1,2,1,1,1,3,4,4,4,1,3,2,1,3,4,1,1,3,4,2,5,5,3,3,3,5,1,4,1,2,3,1,1,1,4,1,4,1,5,3,3,1,4,1,5,1,2,2,1,1,5,5,2,5,1,1,1,1,3,1,4,1,1,1,4,1,1,1,5,2,3,5,3,4,1,1,1,1,1,2,2,1,1,1,1,1,1,5,5,1,3,3,1,2,1,3,1,5,1,1,4,1,1,2,4,1,5,1,1,3,3,3,4,2,4,1,1,5,1,1,1,1,4,4,1,1,1,3,1,1,2,1,3,1,1,1,1,5,3,3,2,2,1,4,3,3,2,1,3,3,1,2,5,1,3,5,2,2,1,1,1,1,5,1,2,1,1,3,5,4,2,3,1,1,1,4,1,3,2,1,5,4,5,1,4,5,1,3,3,5,1,2,1,1,3,3,1,5,3,1,1,1,3,2,5,5,1,1,4,2,1,2,1,1,5,5,1,4,1,1,3,1,5,2,5,3,1,5,2,2,1,1,5,1,5,1,2,1,3,1,1,1,2,3,2,1,4,1,1,1,1,5,4,1,4,5,1,4,3,4,1,1,1,1,2,5,4,1,1,3,1,2,1,1,2,1,1,1,2,1,1,1,1,1,4";
    }
}