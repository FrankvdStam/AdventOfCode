using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2017
{
    public class Day17 : IDay
    {
        public int Day => 17;
        public int Year => 2017;

        public void ProblemOne()
        {
            var spinLock = new LinkedList<int>();
            spinLock.AddFirst(0);
            var insert = 1;
            
            var position = spinLock.First;




            for (int i = 0; i < 2017; i++)
            {
                //step forward x times
                for(int s = 0; s < Input; s++)
                {
                    position = position.Next ?? spinLock.First;
                }

                //Insert
                spinLock.AddAfter(position, insert);
                insert++;

                //Set position to the just inserted value
                position = position.Next;
            }

            var result = position.Next.Value;
            Console.WriteLine(result);
        }

        public void ProblemTwo()
        {
            var spinLock = new LinkedList<int>();
            spinLock.AddFirst(0);
            var insert = 1;

            var position = spinLock.First;


            for (int i = 0; i < 50000000; i++)
            {
                //step forward x times
                for (int s = 0; s < Input; s++)
                {
                    position = position.Next ?? spinLock.First;
                }

                //Insert
                spinLock.AddAfter(position, insert);
                insert++;

                //Set position to the just inserted value
                position = position.Next;
            }

            position = spinLock.First;
            while (position != null)
            {
                if (position.Value == 0)
                {
                    Console.WriteLine(position.Next.Value);
                    return;
                }

                position = position.Next;
            }

            //39170601
        }


        private const int Example = 3;
        private const int Input = 371;
    }
}