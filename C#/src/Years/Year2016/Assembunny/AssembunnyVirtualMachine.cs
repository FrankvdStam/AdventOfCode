using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using Years.Utils;

namespace Years.Year2016.Assembunny
{



    public class AssembunnyVirtualMachine
    {
        public AssembunnyVirtualMachine(string program)
        {
            _instructions = Parse(program);
        }


        public void Run()
        {
            while (_programCounter > 0 && _programCounter < _instructions.Count)
            {
                Step();
            }
        }


        public void Step()
        {
            if (_programCounter >= _instructions.Count)
            {
                return;
            }

            var instruction = _instructions[_programCounter];

            switch (instruction.Opcode)
            {
                case Opcode.Cpy:
                    if (instruction.FirstRegister != null)
                    {
                        _registerValues[instruction.SecondRegister.Value] = _registerValues[instruction.FirstRegister.Value];
                    }
                    else
                    {
                        _registerValues[instruction.SecondRegister.Value] = instruction.FirstNumber.Value;
                    }
                    _programCounter++;
                    break;

                case Opcode.Inc:
                    _registerValues[instruction.FirstRegister.Value]++;
                    _programCounter++;
                    break;

                case Opcode.Dec:
                    _registerValues[instruction.FirstRegister.Value]--;
                    _programCounter++;
                    break;

                case Opcode.Jnz:

                    //Get the value
                    int value;
                    if (instruction.FirstRegister != null)
                    {
                        value = _registerValues[instruction.FirstRegister.Value];
                    }
                    else
                    {
                        value = instruction.FirstNumber.Value;
                    }

                    //Jump if not zero
                    if (value != 0)
                    {
                        _programCounter += instruction.SecondNumber.Value;
                    }
                    else
                    {
                        //Don't get stuck on the same instruction for ever..
                        _programCounter++;
                    }
                    break;

                case Opcode.Tgl:
                    throw new Exception();
            }
        }


        private enum Opcode
        {
            Cpy,
            Inc,
            Dec,
            Jnz,
            Tgl,
        }

        private class Instruction
        {
            public Opcode Opcode { get; set; }
            public char? FirstRegister { get; set; }
            public char? SecondRegister { get; set; }
            public int? FirstNumber { get; set; }
            public int? SecondNumber { get; set; }
        }

        private List<Instruction> _instructions;


        private Dictionary<char, int> _registerValues = new Dictionary<char, int>()
        {
            {'a', 0},
            {'b', 0},
            {'c', 0},
            {'d', 0},
        };

        private int _programCounter = 0;
        private int _cycles = 0;


        private List<Instruction> Parse(string input)
        {
            var instructions = new List<Instruction>();
            foreach (var line in input.SplitNewLine())
            {
                var bits = line.Split(' ');
                var instruction = new Instruction();


                //Decoding arguments first so that they only have to be decoded once

                //There's always one argument
                if (int.TryParse(bits[1], out int firstNumber))
                {
                    instruction.FirstNumber = firstNumber;
                }
                else
                {
                    instruction.FirstRegister = bits[1][0];
                }

                //If there's a second argument..
                if (bits.Length > 2)
                {
                    if (int.TryParse(bits[2], out int secondNumber))
                    {
                        instruction.SecondNumber = secondNumber;
                    }
                    else
                    {
                        instruction.SecondRegister = bits[2][0];
                    }
                }

                switch (bits[0])
                {
                    default:
                        throw new Exception($"Unknown instruction {line}");
                    case "cpy":
                        instruction.Opcode = Opcode.Cpy;
                        break;
                    case "inc":
                        instruction.Opcode = Opcode.Inc;
                        break;
                    case "dec":
                        instruction.Opcode = Opcode.Dec;
                        break;
                    case "jnz":
                        instruction.Opcode = Opcode.Jnz;
                        break;
                    case "tgl":
                        instruction.Opcode = Opcode.Tgl;
                        break;
                }

                
                instructions.Add(instruction);
            }
            return instructions;
        }
    }
}
