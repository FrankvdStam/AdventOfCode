using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework.Constraints;
using Years.Utils;

namespace Years.Year2015
{
    public class Person
    {
        public string Name;
        public Dictionary<string, int> People = new Dictionary<string, int>();
    }

    public class Day13 : IDay
    {
        public int Day => 13;
        public int Year => 2015;


        private int Solve(Dictionary<string, Person> people)
        {
            var permutations = new List<List<string>>();

            foreach (var enumerable in people.Keys.Permute())
            {
                permutations.Add(enumerable.ToList());
            }

            Dictionary<string, int> results = new Dictionary<string, int>();
            foreach (var perm in permutations)
            {
                int score = 0;
                string key = "";
                for (int i = 0; i < perm.Count(); i++)
                {
                    //Get the index of the next or looped around element
                    int secondaryIndex = 0;
                    if (i + 1 < perm.Count())
                    {
                        secondaryIndex = i + 1;
                    }


                    var firststr = perm.ElementAt(i);
                    var secondstr = perm.ElementAt(secondaryIndex);

                    var first = people[perm.ElementAt(i)];
                    var second = first.People[perm.ElementAt(secondaryIndex)];

                    score += people[perm.ElementAt(i)].People[perm.ElementAt(secondaryIndex)];
                    score += people[perm.ElementAt(secondaryIndex)].People[perm.ElementAt(i)];
                    key += perm.ElementAt(i);
                }

                results[key] = score;
            }

            int res = results.Max(j => j.Value);
            return res;
            //var result = results.Where(i => i.Value == results.Max(j => j.Value)).ToList();
        }


        public void ProblemOne()
        {
            var people = ParseInput(Input);
            var res = Solve(people);
            Console.WriteLine(res);
        }

        public void ProblemTwo()
        {
            var people = ParseInput(YouInput + "\r\n" + Input);
            var res = Solve(people);
            Console.WriteLine(res);

            var permutations = new List<List<string>>();

            foreach (var enumerable in people.Keys.Permute())
            {
                permutations.Add(enumerable.ToList());
            }

            Dictionary<string, int> results = new Dictionary<string, int>();
            foreach (var perm in permutations)
            {
                int score = 0;
                string key = "";
                for (int i = 0; i < perm.Count(); i++)
                {
                    //Get the index of the next or looped around element
                    int secondaryIndex = 0;
                    if (i + 1 < perm.Count())
                    {
                        secondaryIndex = i + 1;
                    }

                    score += people[perm.ElementAt(i)].People[perm.ElementAt(secondaryIndex)];
                    score += people[perm.ElementAt(secondaryIndex)].People[perm.ElementAt(i)];
                    key += perm.ElementAt(i);
                }

                results[key] = score;
            }

            //int res = results.Max(j => j.Value);
            //var result = results.Where(i => i.Value == results.Max(j => j.Value)).ToList();
        }


        private Dictionary<string, Person> ParseInput(string input)
        {
            Dictionary<string, Person> people = new Dictionary<string, Person>();

            var lines = input.Split(new string[] {"\r\n"}, StringSplitOptions.None);
            foreach (var line in lines)
            {
                var split = line.Replace('.', ' ').Split(' ');
                var person = AddOrGetPerson(split[0], ref people);
                var nextToPerson = AddOrGetPerson(split[10], ref people);
                string positiveNegative = split[2];
                int change = int.Parse(split[3]);

                //Set positive or negative
                change = split[2] == "gain" ? change : -change;
                person.People[nextToPerson.Name] = change;
            }

            return people;
        }

        private Person AddOrGetPerson(string name, ref Dictionary<string, Person> people)
        {
            if (people.ContainsKey(name))
            {
                return people[name];
            }
            else
            {
                Person p = new Person();
                p.Name = name;
                people[name] = p;
                return p;
            }
        }


        private const string YouInput = @"You would lose 0 happiness units by sitting next to Bob.
You would lose 0 happiness units by sitting next to Carol.
You would lose 0 happiness units by sitting next to David.
You would lose 0 happiness units by sitting next to Eric.
You would lose 0 happiness units by sitting next to Frank.
You would lose 0 happiness units by sitting next to George.
You would lose 0 happiness units by sitting next to Mallory.
You would lose 0 happiness units by sitting next to Alice.
Alice would lose 0 happiness units by sitting next to You.
Bob would lose 0 happiness units by sitting next to You.
Carol would lose 0 happiness units by sitting next to You.
David would lose 0 happiness units by sitting next to You.
Eric would lose 0 happiness units by sitting next to You.
Frank would lose 0 happiness units by sitting next to You.
George would lose 0 happiness units by sitting next to You.
Mallory would lose 0 happiness units by sitting next to You.";


