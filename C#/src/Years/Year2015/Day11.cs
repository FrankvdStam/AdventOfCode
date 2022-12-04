using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    public class Day11 : BaseDay
    {
        public Day11() : base(2015, 11) { }

        private string _problemOneResult;

        public override void ProblemOne()
        {
           // bool correct = IsPasswordValid(StringPasswordToUlong("cqjxxyzz"));


            ulong pwd = StringPasswordToUlong(Input.Replace("\n", String.Empty));
            while (true)
            {
                pwd = IncrementPassword(pwd);
                //Console.WriteLine(UlongPasswordToString(pwd));
                if (IsPasswordValid(pwd))
                {
                    _problemOneResult = UlongPasswordToString(pwd);
                    Console.WriteLine(_problemOneResult);
                    return;
                }
            }
        }

        public override void ProblemTwo()
        {
            ulong pwd = StringPasswordToUlong(_problemOneResult);
            while (true)
            {
                pwd = IncrementPassword(pwd);
                //Console.WriteLine(UlongPasswordToString(pwd));
                if (IsPasswordValid(pwd))
                {
                    _problemOneResult = UlongPasswordToString(pwd);
                    Console.WriteLine(_problemOneResult);
                    return;
                }
            }
        }



        //Strings are slow. To make this blazing fast we can convert 8 char (8 bytes) into an unsigned 64bit integer, that will make things a lot faster.

        private Dictionary<char, ulong> _charEncoding = new Dictionary<char, ulong>()
        {
            {'a', 0 },
            {'b', 1 },
            {'c', 2 },
            {'d', 3 },
            {'e', 4 },
            {'f', 5 },
            {'g', 6 },
            {'h', 7 },
            {'i', 8 },
            {'j', 9 },
            {'k', 10},
            {'l', 11},
            {'m', 12},
            {'n', 13},
            {'o', 14},
            {'p', 15},
            {'q', 16},
            {'r', 17},
            {'s', 18},
            {'t', 19},
            {'u', 20},
            {'v', 21},
            {'w', 22},
            {'x', 23},
            {'y', 24},
            {'z', 25},
        };
        //i, o, or l (8 11 14) - for speed I've made these constants, they could be fetched from the encoding maps.
        private List<ulong> _illegalChars = new List<ulong>()
        {
            8, 11, 14
        };


        private Dictionary<ulong, char> _longEncoding = new Dictionary<ulong, char>()
        {
            { 0 , 'a'},
            { 1 , 'b'},
            { 2 , 'c'},
            { 3 , 'd'},
            { 4 , 'e'},
            { 5 , 'f'},
            { 6 , 'g'},
            { 7 , 'h'},
            { 8 , 'i'},
            { 9 , 'j'},
            { 10, 'k'},
            { 11, 'l'},
            { 12, 'm'},
            { 13, 'n'},
            { 14, 'o'},
            { 15, 'p'},
            { 16, 'q'},
            { 17, 'r'},
            { 18, 's'},
            { 19, 't'},
            { 20, 'u'},
            { 21, 'v'},
            { 22, 'w'},
            { 23, 'x'},
            { 24, 'y'},
            { 25, 'z'},
        };

        private ulong StringPasswordToUlong(string password)
        {
            //Reverse the password so we don't have to reverse the counter or do crazy math.
            password = password.Reverse();
            ulong result = 0;
            for (int i = password.Length - 1; i >= 0; i--)
            {
                result = result | _charEncoding[password[i]] << i * 8;

            }
            return result;
        }

        private string UlongPasswordToString(ulong password)
        {
            //Reverse the password later so we don't have to reverse the counter or do crazy math.
            string stringPassword = "";

            ulong mask = 0x00000000000000ff;
            for (int i = 0; i < 8; i++)
            {
                ulong encodedChar = (password & mask << i * 8) >> i * 8;
                stringPassword += _longEncoding[encodedChar];
            }
            return stringPassword.Reverse();
        }

        private ulong IncrementPassword(ulong password)
        {
            ulong mask = 0x00000000000000ff;

            for (int i = 0; i < 8; i++)
            {
                bool carry = false;

                ulong char_ = (password & mask << i * 8) >> i * 8;

                //If we are about to overflow
                if (char_ + 1 > 25)
                {
                    char_ = 0;
                    carry = true;
                }
                else
                {
                    char_ += 1;
                }
                
                ulong clear_mask = 0xffffffffffffff00;

                //First clear the char
                password = password & (clear_mask << i * 8);

                //Now set the new char
                password = password | char_ << i * 8;

                if (!carry)
                {
                    return password;
                }
            }
            return password;
        }


        //Passwords must include one increasing straight of at least three letters, like abc, bcd, cde, and so on, up to xyz.They cannot skip letters; abd doesn't count.
        //Passwords may not contain the letters i, o, or l, as these letters can be mistaken for other characters and are therefore confusing.
        //Passwords must contain at least two different, non-overlapping pairs of letters, like aa, bb, or zz.
        //
        //For example:
        //
        //hijklmmn meets the first requirement (because it contains the straight hij) but fails the second requirement requirement(because it contains i and l).
        //abbceffg meets the third requirement(because it repeats bb and ff) but fails the first requirement.
        // abbcegjk fails the third requirement, because it only has one double letter(bb).
        
        private bool IsPasswordValid(ulong password)
        {
            int nonOverlappingPairs = 0;
            bool hasStraightOfThree = false;
            for (int i = 0; i < 8; i++)
            {
                ulong previous = ulong.MaxValue;
                ulong first = (password & (0x00000000000000ffUL << i * 8)) >> i * 8 ;
                ulong second = ulong.MaxValue;
                ulong third = ulong.MaxValue;

                if (i != 0)
                {
                    previous = (password & (0x00000000000000ffUL << (i - 1) * 8)) >> (i - 1) * 8;
                }

                if (i < 8 - 1)
                {
                    second = (password & (0x00000000000000ffUL << (i + 1) * 8)) >> (i + 1) * 8;
                }

                if (i < 8 - 2)
                {
                    third = (password & (0x00000000000000ffUL << (i + 2) * 8)) >> (i + 2) * 8;
                }

                if (_illegalChars.Contains(first))
                {
                    return false;
                }

                if (!hasStraightOfThree && i < 8 - 2)
                {

                    if (first > 0 && first - 1 == second && second > 0 && second - 1 == third)
                    {
                        hasStraightOfThree = true;
                    }
                }

                if (nonOverlappingPairs < 2 && i < 8 - 1)
                {
                    if (first == second && first != previous && first != third)
                    {
                        nonOverlappingPairs++;
                    }
                }

            }

            return hasStraightOfThree && nonOverlappingPairs >= 2;
        }

    }
}