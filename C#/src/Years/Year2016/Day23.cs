using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;
using Years.Year2016.Assembunny;

namespace Years.Year2016
{
    public class Day23 : IDay
    {
        public int Day => 23;
        public int Year => 2016;


        public void ProblemOne()
        {
            AssembunnyVirtualMachine avm = new AssembunnyVirtualMachine(Input);
            avm.RegisterValues['a'] = 7;
            avm.Run();
            Console.WriteLine(avm.RegisterValues['a']);
        }

        public void ProblemTwo()
        {
            var result = 99 * 77 + 12.Factorial();
            Console.WriteLine(result);

            return;
            //AssembunnyVirtualMachine avm = new AssembunnyVirtualMachine(Input);
            //avm.RegisterValues['a'] = 12;
            //avm.Run();
            //Console.WriteLine(avm.RegisterValues['a']);
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