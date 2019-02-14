using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day08
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProblemOne(Example, 7, 3);
            ProblemOne(Input, 50, 6);
            //ProblemTwo();
        }


        static bool[,] Copy(bool[,] boolArray)
        {
            bool[,] result = new bool[boolArray.GetLength(0), boolArray.GetLength(1)];
            for (int y = 0; y < boolArray.GetLength(0); y++)
            {
                for (int x = 0; x < boolArray.GetLength(1); x++)
                {
                    result[y, x] = boolArray[y, x];
                }
            }
            return result;
        }

        static void Draw(bool[,] pixels)
        {
            Console.Clear();
            for (int y = 0; y < pixels.GetLength(0); y++)
            {
                for (int x = 0; x < pixels.GetLength(1); x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(pixels[y, x] ? '#' : '.');
                }
            }
        }

        static bool[,] Rect(bool[,] pixels, int width, int height)
        {
            bool[,] newPixels = Copy(pixels);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    newPixels[y, x] = true;
                }
            }
            return newPixels;
        }

        static bool[,] RotateX(bool[,] pixels, int column, int amount)
        {
            int length = pixels.GetLength(0);
            int move = amount % length;
            bool[,] newPixels = Copy(pixels);

            //Clear column
            for (int y = 0; y < length; y++)
            {
                newPixels[y, column] = false;
            }

            //Fill column
            for (int y = 0; y < length; y++)
            {
                if (pixels[y, column])
                {
                    int newPos = 0;

                    if (y + move >= length)
                    {
                        newPos = y + move - length;
                    }
                    else
                    {
                        newPos = y + move;
                    }
                    newPixels[newPos, column] = true;
                }
            }

            return newPixels;
        }
        

        static bool[,] RotateY(bool[,] pixels, int row, int amount)
        {
            int length = pixels.GetLength(1);
            int move = amount % length;
            bool[,] newPixels = Copy(pixels);

            //Clear column
            for (int x = 0; x < length; x++)
            {
                newPixels[row, x] = false;
            }

            //Fill column
            for (int x = 0; x < length; x++)
            {
                if (pixels[row, x])
                {
                    int newPos = 0;

                    if (x + move >= length)
                    {
                        newPos = x + move - length;
                    }
                    else
                    {
                        newPos = x + move;
                    }
                    newPixels[row, newPos] = true;
                }
            }

            return newPixels;
        }
        

        

        static void ProblemOne(string input, int width, int height)
        {
            bool[,] pixels = new bool[height,width];
            
            var lines = input.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                var bits = line.Split(' ');
                switch (bits[0])
                {
                    case "rect":
                        var xy = bits[1].Split('x');
                        int x = int.Parse(xy[0]);
                        int y = int.Parse(xy[1]);
                        pixels = Rect(pixels, x, y);
                        break;
                    case "rotate":
                        int columnOrRow = int.Parse(bits[2].Substring(2, bits[2].Length - 2));
                        int amount = int.Parse(bits[4]);
                        if (bits[1] == "column")
                        {
                            pixels = RotateX(pixels, columnOrRow, amount);
                        }
                        else
                        {
                            pixels = RotateY(pixels, columnOrRow, amount);
                        }
                        break;
                }
            }

            Draw(pixels);
        }




        static void ProblemTwo(string input)
        {
            var lines = input.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                var bits = line.Split(' ');
            }
        }
        

        private static string Input = @"rect 1x1
