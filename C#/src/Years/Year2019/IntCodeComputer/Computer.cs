using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Years.Year2019.IntCodeComputer
{
    public enum State
    {
        Running,
        WaitingForInput,
        PushedOutput,
        Break,
        Halt,
    }

    public class Computer
    {
        public List<long> Memory = new List<long>();

        public long InstructionPointer = 0;
        public long RelativeBasePointer = 0;
        public State State = State.Running;

        //I/O
        public List<long> Input = new List<long>();
        public List<long> Output = new List<long>();

        //Settings:
        public bool PrintDisassembly = true;
        public bool PrintOutput = true;

        public long? BreakPointer = null;


        #region Constructors ========================================================================================================
        public Computer(string program)
        {
            foreach (var s in program.Split(','))
            {
                Memory.Add(long.Parse(s));
            }
        }

        public void Reset(string program)
        {
            Memory.Clear();
            foreach (var s in program.Split(','))
            {
                Memory.Add(long.Parse(s));
            }

            InstructionPointer = 0;
            RelativeBasePointer = 0;
            State = State.Running;

            Input.Clear();
            Output.Clear();
        }
        #endregion


        #region Helpers ========================================================================================================

        public string MemoryToString()
        {
            return string.Join(',', Memory);
        }


        public long ReadMemory(Mode mode, long value)
        {
            switch(mode)
            {
                case Mode.Immediate:
                    return value;
                case Mode.Relative:
                    var address = RelativeBasePointer + value;
                    //Uninitialized memory is treated as 0.
                    if (address >= Memory.Count)
                    {
                        return 0;
                    }
                    return Memory[(int)address];
                case Mode.Position:
                    if (value >= Memory.Count)
                    {
                        return 0;
                    }
                    return Memory[(int)value];
                default: throw new Exception("Reading memory for unsupported mode.");
            }
        }


        public void WriteMemory(long address, long value)
        {
            //If memory is too small, grow it to the requested size
            if (address >= Memory.Count)
            {
                Memory.AddRange(new long[address+1 - Memory.Count]);
            }
            Memory[(int)address] = value;
        }

        public void DisassembleProgram()
        {
            long i = 0;
            while (true)
            {
                var instruction = Instruction.ParseInstruction(i, Memory);
                Console.WriteLine(instruction.Disassemble(this));
                i += (long) instruction.Size;
                if (instruction.Opcode == Opcode.Halt)
                {
                    return;
                }
            }
        }

        #endregion


        #region Code Execution ========================================================================================================
        /// <summary>
        /// Example run function that takes input from the console and pushes output to the console
        /// Consume the Step function to create custom behavior for I/O
        /// </summary>
        public void Run()
        {
            while(true)
            {
                var state = Step();
                switch(State)
                {
                    case State.WaitingForInput:
                        Console.WriteLine("in: ");
                        Console.Out.Flush();
                        var input = Console.ReadLine();
                        Input.Add(long.Parse(input));
                        Console.WriteLine();
                        break;

                    case State.PushedOutput:
                        if (PrintOutput)
                        {
                            Console.WriteLine($"out: {Output[0]}");
                            Output.RemoveAt(0);
                        }
                        break;

                    case State.Break:
                        Console.WriteLine("Breakpoint hit. Press any key to continue.");
                        Console.ReadKey();
                        State = State.Running;
                        break;

                    case State.Halt:
                        return;
                }
            }
        }



        /// <summary>
        /// Executes a single instruction and returns the state after this instruction
        /// </summary>
        /// <returns></returns>
        public State Step()
        {
            //Clear any flags recieved previously
            State = State.Running;

            //========================================================================================================================================================================
            //Parsing

            if (BreakPointer != null)
            {
                if (InstructionPointer == BreakPointer.Value)
                {
                    Debugger.Break();
                }
            }


            var instruction = Instruction.ParseInstruction(InstructionPointer, Memory);
            if (PrintDisassembly)
            {
                Console.WriteLine(instruction.Disassemble(this));
            }


            //========================================================================================================================================================================
            //Pre calculation: calculating a couple things here that will make life easier when executing the instructions
            
            //Instead of fetching the arguments from memory at every twist and turn, we'll fetch them beforehand
            //we will only be fetching the correct amount else we might be reading outside of our memory
            //The variables must always exist
            var numbers = new List<long>() {0, 0, 0};
            if (instruction.Opcode != Opcode.Halt)
            {
                for (int i = 0; i < instruction.Size - 1; i++)
                {
                    numbers[i] = ReadMemory(instruction.ArgumentModes[i], instruction.Arguments[i]);
                }
            }


            //write address - even though early on it is suggested that the write address is always in position mode, it can also be relative.
            //We can figure out the write address beforehand, using the count of arguments.
            long writeAddress = 0;
            if (instruction.ArgumentCount > 0)
            {
                var lastArgumentIndex = instruction.ArgumentCount - 1;
                switch (instruction.ArgumentModes[(int)lastArgumentIndex])
                {
                    case Mode.Relative:
                        writeAddress = RelativeBasePointer + instruction.Arguments[(int)lastArgumentIndex];
                        break;
                    default:
                        writeAddress = instruction.Arguments[(int)lastArgumentIndex];
                        break;
                }
            }



            //Note: when writing to memory, the raw value from the instruction is used.
            //That is because the output location should always be seen as immediate even if it's mode is position.
            //Take this for example: 1,0,0,0,99
            //Will try to read the 3rd 0 and write the result to location 1. The result should be in location 0.

            //========================================================================================================================================================================
            //Executing
            switch (instruction.Opcode)
            {
                case Opcode.Add:
                    WriteMemory(writeAddress, numbers[0] + numbers[1]);
                    break;
                case Opcode.Multiply:
                    WriteMemory(writeAddress, numbers[0] * numbers[1]);
                    break;


                case Opcode.Input:
                    if (Input.Count > 0)
                    {
                        var input = Input[0];
                        //Erase the value we just used as input
                        Input.RemoveAt(0);
                        WriteMemory(writeAddress, input);
                    }
                    else
                    {
                        //We have no input: Do not increment instruction pointer, break out so that input can be supplied
                        State = State.WaitingForInput;
                        return State;
                    }
                    break;
                case Opcode.Output:
                    //Notify consumers of step that we pushed an output, do increment the instruction pointer so that we don't loop.
                    Output.Add(numbers[0]);
                    State = State.PushedOutput;
                    break;


                case Opcode.JumpIfTrue:
                    if (numbers[0] != 0)
                    {
                        InstructionPointer = numbers[1];
                        //Exit without incrementing the instruction pointer
                        return State;
                    }
                    break;
                case Opcode.JumpIfFalse:
                    if (numbers[0] == 0)
                    {
                        InstructionPointer = numbers[1];
                        //Exit without incrementing the instruction pointer
                        return State;
                    }
                    break;


                case Opcode.LessThan:
                    long num = 0;
                    if (numbers[0] < numbers[1])
                    {
                        num = 1;
                    }
                    WriteMemory(writeAddress, num);
                    break;
                case Opcode.Equals:
                    long temp = 0;
                    if (numbers[0] == numbers[1])
                    {
                        temp = 1;
                    }
                    WriteMemory(writeAddress, temp);
                    break;


                case Opcode.AdjustRelativeBase:
                    RelativeBasePointer += numbers[0];
                    break;


                case Opcode.Halt:
                    State = State.Halt;
                    return State;
            }

           
            InstructionPointer += instruction.Size;
            return State;
        }
        #endregion
    }
}
