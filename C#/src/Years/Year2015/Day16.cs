using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    public class Day16 : BaseDay
    {
        public Day16() : base(2015, 16) { }

        public override void ProblemOne()
        {
            var sues = ParseInput(Input);
            var ticketTape = ParseTickerTape(TickerTape);

            List<int> matchingSues = new List<int>();
            foreach (var sue in sues)
            {
                //Determine if this sue matches
                bool isMatch = true;
                foreach (var thing in ticketTape)
                {
                    //If we remember that sue has this thing, check if the numbers match
                    if (sue.Value.ContainsKey(thing.Key))
                    {
                        if (sue.Value[thing.Key] != thing.Value)
                        {
                            isMatch = false;
                            break;
                        }
                    }
                }
                //End
                if (isMatch)
                {
                    matchingSues.Add(sue.Key);
                }
            }
            Console.WriteLine(matchingSues[0]);
        }

        public override void ProblemTwo()
        {
            var sues = ParseInput(Input);
            var ticketTape = ParseTickerTape(TickerTape);

            List<int> matchingSues = new List<int>();
            foreach (var sue in sues)
            {
                //Determine if this sue matches
                bool isMatch = true;
                foreach (var thing in ticketTape)
                {
                    //If we remember that sue has this thing, check if the numbers match
                    if (sue.Value.ContainsKey(thing.Key))
                    {
                        if (MoreThenThings.Contains(thing.Key))
                        {
                            if (sue.Value[thing.Key] <= thing.Value)
                            {
                                isMatch = false;
                                break;
                            }
                        }
                        else if (FewerThenThings.Contains(thing.Key))
                        {
                            if (sue.Value[thing.Key] >= thing.Value)
                            {
                                isMatch = false;
                                break;
                            }
                        }
                        else
                        {
                            if (sue.Value[thing.Key] != thing.Value)
                            {
                                isMatch = false;
                                break;
                            }
                        }
                    }
                }
                //End
                if (isMatch)
                {
                    matchingSues.Add(sue.Key);
                }
            }
            Console.WriteLine(matchingSues[0]);
        }



        private Dictionary<string, int> ParseTickerTape(string tape)
        {
            var result = new Dictionary<string, int>();

            var lines = tape.Replace(":", string.Empty).Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                var bits = line.Split(' ');
                result[bits[0]] = int.Parse(bits[1]);
            }

            return result;
        }


        private Dictionary<int, Dictionary<string, int>> ParseInput(string input)
        {

            Dictionary<int, Dictionary<string, int>> sues = new Dictionary<int, Dictionary<string, int>>();

            var lines = input.Replace(":", string.Empty).Replace(",", string.Empty).SplitNewLine();
            foreach (var line in lines)
            {
                var bits = line.Split(' ');
                int sue = int.Parse(bits[1]);
                sues[sue] = new Dictionary<string, int>();
                sues[sue][bits[2]] = int.Parse(bits[3]);
                sues[sue][bits[4]] = int.Parse(bits[5]);
                sues[sue][bits[6]] = int.Parse(bits[7]);
            }

            return sues;
        }



        private static List<string> MoreThenThings = new List<string>()
        {
            "cats",
            "trees"
        };

        private static List<string> FewerThenThings = new List<string>()
        {
            "pomeranians",
            "goldfish"
        };


        private string TickerTape = @"children: 3
cats: 7
samoyeds: 2
pomeranians: 3
akitas: 0
vizslas: 0
goldfish: 5
trees: 3
cars: 2
perfumes: 1";
    }
}