rotate row y=0 by 7
rect 1x1
rotate row y=0 by 5
rect 1x1
rotate row y=0 by 5
rect 1x1
rotate row y=0 by 2
rect 1x1
rotate row y=0 by 3
rect 1x1
rotate row y=0 by 5
rect 1x1
rotate row y=0 by 3
rect 1x1
rotate row y=0 by 2
rect 1x1
rotate row y=0 by 3
rect 2x1
rotate row y=0 by 7
rect 6x1
rotate row y=0 by 3
rect 2x1
rotate row y=0 by 2
rect 1x2
rotate row y=1 by 10
rotate row y=0 by 3
rotate column x=0 by 1
rect 2x1
rotate column x=20 by 1
rotate column x=15 by 1
rotate column x=5 by 1
rotate row y=1 by 5
rotate row y=0 by 2
rect 1x2
rotate row y=0 by 5
rotate column x=0 by 1
rect 4x1
rotate row y=2 by 15
rotate row y=0 by 5
rotate column x=0 by 1
rect 4x1
rotate row y=2 by 5
rotate row y=0 by 5
rotate column x=0 by 1
rect 4x1
rotate row y=2 by 10
rotate row y=0 by 10
rotate column x=8 by 1
rotate column x=5 by 1
rotate column x=0 by 1
rect 9x1
rotate column x=27 by 1
rotate row y=0 by 5
rotate column x=0 by 1
rect 4x1
rotate column x=42 by 1
rotate column x=40 by 1
rotate column x=22 by 1
rotate column x=17 by 1
rotate column x=12 by 1
rotate column x=7 by 1
rotate column x=2 by 1
rotate row y=3 by 10
rotate row y=2 by 5
rotate row y=1 by 3
rotate row y=0 by 10
rect 1x4
rotate column x=37 by 2
rotate row y=3 by 18
rotate row y=2 by 30
rotate row y=1 by 7
rotate row y=0 by 2
rotate column x=13 by 3
rotate column x=12 by 1
rotate column x=10 by 1
rotate column x=7 by 1
rotate column x=6 by 3
rotate column x=5 by 1
rotate column x=3 by 3
rotate column x=2 by 1
rotate column x=0 by 1
rect 14x1
rotate column x=38 by 3
rotate row y=3 by 12
rotate row y=2 by 10
rotate row y=0 by 10
rotate column x=7 by 1
rotate column x=5 by 1
rotate column x=2 by 1
rotate column x=0 by 1
rect 9x1
rotate row y=4 by 20
rotate row y=3 by 25
rotate row y=2 by 10
rotate row y=0 by 15
rotate column x=12 by 1
rotate column x=10 by 1
rotate column x=8 by 3
rotate column x=7 by 1
rotate column x=5 by 1
rotate column x=3 by 3
rotate column x=2 by 1
rotate column x=0 by 1
rect 14x1
rotate column x=34 by 1
rotate row y=1 by 45
rotate column x=47 by 1
rotate column x=42 by 1
rotate column x=19 by 1
rotate column x=9 by 2
rotate row y=4 by 7
rotate row y=3 by 20
rotate row y=0 by 7
rotate column x=5 by 1
rotate column x=3 by 1
rotate column x=2 by 1
rotate column x=0 by 1
rect 6x1
rotate row y=4 by 8
rotate row y=3 by 5
rotate row y=1 by 5
rotate column x=5 by 1
rotate column x=4 by 1
rotate column x=3 by 2
rotate column x=2 by 1
rotate column x=1 by 3
rotate column x=0 by 1
rect 6x1
rotate column x=36 by 3
rotate column x=25 by 3
rotate column x=18 by 3
rotate column x=11 by 3
rotate column x=3 by 4
rotate row y=4 by 5
rotate row y=3 by 5
rotate row y=2 by 8
rotate row y=1 by 8
rotate row y=0 by 3
rotate column x=3 by 4
rotate column x=0 by 4
rect 4x4
rotate row y=4 by 10
rotate row y=3 by 20
rotate row y=1 by 10
rotate row y=0 by 10
rotate column x=8 by 1
rotate column x=7 by 1
rotate column x=6 by 1
rotate column x=5 by 1
rotate column x=3 by 1
rotate column x=2 by 1
rotate column x=1 by 1
rotate column x=0 by 1
rect 9x1
rotate row y=0 by 40
rotate column x=44 by 1
rotate column x=35 by 5
rotate column x=18 by 5
rotate column x=15 by 3
rotate column x=10 by 5
rotate row y=5 by 15
rotate row y=4 by 10
rotate row y=3 by 40
rotate row y=2 by 20
rotate row y=1 by 45
rotate row y=0 by 35
rotate column x=48 by 1
rotate column x=47 by 5
rotate column x=46 by 5
rotate column x=45 by 1
rotate column x=43 by 1
rotate column x=40 by 1
rotate column x=38 by 2
rotate column x=37 by 3
rotate column x=36 by 2
rotate column x=32 by 2
rotate column x=31 by 2
rotate column x=28 by 1
rotate column x=23 by 3
rotate column x=22 by 3
rotate column x=21 by 5
rotate column x=20 by 1
rotate column x=18 by 1
rotate column x=17 by 3
rotate column x=13 by 1
rotate column x=10 by 1
rotate column x=8 by 1
rotate column x=7 by 5
rotate column x=6 by 5
rotate column x=5 by 1
rotate column x=3 by 5
rotate column x=2 by 5
rotate column x=1 by 5";
        private static string Example = @"rect 3x2
rotate column x=1 by 1
rotate row y=0 by 4
rotate column x=1 by 1";

    }
}
