using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    public class Day05 : BaseDay
    {
        public Day05() : base(2015, 05) { }

        public override void ProblemOne()
        {
            var lines = Input.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            int count = 0;
            foreach (var line in lines)
            {
                if (IsNice(line))
                {
                    count++;
                }
            }
            Console.WriteLine(count);
        }

        public override void ProblemTwo()
        {
            var lines = Input.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var count = 0;
            foreach (var line in lines)
            {
                if (IsNiceTwo(line))
                {
                    count++;
                }
            }
            Console.WriteLine(count);
        }


        static bool IsNiceTwo(string input)
        {
            var chars = input.ToCharArray();

            bool foundMatch = false;
            for (int i = 0; i + 1 < input.Length; i++)
            {
                string pattern = "" + input[i] + input[i + 1];
                //Only look ahead, never back (to avoid duplicate looking)
                for (int j = i + 2; j + 1 < input.Length; j++)
                {
                    string potentialMatch = "" + input[j] + input[j + 1];
                    if (potentialMatch == pattern)
                    {
                        foundMatch = true;
                        break;
                    }
                }

                //Gota break out of the outer loop if inner loop was broken
                if (foundMatch)
                {
                    break;
                }
            }

            if (!foundMatch)
            {
                return false;
            }

            char prevPrevChar = '\0';
            char prevChar = '\0';
            foundMatch = false;
            foreach (char c in chars)
            {
                if (prevPrevChar == c)
                {
                    foundMatch = true;
                    break;
                }

                prevPrevChar = prevChar;
                prevChar = c;
            }

            if (!foundMatch)
            {
                return false;
            }
            return true;
        }



        private char[]   vowels = { 'a', 'e', 'i', 'o', 'u' };
        private string[] illegalStrings = { "ab", "cd", "pq", "xy" };

        private bool IsNice(string input)
        {
            var chars = input.ToCharArray();

            //Check vowel count
            int vowelCount = 0;
            foreach (char c in vowels)
            {
                vowelCount += chars.Count(i => i == c);
            }

            if (vowelCount < 3)
            {
                return false;
            }

            //Check dubble letters
            char prevChar = '\0';
            bool hasDoubleLetters = false;
            foreach (char c in chars)
            {
                if (c == prevChar)
                {
                    hasDoubleLetters = true;
                    break;
                }
                prevChar = c;
            }

            if (!hasDoubleLetters)
            {
                return false;
            }


            //Check illegal strings
            foreach (string illegal in illegalStrings)
            {
                if (input.Contains(illegal))
                {
                    return false;
                }
            }

            return true;
        }
    }
}