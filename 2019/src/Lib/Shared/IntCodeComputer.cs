using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Shared
{
    public enum InstructionMode
    {
        Position = 0,
        Immediate = 1,
        Relative = 2,
    }

    public enum Instruction
    {
        Add = 1,
        Multiply = 2,
        Input = 3,
        Output = 4,
        JumpIfTrue = 5,
        JumpIfFalse = 6,
        LessThan = 7,
        Equals = 8,
        AdjustRelativeBase = 9,
        Halt = 99,
    }

    public enum BreakReason
    {
        Input,
        Output,
        Halt
    }

    public class IntCodeComputer
    {
        public IntCodeComputer() { }

        public IntCodeComputer(string input)
        {
            LoadProgramFromString(input);
        }


        public List<long> Program = new List<long>();
        public List<long> SimulatedInput = new List<long>();
        public List<long> Output = new List<long>();
        private int _simulatedInputIndex = 0;
        private long _relativeBase = 0;

        public bool UseSimulatedInput { get; set; } = false;
        public bool PrintDecompiledInstructions { get; set; } = true;
        public bool WaitAfterDecompiling { get; set; } = false;
        public bool PrintInput { get; set; } = true;
        public bool PrintOutput { get; set; } = true;


        public void LoadProgramFromString(string input)
        {
            Program = ParseProgram(input);
        }
        
        

        #region Running/decompiling ========================================================================================================
        
        public bool Halted { get; private set; } = false;
        private long _position = 0;
        private bool _breakBeforeInput = false;
        private bool _breakAfterOutput = false;
        //I don't like this - it's statefull. It does improve readability by heaps so I'm going to allow it.
        private (Instruction instruction, InstructionMode mode3, InstructionMode mode2, InstructionMode mode1) _currentOpcode;

        #region Helpers ========================================================================================================

        private long GetParam1()
        {
            return GetParam(_currentOpcode.mode1, ReadMemory(_position + 1));
        }

        private long GetParam2()
        {
            return GetParam(_currentOpcode.mode2, ReadMemory(_position + 2));
        }

        private long GetParam3()
        {
            return GetParam(_currentOpcode.mode3, ReadMemory(_position + 3));
        }

        private long GetParam(InstructionMode mode, long address)
        {
            //if (address < 0)
            //{
            //    throw new Exception($"Address can't be negative! {address}");
            //}
            //
            //if (address > Program.Count)
            //{
            //    return 0;//all outside of scope memory is zero, writing to it will automatically initialize it.
            //}

            //We made sure the given memory exists, now we can access it.
            switch (mode)
            {
                case InstructionMode.Position:
                    return ReadMemory(address);
                case InstructionMode.Immediate:
                    return address;
                case InstructionMode.Relative:
                    return ReadMemory(_relativeBase + address);
                default:
                    throw new Exception($"Unsupported mode {mode}.");
            }
        }

        private long GetWriteAddress1()
        {
            return GetWriteAddress(_currentOpcode.mode1, _position + 1);
        }

        private long GetWriteAddress2()
        {
            return GetWriteAddress(_currentOpcode.mode2, _position + 2);
        }

        private long GetWriteAddress3()
        {
            return GetWriteAddress(_currentOpcode.mode3, _position + 3);
        }

        private long GetWriteAddress(InstructionMode mode, long address)
        {
            if (address < 0)
            {
                throw new Exception($"Address can't be negative! {address}");
            }

            if (mode == InstructionMode.Position)
            {
                return ReadMemory(address);
            }

            if (mode == InstructionMode.Relative)
            {
                return _relativeBase + ReadMemory(address);
            }

            throw new Exception("Failure at GetWriteAddress");
        }

        /// <summary>
        /// Writes to memory, dynamically resizes if needed
        /// </summary>
        private void WriteMemory(long address, long value)
        {
            if (address < 0)
            {
                throw new Exception($"Can't write to address bellow zero. Address: {address}.");
            }

            if (address >= Program.Count)
            {
                Program.AddRange(new long[(address + 1) - Program.Count]);
            }

            Program[(int)address] = value;
        }

        /// <summary>
        /// Reads the memory, deals with out of bound cases
        /// </summary>
        private long ReadMemory(long address)
        {
            if (address < 0)
            {
                throw new Exception($"Can't write to address bellow zero. Address: {address}.");
            }

            if (address >= Program.Count)
            {
                return 0;
                //No need to resize if we're only reading
                //Program.AddRange(new long[(address+1) - Program.Count]);
            }

            return Program[(int)address];
        }
        #endregion

        #region running ========================================================================================================
        public void Run()
        {
            while (!Halted)
            {
                Step();
            }
        }

        public void RunUntilInput()
        {
            _breakBeforeInput = true;
            while (_breakBeforeInput && !Halted)
            {
                Step();
            }
        }

        public long RunTillAfterOutput(bool returnOutputAndClearbuffer = false)
        {
            _breakAfterOutput = true;
            while (_breakAfterOutput && !Halted)
            {
                Step();
            }

            if (Halted)
            {
                return 0;
            }

            if (returnOutputAndClearbuffer)
            {
                long output = Output.First();
                Output.Clear();
                return output;
            }
            return 0;
        }

        public bool RunGetNextOutput(out long output)
        {
            output = 0;
            _breakAfterOutput = true;
            while (_breakAfterOutput && !Halted)
            {
                Step();
            }

            if (Halted)
            {
                Output.Clear();
                return false;
            }
            output = Output.First();
            Output.Clear();
            return true;
        }

        public BreakReason RunTillInputOrOutput()
        {
            _breakAfterOutput = true;
            _breakBeforeInput = true;

            int outputCount = Output.Count;
            while (_breakAfterOutput && _breakBeforeInput && !Halted)
            {
                Step();
            }



            _breakAfterOutput = false;
            _breakBeforeInput = false;

            if (Halted)
            {
                return BreakReason.Halt;
            }
            //bit hacky..
            if (outputCount < Output.Count)
            {
                return BreakReason.Output;
            }

            return BreakReason.Input;
        }
        #endregion

        #region Step ========================================================================================================

   
        public void Step()
        {
            //if (_position == 382)
            //{
            //    Debugger.Break();
            //}

            try
            {
                if (PrintDecompiledInstructions)
                {
                    Console.WriteLine(DecompileInstruction(_position));
                    if (WaitAfterDecompiling)
                    {
                        Console.ReadKey();
                    }
                }

                _currentOpcode = DecodeOpcode(ReadMemory(_position));

                switch (_currentOpcode.instruction)
                {
                    case Instruction.Add:
                        WriteMemory(GetWriteAddress3(), GetParam1() + GetParam2());
                        _position += 4;
                        break;

                    case Instruction.Multiply:
                        WriteMemory(GetWriteAddress3(), GetParam1() * GetParam2());
                        _position += 4;
                        break;

                    case Instruction.Input:
                        if (_breakBeforeInput)
                        {
                            _breakBeforeInput = false;
                            return;
                        }

                        var input = GetlongInput();
                        WriteMemory(GetWriteAddress1(), input);
                        _position += 2;
                        break;

                    case Instruction.Output:
                        long output = GetParam1();
                        Output.Add(output);
                        if (PrintOutput)
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.WriteLine("OUT: " + output);
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        _position += 2;

                        if (_breakAfterOutput)
                        {
                            _breakAfterOutput = false;
                            return;
                        }

                        break;

                    case Instruction.JumpIfTrue:
                        if (GetParam1() != 0)
                        {
                            _position = GetParam2();
                        }
                        else
                        {
                            _position += 3;
                        }

                        break;

                    case Instruction.JumpIfFalse:

                        if (GetParam1() == 0)
                        {
                            _position = GetParam2();
                        }
                        else
                        {
                            _position += 3;
                        }

                        break;

                    case Instruction.LessThan:

                        if (GetParam1() < GetParam2())
                        {
                            WriteMemory(GetWriteAddress3(), 1);
                        }
                        else
                        {
                            WriteMemory(GetWriteAddress3(), 0);
                        }

                        _position += 4;
                        break;

                    case Instruction.Equals:

                        if (GetParam1() == GetParam2())
                        {
                            WriteMemory(GetWriteAddress3(), 1);
                        }
                        else
                        {
                            WriteMemory(GetWriteAddress3(), 0);
                        }

                        _position += 4;
                        break;

                    case Instruction.AdjustRelativeBase:
                        _relativeBase += GetParam1();
                        _position += 2;
                        break;

                    case Instruction.Halt:
                        Halted = true;
                        //Halt!
                        return;

                    default:
                        throw new Exception("Something went wrong.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error at {_position}. Halting...\r\n{e.Message}\r\n\r\n{e.StackTrace}");
                Halted = true;
                throw e;
            }
        }

        #endregion

        private string GetAddressOrValueString(InstructionMode mode, long value)
        {
            string positionValue, relativeValue;
            try
            {
                positionValue = ReadMemory(value).ToString();
            }
            catch
            {
                positionValue = "ERR";
            }

            try
            {
                relativeValue = ReadMemory(_relativeBase + value).ToString();
            }
            catch
            {
                relativeValue = "ERR";
            }

            switch (mode)
            {
                case InstructionMode.Position:
                    return $"[{value}]({positionValue})";
                case InstructionMode.Immediate:
                    return value.ToString();
                case InstructionMode.Relative:
                    return $"*{value}({relativeValue})";
            }
            throw new Exception($"unsupported mode {mode}");
        }

        public string DecompileInstruction(long position)
        {
            StringBuilder decomp = new StringBuilder();//maybe we don't need this.
            //long param1 = 0, param2 = 0, param3 = 0;

            var opcode = DecodeOpcode(ReadMemory(position));

            string param1 = GetAddressOrValueString(opcode.mode1, ReadMemory(position + 1));
            string param2 = GetAddressOrValueString(opcode.mode2, ReadMemory(position + 2));
            string param3 = GetAddressOrValueString(opcode.mode3, ReadMemory(position + 3));

            string writeAddress = "";
            if (opcode.mode3 == InstructionMode.Position)
            {
                writeAddress = GetAddressOrValueString(InstructionMode.Position, ReadMemory(position + 3)); //writing is always at the address of the immediate value.
            }

            if (opcode.mode3 == InstructionMode.Relative)
            {
                writeAddress = GetAddressOrValueString(InstructionMode.Relative, ReadMemory(position + 3));
            }

            switch (opcode.instruction)
            {
                case Instruction.Add:
                    decomp.Append($"{position}-{position + 3}\tADD {param1} {param2} {writeAddress}");
                    break;

                case Instruction.Multiply:
                    decomp.Append($"{position}-{position + 3}\tMULT {param1} {param2} {writeAddress}");
                    break;

                case Instruction.Input:
                    decomp.Append($"{position}-{position + 1}\tIN   {param1}");
                    break;

                case Instruction.Output:
                    decomp.Append($"{position}-{position + 1}\tOUT  {param1}");
                    break;

                case Instruction.JumpIfTrue:
                    decomp.Append($"{position}-{position + 3}\tJIT  {param1} {param2} {writeAddress}");
                    break;

                case Instruction.JumpIfFalse:
                    decomp.Append($"{position}-{position + 3}\tJIF  {param1} {param2} {writeAddress}");
                    break;

                case Instruction.LessThan:
                    decomp.Append($"{position}-{position + 3}\tLST  {param1} {param2} {writeAddress}");
                    break;

                case Instruction.Equals:
                    decomp.Append($"{position}-{position + 3}\tEQL  {param1} {param2} {writeAddress}");
                    break;

                case Instruction.AdjustRelativeBase:
                    decomp.Append($"{position}-{position + 1}\tARB   {param1}");
                    break;

                case Instruction.Halt:
                    decomp.Append($"{position}-{position + 1}\tHALT");
                    break;
            }
            return decomp.ToString();
        }

        #endregion

        private long GetlongInput()
        {
            Console.BackgroundColor = ConsoleColor.Red;

            if (UseSimulatedInput)
            {
                if (_simulatedInputIndex >= 0 && _simulatedInputIndex < SimulatedInput.Count)
                {
                    long simIn = SimulatedInput[_simulatedInputIndex];
                    if (PrintInput)
                    {
                        Console.WriteLine($"in: {simIn}");
                    }
                    _simulatedInputIndex++;
                    Console.BackgroundColor = ConsoleColor.Black;
                    return simIn;
                }
                else
                {
                    throw new Exception($"Simulated input missing at {_simulatedInputIndex}.");
                }
            }

            if (PrintInput)
            {
                Console.Write("in: ");
            }
            var input = Console.ReadLine();
            long result;
            while (!long.TryParse(input, out result))
            {
                Console.WriteLine($"Failed to parse {input}. Only longegers are accepted.");
                Console.Write("in: ");
                input = Console.ReadLine();
            }
            Console.BackgroundColor = ConsoleColor.Black;
            return result;
        }


        public override string ToString()
        {
            for (int i = Program.Count - 1; i > -1; i--)
            {
                if (Program[i] == 0)
                {
                    Program.RemoveAt(i);
                }
                else
                {
                    break;
                }
            }


            StringBuilder result = new StringBuilder("");
            for (int i = 0; i < Program.Count; i++)
            {
                result.Append(Program[i].ToString());
                if (i + 1 < Program.Count)
                {
                    result.Append(',');
                }
                
            }
            return result.ToString();
        }


        public static (Instruction instruction, InstructionMode mode3, InstructionMode mode2, InstructionMode mode1) DecodeOpcode(long opcode)
        {
            string paddedOpcode = opcode.ToString().PadLeft(5, '0');
            InstructionMode mode3 = (InstructionMode) long.Parse(paddedOpcode[0].ToString());
            InstructionMode mode2 = (InstructionMode) long.Parse(paddedOpcode[1].ToString());
            InstructionMode mode1 = (InstructionMode) long.Parse(paddedOpcode[2].ToString());
            Instruction instruction = (Instruction)long.Parse(paddedOpcode[3].ToString() + paddedOpcode[4].ToString());
            return (instruction, mode3, mode2, mode1);
        }


        public static List<long> ParseProgram(string input)
        {
            List<long> result = new List<long>();
            var lines = input.Split(new string[] { "," }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                result.Add(long.Parse(line));
            }
            return result;
        }
    }
}
