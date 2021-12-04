using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2017
{
    public class Day18 : IDay
    {
        private enum Opcode
        {
            Snd,
            Set,
            Add,
            Mul,
            Mod,
            Rcv,
            Jgz,
        }

        private class Instruction
        {
            public Opcode Opcode;
            public char? RegisterA;
            public char? RegisterB;
            public long? ValueA;
            public long? ValueB;
        }


        private class SoundVirtualMachine
        {
            public SoundVirtualMachine(List<Instruction> program)
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
                while (true)
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

                long valueA = 0;
                if (instruction.ValueA.HasValue)
                {
                    valueA = instruction.ValueA.Value;
                }

                if (instruction.RegisterA.HasValue)
                {
                    valueA = GetRegister(instruction.RegisterA.Value);
                }


                long valueB = 0;
                if (instruction.ValueB.HasValue)
                {
                    valueB = instruction.ValueB.Value;
                }

                if (instruction.RegisterB.HasValue)
                {
                    valueB = GetRegister(instruction.RegisterB.Value);
                }

                //Console.WriteLine(InstructionPointer + " : " + Input.SplitNewLine()[InstructionPointer] + " - " + valueB);
                

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

                    case Opcode.Mul:
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

                    default:
                        throw new Exception();
                }

                if (instruction.Opcode != Opcode.Jgz)
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
        }


        
        public int Day => 18;
        public int Year => 2017;

        public void ProblemOne()
        {
            var vm = new SoundVirtualMachine(ParseInput(Input));
            var firstRecovery = vm.RunTillRecovery();
            Console.WriteLine(firstRecovery);
        }

        public void ProblemTwo()
        {
            var program1 = new SoundVirtualMachine(ParseInput(Input));
            program1.SetRegister('p', 0);
            var program2 = new SoundVirtualMachine(ParseInput(Input));
            program2.SetRegister('p', 1);

            var stepCountA = program1.StepCount;
            var stepCountB = program2.StepCount;

            while (true)
            {
                program1.Received.AddRange(program2.Send.Clone());
                program2.Send.Clear();
                program1.RunTillRecovery();

                program2.Received.AddRange(program1.Send.Clone());
                program1.Send.Clear();
                program2.RunTillRecovery();

                //Both halted
                if (stepCountA == program1.StepCount && stepCountB == program2.StepCount)
                {
                    Console.WriteLine(program2.SendCount);
                    return;
                }

                stepCountA = program1.StepCount;
                stepCountB = program2.StepCount;
            }





        }

        private readonly Dictionary<string, Opcode> _opcodesLookup = new Dictionary<string, Opcode>()
        {
            { "snd", Opcode.Snd },
            { "set", Opcode.Set },
            { "add", Opcode.Add },
            { "mul", Opcode.Mul },
            { "mod", Opcode.Mod },
            { "rcv", Opcode.Rcv },
            { "jgz", Opcode.Jgz },
        }; 

        private List<Instruction> ParseInput(string input)
        {
            var instructions = new List<Instruction>();
            foreach (var line in input.SplitNewLine())
            {
                var bits = line.Split(' ');

                var instruction = new Instruction();
                instruction.Opcode = _opcodesLookup[bits[0]];
                
                //There is always 1 argument. Argument has 1 case of being a number in my input
                if(long.TryParse(bits[1], out long result))
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



        private const string Example = @"set a 1
add a 2
mul a a
mod a 5
snd a
set a 0
rcv a
jgz a -1
set a 1
jgz a -2";

        private const string Example2 = @"snd 1
snd 2
snd p
rcv a
rcv b
rcv c
rcv d";

        private const string Input = @"set i 31
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
    }
}