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
        //Relative = 2,
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
        Halt = 99,
    }

    public class IntCodeComputer
    {
        public IntCodeComputer() { }

        public IntCodeComputer(string input)
        {
            LoadProgramFromString(input);
        }


        public List<int> Program = new List<int>();
        public List<int> SimulatedInput = new List<int>();
        public List<int> Output = new List<int>();
        private int _simulatedInputIndex = 0;

        public bool UseSimulatedInput { get; set; } = false;
        public bool PrintDecompiledInstructions { get; set; } = false;
        public bool WaitAfterDecompiling { get; set; } = false;


        public void LoadProgramFromString(string input)
        {
            Program = ParseProgram(input);
        }

        #region Running/decompiling
        private int position = 0;
        public bool Halted { get; private set; } = false;
        private bool _breakBeforeInput = false;
        private bool _breakAfterOutput = false;

        private int GetValueForParameter(InstructionMode mode, int value)
        {
            switch (mode)
            {
                case InstructionMode.Position:
                    if (value >= 0 && value < Program.Count)
                    {
                        return Program[value];
                    }
                    else
                    {
                        throw new Exception($"Address {value} is outside of the range of the program.");
                    }
                    break;
                case InstructionMode.Immediate:
                    return value;
                    break;
            }

            return 0;
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
            int param1 = 0, param2 = 0, param3 = 0;

            if (PrintDecompiledInstructions)
            {
                Console.WriteLine(DecompileInstruction(position));
                if (WaitAfterDecompiling)
                {
                    Console.ReadKey();
                }
            }

            var opcode = DecodeOpcode(Program[position]);

            switch (opcode.instruction)
            {
                case Instruction.Add:
                    //Add

                    param1 = GetValueForParameter(opcode.mode1, Program[position + 1]);
                    param2 = GetValueForParameter(opcode.mode2, Program[position + 2]);

                    Program[Program[position + 3]] = param1 + param2;
                    position += 4;
                    break;

                case Instruction.Multiply:
                    //Mult

                    param1 = GetValueForParameter(opcode.mode1, Program[position + 1]);
                    param2 = GetValueForParameter(opcode.mode2, Program[position + 2]);
                    Program[Program[position + 3]] = param1 * param2;
                    position += 4;
                    break;

                case Instruction.Input:
                    if (_breakBeforeInput)
                    {
                        _breakBeforeInput = false;
                        return;
                    }
                    var input = GetIntInput();
                    Program[Program[position + 1]] = input;
                    position += 2;
                    break;

                case Instruction.Output:
                    
                    //Console.WriteLine("OUT: " + Program[position + 1]);
                    int output = GetValueForParameter(opcode.mode1, Program[position + 1]);
                    Output.Add(output);
                    Console.WriteLine("OUT: " + output);
                    position += 2;

                    if (_breakAfterOutput)
                    {
                        _breakAfterOutput = false;
                        return;
                    }

                    break;

                case Instruction.JumpIfTrue:
                    param1 = GetValueForParameter(opcode.mode1, Program[position + 1]);
                    param2 = GetValueForParameter(opcode.mode2, Program[position + 2]);

                    if (param1 != 0)
                    {
                        position = param2;
                    }
                    else
                    {
                        position += 3;
                    }
                    break;

                case Instruction.JumpIfFalse:
                    param1 = GetValueForParameter(opcode.mode1, Program[position + 1]);
                    param2 = GetValueForParameter(opcode.mode2, Program[position + 2]);

                    if (param1 == 0)
                    {
                        position = param2;
                    }
                    else
                    {
                        position += 3;
                    }
                    break;

                case Instruction.LessThan:
                    param1 = GetValueForParameter(opcode.mode1, Program[position + 1]);
                    param2 = GetValueForParameter(opcode.mode2, Program[position + 2]);

                    if (param1 < param2)
                    {
                        Program[Program[position + 3]] = 1;
                    }
                    else
                    {
                        Program[Program[position + 3]] = 0;
                    }
                    position += 4;
                    break;

                case Instruction.Equals:
                    param1 = GetValueForParameter(opcode.mode1, Program[position + 1]);
                    param2 = GetValueForParameter(opcode.mode2, Program[position + 2]);

                    if (param1 == param2)
                    {
                        Program[Program[position + 3]] = 1;
                    }
                    else
                    {
                        Program[Program[position + 3]] = 0;
                    }
                    position += 4;
                    break;

                case Instruction.Halt:
                    Halted = true;
                    //Halt!
                    return;

                default:
                    throw new Exception("Something went wrong.");
            }
        }
    


        private string GetAddressOrValueString(InstructionMode mode, int value)
        {
            switch (mode)
            {
                case InstructionMode.Position:
                    return $"[{value}]";
                case InstructionMode.Immediate:
                    return value.ToString();
            }
            throw new Exception($"unsupported mode {mode}");
        }

        public string DecompileInstruction(int position)
        {
            StringBuilder decomp = new StringBuilder();
            int param1 = 0, param2 = 0, param3 = 0;

            var opcode = DecodeOpcode(Program[position]);

            switch (opcode.instruction)
            {
                case Instruction.Add:
                    decomp.Append($"{position}-{position + 3}\tADD ");
                    decomp.Append(GetAddressOrValueString(opcode.mode1, Program[position+1]));
                    decomp.Append(" ");
                    decomp.Append(GetAddressOrValueString(opcode.mode2, Program[position+2]));
                    decomp.Append(" ");
                    decomp.Append(GetAddressOrValueString(InstructionMode.Position, Program[position+3]));
                    position += 4;
                    break;

                case Instruction.Multiply:
                    decomp.Append($"{position}-{position + 3}\tMULT ");
                    decomp.Append(GetAddressOrValueString(opcode.mode1, Program[position + 1]));
                    decomp.Append(" ");
                    decomp.Append(GetAddressOrValueString(opcode.mode2, Program[position + 2]));
                    decomp.Append(" ");
                    decomp.Append(GetAddressOrValueString(InstructionMode.Position, Program[position + 3]));
                    position += 4;
                    break;

                case Instruction.Input:
                    decomp.Append($"{position}-{position + 1}\tIN   ");
                    decomp.Append(GetAddressOrValueString(opcode.mode1, Program[position + 1]));
                    position += 2;
                    break;

                case Instruction.Output:
                    decomp.Append($"{position}-{position + 1}\tOUT  ");
                    decomp.Append(GetAddressOrValueString(opcode.mode1, Program[position + 1]));
                    position += 2;
                    break;

                case Instruction.JumpIfTrue:
                    decomp.Append($"{position}-{position + 3}\tJIT  ");
                    decomp.Append(GetAddressOrValueString(opcode.mode1, Program[position + 1]));
                    decomp.Append(" ");
                    decomp.Append(GetAddressOrValueString(opcode.mode2, Program[position + 2]));
                    decomp.Append(" ");
                    decomp.Append(GetAddressOrValueString(InstructionMode.Position, Program[position + 3]));
                    position += 4;
                    break;

                case Instruction.JumpIfFalse:
                    decomp.Append($"{position}-{position + 3}\tJIF  ");
                    decomp.Append(GetAddressOrValueString(opcode.mode1, Program[position + 1]));
                    decomp.Append(" ");
                    decomp.Append(GetAddressOrValueString(opcode.mode2, Program[position + 2]));
                    decomp.Append(" ");
                    decomp.Append(GetAddressOrValueString(InstructionMode.Position, Program[position + 3]));
                    position += 4;
                    break;

                case Instruction.LessThan:
                    decomp.Append($"{position}-{position + 3}\tLST  ");
                    decomp.Append(GetAddressOrValueString(opcode.mode1, Program[position + 1]));
                    decomp.Append(" ");
                    decomp.Append(GetAddressOrValueString(opcode.mode2, Program[position + 2]));
                    decomp.Append(" ");
                    decomp.Append(GetAddressOrValueString(InstructionMode.Position, Program[position + 3]));
                    position += 4;
                    break;

                case Instruction.Equals:
                    decomp.Append($"{position}-{position + 3}\tEQL  ");
                    decomp.Append(GetAddressOrValueString(opcode.mode1, Program[position + 1]));
                    decomp.Append(" ");
                    decomp.Append(GetAddressOrValueString(opcode.mode2, Program[position + 2]));
                    decomp.Append(" ");
                    decomp.Append(GetAddressOrValueString(InstructionMode.Position, Program[position + 3]));
                    position += 4;
                    break;


                case Instruction.Halt:
                    decomp.Append($"{position}-{position + 1}\tHALT");
                    position += 2;
                    break;
            }
            return decomp.ToString();
        }

        #endregion

        private int GetIntInput()
        {
            if (UseSimulatedInput)
            {
                int simIn = SimulatedInput[_simulatedInputIndex];
                Console.WriteLine($"in: {simIn}");
                _simulatedInputIndex++;
                return simIn;
            }


            Console.Write("in: ");
            var input = Console.ReadLine();
            int result;
            while (!int.TryParse(input, out result))
            {
                Console.WriteLine($"Failed to parse {input}. Only integers are accepted.");
                Console.Write("in: ");
                input = Console.ReadLine();
            }
            return result;
        }


        public override string ToString()
        {
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


        public static (Instruction instruction, InstructionMode mode3, InstructionMode mode2, InstructionMode mode1) DecodeOpcode(int opcode)
        {
            string paddedOpcode = opcode.ToString().PadLeft(5, '0');
            InstructionMode mode3 = (InstructionMode) int.Parse(paddedOpcode[0].ToString());
            InstructionMode mode2 = (InstructionMode) int.Parse(paddedOpcode[1].ToString());
            InstructionMode mode1 = (InstructionMode) int.Parse(paddedOpcode[2].ToString());
            Instruction instruction = (Instruction)int.Parse(paddedOpcode[3].ToString() + paddedOpcode[4].ToString());
            return (instruction, mode3, mode2, mode1);
        }


        public static List<int> ParseProgram(string input)
        {
            List<int> result = new List<int>();
            var lines = input.Split(new string[] { "," }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                result.Add(int.Parse(line));
            }
            return result;
        }
    }
}
