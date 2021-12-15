using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    public class Day25 : IDay
    {
        public int Day => 25;
        public int Year => 2015;


        /*
            ============================================
            Find the row that this range starts in
            If we move left and down long enough, we will run into the start of the row eventually.
            We could translate that to the following where r is the amount of times we move

            (X, Y) + r(-1, 1) = (1, Yrow)
            
            remove loop
            (x, y) + ( -(x-1), (x-1) ) = (1, Yrow)

            remove x components
             
            y + (x-1) = Yrow
            
            ============================================
            Find the starting value of this field
            Amount of numbers from any row to the other axle is always the same as the row num

             12345
            112345
            22345 
            3345 
            445
            55

            this is a faculty but with addition.
            To find the actual order, we just calculate the rows addition faculty and add the remaining columns

            addition faculty can be writen as  ((n(n + 1)) / 2)
                        
            https://math.stackexchange.com/questions/593318/factorial-but-with-addition
            (the real answer is in the comments)
            -> Unless I'm misunderstanding the notation, this is not a correct answer. 
            I believe it should be ( ( n ( n + 1 ) ) / 2 ), not ( ( n + 1 ) / 2 ). 
            – Oliver Nicholls Aug 25 '17 at 9:37

            This gives us the following formula
            
            (((y + (x - 1)-1) * (y + (x - 1))) / 2) + x

            Simplify

            (((y + x - 2) * (y + x - 1)) / 2) + x
            */

 
        public void ProblemOne()
        {
            int x = 3029;
            int y = 2947;
            long order = (((y + x - 2) * (y + x - 1)) / 2) + x;

            long code = 20151125;
            for (long i = 1; i < order; i++)
            {
                code = (code * 252533) % 33554393;
            }
            Console.WriteLine(code);
        }

        public void ProblemTwo()
        {
            //There is no problem two...
        }
    }
}