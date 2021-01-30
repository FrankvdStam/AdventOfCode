using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2016
{
    public class Day21 : IDay
    {
        public int Day => 21;
        public int Year => 2016;


        private List<Instruction> _instructions;
        public void ProblemOne()
        {
            _instructions = ParseInstructions(Input);

            string scrambled = Scramble("abcdefgh");
            Console.WriteLine(scrambled);
        }

        public void ProblemTwo()
        {
            string scrambled = Unscramble("fbgdceah");
            Console.WriteLine(scrambled);
        }


        private string Scramble(string input)
        {
            List<char> password = new List<char>(input);
            
            foreach (var instruction in _instructions)
            {
                switch (instruction.Operation)
                {
                    case Operation.SwapPosition:
                        SwapPosition(ref password, instruction.FirstIndex, instruction.SecondIndex);
                        break;
                    case Operation.SwapLetter:
                        SwapLetter(ref password, instruction.FirstLetter, instruction.SecondLetter);
                        break;
                    case Operation.RotateRight:
                        RotateRight(ref password, instruction.FirstIndex);
                        break;
                    case Operation.RotateLeft:
                        RotateLeft(ref password, instruction.FirstIndex);
                        break;
                    case Operation.RotatePosition:
                        RotatePosition(ref password, instruction.FirstLetter);
                        break;
                    case Operation.ReversePosition:
                        Reverse(ref password, instruction.FirstIndex, instruction.SecondIndex);
                        break;
                    case Operation.MovePosition:
                        Move(ref password, instruction.FirstIndex, instruction.SecondIndex);
                        break;

                    default:
                        throw new Exception("Exception should have been thrown during parsing, encountered nonexistant operation.");
                }
            }

            return string.Join("", password);
        }







        private string Unscramble(string input)
        {
            List<char> password = new List<char>(input);

            //Move through in reverse order
            _instructions.Reverse();
            foreach (var instruction in _instructions)
            {
                switch (instruction.Operation)
                {
                    //Swapping letters or positions stays the same
                    case Operation.SwapPosition:
                        SwapPosition(ref password, instruction.FirstIndex, instruction.SecondIndex);
                        break;
                    case Operation.SwapLetter:
                        SwapLetter(ref password, instruction.FirstLetter, instruction.SecondLetter);
                        break;
                    //Right because left and vice versa
                    case Operation.RotateRight:
                        RotateLeft(ref password, instruction.FirstIndex);
                        break;
                    case Operation.RotateLeft:
                        RotateRight(ref password, instruction.FirstIndex);
                        break;
                    //See specific function to see how it reverses
                    case Operation.RotatePosition:
                        ReverseRotatePosition(ref password, instruction.FirstLetter);
                        break;
                    //No changes here
                    case Operation.ReversePosition:
                        Reverse(ref password, instruction.FirstIndex, instruction.SecondIndex);
                        break;
                    case Operation.MovePosition:
                        Move(ref password, instruction.SecondIndex, instruction.FirstIndex);
                        break;

                    default:
                        throw new Exception("Exception should have been thrown during parsing, encountered nonexistant operation.");
                }
            }
            return string.Join("", password);
        }



        private void ReverseRotatePosition(ref List<char> password, char letter)
        {
            var buffer = password.Clone();

            //there are 8 possible letters/indices, try them all until it fits
            for (int i = 0; i < password.Count; i++)
            {
                //Rotate reverse on a char
                RotatePositionLeft(ref buffer, password[i]);
                //copy result for later
                var copy = buffer.Clone();

                //To check if this makes sense, apply the rotation forward, this should get us the same sequence as still stored in password

                RotatePosition(ref buffer, letter);
                if (password.SequenceEqual(buffer))
                {
                    //Found it, set password and exit
                    password = copy;
                    return;
                }
                else
                {
                    //clear buffer, try again with next char (index)
                    buffer = password.Clone();
                }
            }
            throw new Exception($"Failed to find reverse rotation position. {string.Join("", password)}");
        }

        private void RotatePositionLeft(ref List<char> password, char letter)
        {
            int index = password.IndexOf(letter);
            if (index >= 4)
            {
                index++;
            }
            index++;

            RotateLeft(ref password, index);
        }







        //swap position X with position Y means that the letters at indexes X and Y(counting from 0) should be swapped.
        //swap letter X with letter Y means that the letters X and Y should be swapped(regardless of where they appear in the string).
        //rotate left/right X steps means that the whole string should be rotated; for example, one right rotation would turn abcd into dabc.
        //rotate based on position of letter X means that the whole string should be rotated to the right based on the index of letter X (counting from 0) as determined before this instruction does any rotations.
        //          Once the index is determined, rotate the string to the right one time, plus a number of times equal to that index, plus one additional time if the index was at least 4.
        //reverse positions X through Y means that the span of letters at indexes X through Y (including the letters at X and Y) should be reversed in order.
        //move position X to position Y means that the letter which is at index X should be removed from the string, then inserted such that it ends up at index Y.


        private void SwapPosition(ref List<char> password, int firstIndex, int secondIndex)
        {
            char buff = password[firstIndex];
            password[firstIndex] = password[secondIndex];
            password[secondIndex] = buff;
        }

        private void SwapLetter(ref List<char> password, char firstLetter, char secondLetter)
        {
            int firstIndex = password.IndexOf(firstLetter);
            int secondIndex = password.IndexOf(secondLetter);
            SwapPosition(ref password, firstIndex, secondIndex);
        }

        private void RotateRight(ref List<char> password, int amount)
        {
            while (amount > 0)
            {
                char last = password.Last();
                password.RemoveAt(password.Count-1);
                password.Insert(0, last);
                amount--;
            }
        }

        private void RotateLeft(ref List<char> password, int amount)
        {
            password.Reverse();
            RotateRight(ref password, amount);
            password.Reverse();
        }


        private void RotatePosition(ref List<char> password, char letter)
        {
            int index = password.IndexOf(letter);
            if (index >= 4)
            {
                index++;
            }
            index++;

            RotateRight(ref password, index);
        }


        private void Reverse(ref List<char> password, int firstIndex, int secondIndex)
        {
            var subList = password.Skip(firstIndex).Take(1 + secondIndex - firstIndex).Reverse();
            List<char> pwd = password.Take(firstIndex).ToList();
            pwd.AddRange(subList);
            pwd.AddRange(password.Skip(secondIndex+1));
            password = pwd;
        }


        private void Move(ref List<char> password, int firstIndex, int secondIndex)
        {
            var buff = password[firstIndex];
            password.RemoveAt(firstIndex);
            password.Insert(secondIndex, buff);
        }









        enum Operation
        {
            SwapPosition,
            SwapLetter,
            RotateRight,
            RotateLeft,
            RotatePosition,
            ReversePosition,
            MovePosition,
        }

        struct Instruction
        {
            public Operation Operation;
            public char FirstLetter;
            public char SecondLetter;
            public int FirstIndex;
            public int SecondIndex;
        }


        private List<Instruction> ParseInstructions(string input)
        {
            var instructions = new List<Instruction>();

            var split = input.SplitNewLine();
            foreach (var s in split)
            {
                var bits = s.Split(' ');
                Instruction i = new Instruction();
                
                if (s.StartsWith("swap position"))
                {
                    //swap position 4 with position 2
                    i.Operation = Operation.SwapPosition;
                    i.FirstIndex = int.Parse(bits[2]);
                    i.SecondIndex = int.Parse(bits[5]);
                }
                else if (s.StartsWith("swap letter"))
                {
                    //swap letter b with letter a
                    i.Operation = Operation.SwapLetter;
                    i.FirstLetter = bits[2].First();
                    i.SecondLetter = bits[5].First();
                }
                else if (s.StartsWith("rotate left"))
                {
                    //rotate left 3 steps
                    i.Operation = Operation.RotateLeft;
                    i.FirstIndex = int.Parse(bits[2]);
                }
                else if (s.StartsWith("rotate right"))
                {
                    //rotate right 3 steps
                    i.Operation = Operation.RotateRight;
                    i.FirstIndex = int.Parse(bits[2]);
                }
                else if (s.StartsWith("rotate based"))
                {
                    //rotate based on position of letter f
                    i.Operation = Operation.RotatePosition;
                    i.FirstLetter = bits[6].First();
                }
                else if (s.StartsWith("reverse positions"))
                {
                    //reverse positions 5 through 6
                    i.Operation = Operation.ReversePosition;
                    i.FirstIndex =  int.Parse(bits[2]);
                    i.SecondIndex = int.Parse(bits[4]);
                }
                else if (s.StartsWith("move position"))
                {
                    //move position 4 to position 0
                    i.Operation = Operation.MovePosition;
                    i.FirstIndex = int.Parse(bits[2]);
                    i.SecondIndex = int.Parse(bits[5]);
                }
                else
                {
                    throw new Exception($"Failed to parse instruction: {s}");
                }

                instructions.Add(i);
            }
            return instructions;
        }




        private const string Input = @"rotate right 3 steps
swap letter b with letter a
move position 3 to position 4
swap position 0 with position 7
swap letter f with letter h
rotate based on position of letter f
rotate based on position of letter b
swap position 3 with position 0
swap position 6 with position 1
move position 4 to position 0
rotate based on position of letter d
swap letter d with letter h
reverse positions 5 through 6
rotate based on position of letter h
reverse positions 4 through 5
move position 3 to position 6
rotate based on position of letter e
rotate based on position of letter c
rotate right 2 steps
reverse positions 5 through 6
rotate right 3 steps
rotate based on position of letter b
rotate right 5 steps
swap position 5 with position 6
move position 6 to position 4
rotate left 0 steps
swap position 3 with position 5
move position 4 to position 7
reverse positions 0 through 7
rotate left 4 steps
rotate based on position of letter d
rotate left 3 steps
swap position 0 with position 7
rotate based on position of letter e
swap letter e with letter a
rotate based on position of letter c
swap position 3 with position 2
rotate based on position of letter d
reverse positions 2 through 4
rotate based on position of letter g
move position 3 to position 0
move position 3 to position 5
swap letter b with letter d
reverse positions 1 through 5
reverse positions 0 through 1
rotate based on position of letter a
reverse positions 2 through 5
swap position 1 with position 6
swap letter f with letter e
swap position 5 with position 1
rotate based on position of letter a
move position 1 to position 6
swap letter e with letter d
reverse positions 4 through 7
swap position 7 with position 5
swap letter c with letter g
swap letter e with letter g
rotate left 4 steps
swap letter c with letter a
rotate left 0 steps
swap position 0 with position 1
reverse positions 1 through 4
rotate based on position of letter d
swap position 4 with position 2
rotate right 0 steps
swap position 1 with position 0
swap letter c with letter a
swap position 7 with position 3
swap letter a with letter f
reverse positions 3 through 7
rotate right 1 step
swap letter h with letter c
move position 1 to position 3
swap position 4 with position 2
rotate based on position of letter b
reverse positions 5 through 6
move position 5 to position 3
swap letter b with letter g
rotate right 6 steps
reverse positions 6 through 7
swap position 2 with position 5
rotate based on position of letter e
swap position 1 with position 7
swap position 1 with position 5
reverse positions 2 through 7
reverse positions 5 through 7
rotate left 3 steps
rotate based on position of letter b
rotate left 3 steps
swap letter e with letter c
rotate based on position of letter a
swap letter f with letter a
swap position 0 with position 6
swap position 4 with position 7
reverse positions 0 through 5
reverse positions 3 through 5
swap letter d with letter e
move position 0 to position 7
move position 1 to position 3
reverse positions 4 through 7";
    }
}