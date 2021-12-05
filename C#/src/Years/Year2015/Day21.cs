using System;
using System.Collections.Generic;
using Years.Utils;
using System.Linq;

namespace Years.Year2015
{
    public class Day21 : IDay
    {
        public int Day => 21;
        public int Year => 2015;

        
        public void ProblemOne()
        {
            var boss = ParseInput(Input);
            FightAllFights(boss);

            var result = _fightResults.Where(i => i.Item1).Min(i => i.Item2.TotalGoldSpent);
            Console.WriteLine(result);
        }

        public void ProblemTwo()
        {
            var result = _fightResults.Where(i => !i.Item1).Max(i => i.Item2.TotalGoldSpent);
            Console.WriteLine(result);
        }



        private List<(bool, Player)> _fightResults = new List<(bool, Player)>();

        /// <summary>
        /// Simulates all the possible combinations of player items and fights them all against the boss, storing results in _fightResults.
        /// </summary>
        private void FightAllFights(Player boss)
        {
            foreach (var player in PermutePlayers())
            {
                bool win = Fight(boss.Clone(), player);
                _fightResults.Add((win, player));
            }
        }


        private bool Fight(Player boss, Player player)
        {
            int playerDamage = player.Damage - boss.Armor;
            if (playerDamage <= 0)
            {
                playerDamage = 1;
            }
            int bossDamage = boss.Damage - player.Armor;
            if (bossDamage <= 0)
            {
                bossDamage = 1;
            }

            while (true)
            {
                
                boss.Health -= playerDamage;
                if (boss.Health <= 0)
                {
                    return true;
                }

                player.Health -= bossDamage;
                if (player.Health <= 0)
                {
                    return false;
                }
            }
        }



        /// <summary>
        /// Find all possible players by combining the equipment
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Player> PermutePlayers()
        {
            //Must pick a weapon
            foreach(var weapon in _weapons)
            {
                foreach (var armor in _armor)
                {
                    foreach (var ringLeft in _rings)
                    {
                        foreach (var ringRight in _rings)
                        {
                            //Console.WriteLine($"{weapon.Name} {armor.Name} {ringLeft.Name} {ringRight.Name}");
                            Player p = new Player()
                            {
                                Health = 100,
                                Damage =            weapon.Damage   + armor.Damage  + ringLeft.Damage   + ringRight.Damage,
                                Armor =             weapon.Armor    + armor.Armor   + ringLeft.Armor    + ringRight.Armor,
                                TotalGoldSpent =    weapon.Cost     + armor.Cost    + ringLeft.Cost     + ringRight.Cost,
                            };
                            yield return p;
                        }
                    }
                }
            }
        }


        class Player
        {
            public int Health;

            public int TotalGoldSpent;
            public int Damage;
            public int Armor;

            public Player Clone()
            {
                return new Player()
                {
                    Health = this.Health,
                    TotalGoldSpent = this.TotalGoldSpent,
                    Damage = this.Damage,
                    Armor = this.Armor,
                };
            }
        }



        //setup the shop:
        class Item
        {
            public string Name;
            public int Cost;
            public int Damage;
            public int Armor;
        }

        readonly List<Item> _weapons = new List<Item>()
        {
            new Item(){ Name = "Dagger"     , Cost = 8 , Damage = 4, Armor = 0},
            new Item(){ Name = "Shortsword" , Cost = 10, Damage = 5, Armor = 0},
            new Item(){ Name = "Warhammer"  , Cost = 25, Damage = 6, Armor = 0},
            new Item(){ Name = "Longsword"  , Cost = 40, Damage = 7, Armor = 0},
            new Item(){ Name = "Greataxe"   , Cost = 74, Damage = 8, Armor = 0},
        };

        readonly List<Item> _armor = new List<Item>()
        {
            new Item(){ Name = "None"        , Cost = 0  , Damage = 0, Armor = 0},
            new Item(){ Name = "Leather"     , Cost = 13 , Damage = 0, Armor = 1},
            new Item(){ Name = "Chainmail"   , Cost = 31 , Damage = 0, Armor = 2},
            new Item(){ Name = "Splintmail"  , Cost = 53 , Damage = 0, Armor = 3},
            new Item(){ Name = "Bandedmail"  , Cost = 75 , Damage = 0, Armor = 4},
            new Item(){ Name = "Platemail"   , Cost = 102, Damage = 0, Armor = 5},
        };

        readonly List<Item> _rings = new List<Item>()
        {
            new Item(){ Name = "None"       , Cost = 0  , Damage = 0, Armor = 0},
            new Item(){ Name = "None"       , Cost = 0  , Damage = 0, Armor = 0},
            new Item(){ Name = "Damage +1"  , Cost = 25 , Damage = 1, Armor = 0},
            new Item(){ Name = "Damage +2"  , Cost = 50 , Damage = 2, Armor = 0},
            new Item(){ Name = "Damage +3"  , Cost = 100, Damage = 3, Armor = 0},
            new Item(){ Name = "Defense +1" , Cost = 20 , Damage = 0, Armor = 1},
            new Item(){ Name = "Defense +2" , Cost = 40 , Damage = 0, Armor = 2},
            new Item(){ Name = "Defense +3" , Cost = 80 , Damage = 0, Armor = 3},
        };

        //Get boss from input
        private Player ParseInput(string input)
        {
            var split = input.Split(new string[] {"\r\n"}, StringSplitOptions.None);
            var hitPoints = int.Parse(split[0].Substring(12));
            var damage = int.Parse(split[1].Substring(8));
            var armor = int.Parse(split[2].Substring(7));

            return new Player()
            {
                Health = hitPoints,
                Damage = damage,
                Armor = armor,
            };
        }

        private const string Input = @"Hit Points: 104
Damage: 8
Armor: 1";
    }
}