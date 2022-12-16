using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;
using Years.Year2019.IntCodeComputer;

namespace Years.Year2022
{
    public class Day10 : BaseDay
    {
        public Day10() : base(2022, 10)
        {
            _program = ParseInput(Input);
        }

        private List<HandheldComputer.Instruction> _program;

        public override void ProblemOne()
        {
            var computer = new HandheldComputer(_program);
            var cycleCheck = new List<int>() { 20, 60, 100, 140, 180, 220 };

            var signal = 0;
            while(cycleCheck.Any())
            {
                computer.Step();
                if(cycleCheck.Contains(computer.Cycle))
                {
                    cycleCheck.Remove(computer.Cycle);
                    signal += computer.Cycle * computer.X;
                }
            }
            Console.WriteLine(signal);

            


            //for(int i = 0; i < 20; i++)
            //{
            //    computer.Step();
            //}
            //
            //var signal = 20 * computer.X;
            //
            //while(!computer.Halt)
            //{
            //    for (int i = 0; i < 40; i++)
            //    {
            //        computer.Step();
            //    }
            //
            //    if (!computer.Halt)
            //    {
            //        var temp = computer.Cycle * computer.X;
            //        Console.WriteLine(temp);
            //        signal += computer.Cycle * computer.X;
            //    }
            //}
        }

        public override void ProblemTwo() 
        {
            var computer = new HandheldComputer(_program);

            while (!computer.Halt)
            {
                computer.Step();
            }

            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 40; x++)
                {
                    Console.Write(computer.Crt[x, y]);
                }
                Console.WriteLine();
            }
        }

        private List<HandheldComputer.Instruction> ParseInput(string input)
        {
            var result = new List<HandheldComputer.Instruction>();

            foreach(var line in input.SplitNewLine())
            {
                var split = line.Split(' ');
                var instruction = split[0] switch
                {
                    "noop" => new HandheldComputer.Instruction { InstructionType = HandheldComputer.InstructionType.Noop },
                    "addx" => new HandheldComputer.Instruction { InstructionType = HandheldComputer.InstructionType.Addx, Parameter = int.Parse(split[1]) },
                    _ => throw new ArgumentException(split[0])
                };
                result.Add(instruction);
            }

            return result;
        }

        private const string SmallExample = @"noop
addx 3
addx -5
";

        private const string Example = @"addx 15
addx -11
addx 6
addx -3
addx 5
addx -1
addx -8
addx 13
addx 4
noop
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx -35
addx 1
addx 24
addx -19
addx 1
addx 16
addx -11
noop
noop
addx 21
addx -15
noop
noop
addx -3
addx 9
addx 1
addx -3
addx 8
addx 1
addx 5
noop
noop
noop
noop
noop
addx -36
noop
addx 1
addx 7
noop
noop
noop
addx 2
addx 6
noop
noop
noop
noop
noop
addx 1
noop
noop
addx 7
addx 1
noop
addx -13
addx 13
addx 7
noop
addx 1
addx -33
noop
noop
noop
addx 2
noop
noop
noop
addx 8
noop
addx -1
addx 2
addx 1
noop
addx 17
addx -9
addx 1
addx 1
addx -3
addx 11
noop
noop
addx 1
noop
addx 1
noop
noop
addx -13
addx -19
addx 1
addx 3
addx 26
addx -30
addx 12
addx -1
addx 3
addx 1
noop
noop
noop
addx -9
addx 18
addx 1
addx 2
noop
noop
addx 9
noop
noop
noop
addx -1
addx 2
addx -37
addx 1
addx 3
noop
addx 15
addx -21
addx 22
addx -6
addx 1
noop
addx 2
addx 1
noop
addx -10
noop
noop
addx 20
addx 1
addx 2
addx 2
addx -6
addx -11
noop
noop
noop
";
    }
}