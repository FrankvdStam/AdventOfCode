using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2018
{
    public class Day11 : IDay
    {
        public int Day => 11;
        public int Year => 2018;

       
        public void ProblemOne()
        {
            var grid = CreateGrid(Input);
            var (x, y, powerLevel) = FindMax(grid, 3);
            Console.WriteLine($"{x+1},{y+1}");
        }

        public void ProblemTwo()
        {
            var grid = CreateGrid(Input);

            var max = 0;
            var maxY = 0;
            var maxX = 0;
            var maxSize = 0;

            for (var size = 0; size <= 300; size++)
            {
                var (x, y, powerLevel) = FindMax(grid, size);
                if (powerLevel > max)
                {
                    max = powerLevel;
                    maxX = x;
                    maxY = y;
                    maxSize = size;
                }
                //Console.WriteLine(size);
            }
            Console.WriteLine($"{maxX + 1},{maxY + 1},{maxSize}");
        }

        private (int x, int y, int powerLevel) FindMax(int[,] grid, int size)
        {
            var max = 0;
            var maxX = 0;
            var maxY = 0;
            for (int y = 0; y < 300 - size; y++)
            {
                for (int x = 0; x < 300 - size; x++)
                {
                    var powerLevel = 0;
                    for (int gridY = 0; gridY < size; gridY++)
                    {
                        for (int gridX = 0; gridX < size; gridX++)
                        {
                            powerLevel += grid[x + gridX, y + gridY];
                        }
                    }

                    if (powerLevel > max)
                    {
                        max = powerLevel;
                        maxX = x;
                        maxY = y;
                    }
                }
            }
            //Our grid is 0 indexed, the puzzle is 1 indexed
            return (maxX, maxY, max);
        }
        
        


        private int PowerLevel(int x, int y, int gridSerialNumber)
        {
            //Find the fuel cell's rack ID, which is its X coordinate plus 10.
            var powerLevel = x + 10;
            //Begin with a power level of the rack ID times the Y coordinate.
            powerLevel *= y;
            //Increase the power level by the value of the grid serial number(your puzzle input).
            powerLevel += gridSerialNumber;
            //Set the power level to itself multiplied by the rack ID.
            powerLevel *= x + 10;
            //Keep only the hundreds digit of the power level(so 12345 becomes 3; numbers with no hundreds digit become 0).
            powerLevel = Math.Abs(powerLevel / 100 % 10);
            //Subtract 5 from the power level.
            powerLevel -= 5;

            return powerLevel;
        }

        private int[,] CreateGrid(int serialNumber)
        {
            var grid = new int[300, 300];
            for (int y = 0; y < 300; y++)
            {
                for (int x = 0; x < 300; x++)
                {
                    grid[x, y] = PowerLevel(x + 1, y + 1, Input);
                }
            }
            return grid;
        }

        private const int Input = 4172;
    }
}