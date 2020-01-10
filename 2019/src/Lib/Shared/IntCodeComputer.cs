using System;
using System.Collections.Generic;
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


        public void LoadProgramFromString(string input)
        {
            Program = ParseProgram(input);
        }


        private void WriteMemory(long address, long value)
        {
            if (address < 0)
            {
                throw  new Exception($"Can't write to address bellow zero. Address: {address}.");
            }

            if (address >= Program.Count)
            {
                Program.AddRange(new long[(address + 1) - Program.Count]);
            }

            Program[(int)address] = value;
        }

        private long ReadMemory(long address)
        {
            if (address < 0)
            {
                throw new Exception($"Can't write to address bellow zero. Address: {address}.");
            }

            if (address >= Program.Count)
            {
                Program.AddRange(new long[(address+1) - Program.Count]);
            }

            return Program[(int)address];
        }

        #region Running/decompiling
        private long position = 0;
        public bool Halted { get; private set; } = false;
        private bool _breakBeforeInput = false;
        private bool _breakAfterOutput = false;

        private long? GetValueForParameter(InstructionMode mode, long value)
        {
            

            switch (mode)
            {
                case InstructionMode.Position:
                    if (value < 0)
                    {
                        return null;
                    }
                    return ReadMemory(value);
                case InstructionMode.Immediate:
                    return value;
                case InstructionMode.Relative:
                    return ReadMemory(_relativeBase + value);
                default:
                    return null;
                    //throw new Exception($"Unsupported mode {mode}.");
            }
        }

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

        public void RunTillAfterOutput()
        {
            _breakAfterOutput = true;
            while (_breakAfterOutput && !Halted)
            {
                Step();
            }
        }


        public void Step()
        {
            try
            {
                if (PrintDecompiledInstructions)
                {
                    Console.WriteLine(DecompileInstruction(position));
                    if (WaitAfterDecompiling)
                    {
                        Console.ReadKey();
                    }
                }

                var opcode = DecodeOpcode(ReadMemory(position));
                long? param1 = GetValueForParameter(opcode.mode1, ReadMemory(position + 1));
                long? param2 = GetValueForParameter(opcode.mode2, ReadMemory(position + 2));
                long? param3 = GetValueForParameter(opcode.mode3, ReadMemory(position + 3));

                long? writeAddress = null;
                if (opcode.mode3 == InstructionMode.Position)
                {
                    writeAddress = ReadMemory(position + 3);
                }

                if (opcode.mode3 == InstructionMode.Relative)
                {
                    writeAddress = _relativeBase + ReadMemory(position + 3);
                }
                //long? writeAddress =  ReadMemory(position + 3);

                switch (opcode.instruction)
                {
                    case Instruction.Add:
                        WriteMemory(writeAddress.Value, param1.Value + param2.Value);
                        position += 4;
                        break;

                    case Instruction.Multiply:
                        WriteMemory(writeAddress.Value, param1.Value * param2.Value);
                        position += 4;
                        break;

                    case Instruction.Input:
                        if (_breakBeforeInput)
                        {
                            _breakBeforeInput = false;
                            return;
                        }

                        var input = GetlongInput();
                        WriteMemory(writeAddress.Value, input);
                        position += 2;
                        break;

                    case Instruction.Output:
                        Output.Add(param1.Value);
                        Console.WriteLine("OUT: " + param1);
                        position += 2;

                        if (_breakAfterOutput)
                        {
                            _breakAfterOutput = false;
                            return;
                        }

                        break;

                    case Instruction.JumpIfTrue:
                        if (param1 != 0)
                        {
                            position = param2.Value;
                        }
                        else
                        {
                            position += 3;
                        }

                        break;

                    case Instruction.JumpIfFalse:

                        if (param1 == 0)
                        {
                            position = param2.Value;
                        }
                        else
                        {
                            position += 3;
                        }

                        break;

                    case Instruction.LessThan:

                        if (param1 < param2)
                        {
                            WriteMemory(writeAddress.Value, 1);
                        }
                        else
                        {
                            WriteMemory(writeAddress.Value, 0);
                        }

                        position += 4;
                        break;

                    case Instruction.Equals:

                        if (param1 == param2)
                        {
                            WriteMemory(writeAddress.Value, 1);
                        }
                        else
                        {
                            WriteMemory(writeAddress.Value, 0);
                        }

                        position += 4;
                        break;

                    case Instruction.AdjustRelativeBase:
                        _relativeBase += param1.Value;
                        position += 2;
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
                Console.WriteLine($"Error at {position}. Halting...\r\n{e.Message}\r\n\r\n{e.StackTrace}");
                Halted = true;
            }
        }
    


        private string GetAddressOrValueString(InstructionMode mode, long value)
        {
            switch (mode)
            {
                case InstructionMode.Position:
                    return $"[{value}]";
                case InstructionMode.Immediate:
                    return value.ToString();
                case InstructionMode.Relative:
                    return $"*{value}";
            }
            throw new Exception($"unsupported mode {mode}");
        }

        public string DecompileInstruction(long position)
        {
            StringBuilder decomp = new StringBuilder();
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
            if (UseSimulatedInput)
            {
                long simIn = SimulatedInput[_simulatedInputIndex];
                Console.WriteLine($"in: {simIn}");
                _simulatedInputIndex++;
                return simIn;
            }


            Console.Write("in: ");
            var input = Console.ReadLine();
            long result;
            while (!long.TryParse(input, out result))
            {
                Console.WriteLine($"Failed to parse {input}. Only longegers are accepted.");
                Console.Write("in: ");
                input = Console.ReadLine();
            }
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
