using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2017.SoundVirtualMachine
{
    public enum Opcode
    {
        Snd,
        Set,
        Add,
        Mul,
        Mod,
        Rcv,
        Jgz,
        Jnz,
        Sub,
    }

    public class Instruction
    {
        public Opcode Opcode;
        public char? RegisterA;
        public char? RegisterB;
        public long? ValueA;
        public long? ValueB;
    }

    public class VirtualMachine
    {
        public VirtualMachine(List<Instruction> program)
        {
            _program = program;
        }

        public long RunTillRecovery()
        {
            if (_recoveryFlag)
            {
                _recoveryFlag = false;
            }

            while (!_recoveryFlag)
            {
                Step();

                if (_haltFlag)
                {
                    return 0;
                }
            }
            return _recoveryFrequency;
        }

        
        public void Run()
        {
            while (!_haltFlag)
            {
                Step();
            }
        }

        public void Step()
        {
            _recoveryFlag = false;
            _sendFlag = false;
            _haltFlag = false;

            if (InstructionPointer < 0 || InstructionPointer >= _program.Count)
            {
                _haltFlag = true;
                return;
            }

            var instruction = _program[(int)InstructionPointer];

            var disasseblyA = "";
            long valueA = 0;
            if (instruction.ValueA.HasValue)
            {
                valueA = instruction.ValueA.Value;
                disasseblyA = valueA.ToString();
            }

            if (instruction.RegisterA.HasValue)
            {
                valueA = GetRegister(instruction.RegisterA.Value);
                disasseblyA = instruction.RegisterA.Value.ToString();
            }

            var disasseblyB = "";
            long valueB = 0;
            if (instruction.ValueB.HasValue)
            {
                valueB = instruction.ValueB.Value;
                disasseblyB = valueB.ToString();
            }

            if (instruction.RegisterB.HasValue)
            {
                valueB = GetRegister(instruction.RegisterB.Value);
                disasseblyB = instruction.RegisterB.Value.ToString();
            }

            if (Disassembly)
            {
                Console.WriteLine(InstructionPointer + ": " + _opcodesLookup.Single(i => i.Value == instruction.Opcode).Key + " " + disasseblyA + " " + disasseblyB);
            }


            switch (instruction.Opcode)
            {
                case Opcode.Snd:
                    SendCount++;
                    _recoveryFrequency = valueA;
                    _sendFlag = true;
                    Send.Add(_recoveryFrequency);
                    //Console.WriteLine($"sound: {_recoveryFrequency}");
                    break;

                case Opcode.Set:
                    SetRegister(instruction.RegisterA.Value, valueB);
                    break;

                case Opcode.Add:
                    var add = valueA;
                    add += valueB;
                    SetRegister(instruction.RegisterA.Value, add);
                    break;

                case Opcode.Sub:
                    var sub = valueA;
                    sub -= valueB;
                    SetRegister(instruction.RegisterA.Value, sub);
                    break;

                case Opcode.Mul:
                    MulCount++;
                    var mul = valueA;
                    mul *= valueB;
                    SetRegister(instruction.RegisterA.Value, mul);
                    break;

                case Opcode.Mod:
                    var mod = valueA;
                    mod %= valueB;
                    SetRegister(instruction.RegisterA.Value, mod);
                    break;

                case Opcode.Rcv:
                    if (valueA != 0)
                    {
                        if (_recoveryFrequency > 0)
                        {
                            _recoveryFlag = true;//part 1
                        }
                    }

                    if (Received.Any())
                    {
                        _recoveryFlag = false;
                        var received = Received.First();
                        Received.RemoveAt(0);
                        SetRegister(instruction.RegisterA.Value, received);
                    }
                    else//If 
                    {
                        //Return, skipping increment of instruction ptr
                        _recoveryFlag = true;
                        return;
                    }
                    break;

                case Opcode.Jgz:
                    if (valueA > 0)
                    {
                        InstructionPointer += valueB;
                    }
                    else
                    {
                        InstructionPointer++;
                    }
                    break;

                case Opcode.Jnz:
                    if (valueA != 0)
                    {
                        InstructionPointer += valueB;
                    }
                    else
                    {
                        InstructionPointer++;
                    }
                    break;

                default:
                    throw new Exception();
            }

            if (instruction.Opcode != Opcode.Jgz && instruction.Opcode != Opcode.Jnz)
            {
                InstructionPointer++;
            }
            StepCount++;
        }


        public void SetRegister(char c, long value)
        {
            _registers[c] = value;
        }

        private long GetRegister(char c)
        {
            if (!_registers.ContainsKey(c))
            {
                _registers[c] = 0;

            }
            return _registers[c];
        }



        public List<long> Received = new List<long>();
        public List<long> Send = new List<long>();
        private bool _haltFlag = false;
        private bool _sendFlag = false;
        private bool _recoveryFlag = false;
        public long InstructionPointer = 0;
        private long _recoveryFrequency = 0;
        private Dictionary<char, long> _registers = new Dictionary<char, long>();
        private List<Instruction> _program;
        public long StepCount = 0;
        public long SendCount = 0;
        public long MulCount = 0;
        public bool Disassembly = false;

        private static Dictionary<string, Opcode> _opcodesLookup = new Dictionary<string, Opcode>()
        {
            {"snd", Opcode.Snd},
            {"set", Opcode.Set},
            {"add", Opcode.Add},
            {"mul", Opcode.Mul},
            {"mod", Opcode.Mod},
            {"rcv", Opcode.Rcv},
            {"jgz", Opcode.Jgz},
            {"jnz", Opcode.Jnz},
            {"sub", Opcode.Sub},
        };

        public static List<Instruction> ParseInput(string input)
        {
            var instructions = new List<Instruction>();
            foreach (var line in input.SplitNewLine())
            {
                var bits = line.Split(' ');

                var instruction = new Instruction();
                instruction.Opcode = _opcodesLookup[bits[0]];

                //There is always 1 argument. Argument has 1 case of being a number in my input
                if (long.TryParse(bits[1], out long result))
                {
                    instruction.ValueA = result;
                }
                else
                {
                    instruction.RegisterA = bits[1][0];
                }

                //If there is a second argument, it can be a value or register
                if (bits.Length == 3)
                {
                    if (long.TryParse(bits[2], out result))
                    {
                        //Safe to say this is an int
                        instruction.ValueB = result;
                    }
                    else
                    {
                        instruction.RegisterB = bits[2][0];
                    }
                }
                instructions.Add(instruction);
            }

            return instructions;
        }
    }
}
