using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    public class Person
    {
        public string Name;
        public Dictionary<string, int> People = new Dictionary<string, int>();
    }

    class Program
    {
        static void Main2(string[] args)
        {
            ParseInput(Example);

            List<string> people = new List<string>()
            {
                "David",
                "Alice",
                "Bob",
                "Carol",
            };

            int score = 0;
            string key = "";
            for (int i = 0; i < people.Count(); i++)
            {
                //Get the index of the next or looped around element
                int secondaryIndex = 0;
                if (i + 1 < people.Count())
                {
                    secondaryIndex = i + 1;
                }

                Console.WriteLine($"{people.ElementAt(i)} gets ");

                score += People[people.ElementAt(i)].People[people.ElementAt(secondaryIndex)];
                score += People[people.ElementAt(secondaryIndex)].People[people.ElementAt(i)];
                key += people.ElementAt(i);
            }
        }


        static void Main(string[] args)
        {
            ParseInput(Input);
            var permutations = new List<List<string>>();

            foreach (var enumerable in People.Keys.Permute())
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

                    score += People[perm.ElementAt(i)].People[perm.ElementAt(secondaryIndex)];
                    score += People[perm.ElementAt(secondaryIndex)].People[perm.ElementAt(i)];
                    key += perm.ElementAt(i);
                }
                results[key] = score;
            }

            int res = results.Max(j => j.Value);
            var result = results.Where(i => i.Value == results.Max(j => j.Value)).ToList();
        }
        

        static void ParseInput(string input)
        {
            var lines = input.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                var split = line.Replace('.',' ').Split(' ');
                var person = AddOrGetPerson(split[0]);
                var nextToPerson = AddOrGetPerson(split[10]);
                string positiveNegative = split[2];
                int change = int.Parse(split[3]);

                //Set positive or negative
                change = split[2] == "gain" ? change : -change;
                person.People[nextToPerson.Name] = change;
            }
        }

        static Dictionary<string, Person> People = new Dictionary<string, Person>();
        
        static Person AddOrGetPerson(string name)
        {
            if (People.ContainsKey(name))
            {
                return People[name];
            }
            else
            {
                Person p = new Person();
                p.Name = name;
                People[name] = p;
                return p;
            }
        }

        public static string Input = @"You would lose 0 happiness units by sitting next to Bob.
You would lose 0 happiness units by sitting next to Carol.
You would lose 0 happiness units by sitting next to David.
You would lose 0 happiness units by sitting next to Eric.
You would lose 0 happiness units by sitting next to Frank.
You would lose 0 happiness units by sitting next to George.
You would lose 0 happiness units by sitting next to Mallory.
You would lose 0 happiness units by sitting next to Alice.
Alice would lose 2 happiness units by sitting next to Bob.
Alice would lose 62 happiness units by sitting next to Carol.
Alice would gain 65 happiness units by sitting next to David.
Alice would gain 21 happiness units by sitting next to Eric.
Alice would lose 81 happiness units by sitting next to Frank.
Alice would lose 4 happiness units by sitting next to George.
Alice would lose 80 happiness units by sitting next to Mallory.
Alice would lose 0 happiness units by sitting next to You.
Bob would gain 93 happiness units by sitting next to Alice.
Bob would gain 19 happiness units by sitting next to Carol.
Bob would gain 5 happiness units by sitting next to David.
Bob would gain 49 happiness units by sitting next to Eric.
Bob would gain 68 happiness units by sitting next to Frank.
Bob would gain 23 happiness units by sitting next to George.
Bob would gain 29 happiness units by sitting next to Mallory.
Bob would lose 0 happiness units by sitting next to You.
Carol would lose 54 happiness units by sitting next to Alice.
Carol would lose 70 happiness units by sitting next to Bob.
Carol would lose 37 happiness units by sitting next to David.
Carol would lose 46 happiness units by sitting next to Eric.
Carol would gain 33 happiness units by sitting next to Frank.
Carol would lose 35 happiness units by sitting next to George.
Carol would gain 10 happiness units by sitting next to Mallory.
Carol would lose 0 happiness units by sitting next to You.
David would gain 43 happiness units by sitting next to Alice.
David would lose 96 happiness units by sitting next to Bob.
David would lose 53 happiness units by sitting next to Carol.
David would lose 30 happiness units by sitting next to Eric.
David would lose 12 happiness units by sitting next to Frank.
David would gain 75 happiness units by sitting next to George.
David would lose 20 happiness units by sitting next to Mallory.
David would lose 0 happiness units by sitting next to You.
Eric would gain 8 happiness units by sitting next to Alice.
Eric would lose 89 happiness units by sitting next to Bob.
Eric would lose 69 happiness units by sitting next to Carol.
Eric would lose 34 happiness units by sitting next to David.
Eric would gain 95 happiness units by sitting next to Frank.
Eric would gain 34 happiness units by sitting next to George.
Eric would lose 99 happiness units by sitting next to Mallory.
Eric would lose 0 happiness units by sitting next to You.
Frank would lose 97 happiness units by sitting next to Alice.
Frank would gain 6 happiness units by sitting next to Bob.
Frank would lose 9 happiness units by sitting next to Carol.
Frank would gain 56 happiness units by sitting next to David.
Frank would lose 17 happiness units by sitting next to Eric.
Frank would gain 18 happiness units by sitting next to George.
Frank would lose 56 happiness units by sitting next to Mallory.
Frank would lose 0 happiness units by sitting next to You.
George would gain 45 happiness units by sitting next to Alice.
George would gain 76 happiness units by sitting next to Bob.
George would gain 63 happiness units by sitting next to Carol.
George would gain 54 happiness units by sitting next to David.
George would gain 54 happiness units by sitting next to Eric.
George would gain 30 happiness units by sitting next to Frank.
George would gain 7 happiness units by sitting next to Mallory.
George would lose 0 happiness units by sitting next to You.
Mallory would gain 31 happiness units by sitting next to Alice.
Mallory would lose 32 happiness units by sitting next to Bob.
Mallory would gain 95 happiness units by sitting next to Carol.
Mallory would gain 91 happiness units by sitting next to David.
Mallory would lose 66 happiness units by sitting next to Eric.
Mallory would lose 75 happiness units by sitting next to Frank.
Mallory would lose 99 happiness units by sitting next to George.
Mallory would lose 0 happiness units by sitting next to You.";


        public static string Example = @"Alice would gain 54 happiness units by sitting next to Bob. 
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
