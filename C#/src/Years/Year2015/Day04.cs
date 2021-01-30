using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    public class Day04 : IDay
    {
        public int Day => 4;
        public int Year => 2015;

        public void ProblemOne()
        {
            long i = 0;
            while(true)
            {
                var hash = Extensions.ComputeHashFromUtf8String(Input + i);

                if (hash[0] == 0 && hash[1] == 0 && hash[2] <= 16)
                {
                    Console.WriteLine(i);
                    return;
                }
                i++;
            }
        }

        public void ProblemTwo()
        {
            long i = 0;
            while (true)
            {
                var hash = Extensions.ComputeHashFromUtf8String(Input + i);

                if (hash[0] == 0 && hash[1] == 0 && hash[2] == 0)
                {
                    Console.WriteLine(i);
                    return;
                }
                i++;
            }
        }

        private const string Example = @"abcdef";
        private const string Input = @"bgvyzdsv";
    }
}