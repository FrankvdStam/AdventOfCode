using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;
using Years.Year2017.SoundVirtualMachine;

namespace Years.Year2017
{
    public class Day23 : IDay
    {
        public int Day => 23;
        public int Year => 2017;

        public void ProblemOne()
        {
            var vm = new VirtualMachine(VirtualMachine.ParseInput(Input));
            vm.Run();
            Console.WriteLine(vm.MulCount);
        }

        public void ProblemTwo()
        {
            var b = VirtualMachine.ParseInput(Input)[0].ValueB.Value;
            b = b * 100 + 100000;
            var count = Enumerable.Range(0, 1001).Count(i => !(b + 17 * i).IsPrime());
            Console.WriteLine(count);
        }


        private const string Input = @"set b 67
set c b
jnz a 2
jnz 1 5
mul b 100
sub b -100000
set c b
sub c -17000
set f 1
set d 2
set e 2
set g d
mul g e
sub g b
jnz g 2
set f 0
sub e -1
set g e
sub g b
jnz g -8
sub d -1
set g d
sub g b
jnz g -13
jnz f 2
sub h -1
set g b
sub g c
jnz g 2
jnz 1 3
sub b -17
jnz 1 -23";
    }
}