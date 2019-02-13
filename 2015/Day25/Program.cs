using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace Day25
{
    class Program
    {
        //Unrolled version
        static void Main(string[] args)
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
            Console.ReadKey();
        }








        //Enter the code at row 2947, column 3029.
        static void Main2(string[] args)
        {
            //long order = GetOrder(3029, 2947);
            long order = GetOrder(3029, 2947);

            long code = 20151125;
            for (long i = 1; i < order; i++)
            {
                code = NextCode(code);
            }

        }

        static long NextCode(long previousCode)
        {
            return (previousCode * 252533) % 33554393;
        }
        
        static long GetOrder(int x, int y)
        {
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

            if (x < 1 || y < 1)
            {
                throw new Exception("Input must be 1 or higher.");
            }

            return (((y + x - 2) * (y + x - 1)) / 2) + x;
        }
    }
}