        private const string Input = @"Alice would lose 2 happiness units by sitting next to Bob.
Alice would lose 62 happiness units by sitting next to Carol.
Alice would gain 65 happiness units by sitting next to David.
Alice would gain 21 happiness units by sitting next to Eric.
Alice would lose 81 happiness units by sitting next to Frank.
Alice would lose 4 happiness units by sitting next to George.
Alice would lose 80 happiness units by sitting next to Mallory.
Bob would gain 93 happiness units by sitting next to Alice.
Bob would gain 19 happiness units by sitting next to Carol.
Bob would gain 5 happiness units by sitting next to David.
Bob would gain 49 happiness units by sitting next to Eric.
Bob would gain 68 happiness units by sitting next to Frank.
Bob would gain 23 happiness units by sitting next to George.
Bob would gain 29 happiness units by sitting next to Mallory.
Carol would lose 54 happiness units by sitting next to Alice.
Carol would lose 70 happiness units by sitting next to Bob.
Carol would lose 37 happiness units by sitting next to David.
Carol would lose 46 happiness units by sitting next to Eric.
Carol would gain 33 happiness units by sitting next to Frank.
Carol would lose 35 happiness units by sitting next to George.
Carol would gain 10 happiness units by sitting next to Mallory.
David would gain 43 happiness units by sitting next to Alice.
David would lose 96 happiness units by sitting next to Bob.
David would lose 53 happiness units by sitting next to Carol.
David would lose 30 happiness units by sitting next to Eric.
David would lose 12 happiness units by sitting next to Frank.
David would gain 75 happiness units by sitting next to George.
David would lose 20 happiness units by sitting next to Mallory.
Eric would gain 8 happiness units by sitting next to Alice.
Eric would lose 89 happiness units by sitting next to Bob.
Eric would lose 69 happiness units by sitting next to Carol.
Eric would lose 34 happiness units by sitting next to David.
Eric would gain 95 happiness units by sitting next to Frank.
Eric would gain 34 happiness units by sitting next to George.
Eric would lose 99 happiness units by sitting next to Mallory.
Frank would lose 97 happiness units by sitting next to Alice.
Frank would gain 6 happiness units by sitting next to Bob.
Frank would lose 9 happiness units by sitting next to Carol.
Frank would gain 56 happiness units by sitting next to David.
Frank would lose 17 happiness units by sitting next to Eric.
Frank would gain 18 happiness units by sitting next to George.
Frank would lose 56 happiness units by sitting next to Mallory.
George would gain 45 happiness units by sitting next to Alice.
George would gain 76 happiness units by sitting next to Bob.
George would gain 63 happiness units by sitting next to Carol.
George would gain 54 happiness units by sitting next to David.
George would gain 54 happiness units by sitting next to Eric.
George would gain 30 happiness units by sitting next to Frank.
George would gain 7 happiness units by sitting next to Mallory.
Mallory would gain 31 happiness units by sitting next to Alice.
Mallory would lose 32 happiness units by sitting next to Bob.
Mallory would gain 95 happiness units by sitting next to Carol.
Mallory would gain 91 happiness units by sitting next to David.
Mallory would lose 66 happiness units by sitting next to Eric.
Mallory would lose 75 happiness units by sitting next to Frank.
Mallory would lose 99 happiness units by sitting next to George.";


        public const string Example = @"Alice would gain 54 happiness units by sitting next to Bob. 
Alice would lose 79 happiness units by sitting next to Carol.
Alice would lose 2 happiness units by sitting next to David. 
Bob would gain 83 happiness units by sitting next to Alice. 
Bob would lose 7 happiness units by sitting next to Carol. 
Bob would lose 63 happiness units by sitting next to David. 
Carol would lose 62 happiness units by sitting next to Alice. 
Carol would gain 60 happiness units by sitting next to Bob. 
Carol would gain 55 happiness units by sitting next to David.
David would gain 46 happiness units by sitting next to Alice. 
David would lose 7 happiness units by sitting next to Bob. 
David would gain 41 happiness units by sitting next to Carol.";
        
    }
}