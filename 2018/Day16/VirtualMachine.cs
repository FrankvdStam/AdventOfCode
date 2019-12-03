using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16
{
    public class VirtualMachine
    {
        private Dictionary<int, int> Registers = new Dictionary<int, int>()
        {
            { 0, 0 },
            { 1, 0 },
            { 2, 0 },
            { 3, 0 },
        };

        public List<Instruction> Program;

        public int CountSampleInstruction(EncodedInstruction instruction)
        {
            bool DictionaryEqual(Dictionary<int, int> a, Dictionary<int, int> b)
            {
                if (a.Count == b.Count)
                {
                    foreach (var pair in a)
                    {
                        int value;
                        if (b.TryGetValue(pair.Key, out value))
                        {
                            // Require value be equal.
                            if (value != pair.Value)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                return true;
            }


            bool TryInstruction(InstructionPointer instr, EncodedInstruction data)
            {
                Registers = data.BeforeRegisters;
                instr(data.Instruction.ParamA, data.Instruction.ParamB, data.Instruction.ParamC);
                return DictionaryEqual(Registers, data.AfterRegisters);
            }

            int count = 0;
            if(TryInstruction(Addr, instruction)) { count++; }
            if(TryInstruction(Addi, instruction)) { count++; }
            if(TryInstruction(Mulr, instruction)) { count++; }
            if(TryInstruction(Muli, instruction)) { count++; }
            if(TryInstruction(Banr, instruction)) { count++; }
            if(TryInstruction(Bani, instruction)) { count++; }
            if(TryInstruction(Borr, instruction)) { count++; }
            if(TryInstruction(Bori, instruction)) { count++; }
            if(TryInstruction(Setr, instruction)) { count++; }
            if(TryInstruction(Seti, instruction)) { count++; }
            if(TryInstruction(Gtir, instruction)) { count++; }
            if(TryInstruction(Gtri, instruction)) { count++; }
            if(TryInstruction(Gtrr, instruction)) { count++; }
            if(TryInstruction(Eqir, instruction)) { count++; }
            if(TryInstruction(Eqri, instruction)) { count++; }
            if(TryInstruction(Eqrr, instruction)) { count++; }

            return count;
        }


        #region Instructions ========================================================================================================

        delegate void InstructionPointer(int paramA, int paramB, int paramC);

        #region Addition ========================================================================================================
        /// <summary>
        /// addr (add register) stores into register C the result of adding register A and register B.
        /// </summary>
        private void Addr(int paramA, int paramB, int paramC)
        {
            Registers[paramC] = Registers[paramA] + Registers[paramB];
        }

        /// <summary>
        /// addi (add immediate) stores into register C the result of adding register A and value B.
        /// </summary>
        private void Addi(int paramA, int paramB, int paramC)
        {
            Registers[paramC] = Registers[paramA] + paramB;
        }
        #endregion

        #region Multiplication ========================================================================================================
        /// <summary>
        /// mulr(multiply register) stores into register C the result of multiplying register A and register B.
        /// </summary>
        private void Mulr(int paramA, int paramB, int paramC)
        {
            Registers[paramC] = Registers[paramA] * Registers[paramB];
        }

        /// <summary>
        /// muli(multiply immediate) stores into register C the result of multiplying register A and value B.
        /// </summary>
        private void Muli(int paramA, int paramB, int paramC)
        {
            //2 1 2
            Registers[paramC] = Registers[paramA] * paramB;
        }
        #endregion

        #region Bitwise AND ========================================================================================================
        /// <summary>
        /// banr (bitwise AND register) stores into register C the result of the bitwise AND of register A and register B.
        /// </summary>
        private void Banr(int paramA, int paramB, int paramC)
        {
            Registers[paramC] = Registers[paramA] & Registers[paramB];
        }

        /// <summary>
        /// bani (bitwise AND immediate) stores into register C the result of the bitwise AND of register A and value B.
        /// </summary>
        private void Bani(int paramA, int paramB, int paramC)
        {
            Registers[paramC] = Registers[paramA] & paramB;
        }
        #endregion

        #region Bitwise OR  ========================================================================================================
        /// <summary>
        /// borr (bitwise OR register) stores into register C the result of the bitwise OR of register A and register B.
        /// </summary>
        private void Borr(int paramA, int paramB, int paramC)
        {
            Registers[paramC] = Registers[paramA] | Registers[paramB];
        }

        /// <summary>
        /// bori (bitwise OR immediate) stores into register C the result of the bitwise OR of register A and value B.
        /// </summary>
        private void Bori(int paramA, int paramB, int paramC)
        {
            Registers[paramC] = Registers[paramA] | paramB;
        }
        #endregion

        #region Assignment ========================================================================================================
        /// <summary>
        /// setr (set register) copies the contents of register A into register C. (Input B is ignored.)
        /// </summary>
        private void Setr(int paramA, int paramB, int paramC)
        {
            Registers[paramC] = Registers[paramA];
        }

        /// <summary>
        /// seti (set immediate) stores value A into register C. (Input B is ignored.)
        /// </summary>
        private void Seti(int paramA, int paramB, int paramC)
        {
            Registers[paramC] = paramA;
        }
        #endregion

        #region Greater-than ========================================================================================================
        /// <summary>
        /// gtir (greater-than immediate/register) sets register C to 1 if value A is greater than register B. Otherwise, register C is set to 0.
        /// </summary>
        private void Gtir(int paramA, int paramB, int paramC)
        {
            Registers[paramC] = paramA > Registers[paramB] ? 1 : 0;
        }

        /// <summary>
        /// gtri (greater-than register/immediate) sets register C to 1 if register A is greater than value B. Otherwise, register C is set to 0.
        /// </summary>
        private void Gtri(int paramA, int paramB, int paramC)
        {
            Registers[paramC] = Registers[paramA] > paramB ? 1 : 0;
        }

        /// <summary>
        /// gtrr (greater-than register/register) sets register C to 1 if register A is greater than register B. Otherwise, register C is set to 0.
        /// </summary>
        private void Gtrr(int paramA, int paramB, int paramC)
        {
            Registers[paramC] = Registers[paramA] > Registers[paramB] ? 1 : 0;
        }
        #endregion

        #region equality ========================================================================================================
        /// <summary>
        /// eqir (equal immediate/register) sets register C to 1 if value A is equal to register B. Otherwise, register C is set to 0.
        /// </summary>
        private void Eqir(int paramA, int paramB, int paramC)
        {
            Registers[paramC] = paramA == Registers[paramB] ? 1 : 0;
        }

        /// <summary>
        /// eqri (equal register/immediate) sets register C to 1 if register A is equal to value B. Otherwise, register C is set to 0.
        /// </summary>
        private void Eqri(int paramA, int paramB, int paramC)
        {
            Registers[paramC] = Registers[paramA] == paramB ? 1 : 0;
        }

        /// <summary>
        /// eqrr (equal register/register) sets register C to 1 if register A is equal to register B. Otherwise, register C is set to 0.
        /// </summary>
        private void Eqrr(int paramA, int paramB, int paramC)
        {
            Registers[paramC] = Registers[paramA] == Registers[paramB] ? 1 : 0;
        }
        #endregion

        #endregion
    }
}
