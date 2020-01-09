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


        public void LoadProgramFromString(string input)
        {
            Program = ParseProgram(input);
        }


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
            


            int position = 0;
            int param1 = 0, param2 = 0, param3 = 0;


            while (true)
            {
                //Decode opcode
                var opcode = DecodeOpcode(Program[position]);

                //var param1 = GetValueForParameter(opcode.mode1, position + 1);
                //var param2 = GetValueForParameter(opcode.mode1, position + 2);
                //var param3 = GetValueForParameter(opcode.mode1, position + 3);

                switch (opcode.instruction)
                {
                    case Instruction.Add:
                        //Add
                        
                        param1 = GetValueForParameter(opcode.mode1, Program[position + 1]);
                        param2 = GetValueForParameter(opcode.mode2, Program[position + 2]);

                        Program[GetValueForParameter(InstructionMode.Immediate, Program[position + 3])] = param1 + param2;
                        position += 4;

                        break;
                    case Instruction.Multiply:
                        //Mult

                        param1 = GetValueForParameter(opcode.mode1, Program[position + 1]);
                        param2 = GetValueForParameter(opcode.mode2, Program[position + 2]);
                        Program[GetValueForParameter(InstructionMode.Immediate, Program[position + 3])] = param1 * param2;
                        position += 4;

                        break;

                    case Instruction.Halt:
                        //Halt!
                        return;


                    default:
                        throw new Exception("Something went wrong.");
                }
            }
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
