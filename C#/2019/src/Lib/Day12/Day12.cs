using System;
using System.Collections.Generic;
using System.Text;
using Lib.Shared;

namespace Lib.Day12
{
    public class Day12 : IDay
    {
        public int Day => 12;

        public void ProblemOne()
        {
            ParseInput(Example);

            
            while (time <= 100)
            {
                
            }
        }

        public List<Vector3i> Moons;
        public List<Vector3i> Velocity;
        int time = 0;

        public void Step(int amount = 1)
        {
            while (amount > 0)
            {
                CalculateGravity();
                UpdateMoons();
                time++;
                amount--;
            }
        }


        #region Helpers ========================================================================================================
        private static int GetAdjustment(int value1, int value2)
        {
            if (value1 > value2)
            {
                return -1;
            }

            if (value1 < value2)
            {
                return 1;
            }

            if (value1 == value2)
            {
                return 0;
            }
            throw new Exception();
        }

        private void CalculateGravity()
        {
            for (int i = 0; i < Moons.Count; i++)
            {
                var outer = Moons[i];
                for (int j = 0; j < Moons.Count; j++)
                {
                    var inner = Moons[j];

                    if (inner == outer)
                    {
                        continue;
                    }

                    var vel = Velocity[i];
                    vel.X += GetAdjustment(outer.X, inner.X);
                    vel.Y += GetAdjustment(outer.Y, inner.Y);
                    vel.Z += GetAdjustment(outer.Z, inner.Z);
                    Velocity[i] = vel;//Do I need this?
                }
            }
        }

        private void UpdateMoons()
        {
            for (int i = 0; i < Moons.Count; i++)
            {
                var moon = Moons[i];
                moon.X += Velocity[i].X;
                moon.Y += Velocity[i].Y;
                moon.Z += Velocity[i].Z;
                Moons[i] = moon;
            }
        }
        #endregion


        public void ProblemTwo()
        {
        }

        private void ParseInput(string input)
        {
            Moons = new List<Vector3i>();
            foreach (string s in input.Split('\n'))
            {
                var replace = s.Replace("<", string.Empty).Replace(">", string.Empty).Replace("=", string.Empty).Replace("x", string.Empty).Replace("y", string.Empty).Replace("z", string.Empty).Replace(" ", string.Empty);
                var numbers = replace.Split(',');
                int x = int.Parse(numbers[0]);
                int y = int.Parse(numbers[1]);
                int z = int.Parse(numbers[2]);
                Moons.Add(new Vector3i(x, y, z));
            }

            Velocity = new List<Vector3i>();
            for (int i = 0; i < Moons.Count; i++)
            {
                Velocity.Add(new Vector3i(0, 0, 0));
            }
        }

        private const string Example = @"<x=-8, y=-10, z=0>
<x=5, y=5, z=10>
<x=2, y=-7, z=3>
<x=9, y=-8, z=-3>";


        #region Test ========================================================================================================

        public void Test()
        {

        }

        #endregion
    }
}
