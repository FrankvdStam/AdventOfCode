using System;
using System.Collections.Generic;
using System.Text;

namespace Years.Year2019.IntCodeComputer
{
    public enum Opcode
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
    

    public static partial class Extensions
    {
        //Lookup tables for opcodes:
        //(not using dictionaries - switch cases resolve to lookup tables according to documentation)

        /// <summary>
        /// Opcode to string, for disassembly/debug. GetString to disambiguiate between .net object ToString 
        /// </summary>
        /// <param name="opcode"></param>
        /// <returns></returns>
        public static string GetString(this Opcode opcode)
        {
            switch (opcode)
            {
                case Opcode.Add               : return "ADD";
                case Opcode.Multiply          : return "MUL";
                case Opcode.Input             : return "INP";
                case Opcode.Output            : return "OUT";
                case Opcode.JumpIfTrue        : return "JIT";
                case Opcode.JumpIfFalse       : return "JIF";
                case Opcode.LessThan          : return "LTN";
                case Opcode.Equals            : return "EQL";
                case Opcode.AdjustRelativeBase: return "ARB";
                case Opcode.Halt              : return "HLT";
                default: throw new Exception("Unsupported opcode.");
            }
        }


        /// <summary>
        /// Returns the amount of arguments expected with this opcode
        /// </summary>
        /// <param name="opcode"></param>
        /// <returns></returns>
        public static int GetArgumentCount(this Opcode opcode)
        {
            switch (opcode)
            {
                case Opcode.Add               : return 3;
                case Opcode.Multiply          : return 3;
                case Opcode.Input             : return 1;
                case Opcode.Output            : return 1;
                case Opcode.JumpIfTrue        : return 2;
                case Opcode.JumpIfFalse       : return 2;
                case Opcode.LessThan          : return 3;
                case Opcode.Equals            : return 3;
                case Opcode.AdjustRelativeBase: return 1;
                case Opcode.Halt              : return 0;
                default: throw new Exception("Unsupported opcode.");
            }
        }


        /// <summary>
        /// Parses a number into an opcode
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static Opcode ToOpcode(this long number)
        {
            switch (number)
            {
                case 1 : return Opcode.Add                  ;
                case 2 : return Opcode.Multiply             ;
                case 3 : return Opcode.Input                ;
                case 4 : return Opcode.Output               ;
                case 5 : return Opcode.JumpIfTrue           ;
                case 6 : return Opcode.JumpIfFalse          ;
                case 7 : return Opcode.LessThan             ;
                case 8 : return Opcode.Equals               ;
                case 9 : return Opcode.AdjustRelativeBase   ;
                case 99: return Opcode.Halt                 ;
                default: throw new Exception("Unsupported opcode.");
            }
        }
    }
}
