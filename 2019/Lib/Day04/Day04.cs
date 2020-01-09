using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Day04
{
    public class Day04 : IDay
    {
        public int Day => 4;

        public Day04()
        {
            //Test();
        }

        private const int StartRange = 272091;
        private const int EndRange = 815432;

        public void ProblemOne()
        {  
            List<int> passwords = new List<int>();
            for (int i = StartRange; i <= EndRange; i++)
            {
                if (IsValidPassword(i.ToString()))
                {
                    passwords.Add(i);
                }
            }

            var result = passwords.Count();
        }

        public void ProblemTwo()
        {
            List<int> passwords = new List<int>();
            for (int i = StartRange; i <= EndRange; i++)
            {
                if (AdvancedIsValidPassword(i.ToString()))
                {
                    passwords.Add(i);
                }
            }

            var result = passwords.Count();
        }



        private void Test()
        {
            if (!IsValidPassword("111111"))
            {
                throw new Exception();
            }
            if (IsValidPassword("223450"))
            {
                throw new Exception();
            }
            if (IsValidPassword("123789"))
            {
                throw new Exception();
            }



            if (!AdvancedIsValidPassword("112233"))
            {
                throw new Exception();
            }
            if (AdvancedIsValidPassword("123444"))
            {
                throw new Exception();
            }
            if (!AdvancedIsValidPassword("111122"))
            {
                throw new Exception();
            }
        }


        #region Password validaion ========================================================================================================
        /// <summary>
        /// Validates a given password
        /// </summary>
        private bool AdvancedIsValidPassword(string password)
        {
            return IsCorrectLengthAndNumeric(password) && AdvancedHasSameAdjacentDigits(password) && NeverDecreases(password);
        }

        /// <summary>
        /// Validates a given password
        /// </summary>
        private bool IsValidPassword(string password)
        {
            return IsCorrectLengthAndNumeric(password) && HasSameAdjacentDigits(password) && NeverDecreases(password);
        }
        

        /// <summary>
        /// Checks if password length is 6 and if it is all numeric by trying to parse it as int
        /// </summary>
        private bool IsCorrectLengthAndNumeric(string password)
        {
            if (password.Length != 6)
            {
                return false;
            }

            if(int.TryParse(password, out _))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if the numbers are never decreasing in sequence
        /// </summary>
        private bool NeverDecreases(string password)
        {
            int currentNumber = 0;
            for (int i = 0; i < password.Length; i++)
            {
                int number = int.Parse(password[i].ToString());
                if (number >= currentNumber)
                {
                    currentNumber = number;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

       
        /// <summary>
        /// Checks if the password has the same char twice
        /// </summary>
        private bool HasSameAdjacentDigits(string password)
        {
            for (int i = 0; i < password.Length-1; i++)
            {
                if (password[i] == password[i + 1])
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the password has the same char twice
        /// </summary>
        private bool AdvancedHasSameAdjacentDigits(string password)
        {
            //We can count the chars in the string - if they appear more than twice we know enough.
            //We don't have to worry about cases like 111211 - in theory valid but it gets disqualified for decreasing values
            
            Dictionary<char, int> charsOccurrences = new Dictionary<char, int>();
            foreach (char c in password)
            {
                if (!charsOccurrences.TryGetValue(c, out int count))
                {
                    count = 0;
                }
                count++;
                charsOccurrences[c] = count;
            }

            var pairs = charsOccurrences.Where(i => i.Value == 2).ToList();
            foreach (var pair in pairs)
            {
                for (int i = 0; i < password.Length - 1; i++)
                {
                    if (password[i] == pair.Key && password[i] == password[i + 1])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion
    }
}
