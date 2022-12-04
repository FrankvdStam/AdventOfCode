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

    public class Day14 : BaseDay
    {
        public Day14() : base(2015, 14) {}

        public override void ProblemOne()
        {
            var deer = ParseInput(Input.RemoveTrailingNewline());

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

        public override void ProblemTwo()
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
            var split = input.SplitNewLine();
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

#pragma warning disable CS0414
        private const string Example = @"Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.
Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.";
    }
}