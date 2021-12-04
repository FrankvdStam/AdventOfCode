using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using Years.Utils;
using Years.Year2017.SoundVirtualMachine;

namespace Years.Year2017
{
    public class Day18 : IDay
    {
       


        
        public int Day => 18;
        public int Year => 2017;

        public void ProblemOne()
        {
            var vm = new VirtualMachine(VirtualMachine.ParseInput(Input));
            var firstRecovery = vm.RunTillRecovery();
            Console.WriteLine(firstRecovery);
        }

        public void ProblemTwo()
        {
            var program1 = new VirtualMachine(VirtualMachine.ParseInput(Input));
            program1.SetRegister('p', 0);
            var program2 = new VirtualMachine(VirtualMachine.ParseInput(Input));
            program2.SetRegister('p', 1);

            var stepCountA = program1.StepCount;
            var stepCountB = program2.StepCount;

            while (true)
            {
                program1.Received.AddRange(program2.Send.Clone());
                program2.Send.Clear();
                program1.RunTillRecovery();

                program2.Received.AddRange(program1.Send.Clone());
                program1.Send.Clear();
                program2.RunTillRecovery();

                //Both halted
                if (stepCountA == program1.StepCount && stepCountB == program2.StepCount)
                {
                    Console.WriteLine(program2.SendCount);
                    return;
                }

                stepCountA = program1.StepCount;
                stepCountB = program2.StepCount;
            }
        }
        

        private const string Example = @"set a 1
add a 2
mul a a
mod a 5
snd a
set a 0
rcv a
jgz a -1
set a 1
jgz a -2";

        private const string Example2 = @"snd 1
snd 2
snd p
rcv a
rcv b
rcv c
rcv d";

        private const string Input = @"set i 31
set a 1
mul p 17
jgz p p
mul a 2
add i -1
jgz i -2
add a -1
set i 127
set p 826
mul p 8505
mod p a
mul p 129749
add p 12345
mod p a
set b p
mod b 10000
snd b
add i -1
jgz i -9
jgz a 3
rcv b
jgz b -1
set f 0
set i 126
rcv a
rcv b
set p a
mul p -1
add p b
jgz p 4
snd a
set a b
jgz 1 3
snd b
set f 1
add i -1
jgz i -11
snd a
jgz f -16
jgz a -19";
    }
}