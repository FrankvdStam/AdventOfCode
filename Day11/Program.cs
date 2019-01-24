using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] grid = new int[301, 301];
            int[,] threeByThreeGrid = new int[301, 301];

            for (int x = 1; x <= 300; x++)
            {
                for (int y = 1; y <= 300; y++)
                {
                    grid[x,y] = GetPowerLevel(x, y, 4172);
                }
            }

            int _x,_y;
            int highest = 0;
            for (int x = 1; x <= 300; x++)
            {
                for (int y = 1; y <= 300; y++)
                {
                    int value = GetThreeByThreeSum(x, y, grid);
                    if(value > highest)
                    {
                        highest = value;
                        _x = x;
                        _y = y;
                    }
                }
            }

            DrawGrid(10, 10, grid);
        }

        static int GetThreeByThreeSum(int x, int y, int[,] grid)
        {
            int sum = 0;

            for(int _x = x; _x < x+3; _x++)
            {
                for (int _y = y; _y < y + 3; _y++)
                {
                    if(_x < 301 && _y < 301)
                    {
                        sum += grid[x, y];
                    }
                }
            }

            return sum;
        }

        static void DrawGrid(int width, int height, int[,] grid)
        {
            for (int x = 1; x <= width; x++)
            {
                for (int y = 1; y <= height; y++)
                {
                    Console.CursorLeft = x;
                    Console.CursorTop = y;
                    Console.Write(grid[x, y] > 0 ? grid[x, y] : 0);
                }
            }
        }

        
            
            /*
            
    Find the fuel cell's rack ID, which is its X coordinate plus 10.
    Begin with a power level of the rack ID times the Y coordinate.
    Increase the power level by the value of the grid serial number (your puzzle input).
    Set the power level to itself multiplied by the rack ID.
    Keep only the hundreds digit of the power level (so 12345 becomes 3; numbers with no hundreds digit become 0).
    Subtract 5 from the power level.

        */
        static int GetPowerLevel(int x, int y, int gridSerialNumber)
        { 
            int rackId = x + 10;
            int powerLevel = rackId * y + gridSerialNumber;
            powerLevel *= rackId;
            powerLevel = powerLevel < 100 ? 0 : (powerLevel/100 ) % 10;
            powerLevel -= 5;
            return powerLevel;
        }
        
        /*

For example, to find the power level of the fuel cell at 3,5 in a grid with serial number 8:

    The rack ID is 3 + 10 = 13.
    The power level starts at 13 * 5 = 65.
    Adding the serial number produces 65 + 8 = 73.
    Multiplying by the rack ID produces 73 * 13 = 949.
    The hundreds digit of 949 is 9.
    Subtracting 5 produces 9 - 5 = 4.

So, the power level of this fuel cell is 4.
             */
         
    }
}
