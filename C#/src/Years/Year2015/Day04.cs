using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    class Program4
    {
        private static string input = "bgvyzdsv";
        private static string example = "abcdef";


        #region https://www.reddit.com/r/adventofcode/comments/3vdn8a/day_4_solutions/
        class MD5Hasher
        {
            private bool doesMD5HashStartWithNZeros(MD5 md5Hash, string input, long howMany)
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                if (howMany > data.Length)
                    // technically, it could go up to double the length, but let's be reasonable
                    throw new ArgumentException();

                long half = howMany / 2;
                //  first, check the whole bytes.
                for (int i = 0; i < half; i++)
                    if (data[i] != 0)
                        return false;

                // do we need another half a byte?
                if (howMany % 2 == 1)
                {
                    if (data[half] > 0x0f)
                        return false;
                }
                return true;
            }

            public long FindLowestHashThatStartsWithNZeros(string key, long howMany)
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    long counter = 0;
                    string currentString = key + counter;

                    while (!doesMD5HashStartWithNZeros(md5Hash, currentString, howMany))
                    {
                        counter++;
                        currentString = key + counter;
                    }
                    return counter;
                }

            }
        }

        static void Main2(string[] args)
        {
            MD5Hasher hasher = new MD5Hasher();

            string key = "bgvyzdsv";
            DateTime start = DateTime.Now;
            long fiveZerosResult = hasher.FindLowestHashThatStartsWithNZeros(key, 5);
            Console.WriteLine("5 zeros: {0}", fiveZerosResult);

            long sixZerosResult = hasher.FindLowestHashThatStartsWithNZeros(key, 6);
            Console.WriteLine("6 zeros: {0}", sixZerosResult);

            if (sixZerosResult <= fiveZerosResult)
                throw new Exception("test case failed");

            long totalIterations = fiveZerosResult + sixZerosResult;
            double numMilliseconds = (DateTime.Now - start).TotalMilliseconds;

            Console.WriteLine("{0} iterations in {1} milliseconds", totalIterations, numMilliseconds);
            Console.ReadKey();
        }
        #endregion

        static void Main(string[] args)
        {
            ProblemOne(input);
            Console.ReadKey();
        }

        static void ProblemOne(string input)
        {
            int count = 0;
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            StringBuilder sb = new StringBuilder();
            Dictionary<string, string> hashes = new Dictionary<string, string>(count);

            for (int i = 0; i < count; i++)
            {
                foreach (byte b in md5.ComputeHash(Encoding.ASCII.GetBytes(input)))
                {
                    sb.Append(b.ToString("x2"));
                }
                hashes[input + i] = sb.ToString();
                sb.Clear();
            }

            var bestHash = FindBestHash(hashes);
            Console.WriteLine(bestHash.Key + "\n" + bestHash.Value);
        }

        static KeyValuePair<string, string> FindBestHash(Dictionary<string, string> hashes)
        {
            KeyValuePair<string, string> bestHash = new KeyValuePair<string, string>();
            int best = int.MaxValue;
            int trailingZeros = int.MaxValue;



            foreach (var keyValuePair in hashes)
            {
                if (!keyValuePair.Value.StartsWith("00000"))
                {
                    continue;
                }

                int length = 0;
                //Minimum of 5 trailing zeros
                int curzeros = 5;
                for (int i = 5; i < keyValuePair.Value.Length; i++)
                {
                    char c = keyValuePair.Value[i];
                    //Count zero's
                    if (c == '0')
                    {
                        curzeros++;
                        continue;
                    }
                    else
                    {
                        if (int.TryParse(c.ToString(), out int result))
                        {
                            length++;
                        }
                        else
                        {
                            //Number ended.
                            bool hasNumber = int.TryParse(keyValuePair.Value.Substring(curzeros, length), out int num);

                            //new best
                            if ((trailingZeros > curzeros) || (hasNumber && trailingZeros == curzeros && best > num))
                            {
                                trailingZeros = curzeros;
                                best = num;
                                bestHash = keyValuePair;
                            }
                            break;
                        }
                    }
                }
            }


            return bestHash;
        }

        public static string GetHash(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            var bytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();

        }
    }

    public class Day04 : IDay
    {
        public int Day => 4;
        public int Year => 2015;

        public void ProblemOne()
        {
        }

        public void ProblemTwo()
        {
        }
    }
}