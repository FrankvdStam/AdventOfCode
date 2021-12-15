using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2016
{
    //Solution stolen from reddit user u/gusknows
    //https://www.reddit.com/r/adventofcode/comments/5hoia9/2016_day_11_solutions/

    public class Day11 : IDay
    {
        public int Day => 11;
        public int Year => 2016;

        public void ProblemOne()
        {
            int[] floorTest = { 0, 8, 2, 0, 0 };
            Console.WriteLine(FindMinimumMoves(floorTest));
        }

        /*
         An elerium generator.
        An elerium-compatible microchip.
        A dilithium generator.
        A dilithium-compatible microchip
         */
        public void ProblemTwo()
        {
            int[] floorTest = { 0, 12, 2, 0, 0 };
            Console.WriteLine(FindMinimumMoves(floorTest));
        }

        private int FindMinimumMoves(int[] floorTest)
        {
            int moveCount = 0;
            //int[] floorTest = {0, 5, 1, 4, 0};
            int totalPieces = floorTest.Sum();
            int elevatorPieces = Math.Min(floorTest[1], 2);
            floorTest[1] -= elevatorPieces;
            int currentFloor = 1;
            while (floorTest[4] + 1 != totalPieces)
            {
                // go down
                while (elevatorPieces < 2 && currentFloor > 1)
                {
                    currentFloor--;
                    int piecesTaken = Math.Min(floorTest[currentFloor], 2 - elevatorPieces);
                    if (piecesTaken > 0)
                    {
                        elevatorPieces += piecesTaken;
                        floorTest[currentFloor] -= piecesTaken;
                    }

                    moveCount++;
                }

                // go up
                while (currentFloor < 4)
                {
                    currentFloor++;
                    int piecesTaken = Math.Min(floorTest[currentFloor], 2 - elevatorPieces);
                    if (piecesTaken > 0)
                    {
                        elevatorPieces += piecesTaken;
                        floorTest[currentFloor] -= piecesTaken;
                    }

                    moveCount++;
                }

                floorTest[4] += 1;
                elevatorPieces--;
            }

            return moveCount;
        }

        //8 - 2 - 0 - 0

#pragma warning disable CS0414
        private string Input = @"The first floor contains a polonium generator, a thulium generator, a thulium-compatible microchip, a promethium generator, a ruthenium generator, a ruthenium-compatible microchip, a cobalt generator, and a cobalt-compatible microchip.
The second floor contains a polonium-compatible microchip and a promethium-compatible microchip.
The third floor contains nothing relevant.
The fourth floor contains nothing relevant.";
    }
}