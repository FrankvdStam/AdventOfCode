using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2016
{
    public class Day23 : IDay
    {
        public int Day => 23;
        public int Year => 2016;


        private List<string> _registers = new List<string>() { "a", "b", "c", "d", };

        private Dictionary<string, int> _registerValues = new Dictionary<string, int>()
        {
            {"a", 0},
            {"b", 0},
            {"c", 0},
            {"d", 0},
        };

        private static int _programCounter = 0;
        private static int _cycles = 0;



        public void ProblemOne()
        {
            var lines = Input.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            while (_programCounter >= 0 && _programCounter < lines.Length)
            {
                var line = lines[_programCounter];

                var bits = line.Split(' ');
                switch (bits[0])
                {
                    case "cpy":
                        Copy(line);
                        _programCounter++;
                        break;
                    case "inc":
                        Increment(line);
                        _programCounter++;
                        break;
                    case "dec":
                        Decrement(line);
                        _programCounter++;
                        break;
                    case "jnz":
                        JumpNotZero(line);
                        break;
                }

                _cycles++;
                //PrintRegisters();
            }

            //Print results:
            //PrintRegisters();
            Console.WriteLine(_registerValues["a"]);
        }

        public void ProblemTwo()
        {
        }








        private void Copy(string input)
        {
            var bits = input.Split(' ');
            if (_registers.Contains(bits[1]))
            {
                _registerValues[bits[2]] = _registerValues[bits[1]];
            }
            else
            {
                _registerValues[bits[2]] = int.Parse(bits[1]);
            }
        }

        private void Increment(string input)
        {
            var bits = input.Split(' ');
            _registerValues[bits[1]]++;
        }

        private void Decrement(string input)
        {
            var bits = input.Split(' ');
            _registerValues[bits[1]]--;
        }

        private void JumpNotZero(string input)
        {
            var bits = input.Split(' ');

            //Get the value
            int value;
            if (_registers.Contains(bits[1]))
            {
                value = _registerValues[bits[1]];
            }
            else
            {
                value = int.Parse(bits[1]);
            }

            //Jump if not zero
            if (value != 0)
            {
                _programCounter += int.Parse(bits[2]);
            }
            else
            {
                //Don't get stuck on the same instruction for ever..
                _programCounter++;
            }
        }



        private const string Example = @"cpy 2 a
tgl a
tgl a
tgl a
cpy 1 a
dec a
dec a";

        private const string Input = @"cpy a b
dec b
cpy a d
cpy 0 a
cpy b c
inc a
dec c
jnz c -2
dec d
jnz d -5
dec b
cpy b c
cpy c d
dec d
inc c
jnz d -2
tgl c
cpy -16 c
jnz 1 c
cpy 99 c
jnz 77 d
inc a
inc d
jnz d -2
inc c
jnz c -5";
    }
}