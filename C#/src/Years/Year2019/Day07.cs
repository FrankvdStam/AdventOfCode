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
            var permutations = GetPermutations(new List<long> {0, 1, 2, 3, 4}, 5).ToList();
            Dictionary<List<long>, long> results = new Dictionary<List<long>, long>();
            foreach (var permutation in permutations)
            {
                long thrusterSignal = CalculateThrusterSignal(permutation, ActualProgram);
                results.Add(permutation.ToList(), thrusterSignal);
            }

            long maxThrusterSignal = results.Values.Max();
        }






        public void ProblemTwo()
        {
            Console.Clear();
            
            var permutations = GetPermutations(new List<long> { 5, 6, 7, 8, 9 }, 5).ToList();
            Dictionary<List<long>, long> results = new Dictionary<List<long>, long>();
            foreach (var permutation in permutations)
            {
                long thrusterSignal = CalculateThrusterSignalWithFeedbackLoop(permutation.ToList(), ActualProgram);
                results.Add(permutation.ToList(), thrusterSignal);
            }
            long maxThrusterSignal = results.Values.Max();
        }

        public long CalculateThrusterSignal(IEnumerable<long> phaseSequence, string program)
        {
            long output = 0;
            foreach (int phaseSeq in phaseSequence)
            {
                Console.WriteLine("\r\n=== Spinning up intcomputer ===\r\n");

                Computer c = new Computer(program);
                c.Input.Add(phaseSeq);
                c.Input.Add(output);
                c.PrintDisassembly = true;
                c.Run();

                output = c.Output.First();
            }

            Console.WriteLine("\r\n=== Done! ===\r\n");
            Console.WriteLine("Result: " + output);
            return output;
        }


        public long CalculateThrusterSignalWithFeedbackLoop(List<long> phaseSequence, string program)
        {
            bool printDecompiledInstructions = true;
            List<Computer> computers = new List<Computer>()
            {
                new Computer(program) {PrintDisassembly = printDecompiledInstructions, Input = new List<long>(){phaseSequence[0], 0}},
                new Computer(program) {PrintDisassembly = printDecompiledInstructions, Input = new List<long>(){phaseSequence[1]}},
                new Computer(program) {PrintDisassembly = printDecompiledInstructions, Input = new List<long>(){phaseSequence[2]}},
                new Computer(program) {PrintDisassembly = printDecompiledInstructions, Input = new List<long>(){phaseSequence[3]}},
                new Computer(program) {PrintDisassembly = printDecompiledInstructions, Input = new List<long>(){phaseSequence[4]}},
            };

            long output = 0;
            while (true)
            {
                for (int i = 0; i < computers.Count; i++)
                {
                    Console.WriteLine($"Running on {i}");
                    int first = i;
                    int second = i + 1 < computers.Count ? i + 1 : 0;//wrap around to first computer
                    computers[first].Run();
                    if (computers[first].State == State.Halt)
                    {
                        return output;
                        //Done!
                    }
                    output = computers[first].Output.First();
                    computers[first].Output.Clear();
                    computers[second].Input.Add(output);
                }
            }
        }

        //https://stackoverflow.com/questions/756055/listing-all-permutations-of-a-string-integer
        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] {t});

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] {t2}));
        }

        private const string ActualProgram =
            @"3,8,1001,8,10,8,105,1,0,0,21,46,55,68,89,110,191,272,353,434,99999,3,9,1002,9,3,9,1001,9,3,9,102,4,9,9,101,4,9,9,1002,9,5,9,4,9,99,3,9,102,3,9,9,4,9,99,3,9,1001,9,5,9,102,4,9,9,4,9,99,3,9,1001,9,5,9,1002,9,2,9,1001,9,5,9,1002,9,3,9,4,9,99,3,9,101,3,9,9,102,3,9,9,101,3,9,9,1002,9,4,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,99,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,99,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,99";

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
