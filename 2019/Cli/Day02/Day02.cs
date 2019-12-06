using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Cli.Day02
{
    public class Day02 : IDay
    {
        public int Day => 2;

        public Day02()
        {
            TestPrograms();
        }

        public void ProblemOne()
        {
            List<int> program = ParseInput(Input);
            program[1] = 12;
            program[2] = 2;

            RunProgram(program);
            int value = program[0];
            Console.WriteLine($"Halted with position 0: {value}");
        }

        public void ProblemTwo()
        {
            List<int> program = ParseInput(Input);

            for (int x = 0; x <= 99; x++)
            {
                for (int y = 0; y <= 99; y++)
                {
                    var programClone = program.Clone();
                    programClone[1] = x;
                    programClone[2] = y;
                    RunProgram(programClone);
                    Console.WriteLine($"({x}, {y})");
                    int value = programClone[0];

                    if (value == 19690720)
                    {
                        //516000

                    }

                }
            }
        }

       

        public void RunProgram(List<int> program)
        {
            int position = 0;

            int? locationA = null;
            int? locationB = null;
            int? locationC = null;


            while (true)
            {
                if (position + 3 < program.Count)
                {

                    locationA = program[position + 1];
                    locationB = program[position + 2];
                    locationC = program[position + 3];
                }
            
                switch (program[position])
                {
                    case 1:
                        //Add
                        //Console.WriteLine($"Adding: {program[position]} location {locationC} = {program[locationA.Value]} + {program[locationB.Value]}");

                        program[locationC.Value] = program[locationA.Value] + program[locationB.Value];
                        position += 4;

                        break;
                    case 2:
                        //Mult
                        //Console.WriteLine($"Multiplying: {program[position]} location {locationC} = {program[locationA.Value]} * {program[locationB.Value]}");
                        program[locationC.Value] = program[locationA.Value] * program[locationB.Value];
                        position += 4;

                        break;

                    case 99:
                        //Halt!
                        return;


                    default:
                        throw new Exception("Something went wrong.");
                }

                locationA = null;
                locationB = null;
                locationC = null;
            }
        }



        
       


        private void TestPrograms()
        {
            if(!TestProgram("1,0,0,0,99"         , "2,0,0,0,99"          ))
            {
                throw new Exception("Test program failed!");
            }
            if(!TestProgram("2,3,0,3,99"         , "2,3,0,6,99"          ))
            {
                throw new Exception("Test program failed!");
            }
            if (!TestProgram("2,4,4,5,99,0"       , "2,4,4,5,99,9801"     ))
            {
                throw new Exception("Test program failed!");
            }
            if (!TestProgram("1,1,1,4,99,5,6,0,99", "30,1,1,4,2,5,6,0,99" ))
            {
                throw new Exception("Test program failed!");
            }
        }

        private bool TestProgram(string input, string result)
        {
            var inputProgram = ParseInput(input);
            var resultProgram = ParseInput(result);

            RunProgram(inputProgram);

            if (inputProgram.Count != resultProgram.Count)
            {
                return false;
            }

            for (int i = 0; i < inputProgram.Count; i++)
            {
                if (inputProgram[i] != resultProgram[i])
                {
                    return false;
                }
            }

            return true;
        }

        public List<int> ParseInput(string input)
        {
            List<int> result = new List<int>();
            var lines = input.Split(new string[] { "," }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                result.Add(int.Parse(line));
            }
            return result;
        }

        private string Example = @"1,9,10,3,2,3,11,0,99,30,40,50";

        private string Input =
            @"1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,6,1,19,2,19,13,23,1,23,10,27,1,13,27,31,2,31,10,35,1,35,9,39,1,39,13,43,1,13,43,47,1,47,13,51,1,13,51,55,1,5,55,59,2,10,59,63,1,9,63,67,1,6,67,71,2,71,13,75,2,75,13,79,1,79,9,83,2,83,10,87,1,9,87,91,1,6,91,95,1,95,10,99,1,99,13,103,1,13,103,107,2,13,107,111,1,111,9,115,2,115,10,119,1,119,5,123,1,123,2,127,1,127,5,0,99,2,14,0,0";
    }
}
