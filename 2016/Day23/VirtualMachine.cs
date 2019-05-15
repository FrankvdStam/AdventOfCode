using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23
{
    public enum Registers
    {
        A = 0,
        B = 1,
        C = 2,
        D = 3,
    }

    public enum InstructionType
    {
        Cpy,
        Inc,
        Dec,
        Jnz,
        Tgl,
    }

    public class Instruction
    {
        public Instruction() { }

        public Instruction(string input)
        {

        }
    }

    public class VirtualMachine
    {

    }
}
