using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23
{
    public enum InstructionType
    {
        hlf,
        tpl,
        inc,
        jmp,
        jie,
        jio
    }

    public enum Register
    {
        None,
        a,
        b,
    }

    public class Instruction
    {
        public InstructionType Type;
        public int Offset;
        public Register Register;

        public override string ToString()
        {
            string toString = Type.ToString();
            toString += Register == Register.None ? "" : " " + Register.ToString();
            toString += Offset == 0 ? "" : " " + Offset.ToString();
            return toString;
        }
    }

    class Program
    {
        private static string Input = @"jio a, +16
inc a
inc a
tpl a
tpl a
tpl a
inc a
inc a
tpl a
inc a
inc a
tpl a
tpl a
tpl a
inc a
jmp +23
tpl a
inc a
inc a
tpl a
inc a
inc a
tpl a
tpl a
inc a
inc a
tpl a
inc a
tpl a
inc a
tpl a
inc a
inc a
tpl a
inc a
tpl a
tpl a
inc a
jio a, +8
inc b
jie a, +4
tpl a
inc a
jmp +2
hlf a
jmp -7";


        static void Main(string[] args)
        {
            var instructions = ParseInput(Input);
            Stopwatch w = new Stopwatch();
            w.Start();
            while (ProgramCounter >= 0 && ProgramCounter < instructions.Count)
            {
                RunInstruction(instructions[(int)ProgramCounter]);
                Cycles++;
                
                if (Cycles % 100000 == 0)
                {
                    Console.WriteLine(Cycles);
                }
            }
            w.Stop();
            Console.WriteLine($"Finished {Cycles} in {w.Elapsed}.");
            Console.WriteLine($"A: {RegisterA} B: {RegisterB}");
            Console.ReadKey();
        }


        public static long RegisterA = 1;
        public static long RegisterB;
        public static long Cycles;
        public static long ProgramCounter;

        private static long GetRegisterValue(Register register)
        {
            if (register == Register.a)
            {
                return RegisterA;
            }
            else
            {
                return RegisterB;
            }
        }

        private static void SetRegisterValue(Register register, long value)
        {
            if (register == Register.a)
            {
                RegisterA = value;
            }
            else
            {
                RegisterB = value;
            }
        }

        private static void RunInstruction(Instruction instruction)
        {
            long value;
            switch (instruction.Type)
            {
                case InstructionType.hlf:
                    value = GetRegisterValue(instruction.Register);
                    SetRegisterValue(instruction.Register, value / 2);
                    ProgramCounter++;
                    break;
                case InstructionType.tpl:
                    value = GetRegisterValue(instruction.Register);
                    SetRegisterValue(instruction.Register, value * 3);
                    ProgramCounter++;
                    break;
                case InstructionType.inc:
                    value = GetRegisterValue(instruction.Register);
                    SetRegisterValue(instruction.Register, value + 1);
                    ProgramCounter++;
                    break;
                case InstructionType.jmp:
                    ProgramCounter += instruction.Offset;
                    break;
                case InstructionType.jie:
                    if (GetRegisterValue(instruction.Register) % 2 == 0)
                    {
                        ProgramCounter += instruction.Offset;
                    }
                    else
                    {
                        ProgramCounter++;
                    }
                    break;
                case InstructionType.jio:
                    if (GetRegisterValue(instruction.Register) == 1)
                    {
                        ProgramCounter += instruction.Offset;
                    }
                    else
                    {
                        ProgramCounter++;
                    }
                    break;
            }
        }
        
        private static List<Instruction> ParseInput(string input)
        {
            var instructions = new List<Instruction>();
            var lines = input.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                var bits = line.Replace(",", "").Split(' ');
                if (!Enum.TryParse(bits[0], out InstructionType type))
                {
                    throw new Exception();
                }

                Instruction instruction = new Instruction();
                instruction.Type = type;

                List<InstructionType> registerFirstArgument = new List<InstructionType>()
                {
                    InstructionType.hlf,
                    InstructionType.tpl,
                    InstructionType.inc,
                    InstructionType.jie,
                    InstructionType.jio,
                };

                List<InstructionType> offsetFirstArgument = new List<InstructionType>()
                {
                    InstructionType.jmp,
                };

                List<InstructionType> offsetSecondArgument = new List<InstructionType>()
                {
                    InstructionType.jie,
                    InstructionType.jio,
                };

                //Instructions with the register as second argument
                if (registerFirstArgument.Contains(type))
                {
                    if (!Enum.TryParse(bits[1], out Register register))
                    {
                        throw new Exception();
                    }
                    instruction.Register = register;
                }

                //Instructions with the offset as first argument
                if (offsetFirstArgument.Contains(type))
                {
                    instruction.Offset = int.Parse(bits[1]);
                }

                //instructions with the offset as 2nd argument
                if (offsetSecondArgument.Contains(type))
                {
                    instruction.Offset = int.Parse(bits[2]);
                }
                instructions.Add(instruction);
            }

            return instructions;
        }
    }
}
