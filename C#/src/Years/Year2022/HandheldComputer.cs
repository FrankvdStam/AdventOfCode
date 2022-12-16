using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Years.Year2015;

namespace Years.Year2022
{
    internal class HandheldComputer
    {
        internal enum InstructionType
        {
            Noop,
            Addx,
        }

        internal class Instruction
        {
            public InstructionType InstructionType { get; set; }
            public int? Parameter { get; set; }
        }

        public readonly Dictionary<InstructionType, int> InstructionLookup = new()
        {
            { InstructionType.Noop, 1 },
            { InstructionType.Addx, 2 },
        };

        public int X = 1;
        private int _instructionPointer = 0;
        private int _currentCycle = 0;
        public int Cycle = 0;
        public bool Halt = false;

        public HandheldComputer(List<Instruction> program)
        {
            if(program.Count == 0)
            {
                throw new ArgumentException();
            }

            _program = program;
            _currentCycle = InstructionLookup[_program[_instructionPointer].InstructionType];
        }

        public void Step()
        {
            if (Halt) { return; }

            var instruction = _program[_instructionPointer];
            var cycles = InstructionLookup[instruction.InstructionType];

            //Detect end of an instruction
            if (_currentCycle == 0)
            {
                //Execute the actually instruction at the end of the cycles
                switch (instruction.InstructionType)
                {
                    case InstructionType.Addx:
                        X += instruction.Parameter.Value;
                        break;
                }

                //Setup the next instruction
                _instructionPointer++;
                if(_program.Count <= _instructionPointer)
                {
                    Halt = true;
                    return;
                }

                var nextInstruction = _program[_instructionPointer];
                _currentCycle = InstructionLookup[nextInstruction.InstructionType];
            }

            _currentCycle--;
            Cycle++;

            UpdateCrt();
        }

        public readonly char[,] Crt = new char[40,6];

        private void UpdateCrt()
        {
            var cycle = Cycle - 1;

            var x = cycle % 40;
            var y = cycle / 40;


            if(x == X || x == X-1 || x == X+1)
            {
                Crt[x, y] = '#'; 
            }
            else
            {
                Crt[x, y] = '.';
            }
        }


        private List<Instruction> _program;
    }
}
