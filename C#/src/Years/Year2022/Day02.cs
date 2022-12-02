using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;
using static System.Formats.Asn1.AsnWriter;

namespace Years.Year2022
{
    public class Day02 : BaseDay
    {
        public Day02() : base(2022, 2) 
        {
            foreach(var l in Input.Split("\n", StringSplitOptions.RemoveEmptyEntries))
            {
                var left = l[0] switch
                {
                    'A' => Rps.Rock,
                    'B' => Rps.Paper,
                    'C' => Rps.Scissors,
                    _ => throw new Exception(),
                };

                var right = l[2] switch
                {
                    'X' => Rps.Rock,
                    'Y' => Rps.Paper,
                    'Z' => Rps.Scissors,
                    _ => throw new Exception(),
                };

                _guide.Add((left, right));
            }
        }

        enum Rps
        {
            Rock,
            Paper,
            Scissors
        }

        private readonly Dictionary<Rps, (int score, Rps win)> _lookup = new Dictionary<Rps, (int score, Rps win)>()
        {
            { Rps.Rock      , (1, Rps.Scissors) },
            { Rps.Paper     , (2, Rps.Rock)     },
            { Rps.Scissors  , (3, Rps.Paper)    },
        };
        

        private List<(Rps left, Rps right)> _guide = new List<(Rps left, Rps right)>();


        private int PlayRound((Rps left, Rps right) round)
        {
            var score = _lookup[round.right].score;

            if (_lookup[round.right].win == round.left)
            {
                score += 6;
            }
            else if (round.right == round.left)
            {
                score += 3;
            }

            return score;
        }

        public override void ProblemOne()
        {
            var score = 0;
            foreach(var round in _guide)
            {
                score += PlayRound(round);
            }
            Console.WriteLine(score);
        }

        public override void ProblemTwo()
        {
            var score = 0;
            foreach (var round in _guide)
            {
                var move = round.right switch
                {
                    //Lose
                    Rps.Rock => _lookup[round.left].win,

                    //Draw
                    Rps.Paper => round.left,

                    //Win
                    Rps.Scissors => _lookup.Where(i => i.Value.win == round.left).First().Key,

                    _ => throw new Exception(),
                };

                var r = (round.left, move);
                score += PlayRound(r);
            }
            Console.WriteLine(score);
        }

        private const string Example = @"A Y
B X
C Z";
    }
}