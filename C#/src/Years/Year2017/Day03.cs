using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2017
{
    public class Day03 : IDay
    {
        public int Day => 3;
        public int Year => 2017;

        public void ProblemOne()
        {
            int input = 368078;
            int square = FindSquare(input);
            FindCoords(square, input, out int x, out int y, out int middle);

            int distance = Math.Abs(x - middle) + Math.Abs(y - middle);

            Console.WriteLine(distance);

        }

        public void ProblemTwo()
        {
            //For this problem, math won't cut it anymore and we have to brute force it by calculating the memory.

            int square = 9;
            int middle = ((square - 1) / 2) + 1;
            int x = middle;
            int y = middle;


            int[,] sheet = new int[square, square];



            sheet[x - 1, y - 1] = 1;
            x++;
            sheet[x - 1, y - 1] = 1;


            int step = 3;

            while (step < Math.Pow(square, 2) + 1)
            {
                int currentSquare = FindFirstSquare(step);
                FindCoords(currentSquare, step, out x, out y, out int currentMiddle);
                int difference = middle - currentMiddle;
                x += difference;
                y += difference;

                int surroundingSum = SumSurrounding(sheet, x - 1, y - 1);
                sheet[x - 1, y - 1] = surroundingSum;
                step++;
            }

            var result = sheet[0, 0];
            Console.WriteLine(result);
            //PrintArray(sheet);
        }





        static void PrintArray(int[,] array)
        {
            for (int y = 0; y < array.GetLength(1); y++)
            {
                for (int x = 0; x < array.GetLength(0); x++)
                {
                    Console.Write(array[x, y]);
                    Console.Write('\t');
                }
                Console.Write('\n');
            }
        }

        #region Problem two ========================================================================================================

        #region Sum surrounding fields ========================================================================================================
        private readonly int[,] SurroundingTranslation =
        {
            //left
            { -1,  0  },
            //up
            {  0,  -1 },
            //right
            {  1,  0  },
            //down
            {  0,  1  },
            
            //left up
            { -1, -1  },
            //right-up
            {  1, -1  },
            //left down
            { -1,  1  },
            //right down
            {  1,  1  },
        };


        private int SumSurrounding(int[,] sheet, int x, int y)
        {
            int sum = 0;

            //For every direction
            for (int i = 0; i < SurroundingTranslation.GetLength(0); i++)
            {
                int _x = x + SurroundingTranslation[i, 0];
                int _y = y + SurroundingTranslation[i, 1];

                //Check if we are still in bounds of the array
                if (_x >= 0 && _x < sheet.GetLength(0) && _y >= 0 && _y < sheet.GetLength(1))
                {
                    sum += sheet[_x, _y];
                }
            }

            return sum;
        }
        #endregion

        private int FindFirstSquare(int steps)
        {
            //Find the first square size that contains our step by finding the first odd-number square
            int i = 1;
            while (true)
            {
                if (i % 2 != 0)
                {
                    int square = i * i;
                    if (square >= steps)
                    {
                        return i;
                    }
                }
                else
                {
                    //Make i odd
                    i++;
                }

                //Skip 1 cycle of even numbers by incrementing by 2
                i += 2;
            }
        }
        #endregion


        #region Problem one ========================================================================================================



        private int FindSquareSize(int maxNum)
        {
            /*
             There is a relation to the maximum number a memory square can hold and the width and height it has.

            Max num: 1
            Formula: 1x1 = 1
            1

            Max num: 9
            Formula: 3x3 = 9
            543
            612
            789

            So in order to calculate the square of the max number we need to find in it, we have to find the first integer root that is equal or bigger then maxNum. 
            */
            while (true)
            {
                double root = Math.Sqrt(maxNum);
                //If this root has no decimals AND it is not an even number (center point remains calculatable
                if (root % 1 == 0 && root % 2 != 0)
                {
                    return (int)root;
                }

                maxNum++;
            }

        }

        private int FindSquare(int num)
        {
            /*

            Edit: ended up not needing to calculate the size, finding the exact square where the number resides is kind of the same

             There is a relation to the maximum number a memory square can hold and the width and height it has.

            Max num: 1
            Formula: 1x1 = 1
            1

            Max num: 9
            Formula: 3x3 = 9
            543
            612
            789

            So in order to calculate the square of the max number we need to find in it, we have to find the first integer root that is equal or bigger then maxNum. 
            */


            /*
            Instead of generating a square memory and finding the position of our number,
            we can use the properties we already know to get closer to it first.

            We can calculate the square that our number resides in (so that we may skip all previous squares)
            and search in that specific square.

            We can do this by calculating the max number we can store in that square.
            We are interested in the first square that can contain it. We can find that by squaring a number starting at 1 and incrementing by 2
             */

            int i = 1;
            while (true)
            {
                int square = i * i;
                if (square >= num)
                {
                    return i;
                }

                i += 2;
            }
        }


        private void FindCoords(int square, int num, out int x, out int y, out int middle)
        {
            //The previous square size is always 2 less then the square size unless the square size is 1. (this is just another math property of the square memory)
            //The exit number is the largest number of the previous square - we know that this number resides in the bottom right corner, 1 off the edge (so square size x -1 and square size y -1)
            int exitNumber;
            int previousSquareSize;
            if (square == 1)
            {
                exitNumber = 1;
                previousSquareSize = 1;
                throw new Exception();
            }
            else
            {
                previousSquareSize = square - 2;
                exitNumber = previousSquareSize * previousSquareSize;
            }

            //The amount of steps I have to take left is the difference between my num and my exit number
            int steps = num - exitNumber;

            //Now we can calculate what to do with x and y starting at the middle.
            //These values will only make sense after drawing it out.

            //Coordinate of the middle (don't need seperate x/y because they are always the same
            middle = ((square - 1) / 2) + 1;
            x = square;
            y = square;


            int temp;
            //Every side of the square in terms of steps is actually it's previous size plus 1
            int side = previousSquareSize + 1;

            //Format:
            //Calculate amount of steps to take in temp
            //Subtract taken steps from steps
            //add the move to the coordinates

            //Go up
            temp = Aprox(side, steps);
            steps -= temp;
            y -= temp;

            //Go left
            temp = Aprox(side, steps);
            steps -= temp;
            x -= temp;

            //Go down
            temp = Aprox(side, steps);
            steps -= temp;
            y += temp;

            //Go right
            temp = Aprox(side, steps);
            steps -= temp;
            x += temp;
        }

        //IDK if there is an official formula or term for this type of logic
        private int Aprox(int num, int sub)
        {
            if (sub == 0)
            {
                return 0;
            }

            if (sub >= num)
            {
                return num;
            }
            else
            {
                return sub;
            }
        }
        #endregion
    }
}