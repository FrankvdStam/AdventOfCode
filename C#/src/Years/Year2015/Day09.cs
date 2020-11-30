using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    
    class Program9
    {
        static void Main(string[] args)
        {
            problemOne(input);
            //Porblem two: swap maxvalue to 0 and < to >
        }

        static void problemOne(string input)
        {
            Dictionary<string, int> distances = new Dictionary<string, int>();
            List<string> cities = new List<string>();
            var lines = input.Split(new string[] { "\r\n" }, StringSplitOptions.None);
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

            string bestRoute = "";
            int bestDistance = 0;
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
        }

        private static string example = @"London to Dublin = 464
London to Belfast = 518
Dublin to Belfast = 141";


        private static string input = @"Tristram to AlphaCentauri = 34
Tristram to Snowdin = 100
Tristram to Tambi = 63
Tristram to Faerun = 108
Tristram to Norrath = 111
Tristram to Straylight = 89
Tristram to Arbre = 132
AlphaCentauri to Snowdin = 4
AlphaCentauri to Tambi = 79
AlphaCentauri to Faerun = 44
AlphaCentauri to Norrath = 147
AlphaCentauri to Straylight = 133
AlphaCentauri to Arbre = 74
Snowdin to Tambi = 105
Snowdin to Faerun = 95
Snowdin to Norrath = 48
Snowdin to Straylight = 88
Snowdin to Arbre = 7
Tambi to Faerun = 68
Tambi to Norrath = 134
Tambi to Straylight = 107
Tambi to Arbre = 40
Faerun to Norrath = 11
Faerun to Straylight = 66
Faerun to Arbre = 144
Norrath to Straylight = 115
Norrath to Arbre = 135
Straylight to Arbre = 127";
    }


    public class Day09 : IDay
    {
        public int Day => 9;
        public int Year => 2015;

        public void ProblemOne()
        {
        }

        public void ProblemTwo()
        {
        }
    }
}