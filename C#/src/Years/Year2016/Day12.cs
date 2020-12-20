using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Years.Utils;

namespace Years.Year2016
{
    public class Day12 : IDay
    {
        public int Day => 12;
        public int Year => 2016;


        private List<string> Registers = new List<string>() { "a", "b", "c", "d", };

        private Dictionary<string, int> RegisterValues = new Dictionary<string, int>()
        {
            {"a", 0},
            {"b", 0},
            {"c", 0},
            {"d", 0},
        };

        private static int ProgramCounter = 0;
        private static int cycles = 0;



        public void ProblemOne()
        {
            var lines = Input.Split(new string[] { "\r\n" }, StringSplitOptions.None);

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
            //PrintRegisters();
            Console.WriteLine(RegisterValues["a"]);
        }

        public void ProblemTwo()
        {
            Registers = new List<string>() { "a", "b", "c", "d", };
            RegisterValues = new Dictionary<string, int>()
            {
                {"a", 0},
                {"b", 0},
                {"c", 1},
                {"d", 0},
            };
            ProgramCounter = 0;
            cycles = 0;

            var lines = Input.Split(new string[] { "\r\n" }, StringSplitOptions.None);

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
            //PrintRegisters();
            Console.WriteLine(RegisterValues["a"]);
        }












        private void PrintRegisters()
        {
            Console.Clear();
            foreach (var pair in RegisterValues)
            {
                LogLine(pair.Key + ": " + pair.Value);
            }
            LogLine("Cycles: " + cycles);
        }

        private void Log(string message)
        {
            Console.Write(message);
            Debug.Write(message);
        }

        private void LogLine(string message)
        {
            Console.WriteLine(message);
            Debug.WriteLine(message);
        }

        private void Copy(string input)
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

        private void Increment(string input)
        {
            var bits = input.Split(' ');
            RegisterValues[bits[1]]++;
        }

        private void Decrement(string input)
        {
            var bits = input.Split(' ');
            RegisterValues[bits[1]]--;
        }

        private void JumpNotZero(string input)
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

        private void Jump(int amount)
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