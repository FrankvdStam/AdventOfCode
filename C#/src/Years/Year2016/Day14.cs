using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Years.Utils;

namespace Years.Year2016
{
    public class Day14 : IDay
    {
        public int Day => 14;
        public int Year => 2016;

        public void ProblemOne()
        {
            var trippletHashLookup = new List<(byte, long, string)>();
            var keys = new List<(byte, long, string)>();

            using (MD5 md5 = MD5.Create())
            {
                long i = 0;
                while (true)
                {
                    var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(Input + i));

                    var tripplet = ContainsTripplet(hash);
                    var quanTupplet = ContainsQuantupple(hash);



                    if (tripplet != null /* && quanTupplet == null*/)
                    {
                        trippletHashLookup.Add((tripplet.Value, i, hash.ToHexString()));
                    }
                    
                    if (quanTupplet != null)
                    {
                        keys.AddRange(trippletHashLookup.Where(j => j.Item1 == quanTupplet.Value && j.Item2 >= i - 1000 && j.Item3 != hash.ToHexString()));
                        if (keys.Count >= 64)
                        {
                            Console.WriteLine(keys[63].Item2);
                            return;
                        }
                    }
                    i++;
                }
            }
        }


        public void ProblemTwo()
        {
            var trippletHashLookup = new List<(byte, long, string)>();
            var keys = new List<(byte, long, string)>();

            using (MD5 md5 = MD5.Create())
            {
                long i = 0;
                while (true)
                {

                    byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(Input + i));
                    for (int j = 0; j < 2016; j++)
                    {
                        hash = md5.ComputeHash(Encoding.ASCII.GetBytes(hash.ToHexString()));
                    }


                    var tripplet = ContainsTripplet(hash);
                    var quanTupplet = ContainsQuantupple(hash);



                    if (tripplet != null)
                    {
                        trippletHashLookup.Add((tripplet.Value, i, hash.ToHexString()));
                    }

                    if (quanTupplet != null)
                    {
                        keys.AddRange(trippletHashLookup.Where(j => j.Item1 == quanTupplet.Value && j.Item2 >= i - 1000 && j.Item3 != hash.ToHexString()));
                        if (keys.Count >= 64)
                        {
                            Console.WriteLine(keys[63].Item2);
                            return;
                        }
                    }
                    i++;
                }
            }
        }



        private byte? ContainsTripplet(byte[] hash)
        {
            byte mask = 0b00001111;

            for (int i = 0; i < hash.Length; i++)
            {
                if (i + 1 < hash.Length)
                {
                    //In hex format we compare 3 chars, in binary we compare 4 bits of each byte
                    byte first = hash[i];
                    byte second = hash[i + 1];

                    if (
                        //check first 3 hex chars, skip last
                        (
                            (first & mask) == (first >> 4)
                            &&
                            (first & mask) == (second >> 4)
                        )
                        ||
                        //check last 3 hex chars, skip first
                        (
                            (first & mask) == (second >> 4)
                            &&
                            (second & mask) == (second >> 4)
                        )
                    )
                    {
                        //We can safely grab a char from the middle
                        return (byte)(first & mask);
                    }
                }
            }
            return null;
        }

        private byte? ContainsQuantupple(byte[] hash)
        {
            byte mask = 0b00001111;

            for (int i = 0; i < hash.Length; i++)
            {
                if (i + 2 < hash.Length)
                {
                    //In hex format we compare 3 chars, in binary we compare 4 bits of each byte
                    byte first = hash[i];
                    byte second = hash[i + 1];
                    byte thrird = hash[i + 2];

                    if (
                        //check first 5 hex chars, skip last
                        (
                            //Check if first byte contains same high and low bits
                            (first & mask) == (first >> 4)
                            &&
                            //Check if first and second byte contain the same low bits
                            (second & mask) == (first & mask)
                            &&
                            //Check if second byte contains same high and low bits
                            (second & mask) == (second >> 4)
                            &&
                            //Check if third byte contains same high bits as first bytes low bits
                            (thrird >> 4) == (first & mask)
                        )
                        ||
                        //check last 5 hex chars, skip first
                        (
                            //Check if first byte contains same low bits as second
                            (first & mask) == (second & mask)
                            &&
                            //Check if second byte contains same high and low bits
                            (second & mask) == (second >> 4)
                            &&
                            (second & mask) == (thrird & mask)
                            &&
                            //Check if third byte contains same high bits as first bytes low bits
                            (thrird >> 4) == (first & mask)
                        )
                    )
                    {
                        //We can safely grab a char from the middle
                        return (byte)(first & mask);
                    }
                }
            }
            return null;
        }


        private const string Input = @"ahsbgdzn";
        private const string Example = @"abc";
    }
}