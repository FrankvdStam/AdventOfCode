using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Shared
{
    public class IntCodeComputer
    {
        public IntCodeComputer() { }

        public IntCodeComputer(string input)
        {
            LoadProgramFromString(input);
        }


        public List<int> Program = new List<int>();


        public void LoadProgramFromString(string input)
        {
            Program = ParseProgram(input);
        }

        public void Run()
        {
            int position = 0;

            int? locationA = null;
            int? locationB = null;
            int? locationC = null;

            while (true)
            {
                if (position + 3 < Program.Count)
                {

                    locationA = Program[position + 1];
                    locationB = Program[position + 2];
                    locationC = Program[position + 3];
                }

                switch (Program[position])
                {
                    case 1:
                        //Add
                        //Console.WriteLine($"Adding: {program[position]} location {locationC} = {program[locationA.Value]} + {program[locationB.Value]}");

                        Program[locationC.Value] = Program[locationA.Value] + Program[locationB.Value];
                        position += 4;

                        break;
                    case 2:
                        //Mult
                        //Console.WriteLine($"Multiplying: {program[position]} location {locationC} = {program[locationA.Value]} * {program[locationB.Value]}");
                        Program[locationC.Value] = Program[locationA.Value] * Program[locationB.Value];
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




        public static List<int> ParseProgram(string input)
        {
            List<int> result = new List<int>();
            var lines = input.Split(new string[] { "," }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                result.Add(int.Parse(line));
            }
            return result;
        }
    }
}
