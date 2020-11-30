using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Day16
{
    public class EncodedInstruction
    {
        public Instruction Instruction;
        public Dictionary<int, int> BeforeRegisters;
        public Dictionary<int, int> AfterRegisters;
    }

    public struct Instruction
    {
        public int Opcode;
        public int ParamA;
        public int ParamB;
        public int ParamC;
    }
}
