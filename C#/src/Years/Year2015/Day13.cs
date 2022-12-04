using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Years.Utils;

namespace Years.Year2015
{
    public class Person
    {
        public string Name;
        public Dictionary<string, int> People = new Dictionary<string, int>();
    }

    public class Day13 : BaseDay
    {
        public Day13() : base(2015, 13)
        {

        }

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


        public override void ProblemOne()
        {
            var people = ParseInput(Input);
            var res = Solve(people);
            Console.WriteLine(res);
        }

        public override void ProblemTwo()
        {
            var people = ParseInput(YouInput + "\r\n" + Input);
            var res = Solve(people);
            Console.WriteLine(res);
        }


        private Dictionary<string, Person> ParseInput(string input)
        {
            Dictionary<string, Person> people = new Dictionary<string, Person>();

            var lines = input.Split(new string[] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
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