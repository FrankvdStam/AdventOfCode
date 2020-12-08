using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2019.IntCodeComputer
{
    
       
    public enum Mode
    {
        Position = 0,
        Immediate = 1,
        Relative = 2,
    }


    public class Instruction
    {
        public Opcode Opcode = Opcode.Halt;
        public uint Size = 2;
        public uint ArgumentCount = 0;
        public List<long> Arguments = new List<long>();
        public List<Mode> ArgumentModes = new List<Mode>();
        public long WriteAddress = 0;


        public static Instruction ParseInstruction(long instructionPointer, List<long> program)
        {
            Instruction instruction = new Instruction();
            var number = program[(int)instructionPointer];

            //Parse the opcode by splitting the digits, we can look at each digit individually
            //TODO: check if reversing is in order
            var digits = number.SplitDigits();
            digits.Reverse();

            //Opcode can be width 2 in case of the halt opcode.
            if (digits.Count >= 2 && digits[0] == 9 && digits[1] == 9)
            {
                instruction.Opcode = Opcode.Halt;
                instruction.Size = 2;
            }
            //else instruction data is always 1 while it might take 2 digits (trailing 0)
            else
            {
                instruction.Opcode = digits[0].ToOpcode();

                //Remove the opcode itself so that only arguments remain.
                //If there are any modes specified, we'll have to remove 2 digits.
                digits.RemoveAt(0);
                if (digits.Count > 0)
                {
                    digits.RemoveAt(0);
                }

                //Figure out how many arguments to expect
                var argumentCount = instruction.Opcode.GetArgumentCount();
                instruction.Size = (uint)(argumentCount + 1);

                //Always fill in the blanks of the opcode
                while (digits.Count < argumentCount)
                {
                    digits.Add(0);
                }

                //Parse the modes of the arguments and the numeric values
                instruction.ArgumentCount = (uint)digits.Count;
                for(int i = 0; i < digits.Count; i++)
                {
                    instruction.ArgumentModes.Add(digits[i].ToMode());
                    var value = program[(int)(instructionPointer + i + 1)];
                    instruction.Arguments.Add(value);
                }
            }
            return instruction;
        }


        public string Disassemble(Computer computer)
        {
            StringBuilder disassembly = new StringBuilder();
            disassembly.Append(computer.InstructionPointer.ToString().PadRight(8, ' '));
            disassembly.Append(Opcode.GetString().PadRight(8, ' '));

            for(int i = 0; i < ArgumentCount; i++)
            {
                StringBuilder argument = new StringBuilder();

                switch (ArgumentModes[i])
                {
                    case Mode.Position:
                        argument.Append('[');
                        argument.Append(Arguments[i]);
                        argument.Append(']');
                        argument.Append(computer.ReadMemory(Mode.Position, Arguments[i]));
                        break;
                    case Mode.Immediate:
                        argument.Append(Arguments[i]);
                        break;
                    case Mode.Relative:
                        argument.Append(Arguments[i]);
                        argument.Append('+');
                        argument.Append(computer.RelativeBasePointer);
                        argument.Append('*');
                        argument.Append(' ');
                        argument.Append(computer.ReadMemory(Mode.Position, Arguments[i] + computer.RelativeBasePointer));
                        break;
                }

                disassembly.Append(argument.ToString().PadRight(12, ' '));
                disassembly.Append(' ');
            }

            return disassembly.ToString();
        }
    }


    public static partial class Extensions
    {
        //Lookup table - see opcode
        public static Mode ToMode(this long number)
        {
            switch (number)
            {
                case 0: return Mode.Position;
                case 1: return Mode.Immediate;
                case 2: return Mode.Relative;
                default: throw new Exception($"Unsupported mode from number: {number}");
            }
        }
    }
}
