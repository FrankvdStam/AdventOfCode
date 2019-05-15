using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();

            ParseInput(Example);
            ProblemOne();
            //ProblemTwo();
        }

        static void ProblemOne()
        {
            int input = 368078;
            int square = FindSquare(input);
            FindCoords(square, input, out int x, out int y, out int middle);

            int distance = Math.Abs(x - middle) + Math.Abs(y - middle); 

            Console.WriteLine($"Square is {square} with the middle at ({middle},{middle}) and the requested number {input} is at ({x},{y}) with a distance of {distance}");

        }

        static void ProblemTwo()
        {

        }


        static void Test()
        {
            int a1 = FindSquareSize(1);
            int a2 = FindSquareSize(7);
            int a3 = FindSquareSize(9);
            int a4 = FindSquareSize(12);
            int a5 = FindSquareSize(23);



            int b1 = FindSquare(1);
            int b2 = FindSquare(7);
            int b3 = FindSquare(9);
            int b4 = FindSquare(12);
            int b5 = FindSquare(23);
        }


        static int FindSquareSize(int maxNum)
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

        static int FindSquare(int num)
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


        static void FindCoords(int square, int num, out int x, out int y, out int middle)
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
            middle = ( (square -1) /2)+1;
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
        static int Aprox(int num, int sub)
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

        static void ParseInput(string input)
        {
            var lines = input.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                var bits = line.Split(' ');
            }
        }


        private static string Input = @"";
        private static string Example = @"";

    }
}
