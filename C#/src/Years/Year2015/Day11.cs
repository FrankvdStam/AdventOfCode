using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    class Program11
    {


        static string GenPass(string password)
        {
            var bytes = Encoding.ASCII.GetBytes(password);

            for (int i = bytes.Length - 1; i > -1; i--)
            {
                bytes[i]++;

                //Get rid of illegal chars
                while (IllegalChars.Contains((char)bytes[i]))
                {
                    bytes[i]++;
                }

                if (bytes[i] > (byte)'z')
                {
                    bytes[i] = (byte)'a';
                }
                else
                {
                    return Encoding.ASCII.GetString(bytes);
                }
            }

            return Encoding.ASCII.GetString(bytes);
        }



        static void Main(string[] args)
        {
            //"cqkaabcc"

            string password = "cqjxxzaa";
            password = OptimizeInitialPassword(password);

            while (!IsPasswordValid(password))
            {
                password = GenPass(password);


                Console.WriteLine(password);
            }
            Console.WriteLine(password);
            Console.ReadKey();
        }

        //remove any illegal chars that currently are in the string
        static string OptimizeInitialPassword(string password)
        {
            var bytes = Encoding.ASCII.GetBytes(password);

            for (int i = 0; i < bytes.Length; i++)
            {
                //Once we find an illegal char, we must will the rest of the string with a's 
                //Else we will skip ahead.
                if (IllegalChars.Contains((char)bytes[i]))
                {
                    bytes[i]++;
                    for (int j = i + 1; j < bytes.Length; j++)
                    {
                        bytes[j] = (byte)'a';
                    }
                    return Encoding.ASCII.GetString(bytes);
                }
            }

            return Encoding.ASCII.GetString(bytes);
        }

        static string NextPassword(string password)
        {
            char a = 'a';
            char z = 'z';


            var bytes = Encoding.ASCII.GetBytes(password);
            for (int i = bytes.Length - 1; i > -1; i--)
            {
                if (bytes[i] == z)
                {
                    bytes[i] = (byte)a;
                    bytes[i - 1]++;

                    //if (IllegalChars.Contains((char)bytes[i - 1]))
                    //{
                    //    bytes[i - 1]++;
                    //}

                    if (bytes[i - 1] < z + 1)
                    {
                        return Encoding.ASCII.GetString(bytes);
                    }
                    continue;
                }
                else
                {
                    bytes[i]++;
                    if (IllegalChars.Contains((char)bytes[i]))
                    {
                        bytes[i]++;
                    }
                    return Encoding.ASCII.GetString(bytes);
                }
            }
            throw new Exception("No next password?");
        }



        static bool IsPasswordValid(string password)
        {
            return ContainsStraight(password) && !ContainsIllegalChars(password) && ContainsTwoDifferentNonOverlappingPairs(password);
        }

        static bool ContainsStraight(string password)
        {
            for (int i = 0; i + 2 < password.Length; i++)
            {
                if (password[i] == password[i + 1] - 1 && password[i] == password[i + 2] - 2)
                {
                    return true;
                }
            }
            return false;
        }

        private static readonly char[] IllegalChars = { 'i', 'o', 'l' };
        static bool ContainsIllegalChars(string password)
        {
            foreach (char c in password)
            {
                if (IllegalChars.Contains(c))
                {
                    return true;
                }
            }
            return false;
        }

        static bool ContainsTwoDifferentNonOverlappingPairs(string password)
        {
            List<char> pairChars = new List<char>();
            for (int i = 0; i + 1 < password.Length; i++)
            {
                if (password[i] == password[i + 1])
                {
                    if (!pairChars.Contains(password[i]))
                    {
                        if (pairChars.Any())//We will add the 2nd unique item bellow. No need to wait for this, we know that there is 2 unique pairs.
                        {
                            return true;
                        }
                        pairChars.Add(password[i]);
                    }
                    i++;//Increment twice to avoid overlap.
                }
            }
            return false;
        }
    }


    public class Day11 : IDay
    {
        public int Day => 11;
        public int Year => 2015;

        public void ProblemOne()
        {
        }

        public void ProblemTwo()
        {
        }
    }
}