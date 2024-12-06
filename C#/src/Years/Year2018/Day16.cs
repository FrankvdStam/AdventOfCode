using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2018
{
    public class Day16 : BaseDay
    {
        public Day16() : base(2018, 16)
        {
            ParseInput(Input);
        }

        public override void ProblemOne()
        {

            //TestInstructionOnSamples();
            //FindInstructionValues();

            var instructionTypes = (WristComputer.InstructionType[])Enum.GetValues(typeof(WristComputer.InstructionType));

            var count = 0;
            foreach (var sample in _samples)
            {
                var treeOrMoreCount = 0;

                var i = new WristComputer.Instruction();
                i.RegisterA = sample.instruction[1];
                i.RegisterB = sample.instruction[2];
                i.RegisterC = sample.instruction[3];

                foreach(var instructionType in instructionTypes)
                {
                    i.InstructionType = instructionType;
                    if(TryInstruction(sample, i))
                    {
                        treeOrMoreCount++;
                    }
                }

                if(treeOrMoreCount >= 3)
                {
                    count++;
                }
            }
            Console.WriteLine(count);
        }

        private bool TryInstruction((List<int> before, List<int> instruction, List<int> after) sample, WristComputer.Instruction instruction, Dictionary<int, List<WristComputer.InstructionType>> instructionValues = null)
        {
            var computer = new WristComputer();
            computer.Registers[0] = sample.before[0];
            computer.Registers[1] = sample.before[1];
            computer.Registers[2] = sample.before[2];
            computer.Registers[3] = sample.before[3];

            computer.Program.Add(instruction);
            computer.Step();

            var match = computer.Registers[0] == sample.after[0] &&
                        computer.Registers[1] == sample.after[1] &&
                        computer.Registers[2] == sample.after[2] &&
                        computer.Registers[3] == sample.after[3];

            //Helper code to figure out the instruction values
            if(!match && instructionValues != null && instructionValues.ContainsKey(sample.instruction[0]))
            {
                instructionValues[sample.instruction[0]].Remove(instruction.InstructionType);
            }

            return match;
        }

        private void FindInstructionValues()
        {
            var instructionTypes = (WristComputer.InstructionType[])Enum.GetValues(typeof(WristComputer.InstructionType));

            var unresolvedInstructions = ((WristComputer.InstructionType[])Enum.GetValues(typeof(WristComputer.InstructionType))).ToList();
            var unresolvedOpcodes = Enumerable.Range(0, 16).ToList();

            while(unresolvedInstructions.Count > 0)
            {
                //Setup
                var instructionValues = new Dictionary<int, List<WristComputer.InstructionType>>();
                foreach(var unresolved in unresolvedOpcodes)
                {
                    instructionValues.Add(unresolved, unresolvedInstructions.ToList());
                }

                //Run the samples
                foreach (var sample in _samples)
                {
                    var i = new WristComputer.Instruction();
                    i.RegisterA = sample.instruction[1];
                    i.RegisterB = sample.instruction[2];
                    i.RegisterC = sample.instruction[3];

                    foreach (var instructionType in instructionTypes)
                    {
                        i.InstructionType = instructionType;
                        TryInstruction(sample, i, instructionValues);      //Remove all instructions that can not be the given opcode
                    }
                }

                //If any of the unresolved instructions reach a count of 1, then that is the only possible code that can correspond to that instruction
                foreach(var pair in instructionValues)
                {
                    if(pair.Value.Count == 1)
                    {
                        Console.WriteLine($"Resolved {pair.Key} to {pair.Value.First()}");
                        unresolvedInstructions.Remove(pair.Value.First());
                        unresolvedOpcodes.Remove(pair.Key);
                    }
                }
            }
        }

        private void TestInstructionOnSamples()
        {
            foreach (var sample in _samples)
            {
                var i = new WristComputer.Instruction();
                i.InstructionType = (WristComputer.InstructionType)sample.instruction[0];
                i.RegisterA = sample.instruction[1];
                i.RegisterB = sample.instruction[2];
                i.RegisterC = sample.instruction[3];

                if (!TryInstruction(sample, i))
                {
                    Console.WriteLine($"{sample.instruction[0]}");
                }
            }
        }

        public override void ProblemTwo()
        {
            var computer = new WristComputer();
            computer.Program = _testProgram;
            computer.RunTilHalt();
            var result = computer.Registers[0];
            Console.WriteLine(result);
        }


        private readonly List<(List<int> before, List<int> instruction, List<int> after)> _samples = new();
        private readonly List<WristComputer.Instruction> _testProgram = new List<WristComputer.Instruction>();
        private void ParseInput(string input)
        {
            var halves = input.Split(new string[] { "\n\n\n\n" }, StringSplitOptions.RemoveEmptyEntries);

            var samples = halves[0].SplitNewLine();
            var program = halves[1].SplitNewLine();

            List<int> Parse(string numbers) => numbers.Split(' ').Select(i => int.Parse(i)).ToList();
            string GetNumString(string numbers)
            {
                var open = numbers.IndexOf('[');
                var close = numbers.IndexOf(']');
                numbers = numbers.Substring(open + 1, close - open - 1);
                numbers = numbers.Replace(",", string.Empty);

                return numbers;
            }

            for(int i = 0; i < samples.Length; i+=3)
            {
                var first = samples[i];
                var middle = samples[i + 1];
                var last = samples[i+2];

                var before = Parse(GetNumString(first));
                var instruction = Parse(middle);
                var after = Parse(GetNumString(last));

                _samples.Add((before, instruction, after));
            }

            foreach(var l in program)
            {
                var nums = Parse(l);
                var i = new WristComputer.Instruction();
                i.Opcode = nums[0];
                i.InstructionType = (WristComputer.InstructionType)nums[0];
                i.RegisterA = nums[1];
                i.RegisterB = nums[2];
                i.RegisterC = nums[3];
                _testProgram.Add(i);
            }
        }

        private const string Example = @"Before: [3, 2, 1, 1]
9 2 1 2
After:  [3, 2, 2, 1]




";
    }
}