using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Years.Utils;

namespace Years.Year2016
{
    public class Day05 : IDay
    {
        public int Day => 5;
        public int Year => 2016;


        public void ProblemOne()
        {
            string password = "";
            long counter = 0;

            while (password.Length < 8)
            {
                var hash = Extensions.ComputeHashFromUtf8String(Input + counter);
                if (hash[0] == 0 && hash[1] == 0 && hash[2] <= 16)
                {

                    string hashString = hash.ToHexString();
                    password += hashString[5];
                }

                //if (counter % 100000 == 0)
                //{
                //    Console.WriteLine(counter + " " + password.Length);
                //}

                counter++;
            }
            
            Console.WriteLine(password);
        }

        public void ProblemTwo()
        {
            char[] password = new char[8];
            long counter = 0;

            while (password.Contains('\0'))
            {
                var hash = Extensions.ComputeHashFromUtf8String(Input + counter);
                if (hash[0] == 0 && hash[1] == 0 && hash[2] <= 16)
                {
                    var hexString = hash.ToHexString();

                    if (int.TryParse(hexString[5].ToString(), out int index) && index >= 0 && index <= 7 && password[index] == '\0')
                    {
                        password[index] = hexString[6];
                    }
                }

                //if (counter % 100000 == 0)
                //{
                //    Console.WriteLine(counter + " " + password.Count(i => i != '\0'));
                //}

                counter++;
            }
            Console.WriteLine(string.Join(null, password));
        }


        private static string Input = @"uqwqemis";
        private static string Example = @"";

    }
}