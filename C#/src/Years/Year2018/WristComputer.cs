using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Years.Year2018
{
    

    internal class WristComputer
    {
        internal enum CpuState
        {
            Running,
            Halt,
        }

        internal enum InstructionType
        {
            Addr = 4,
            Addi = 0,
            Mulr = 15,
            Muli = 6,
            Banr = 8,
            Bani = 7,
            Borr = 2,
            Bori = 12,
            Setr = 10,
            Seti = 5,
            Gtir = 11,
            Gtri = 3,
            Gtrr = 9,
            Eqir = 14,
            Eqri = 13,
            Eqrr = 1,
        }

        internal class Instruction
        {
            public int Opcode;
            public InstructionType InstructionType;
            public int RegisterA;
            public int RegisterB;
            public int RegisterC;
            //public bool Immediate =>
            //    InstructionType is InstructionType.Addi or InstructionType.Muli or InstructionType.Bani
            //    or InstructionType.Bori or InstructionType.Seti or InstructionType.Gtri or InstructionType.Eqri;
        }


        public WristComputer()
        {

        }

        public CpuState State = CpuState.Running;

        public List<Instruction> Program = new List<Instruction>();
        private int _instructionPointer = 0;

        public readonly Dictionary<int, int> Registers = new Dictionary<int, int>()
        {
            { 0,0},
            { 1,0},
            { 2,0},
            { 3,0},
        };


        public void RunTilHalt()
        {
            while(State != CpuState.Halt)
            {
                Step();
            }
        }

        public void Step()
        {
            if(State == CpuState.Halt)
            {
                return;
            }

            var instruction = Program[_instructionPointer];

            var regAValue = Registers[instruction.RegisterA];
            var regBValue = Registers[instruction.RegisterB];
            var regBImmediate = instruction.RegisterB;


            switch(instruction.InstructionType)
            {
                //addr (add register) stores into register C the result of adding register A and register B.
                case InstructionType.Addr:
                    Registers[instruction.RegisterC] = Registers[instruction.RegisterA] + Registers[instruction.RegisterB];
                    break;

                //addi (add immediate) stores into register C the result of adding register A and value B.
                case InstructionType.Addi:
                    Registers[instruction.RegisterC] = Registers[instruction.RegisterA] + instruction.RegisterB;
                    break;

                //mulr (multiply register) stores into register C the result of multiplying register A and register B.
                case InstructionType.Mulr:
                    Registers[instruction.RegisterC] = Registers[instruction.RegisterA] * Registers[instruction.RegisterB];
                    break;

                //muli (multiply immediate) stores into register C the result of multiplying register A and value B.
                case InstructionType.Muli:
                    Registers[instruction.RegisterC] = Registers[instruction.RegisterA] * instruction.RegisterB;
                    break;

                //banr (bitwise AND register) stores into register C the result of the bitwise AND of register A and register B.
                case InstructionType.Banr:
                    Registers[instruction.RegisterC] = Registers[instruction.RegisterA] & Registers[instruction.RegisterB];
                    break;

                //bani (bitwise AND immediate) stores into register C the result of the bitwise AND of register A and value B.
                case InstructionType.Bani:
                    Registers[instruction.RegisterC] = Registers[instruction.RegisterA] & instruction.RegisterB;
                    break;

                //borr (bitwise OR register) stores into register C the result of the bitwise OR of register A and register B.
                case InstructionType.Borr:
                    Registers[instruction.RegisterC] = Registers[instruction.RegisterA] | Registers[instruction.RegisterB];
                    break;

                //bori (bitwise OR immediate) stores into register C the result of the bitwise OR of register A and value B.
                case InstructionType.Bori:
                    Registers[instruction.RegisterC] = Registers[instruction.RegisterA] | instruction.RegisterB;
                    break;

                //setr (set register) copies the contents of register A into register C. (Input B is ignored.)
                case InstructionType.Setr:
                    Registers[instruction.RegisterC] = Registers[instruction.RegisterA];
                    break;

                //seti (set immediate) stores value A into register C. (Input B is ignored.)
                case InstructionType.Seti:
                    Registers[instruction.RegisterC] = instruction.RegisterA;
                    break;

                //gtir (greater-than immediate/register) sets register C to 1 if value A is greater than register B. Otherwise, register C is set to 0.
                case InstructionType.Gtir:
                    Registers[instruction.RegisterC] = instruction.RegisterA > Registers[instruction.RegisterB] ? 1 : 0;
                    break;

                //gtri (greater-than register/immediate) sets register C to 1 if register A is greater than value B. Otherwise, register C is set to 0.
                case InstructionType.Gtri:
                    Registers[instruction.RegisterC] = Registers[instruction.RegisterA] > instruction.RegisterB ? 1 : 0;
                    break;

                //gtrr (greater-than register/register) sets register C to 1 if register A is greater than register B. Otherwise, register C is set to 0.
                case InstructionType.Gtrr:
                    Registers[instruction.RegisterC] = Registers[instruction.RegisterA] > Registers[instruction.RegisterB] ? 1 : 0;
                    break;

                //eqir (equal immediate/register) sets register C to 1 if value A is equal to register B. Otherwise, register C is set to 0.
                case InstructionType.Eqir:
                    Registers[instruction.RegisterC] = instruction.RegisterA == Registers[instruction.RegisterB] ? 1 : 0;
                    break;

                //eqri (equal register/immediate) sets register C to 1 if register A is equal to value B. Otherwise, register C is set to 0.
                case InstructionType.Eqri:
                    Registers[instruction.RegisterC] = Registers[instruction.RegisterA] == instruction.RegisterB ? 1 : 0;
                    break;

                //eqrr (equal register/register) sets register C to 1 if register A is equal to register B. Otherwise, register C is set to 0.
                case InstructionType.Eqrr:
                    Registers[instruction.RegisterC] = Registers[instruction.RegisterA] == Registers[instruction.RegisterB] ? 1 : 0;
                    break;
            }

            _instructionPointer++;
            
            if(_instructionPointer >= Program.Count)
            {
                State = CpuState.Halt;
            }
            
            //TODO: halt
        }
    }
}
