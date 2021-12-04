using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2017
{
    public class Day15 : IDay
    {
        private class Generator
        {
            public Generator(long multiplier, long divisor, long initial)
            {
                _multiplier = multiplier;
                _divisor = divisor;
                _previous = initial;
            }

            public long Next()
            {
                _previous = (_previous * _multiplier) % 2147483647;
                return _previous;
            }

            public long NextDiviseAble()
            {
                while (true)
                {
                    var value = Next();
                    if (value % _divisor == 0)
                    {
                        return value;
                    }
                }
            }

            private long _multiplier;
            private long _divisor;
            private long _previous;
        }


        public int Day => 15;
        public int Year => 2017;

        public void ProblemOne()
        {
            ParseInput(Input, out int initialA, out int initialB);

            var generatorA = new Generator(16807, 0, initialA);
            var generatorB = new Generator(48271, 0, initialB);

            var count = 0;
            for (int i = 0; i < 40_000_000; i++)
            {
                var a = generatorA.Next();
                var b = generatorB.Next();

                if ((a & (long) 0xffff) == (b & (long) 0xffff))
                {
                    count++;
                }
            }
            Console.WriteLine(count);
        }

        public void ProblemTwo()
        {
            ParseInput(Input, out int initialA, out int initialB);

            var generatorA = new Generator(16807, 4, initialA);
            var generatorB = new Generator(48271, 8, initialB);

            var count = 0;
            for (int i = 0; i < 5_000_000; i++)
            {
                var a = generatorA.NextDiviseAble();
                var b = generatorB.NextDiviseAble();

                if ((a & (long)0xffff) == (b & (long)0xffff))
                {
                    count++;
                }
            }
            Console.WriteLine(count);
        }


        private void ParseInput(string input, out int a, out int b)
        {
            var split = input.SplitNewLine();
            a = int.Parse(split[0].Split(' ')[4]);
            b = int.Parse(split[1].Split(' ')[4]);
        }

        private const string Example = @"Generator A starts with 65
Generator B starts with 8921";
        private const string Input = @"Generator A starts with 116
Generator B starts with 299";
    }
}