using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    public class Reindeer
    {
        public string Name;
        public int Speed;
        public int Duration;
        public int RestDuration;
        public bool IsResting;
        public int RemainingSeconds;
        public int Position;
        public int Points = 0;
    }

    public class Day14 : IDay
    {
        public int Day => 14;
        public int Year => 2015;

        public void ProblemOne()
        {
            var deer = ParseInput(Input);

            for (int i = 0; i < 2503; i++)
            {
                MoveSecond(deer);
            }
            //PrintDeer(deer);
            var distance = deer.Max(i => i.Position);
            _points = deer.Max(i => i.Points);
            Console.WriteLine(distance);
        }

        private int _points;

        public void ProblemTwo()
        {
            Console.WriteLine(_points);
        }



        private void PrintDeer(List<Reindeer> deer)
        {
            Console.Clear();
            foreach (var d in deer)
            {
                Console.WriteLine($"{d.Name}: {d.Position} - {d.Points}");
            }
        }


        private void MoveSecond(List<Reindeer> deer)
        {
            foreach (var d in deer)
            {
                //Switch states when no more seconds remain
                if (d.RemainingSeconds <= 0)
                {
                    d.IsResting = !d.IsResting;

                    d.RemainingSeconds = d.IsResting ? d.RestDuration : d.Duration;
                }

                //Move or rest
                d.RemainingSeconds--;
                if (!d.IsResting)
                {
                    d.Position += d.Speed;
                }
            }

            foreach (var d in deer.Where(i => i.Position == deer.Max(j => j.Position)))
            {
                d.Points++;
            }
        }

        private void MoveSecondWithScore(List<Reindeer> deer)
        {
            foreach (var d in deer)
            {
                //Switch states when no more seconds remain
                if (d.RemainingSeconds <= 0)
                {
                    d.IsResting = !d.IsResting;

                    d.RemainingSeconds = d.IsResting ? d.RestDuration : d.Duration;
                }
                d.RemainingSeconds--;

                //Move or rest
                if (!d.IsResting)
                {
                    d.Position += d.Speed;
                }
            }

            foreach (var d in deer.Where(i => i.Position == deer.Max(j => j.Position)))
            {
                d.Points++;
            }
        }


        private List<Reindeer> ParseInput(string input)
        {
            var result = new List<Reindeer>();
            var split = input.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (string line in split)
            {
                var bits = line.Split(' ');
                Reindeer r = new Reindeer();
                r.Name = bits[0];
                r.Speed = Int32.Parse(bits[3]);
                r.Duration = Int32.Parse(bits[6]);
                r.RestDuration = Int32.Parse(bits[13]);
                r.RemainingSeconds = r.Duration;
                result.Add(r);
            }
            return result;
        }

        private string Example = @"Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.
Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.";


        public string Input = @"Dancer can fly 27 km/s for 5 seconds, but then must rest for 132 seconds.
Cupid can fly 22 km/s for 2 seconds, but then must rest for 41 seconds.
Rudolph can fly 11 km/s for 5 seconds, but then must rest for 48 seconds.
Donner can fly 28 km/s for 5 seconds, but then must rest for 134 seconds.
Dasher can fly 4 km/s for 16 seconds, but then must rest for 55 seconds.
Blitzen can fly 14 km/s for 3 seconds, but then must rest for 38 seconds.
Prancer can fly 3 km/s for 21 seconds, but then must rest for 40 seconds.
Comet can fly 18 km/s for 6 seconds, but then must rest for 103 seconds.
Vixen can fly 18 km/s for 5 seconds, but then must rest for 84 seconds.";
    }
}