using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    class Program22
    {

        static void Main()
        {
            Program22 p = new Program22();
            p.Run();
        }

        static int bossH = 55;
        static int bossAt = 8;

        static int costMissile = 53;
        static int costDrain = 73;
        static int costPoison = 173;
        static int costShield = 113;
        static int costRecharge = 229;

        static int heroH = 50;
        static int heroMana = 500;
        static int heroAr = 0;
        static int cost = 0;

        static int poison = 0;
        static int recharge = 0;
        static int shield = 0;

        static int boss;
        static int hero;
        static int mana;

        Random rand = new Random();

        public void Run()
        {
            int answer = 9999999;

            for (int i = 0; i < 3000000; i++)
            {
                if (Fight())
                {
                    answer = Math.Min(answer, cost);
                    Console.Out.WriteLine(answer);
                }
            }

            Console.Out.WriteLine("answer: " + answer);
        }

        enum ActionType
        {
            Nothing,
            Missile,
            Drain,
            Poison,
            Shield,
            Recharge
        }

        ActionType Choose()
        {
            if (mana < costPoison)
            {
                return ActionType.Nothing;
            }

            while (true)
            {
                int next = rand.Next(5);
                if (next == 0 && mana >= costMissile)
                {
                    return ActionType.Missile;
                }
                else if (next == 1 && mana >= costDrain)
                {
                    return ActionType.Drain;
                }
                else if (next == 2 && mana >= costPoison)
                {
                    return ActionType.Poison;
                }
                else if (next == 3 && mana >= costRecharge)
                {
                    return ActionType.Recharge;
                }
                else if (next == 4 && mana >= costShield)
                {
                    return ActionType.Shield;
                }
            }
        }

        bool Fight()
        {
            bool turn = true;
            ActionType type = ActionType.Nothing;

            hero = heroH;
            boss = bossH;
            mana = heroMana;
            cost = 0;
            poison = 0;
            recharge = 0;
            shield = 0;

            while (true)
            {
                if (poison > 0)
                {
                    poison--;
                    boss -= 3;
                }

                if (recharge > 0)
                {
                    recharge--;
                    mana += 101;
                }

                if (shield > 0)
                {
                    shield--;
                }

                if (shield == 0)
                {
                    heroAr = 0;
                }

                if (boss <= 0)
                {
                    return true;
                }

                if (hero <= 0)
                {
                    return false;
                }

                if (turn)
                {
                    // hard mode
                    hero--;
                    if (hero <= 0)
                    {
                        return false;
                    }

                    type = Choose();
                    if (type == ActionType.Nothing)
                    {
                        return false;
                    }

                    if (type == ActionType.Drain)
                    {
                        boss -= 2;
                        hero += 2;
                        cost += costDrain;
                        mana -= costDrain;
                    }
                    else if (type == ActionType.Missile)
                    {
                        boss -= 4;
                        cost += costMissile;
                        mana -= costMissile;
                    }
                    else if (type == ActionType.Poison)
                    {
                        poison = 6;
                        cost += costPoison;
                        mana -= costPoison;
                    }
                    else if (type == ActionType.Recharge)
                    {
                        recharge = 5;
                        cost += costRecharge;
                        mana -= costRecharge;
                    }
                    else if (type == ActionType.Shield)
                    {
                        shield = 6;
                        heroAr = 7;
                        cost += costShield;
                        mana -= costShield;
                    }
                }
                else
                {
                    hero -= Math.Max(1, bossAt - heroAr);
                }

                turn = !turn;
            }
        }
    }


    public class Day22 : IDay
    {
        public int Day => 22;
        public int Year => 2015;

        public void ProblemOne()
        {
        }

        public void ProblemTwo()
        {
        }
    }
}