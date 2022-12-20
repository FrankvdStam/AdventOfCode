using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Years.Utils;

namespace Years.Year2022
{
    public class Day11 : BaseDay
    {
        public Day11() : base(2022, 11)
        {
            _parsedMonkeys = ParseInput(Input);
        }

        private List<Monkey> _parsedMonkeys;


        private void PlayRound(List<Monkey> monkeys, bool divide = true, int denominator = 1)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.StartingItems.Any())
                {
                    monkey.Inspection++;
                    var item = monkey.StartingItems[0];
                    monkey.StartingItems.RemoveAt(0);

                    //Apply operation
                    var param = monkey.OperationParam2 ?? item;
                    if (monkey.OperationMultiply)
                    {
                        item *= param;
                    }
                    else
                    {
                        item += param;
                    }

                    //get bored
                    if (divide)
                    {
                        item /= 3;
                    }
                    else
                    {
                        item = item % denominator;
                    }

                    //Console.WriteLine(item);

                    //Throw item
                    var remainder = item % monkey.Divisor;
                    if (remainder == 0)
                    {
                        monkeys[monkey.IndexTrue].StartingItems.Add(item);
                    }
                    else
                    {
                        monkeys[monkey.IndexFalse].StartingItems.Add(item);
                    }
                }
            }
        
            //foreach(var m in _monkeys)
            //{
            //    Console.WriteLine($"{m.Index} {string.Join(',', m.StartingItems)}");
            //}
        }

        public override void ProblemOne()
        {
            var monkeys = _parsedMonkeys.Clone();
            for (int i = 0; i < 20; i++)
            {
                PlayRound(monkeys);
                //Console.ReadLine();
            }

            var sorted = monkeys.OrderByDescending(i => i.Inspection).ToList();
            var monkeyBusiness = sorted[0].Inspection * sorted[1].Inspection;
            Console.WriteLine(monkeyBusiness);
        }

        public override void ProblemTwo()
        {
            var monkeys = _parsedMonkeys.Clone();
            var commonDenominator = monkeys.Select(i => i.Divisor).Aggregate((a, b) => a * b);

            for (int i = 0; i < 10000; i++)
            {
                PlayRound(monkeys, false, commonDenominator);
                //Console.ReadLine();
            }

            var sorted = monkeys.OrderByDescending(i => i.Inspection).ToList();
            var first = new BigInteger(sorted[0].Inspection);
            var second = new BigInteger(sorted[1].Inspection);
            var monkeyBusiness = first * second;
            Console.WriteLine(monkeyBusiness);
        }


        private List<Monkey> ParseInput(string input)
        {
            var result = new List<Monkey>();
            var lines = input.SplitNewLine();
            
            for(int i = 0; i < lines.Length; i+=6)
            {
                var monkey = new Monkey();
                monkey.Index = int.Parse(lines[i].Split(' ')[1].Replace(":", ""));
                monkey.StartingItems = lines[i + 1].Replace("  Starting items: ", "").Split(',').Select(i => long.Parse(i)).ToList();
                
                var operationSplit = lines[i + 2].Split(' ');
                monkey.OperationMultiply = operationSplit[6] == "*";
                if(operationSplit[7] != "old")
                {
                    monkey.OperationParam2 = int.Parse(operationSplit[7]);
                }

                monkey.Divisor = int.Parse(lines[i + 3].Replace("  Test: divisible by ", ""));
                monkey.IndexTrue  = int.Parse(lines[i + 4].Replace("    If true: throw to monkey ", ""));
                monkey.IndexFalse = int.Parse(lines[i + 5].Replace("    If false: throw to monkey ", ""));
                result.Add(monkey);
            }

            return result;
        }


        public class Monkey
        {
            public int Index;
            public List<long> StartingItems = new();
            public bool OperationMultiply;
            public long? OperationParam2;
            public int Divisor;
            public int IndexTrue;
            public int IndexFalse;

            public int Inspection = 0;
        }

        private const string Example = @"
Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1
";
    }
}