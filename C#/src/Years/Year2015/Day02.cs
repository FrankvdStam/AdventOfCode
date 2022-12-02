using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    public class Box
    {
        public Box(int length, int width, int height)
        {
            Length = length;
            Width = width;
            Height = height;

            List<int> sides = new List<int>();
            sides.Add(length);
            sides.Add(Width);
            sides.Add(Height);

            int smallestSide = sides.Min();
            sides.Remove(smallestSide);
            int secondSmallestSide = sides.Min();
            sides = null;

            List<int> sizes = new List<int>();
            sizes.Add(Length * Width);
            sizes.Add(Width * Height);
            sizes.Add(Height * Length);

            int smallestSurface = sizes.Min();

            for (int i = 0; i < sizes.Count; i++)
            {
                sizes[i] = 2 * sizes[i];
            }

            RequiredRibbonLength = (smallestSide + smallestSide + secondSmallestSide + secondSmallestSide) + (Length * Width * Height);
            RequiredWappingPaper = sizes.Sum() + smallestSurface;
        }


        public readonly int Length;
        public readonly int Width;
        public readonly int Height;
        public readonly int RequiredRibbonLength;
        public readonly int RequiredWappingPaper;

    }

    public class Day02 : BaseDay
    {
        public Day02() : base(2015, 02)
        {
            var lines = Input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                var split = line.Split(new string[] { "x" }, StringSplitOptions.None);
                int length = int.Parse(split[0]);
                int width = int.Parse(split[1]);
                int height = int.Parse(split[2]);
                _boxes.Add(new Box(length, width, height));
            }

        }

        private readonly List<Box> _boxes = new List<Box>();

        public override void ProblemOne()
        {
            Console.WriteLine(_boxes.Sum(i => i.RequiredWappingPaper));
        }

        public override void ProblemTwo()
        {
            Console.WriteLine(_boxes.Sum(i => i.RequiredRibbonLength));
        }
    }
}