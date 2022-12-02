using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    public class Day09 : BaseDay
    {
        public Day09() : base(2015, 09)
        {

        }

        private (Dictionary<string, int> distances, List<string> cities) ParseCities(string input)
        {
            var distances = new Dictionary<string, int>();
            var cities = new List<string>();
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var split = line.Split(' ');
                //Put the cities in both ways
                distances[split[0] + split[2]] = int.Parse(split[4]);
                distances[split[2] + split[0]] = int.Parse(split[4]);

                if (!cities.Contains(split[0]))
                {
                    cities.Add(split[0]);
                }

                if (!cities.Contains(split[2]))
                {
                    cities.Add(split[2]);
                }
            }

            return (distances, cities);
        }


        public override void ProblemOne()
        {
            var (distances, cities) = ParseCities(Input);

            var bestRoute = "";
            var bestDistance = int.MaxValue;
            foreach (var permutation in cities.Permute())
            {
                string route = "";
                int dist = 0;
                string prevCity = null;
                foreach (var city in permutation)
                {
                    route += city + " ";
                    if (prevCity == null)
                    {
                        prevCity = city;
                        continue;
                    }
                    dist += distances[city + prevCity];

                    prevCity = city;
                }

                if (dist < bestDistance)
                {
                    bestDistance = dist;
                    bestRoute = route;
                }
            }
            Console.WriteLine(bestDistance);
        }

        public override void ProblemTwo()
        {
            var (distances, cities) = ParseCities(Input);

            var bestRoute = "";
            var bestDistance = 0;
            foreach (var permutation in cities.Permute())
            {
                string route = "";
                int dist = 0;
                string prevCity = null;
                foreach (var city in permutation)
                {
                    route += city + " ";
                    if (prevCity == null)
                    {
                        prevCity = city;
                        continue;
                    }
                    dist += distances[city + prevCity];

                    prevCity = city;
                }

                if (dist > bestDistance)
                {
                    bestDistance = dist;
                    bestRoute = route;
                }
            }
            Console.WriteLine(bestDistance);
        }
    }
}