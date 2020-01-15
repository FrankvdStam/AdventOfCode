﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Day03;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Tests
{
    [TestFixture]
    public class Day03Tests
    {


        [TestCase("R8,U5,L5,D3", "U7,R6,D4,L4", 6, 30)]
        [TestCase("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", 159, 610)]
        [TestCase("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 135, 410)]
        public void TestExampleProgram(string line1, string line2, int expectedDistance, int expectedSteps)
        {
            Assert.AreEqual(expectedDistance, Day03.FindMinimumManhattanDistanceOfIntersectedLines(line1, line2));
            Assert.AreEqual(expectedSteps, Day03.FindMinimumNumberOfStepsToIntersection(line1, line2));
        }
    }
}
