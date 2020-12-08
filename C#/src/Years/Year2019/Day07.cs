using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Years.Utils;
using Years.Year2019.IntCodeComputer;

namespace Years.Year2019
{
    public class Day07 : IDay
    {
        public int Day => 7;
        public int Year => 2019;

        public void ProblemOne()
        {
            var permutations = new List<long> {0, 1, 2, 3, 4}.Permute();
            long max = 0;
            foreach (var permutation in permutations)
            {
                long thrusterSignal = CalculateThrusterSignal(permutation, Input);
                if (max < thrusterSignal)
                {
                    max = thrusterSignal;
                }
            }
            Console.WriteLine(max);
        }


        public void ProblemTwo()
        {
            var permutations = new List<long> {5, 6, 7, 8, 9}.Permute();
            long max = 0;
            foreach (var permutation in permutations)
            {
                long thrusterSignal = CalculateThrusterSignalWithFeedbackLoop(permutation.ToList(), Input);
                if (max < thrusterSignal)
                {
                    max = thrusterSignal;
                }
            }
            Console.WriteLine(max);
        }





        private long CalculateThrusterSignal(IEnumerable<long> phaseSequence, string program)
        {
            //we can abuse the fact that computers will halt after giving their first input, we can just discard the computer after getting the ouput.

            long output = 0;
            foreach (var phaseSeq in phaseSequence)
            {
                Computer computer = new Computer(program);
                computer.Input.Add(phaseSeq);
                computer.Input.Add(output);
                computer.PrintDisassembly = false;
                computer.PrintOutput = false;
                computer.Run();
                output = computer.Output.First();
            }
            return output;
        }


        private long CalculateThrusterSignalWithFeedbackLoop(List<long> phaseSequence, string program)
        {
            List<Computer> computers = new List<Computer>()
            {
                new Computer(program) {PrintDisassembly = false, PrintOutput = false, Input = new List<long>(){phaseSequence[0]}},
                new Computer(program) {PrintDisassembly = false, PrintOutput = false, Input = new List<long>(){phaseSequence[1]}},
                new Computer(program) {PrintDisassembly = false, PrintOutput = false, Input = new List<long>(){phaseSequence[2]}},
                new Computer(program) {PrintDisassembly = false, PrintOutput = false, Input = new List<long>(){phaseSequence[3]}},
                new Computer(program) {PrintDisassembly = false, PrintOutput = false, Input = new List<long>(){phaseSequence[4]}},
            };

            long output = 0;


            while (true)
            {
                for (int i = 0; i < computers.Count; i++)
                {
                    computers[i].Input.Add(output);
                    RunTillInputOutput(computers[i]);
                    if (computers[i].State == State.Halt)
                    {
                        return output;
                    }

                    output = computers[i].Output[0];
                    computers[i].Output.RemoveAt(0);
                }
            }
        }


        private void RunTillInputOutput(Computer c)
        {
            var state = c.Step();
            while (true)
            {
                if (state == State.WaitingForInput || state == State.PushedOutput || state == State.Halt)
                {
                    return;
                }
                state = c.Step();
            }
        }










        private const string Input = @"3,8,1001,8,10,8,105,1,0,0,21,46,55,68,89,110,191,272,353,434,99999,3,9,1002,9,3,9,1001,9,3,9,102,4,9,9,101,4,9,9,1002,9,5,9,4,9,99,3,9,102,3,9,9,4,9,99,3,9,1001,9,5,9,102,4,9,9,4,9,99,3,9,1001,9,5,9,1002,9,2,9,1001,9,5,9,1002,9,3,9,4,9,99,3,9,101,3,9,9,102,3,9,9,101,3,9,9,1002,9,4,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,99,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,99,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,99";
        //part 1
        private const string ExampleProgram1 = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0"; //43210

        private const string ExampleProgram2 =
            "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0"; //54321

        private const string ExampleProgram3 =
            "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0"; //65210


        //part 2
        private const string ExampleProgram4 =
            "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5"; //139629729

        private const string ExampleProgram5 =
            "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10"; //18216
    }
}
