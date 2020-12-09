using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    public class HashKey
    {
        public string Hash;

        /// <summary>
        /// Calculation index
        /// </summary>
        public int Index;

        /// <summary>
        /// If this ends up being a key, this holds the index of key
        /// </summary>
        public int KeyIndex;
        public bool IsKey;

        public bool HasTriplet;
        public char Triplet;

        public List<char> FiveChars;
    }


    class Program
    {
        static void Main(string[] args)
        {
            ProblemOne("abc");
            //ProblemTwo();
        }

        static void ProblemOne(string input)
        {
            List<HashKey> hashes = new List<HashKey>();


            int nexthashIndex = 0;
            int hashIndex = 0;
            int keyIndex = 1;

            using (MD5 md5Hash = MD5.Create())
            {
                //buffer 1000 hashes in advance
                while (nexthashIndex < 1000)
                {
                    if (nexthashIndex == 816)
                    {

                    }

                    if (nexthashIndex % 100 == 0)
                    {
                        Console.WriteLine(nexthashIndex);
                    }

                    string hash = GetHash(md5Hash, input + nexthashIndex);
                    var fives = FindXLets(hash, 5);

                    if (TryFindMultiChar(hash, 3, out char triplet))
                    {
                        hashes.Add(new HashKey()
                        {
                            Hash = hash,
                            Index = nexthashIndex,
                            Triplet = triplet,
                            HasTriplet = true,
                            FiveChars = fives,
                        });
                    }
                    else
                    {
                        hashes.Add(new HashKey()
                        {
                            Index = nexthashIndex,
                            Hash = hash,
                            FiveChars = fives,
                        });
                    }

                    nexthashIndex++;
                }

                //Now continue from here, jumping back to index 0 to examine the coming set.
                while (true)
                {
                    if (nexthashIndex % 100 == 0)
                    {
                        Console.WriteLine(nexthashIndex);
                    }

                    //First calculate the next hash that will be appended at the end (first will be 1001)
                    string hash = GetHash(md5Hash, input + nexthashIndex);
                    var fives = FindXLets(hash, 5);

                    if (TryFindMultiChar(hash, 3, out char triplet))
                    {
                        hashes.Add(new HashKey()
                        {
                            Hash = hash,
                            Index = nexthashIndex,
                            Triplet = triplet,
                            HasTriplet = true,
                            FiveChars = fives,
                        });
                    }
                    else
                    {
                        hashes.Add(new HashKey()
                        {
                            Index = nexthashIndex,
                            Hash = hash,
                            FiveChars = fives,
                        });
                    }
                    nexthashIndex++;


                    //now we check for the current hash (first will be 0, jumping back from 1000) if it meets the requirements.
                    var potentialKey = hashes[hashIndex];
                    //Only check if its a key if it is also a triplet.
                    if (potentialKey.HasTriplet)
                    {
                        if (hashes.Any(i =>
                            i.Index > potentialKey.Index && i.Index <= potentialKey.Index + 1000 &&
                            i.FiveChars.Contains(potentialKey.Triplet)))
                        {
                            //Found a key!
                            potentialKey.IsKey = true;
                            potentialKey.KeyIndex = keyIndex;
                            keyIndex++;

                            if (keyIndex == 64)
                            {

                            }
                        }
                    }

                    hashIndex++;
                    if (hashIndex == 22728)
                    {

                    }
                }
            }
        }


        static List<char> FindXLets(string input, int count)
        {
            var xLets = new List<char>();

            if (count < 1)
            {
                throw new ArgumentException();
            }

            int charCounter = 0;
            char c = '\0';

            for (int i = 0; i < input.Length; i++)
            {
                if (c != input[i])
                {
                    c = input[i];
                    charCounter = 1;
                }
                else
                {
                    charCounter++;
                    if (charCounter == count)
                    {
                        //Check char AFTER the current x-let, it could be the same and then this set doesn't count.
                        //BUT! only do it if there still are chars left in the string

                        if (i + 1 < input.Length && input[i + 1] != c)
                        {
                            xLets.Add(c);
                        }

                        if (i + 1 >= input.Length)
                        {
                            xLets.Add(c);
                        }
                    }
                }
            }
            return xLets;
        }

        static bool TryFindMultiChar(string input, int count, out char xLet)
        {
            if (count < 1)
            {
                throw new ArgumentException();
            }

            xLet = '\0';

            int charCounter = 0;
            char c = '\0';

            for (int i = 0; i < input.Length; i++)
            {
                if (c != input[i])
                {
                    c = input[i];
                    charCounter = 1;
                }
                else
                {
                    charCounter++;
                    if (charCounter == count)
                    {
                        //Check char AFTER the current x-let, it could be the same and then this set doesn't count.
                        //BUT! only do it if there still are chars left in the string
                        xLet = c;
                        if (i + 1 < input.Length && input[i + 1] != c)
                        {
                            return true;
                        }

                        if (i + 1 >= input.Length)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        static string GetHash(MD5 md5, string input)
        {
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                stringBuilder.Append(hash[i].ToString("X2"));
            }
            return stringBuilder.ToString().ToLower();
        }
    }
}
