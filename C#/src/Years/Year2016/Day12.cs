using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Years.Utils;
using Years.Year2016.Assembunny;

namespace Years.Year2016
{
    public class Day12 : IDay
    {
        public int Day => 12;
        public int Year => 2016;


        public void ProblemOne()
        {
            AssembunnyVirtualMachine avm = new AssembunnyVirtualMachine(Input);
            avm.Run();
            Console.WriteLine(avm.RegisterValues['a']);
        }

        public void ProblemTwo()
        {
            AssembunnyVirtualMachine avm = new AssembunnyVirtualMachine(Input);
            avm.RegisterValues['c'] = 1;
            avm.Run();
            Console.WriteLine(avm.RegisterValues['a']);
        }


        private static string Input = @"cpy 1 a
cpy 1 b
cpy 26 d
jnz c 2
jnz 1 5
cpy 7 c
inc d
dec c
jnz c -2
cpy a c
inc a
dec b
jnz b -2
cpy c b
dec d
jnz d -6
cpy 13 c
cpy 14 d
inc a
dec d
jnz d -2
dec c
jnz c -5";

        private static string Example = @"cpy 41 a
inc a
inc a
dec a
jnz a 2
dec a";
    }
}