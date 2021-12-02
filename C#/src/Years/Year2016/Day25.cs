using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;
using Years.Year2016.Assembunny;

namespace Years.Year2016
{
    public class Day25 : IDay
    {
        public int Day => 25;
        public int Year => 2016;

        public void ProblemOne()
        {
            for (int i = 1; i < int.MaxValue; i++)
            {
                if (RunVm(i, 1000000))
                {
                    Console.WriteLine(i);
                    return;
                }
            }
        }

        private bool RunVm(int number, int cycles)
        {
            var vm = new AssembunnyVirtualMachine(Input);
            vm.RegisterValues['a'] = number;

            bool run = true;
            bool succes = true;
            int? signalState = null;
            vm.OnOutput += output =>
            {
                //if output is not 1 or 0, stop running.
                if (output != 0 && output != 1)
                {
                    run = false;
                    succes = false;
                }

                if (!signalState.HasValue)
                {
                    signalState = output;
                }
                else
                {
                    //Check if signal is alternating
                    if ((output == 1 && signalState == 0) || (signalState == 1 && output == 0))
                    {
                        signalState = output;
                    }
                    else
                    {
                        run = false;
                        succes = false;
                    }
                }
            };

            for (int i = 0; i < cycles; i++)
            {
                vm.Step();

                //Break out of loop as soon as failure is detected
                if (!run)
                {
                    break;
                }
            }
            return succes;
        }

        public void ProblemTwo()
        {
        }

        private const string Input = @"cpy a d
cpy 7 c
cpy 362 b
inc d
dec b
jnz b -2
dec c
jnz c -5
cpy d a
jnz 0 0
cpy a b
cpy 0 a
cpy 2 c
jnz b 2
jnz 1 6
dec b
dec c
jnz c -4
inc a
jnz 1 -7
cpy 2 b
jnz c 2
jnz 1 4
dec b
dec c
jnz 1 -4
jnz 0 0
out b
jnz a -19
jnz 1 -21";
    }
}