﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProblemOne(Input);
            ProblemTwo(Input);
            //ProblemTwo();
        }

        static List<string> Registers = new List<string>(){"a", "b", "c", "d", };

        private static Dictionary<string, int> RegisterValues = new Dictionary<string, int>()
        {
            {"a", 0},
            {"b", 0},
            {"c", 0},
            {"d", 0},
        };

        private static int ProgramCounter = 0;
        private static int cycles = 0;

        static void ProblemTwo(string input)
        {
            RegisterValues["c"] = 1;
            ProblemOne(input);
        }

        static void ProblemOne(string input)
        {
            var lines = input.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            while (ProgramCounter >= 0 && ProgramCounter < lines.Length)
            {
                var line = lines[ProgramCounter];

                var bits = line.Split(' ');
                switch (bits[0])
                {
                    case "cpy":
                        Copy(line);
                        ProgramCounter++;
                        break;
                    case "inc":
                        Increment(line);
                        ProgramCounter++;
                        break;
                    case "dec":
                        Decrement(line);
                        ProgramCounter++;
                        break;
                    case "jnz":
                        JumpNotZero(line);
                        break;
                }

                cycles++;
                //PrintRegisters();
            }

            //Print results:
            PrintRegisters();
        }

        static void PrintRegisters()
        {
            Console.Clear();
            foreach (var pair in RegisterValues)
            {
                LogLine(pair.Key + ": " + pair.Value);
            }
            LogLine("Cycles: " + cycles);
        }

        static void Log(string message)
        {
            Console.Write(message);
            Debug.Write(message);
        }

        static void LogLine(string message)
        {
            Console.WriteLine(message);
            Debug.WriteLine(message);
        }

        static void Copy(string input)
        {
            var bits = input.Split(' ');
            if (Registers.Contains(bits[1]))
            {
                RegisterValues[bits[2]] = RegisterValues[bits[1]];
            }
            else
            {
                RegisterValues[bits[2]] = int.Parse(bits[1]);
            }
        }

        static void Increment(string input)
        {
            var bits = input.Split(' ');
            RegisterValues[bits[1]]++;
        }

        static void Decrement(string input)
        {
            var bits = input.Split(' ');
            RegisterValues[bits[1]]--;
        }

        static void JumpNotZero(string input)
        {
            var bits = input.Split(' ');

            //Get the value
            int value;
            if (Registers.Contains(bits[1]))
            {
                value = RegisterValues[bits[1]];
            }
            else
            {
                value = int.Parse(bits[1]);
            }

            //Jump if not zero
            if (value != 0)
            {
                ProgramCounter += int.Parse(bits[2]);
            }
            else
            {
                //Don't get stuck on the same instruction for ever..
                ProgramCounter++;
            }
        }

        static void Jump(int amount)
        {

        }


        private static string Input = @"cpy 1 a
cpy 1 b
cpy 26 d
jnz c 2
jnz 1 5
cpy 7 c
inc d
dec c
jnz c -2
cpy a c
inc a
dec b
jnz b -2
cpy c b
dec d
jnz d -6
cpy 13 c
cpy 14 d
inc a
dec d
jnz d -2
dec c
jnz c -5";

        private static string Example = @"cpy 41 a
inc a
inc a
dec a
jnz a 2
dec a";

    }
}
