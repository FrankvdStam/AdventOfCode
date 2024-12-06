using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Years.Year2017;

namespace Tests
{
    [TestFixture]
    public class Year2017Day10
    {
        private readonly Day10 _day10Instance = new Day10();


        [TestCase(""            , "a2582a3a0e66e6e86e3812dcb672a272")]
        [TestCase("AoC 2017"    , "33efeb34ea91902bb2f59c9920caa6cd")]
        [TestCase("1,2,3"       , "3efbe78a8d82f29979031a4aa0b16a9d")]
        [TestCase("1,2,4"       , "63960835bcdc130f0b66d7ff4f6a5a8e")]
        public void TestHash(string input, string expectedHash)
        {
            Assert.That(KnotHasher.Calculate(input), Is.EqualTo(expectedHash));
        }
    }
}
