using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Day05
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProblemOne("uqwqemis");
            ProblemTwo("uqwqemis");
        }

        //https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.md5?view=netframework-4.7.2
        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        static void ProblemOne(string input)
        {
            string password = "";
            using (MD5 md5Hash = MD5.Create())
            {
                long counter = 0;

                while (password.Length < 8)
                {
                    string hash = GetMd5Hash(md5Hash, input+counter);

                    if (hash.StartsWith("00000"))
                    {
                        password += hash[5];
                    }

                    if (counter % 100000 == 0)
                    {
                        Console.WriteLine(counter + " " + password.Length);
                    }

                    counter++;
                }
            }
        }
        


        static void ProblemTwo(string input)
        {
            char[] password = new char[8];
            using (MD5 md5Hash = MD5.Create())
            {
                long counter = 0;

                while (password.Contains('\0'))
                {
                    string hash = GetMd5Hash(md5Hash, input + counter);

                    if (hash.StartsWith("00000"))
                    {
                        if (int.TryParse(hash[5].ToString(), out int index) && index >= 0 && index <= 7 && password[index] == '\0')
                        {
                            password[index] = hash[6];
                        }
                    }

                    if (counter % 100000 == 0)
                    {
                        Console.WriteLine(counter + " " + password.Count(i => i != '\0'));
                    }

                    counter++;
                }
            }

            string result = "";
            foreach (char c in password)
            {
                result += c;
            }
        }

        static void ParseInput(string input)
        {
            var lines = input.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                var bits = line.Split(' ');
            }
        }


        private static string Input = @"";
        private static string Example = @"";

    }
}
