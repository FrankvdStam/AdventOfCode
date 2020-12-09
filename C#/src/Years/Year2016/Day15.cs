using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2016
{
    public class Day15 : IDay
    {
        public int Day => 15;
        public int Year => 2016;

        public void ProblemOne()
        {
            var discs = ParseDiscs(Input);

            int t = 0;
            while (true)
            {
                if (DoesCapsuleFall(discs, t))
                {
                    Console.WriteLine(t);
                    return;
                }
                t++;
            }
        }


        public void ProblemTwo()
        {
            var discs = ParseDiscs(Input);

            //but a new disc with 11 positions and starting at position 0 has appeared exactly one second below the previously-bottom disc.
            discs.Add((0, 11));

            int t = 0;
            while (true)
            {
                if (DoesCapsuleFall(discs, t))
                {
                    Console.WriteLine(t);
                    return;
                }
                t++;
            }
        }



        private bool DoesCapsuleFall(List<(int position, int size)> discs, int t)
        {
            //At time plus one we check the first disc.
            for (int i = 0; i < discs.Count; i++)
            {
                int time = t + i + 1;
                //Remove all full revelations of discs, keeping only the relevant change
                int rest = time % discs[i].size;
                int position = discs[i].position + rest;
                if (position >= discs[i].size)
                {
                    position = position - discs[i].size;
                }

                if (position != 0)
                {
                    return false;
                }
            }
            return true;
        }


        private List<(int position, int size)> AdvanceDiscs(List<(int position, int size)> discs)
        {
            var nextDiscs = new List<(int position, int size)>();

            foreach (var disc in discs)
            {
                (int position, int size) newDisc = (disc.position + 1, disc.size);
                if (disc.position + 1 >= disc.size)
                {
                    newDisc.position = 0;
                }
                nextDiscs.Add(newDisc);
            }
            return nextDiscs;
        }



        private List<(int position, int size)> ParseDiscs(string input)
        {
            var discs = new List<(int, int)>();
            foreach (var s in input.SplitNewLine())
            {
                var bits = s.Split(' ');
                int positions       = int.Parse(bits[3]);
                int currentPosition = int.Parse(bits[11].Replace('.', ' '));
                discs.Add((currentPosition, positions));
            }

            return discs;
        }



        private const string Example = @"Disc #1 has 5 positions; at time=0, it is at position 4.
Disc #2 has 2 positions; at time=0, it is at position 1.";

        private const string Input = @"Disc #1 has 17 positions; at time=0, it is at position 1.
Disc #2 has 7 positions; at time=0, it is at position 0.
Disc #3 has 19 positions; at time=0, it is at position 2.
Disc #4 has 5 positions; at time=0, it is at position 0.
Disc #5 has 3 positions; at time=0, it is at position 0.
Disc #6 has 13 positions; at time=0, it is at position 5.";

    }
}