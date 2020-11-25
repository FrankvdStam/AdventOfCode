﻿using Lib.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Day02
{
    public class Day02 : IDay
    {
        public int Day => 2;


        public void ProblemOne()
        {
            IntCodeComputer computer = new IntCodeComputer(Input);
            computer.Program[1] = 12;
            computer.Program[2] = 2;
            computer.PrintDecompiledInstructions = true;
            computer.Run();
            long value = computer.Program[0];
            Console.WriteLine($"Halted with position 0: {value}");
            Console.ReadKey();
        }

        public void ProblemTwo()
        {
            IntCodeComputer computer = new IntCodeComputer();
            List<long> program = IntCodeComputer.ParseProgram(Input);

            for (int x = 0; x <= 99; x++)
            {
                for (int y = 0; y <= 99; y++)
                {
                    computer.Program = program.Clone();
                    computer.Program[1] = x;
                    computer.Program[2] = y;
                    computer.Run();
                    Console.WriteLine($"({x}, {y})");
                    long value = computer.Program[0];

                    if (value == 19690720)
                    {
                        //516000

                    }

                }
            }
        }

        

        
        
        private string Example = @"1,9,10,3,2,3,11,0,99,30,40,50";

        private string Input = @"1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,6,1,19,2,19,13,23,1,23,10,27,1,13,27,31,2,31,10,35,1,35,9,39,1,39,13,43,1,13,43,47,1,47,13,51,1,13,51,55,1,5,55,59,2,10,59,63,1,9,63,67,1,6,67,71,2,71,13,75,2,75,13,79,1,79,9,83,2,83,10,87,1,9,87,91,1,6,91,95,1,95,10,99,1,99,13,103,1,13,103,107,2,13,107,111,1,111,9,115,2,115,10,119,1,119,5,123,1,123,2,127,1,127,5,0,99,2,14,0,0";
    }
}
