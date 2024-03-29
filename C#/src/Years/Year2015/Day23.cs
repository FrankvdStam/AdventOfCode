using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Years.Utils;

namespace Years.Year2015
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


    public class Day23 : IDay
    {
        public int Day => 23;
        public int Year => 2015;

        public void ProblemOne()
        {
            var instructions = ParseInput(Input);
            
            while (ProgramCounter >= 0 && ProgramCounter < instructions.Count)
            {
                RunInstruction(instructions[(int)ProgramCounter]);
            }
            Console.WriteLine($"{RegisterB}");
        }

        public void ProblemTwo()
        {
            var instructions = ParseInput(Input);

            RegisterA = 1;
            RegisterB = 0;
            ProgramCounter = 0;


            while (ProgramCounter >= 0 && ProgramCounter < instructions.Count)
            {
                RunInstruction(instructions[(int)ProgramCounter]);
            }
            Console.WriteLine($"{RegisterB}");
        }


        public long RegisterA;
        public long RegisterB;
        public long ProgramCounter;

        private long GetRegisterValue(Register register)
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

        private void SetRegisterValue(Register register, long value)
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

        private void RunInstruction(Instruction instruction)
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

        private List<Instruction> ParseInput(string input)
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

        private const string Input = @"jio a, +16
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


    }
}