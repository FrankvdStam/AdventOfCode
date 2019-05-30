using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    public static class Extension
    {
        public static void Print(this Dictionary<string, int> dictionary)
        {
            int maxLength = dictionary.Keys.Max(i => i.Length);
            foreach (KeyValuePair<string, int> pair in dictionary)
            {
                Console.WriteLine($"{pair.Key.PadRight(maxLength, ' ')}: {pair.Value}");
            }
        }
    }


    //TODO: regactor first register to be numeric optionally
    public class Instruction
    {
        public string Operator;
        public string FirstRegister;
        public string SecondRegister;
        public int FirstNumeric;
        public int SecondNumeric;
        public bool HasSecondRegister;
        public bool FirstRegisterIsConst;
        public bool SecondRegisterIsConst;


        public override string ToString()
        {
            string result = Operator;
            result += " ";
            result += FirstRegisterIsConst ? FirstNumeric.ToString() : FirstRegister;

            if (HasSecondRegister)
            {
                result += " ";
                result += SecondRegisterIsConst ? SecondNumeric.ToString() : SecondRegister;
            }

            return result;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var instructions = ParseInput(Input, out Dictionary<string, int> registers);
            int frequency = 0;
            int instructionPointer = 0;
            while (instructionPointer >= 0 && instructionPointer < instructions.Count)
            {
                Console.Clear();
                registers.Print();

                

                var instruction = instructions[instructionPointer];
                //Debug.WriteLine($"{instruction.Operator} - {registers["a"]}");
                switch (instruction.Operator)
                {
                    case "snd":
                        frequency = registers[instruction.FirstRegister];
                        break;
                    case "set":
                        if (instruction.SecondRegisterIsConst)
                        {
                            registers[instruction.FirstRegister] = instruction.SecondNumeric;
                        }
                        else
                        {
                            registers[instruction.FirstRegister] = registers[instruction.SecondRegister];
                        }
                        break;
                    case "add":
                        if (instruction.SecondRegisterIsConst)
                        {
                            registers[instruction.FirstRegister] += instruction.SecondNumeric;
                        }
                        else
                        {
                            registers[instruction.FirstRegister] += registers[instruction.SecondRegister];
                        }
                        break;
                    case "mul":
                        if (instruction.SecondRegisterIsConst)
                        {
                            registers[instruction.FirstRegister] *= instruction.SecondNumeric;
                        }
                        else
                        {
                            registers[instruction.FirstRegister] *= registers[instruction.SecondRegister];
                        }
                        break;
                    case "mod":
                        if (instruction.SecondRegisterIsConst)
                        {
                            registers[instruction.FirstRegister] = registers[instruction.FirstRegister] % instruction.SecondNumeric;
                        }
                        else
                        {
                            registers[instruction.FirstRegister] = registers[instruction.FirstRegister] % registers[instruction.SecondRegister];
                        }
                        break;
                    case "rcv":
                        if (registers[instruction.FirstRegister] != 0)
                        {
                            Console.Out.WriteLine($"Recovered: {frequency}");
                            Console.ReadKey();
                            return;
                        }
                        break;
                    case "jgz":
                        //Instread of having each switch case state have it's own programcounter increment, I only increment once.
                        //That means I need to subtract 1 from whatever position I'm jumping to here.
                        int first  = instruction.FirstRegisterIsConst  ? instruction.FirstNumeric  : registers[instruction.FirstRegister];
                        int second = instruction.SecondRegisterIsConst ? instruction.SecondNumeric : registers[instruction.SecondRegister];

                        if (first > 0)
                        {
                            instructionPointer += second - 1;
                        }
                        break;
                }

                Console.WriteLine(instruction);
                registers.Print();

                instructionPointer++;
//                Console.ReadKey();
            }
        }






        static List<Instruction> ParseInput(string input, out Dictionary<string, int> registers)
        {
            registers = new Dictionary<string, int>();
            List<Instruction> instructions = new List<Instruction>();

            var lines = input.Split(new string[]{"\r\n"}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                Instruction instruction = new Instruction();

                var bits = line.Split(' ');

                //Decode the instruction
                instruction.Operator = bits[0];

                if (int.TryParse(bits[1], out int num))
                {
                    instruction.FirstRegisterIsConst = true;
                    instruction.FirstNumeric = num;
                }
                else
                {
                    instruction.FirstRegisterIsConst = false;
                    instruction.FirstRegister = bits[1];

                    if (!registers.ContainsKey(instruction.FirstRegister))
                    {
                        registers.Add(instruction.FirstRegister, 0);
                    }
                }
                

                

                if (bits.Length > 2)
                {
                    instruction.HasSecondRegister = true;

                    if (int.TryParse(bits[2], out int number))
                    {
                        instruction.SecondRegisterIsConst = true;
                        instruction.SecondNumeric = number;
                    }
                    else
                    {
                        instruction.SecondRegisterIsConst = false;
                        instruction.SecondRegister = bits[2];

                        if (!registers.ContainsKey(instruction.SecondRegister))
                        {
                            registers.Add(instruction.SecondRegister, 0);
                        }
                    }
                }
                else
                {
                    instruction.HasSecondRegister = false;
                }

                instructions.Add(instruction);
            }

            return instructions;
        }


        public static string Input = @"set i 31
set a 1
mul p 17
jgz p p
mul a 2
add i -1
jgz i -2
add a -1
set i 127
set p 826
mul p 8505
mod p a
mul p 129749
add p 12345
mod p a
set b p
mod b 10000
snd b
add i -1
jgz i -9
jgz a 3
rcv b
jgz b -1
set f 0
set i 126
rcv a
rcv b
set p a
mul p -1
add p b
jgz p 4
snd a
set a b
jgz 1 3
snd b
set f 1
add i -1
jgz i -11
snd a
jgz f -16
jgz a -19";



        public static string Example = @"set a 1
add a 2
mul a a
mod a 5
snd a
set a 0
rcv a
jgz a -1
set a 1
jgz a -2";
    }
}
