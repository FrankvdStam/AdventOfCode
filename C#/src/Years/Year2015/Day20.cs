using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{

    class Program20
    {

        static void Main(string[] args)
        {
            const int input = 29000000;
            int[] houses = new int[input];

            int elf = 1;
            while (elf < input)
            {
                int position = elf;
                int presents = elf * 11;
                //Visit 50 houses.
                for (int i = 0; i < 50; i++)
                {
                    if (position < houses.Length)
                    {
                        houses[position] += presents;
                        position += elf;
                    }
                }
                elf++;
            }

            //776160 too high
            for (int i = 0; i < houses.Length; i++)
            {
                if (houses[i] >= input)
                {

                }
            }
        }

        //Prob 1
        static void Main2(string[] args)
        {
            const int input = 29000000;
            int[] houses = new int[input];

            int elf = 1;
            while (elf < input)
            {
                int position = elf;
                int presents = elf * 10;
                while (position < input)
                {
                    houses[position] += presents;
                    position += elf;
                }
                elf++;
            }


            for (int i = 0; i < houses.Length; i++)
            {
                if (houses[i] >= input)
                {

                }
            }
        }
    }


    public class Day20 : IDay
    {
        public int Day => 20;
        public int Year => 2015;

        public void ProblemOne()
        {
        }

        public void ProblemTwo()
        {
        }
    }
}