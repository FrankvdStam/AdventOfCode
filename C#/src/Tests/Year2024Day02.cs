using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Years.Utils;

namespace Tests
{
    [TestFixture]
    public class Year2024Day02
    {
        
        private readonly Years.Year2024.Day02 _sut;

        public Year2024Day02()
        {
            _sut = new Years.Year2024.Day02();
        }


        [TestCase("7 6 4 2 1", true)]
        [TestCase("1 2 7 8 9", false)]
        [TestCase("9 7 6 2 1", false)]
        [TestCase("1 3 2 4 5", false)]
        [TestCase("8 6 4 4 1", false)]
        [TestCase("1 3 6 7 9", true)]
        public void SafeWithoutTolerance(string report, bool expected)
        {
            var levels = report.SplitWhitespace().Select(int.Parse).ToList();
            var result = _sut.CountErrors(levels) == 0;
            Assert.That(result, Is.EqualTo(expected));
        }

        //Community edge cases
        [TestCase("48 46 47 49 51 54 56", true)]
        [TestCase("1 1 2 3 4 5", true)]
        [TestCase("1 2 3 4 5 5", true)]
        [TestCase("5 1 2 3 4 5", true)]
        [TestCase("1 4 3 2 1", true)]
        [TestCase("1 6 7 8 9", true)]
        [TestCase("1 2 3 4 3", true)]
        [TestCase("9 8 7 6 7", true)]
        [TestCase("7 10 8 10 11", true)]
        [TestCase("29 28 27 25 26 25 22 20", true)]
        [TestCase("90 89 86 84 83 79", true)]
        [TestCase("97 96 93 91 85", true)]
        [TestCase("29 26 24 25 21", true)]
        [TestCase("36 37 40 43 47", true)]
        [TestCase("43 44 47 48 49 54", true)]
        [TestCase("35 33 31 29 27 25 22 18", true)]
        [TestCase("77 76 73 70 64", true)]
        [TestCase("68 65 69 72 74 77 80 83", true)]
        [TestCase("37 40 42 43 44 47 51", true)]
        [TestCase("70 73 76 79 86", true)]
        public void SafeWithTolerance(string report, bool expected)
        {
            var levels = report.SplitWhitespace().Select(int.Parse).ToList();
            var result = _sut.IsSafeWithTolerance(levels);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
