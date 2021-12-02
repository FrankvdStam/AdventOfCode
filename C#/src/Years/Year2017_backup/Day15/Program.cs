using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    public class Generator
    {
        public Generator(UInt64 startingValue, UInt64 multiplicationFactor, UInt64 multipleOf)
        {
            StartingValue = startingValue;
            MultiplicationFactor = multiplicationFactor;
            MultipleOf = multipleOf;
            PreviousValue = startingValue;
        }

        public UInt64 Next()
        {
            UInt64 next = (PreviousValue * MultiplicationFactor) % Mod;
            PreviousValue = next;
            return next;
        }

        public UInt64 Part2Next()
        {
            while (true)
            {
                UInt64 next = (PreviousValue * MultiplicationFactor) % Mod;
                PreviousValue = next;
                if (next % MultipleOf == 0)
                {
                    return next;
                }
            }
        }

        private UInt64 StartingValue;
        private UInt64 MultiplicationFactor;
        private UInt64 Mod = 2147483647;
        private UInt64 MultipleOf;
        private UInt64 PreviousValue;
    }


    class Program
    {
        static void Main(string[] args)
        {
            test2();

            Generator a = new Generator(65,   16807, 4);
            Generator b = new Generator(8921, 48271, 8);

            /*
            Generator a = new Generator(116  , 16807, 4);
            Generator b = new Generator(299  , 48271, 8);
            */
            int score = 0;

            for (int i = 0; i < 40_000_000; i++)
            {
                if (Judge(a.Next(), b.Next()))
                {
                    score++;
                }

                if (i % 1_000_000 == 0)
                {
                    Console.WriteLine(i);
                }
            }
            Console.WriteLine($"Score: {score}");

            score = 0;
            for (int i = 0; i < 5_000_000; i++)
            {
                if (i == 1056)
                {

                }

                if (Judge(a.Part2Next(), b.Part2Next()))
                {
                    score++;
                }

                if (i % 1_000_000 == 0)
                {
                    Console.WriteLine(i);
                }
            }
            Console.WriteLine($"Score: {score}");

            Console.ReadKey();
        }

        static bool Judge(UInt64 a, UInt64 b)
        {
            return ((UInt16)a) == ((UInt16)b);
        }


        static void test()
        {
            Generator a = new Generator(65, 16807, 4);
            var a1 = a.Next();
            var a2 = a.Next();
            var a3 = a.Next();
            var a4 = a.Next();
            var a5 = a.Next();
        }


        static void test2()
        {
            Generator a = new Generator(65, 16807, 4);
            var a1 = a.Part2Next();
            var a2 = a.Part2Next();
            var a3 = a.Part2Next();
            var a4 = a.Part2Next();
            var a5 = a.Part2Next();

            Generator b = new Generator(8921, 48271, 8);
            var b1 = b.Part2Next();
            var b2 = b.Part2Next();
            var b3 = b.Part2Next();
            var b4 = b.Part2Next();
            var b5 = b.Part2Next();
        }
    }
}
