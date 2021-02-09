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
            while (_programCounter >= 0 && _programCounter < _instructions.Count)
            {
                //Console.WriteLine($"{_programCounter} a:{RegisterValues['a']} b:{RegisterValues['b']} c:{RegisterValues['c']} d:{RegisterValues['d']}");
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

            //Decode values
            int? firstValue = null;
            int? secondValue = null;

            if (instruction.FirstRegister != null)
            {
                firstValue = RegisterValues[instruction.FirstRegister.Value];
            }
            else
            {
                firstValue = instruction.FirstNumber.Value;
            }

            if (instruction.SecondRegister != null)
            {
                secondValue = RegisterValues[instruction.SecondRegister.Value];
            }

            if (instruction.SecondNumber != null)
            {
                secondValue = instruction.SecondNumber.Value;
            }


            switch (instruction.Opcode)
            {
                case Opcode.Cpy:
                    RegisterValues[instruction.SecondRegister.Value] = firstValue.Value;
                    _programCounter++;
                    break;

                case Opcode.Inc:
                    RegisterValues[instruction.FirstRegister.Value]++;
                    _programCounter++;
                    break;

                case Opcode.Dec:
                    RegisterValues[instruction.FirstRegister.Value]--;
                    _programCounter++;
                    break;

                case Opcode.Jnz:
                    //Jump if not zero
                    if (firstValue != 0)
                    {
                        _programCounter += secondValue.Value;
                    }
                    else
                    {
                        //Don't get stuck on the same instruction for ever..
                        _programCounter++;
                    }
                    break;

                case Opcode.Tgl:
                    var address = _programCounter + RegisterValues[instruction.FirstRegister.Value];
                    if (address >= 0 && address < _instructions.Count)
                    {
                        var toggle = _instructions[address];

                        switch (toggle.Opcode)
                        {
                            //For one-argument instructions, inc becomes dec
                            case Opcode.Inc:
                                toggle.Opcode = Opcode.Dec;
                                break;

                            //and all other one-argument instructions become inc.
                            case Opcode.Dec:
                            case Opcode.Tgl:
                                toggle.Opcode = Opcode.Inc;
                                break;


                            //For two-argument instructions, jnz becomes cpy    
                            case Opcode.Jnz:
                                toggle.Opcode = Opcode.Cpy;
                                break;

                            //and all other two-instructions become jnz.
                            case Opcode.Cpy:
                                toggle.Opcode = Opcode.Jnz;
                                break;
                            
                        }

                    }
                    _programCounter++;
                    break;
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


        public Dictionary<char, int> RegisterValues = new Dictionary<char, int>()
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
