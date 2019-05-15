using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21
{
    class Program
    {
        private static string Example  = @"abcde";

        private static string ExampleInstructions = @"swap position 4 with position 0
swap letter d with letter b
reverse positions 0 through 4
rotate left 1 step
move position 1 to position 4
move position 3 to position 0
rotate based on position of letter b
rotate based on position of letter d";


        static void Main(string[] args)
        {
            //ProblemOne(Example, ExampleInstructions);
            ProblemOne(Input, InputInstruction);
            //ProblemTwo();
        }

        static void ProblemOne(string input, string instructions)
        {
            char[] et = new[] {'a', 'b', 'c', 'd', 'e'};
            Rotate(ref et, true, 1);


            char[] chars = Encoding.ASCII.GetChars(Encoding.ASCII.GetBytes(input));

            var lines = instructions.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            lines = lines.Reverse().ToArray();
            foreach (var line in lines)
            {
                var bits = line.Split(' ');
                switch (bits[0])
                {
                    case "swap":
                        Swap(ref chars, line);
                        break;
                    case "reverse":
                        Reverse(ref chars, int.Parse(bits[2]), int.Parse(bits[4]));
                        break;
                    case "rotate":
                        Rotate(ref chars, line);
                        break;
                    case "move":
                        Move(ref chars, int.Parse(bits[2]), int.Parse(bits[5]));
                        break;
                }

                if (chars.Length != 8)
                {

                }

                string temp = Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(chars));
            }
            string result = Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(chars));
        }
        
        static void ProblemTwo(string input)
        {

        }
        
        #region Swap ========================================================================================================

        static void Swap(ref char[] chars, string input)
        {
            var bits = input.Split(' ');
            if (bits[1] == "position")
            {
                Swap(ref chars, int.Parse(bits[2]), int.Parse(bits[5]));
            }
            else
            {
                char from = bits[2][0];
                char with = bits[5][0];
                Swap(ref chars, Array.IndexOf(chars, from), Array.IndexOf(chars, with));
            }
        }

        static void Swap(ref char[] chars, int from, int to)
        {
            char buffer = chars[to];
            chars[to] = chars[from];
            chars[from] = buffer;
        }
        #endregion

        #region Rotate ========================================================================================================

        static void Rotate(ref char[] chars, string input)
        {
            var bits = input.Split(' ');
            if (bits[1] == "based")
            {
                char letter = bits[6][0];
                int index = Array.IndexOf(chars, letter);

                //Weird:
                if (index >= 4)
                {
                    index++;
                }
                index++;
                //End weird.
                Rotate(ref chars, false, index);
            }
            else
            {
                Rotate(ref chars, bits[1] == "left", int.Parse(bits[2]));
            }
        }

        static void Rotate(ref char[] chars, bool left, int steps)
        {
            char[] temp = (char[])chars.Clone();

            //Calulate the shift: a left shift is a reversed right shift
            int shift;
            if (left)
            {
                shift = chars.Length-steps;
            }
            else
            {
                shift = steps;
            }

            //Shift always right, because the shift has already been adjusted accordingly:
            for (int i = 0; i < temp.Length; i++)
            {
                int index = (i + shift) % chars.Length;
                int test = 5 % 5;
                chars[(i + shift) % chars.Length] = temp[i];
            }
        }
        #endregion


        static void Reverse(ref char[] chars, int start, int end)
        {
            List<char> newChars = new List<char>();
            if (start - 1 >= 0)
            {
                newChars.AddRange(chars.Take(start));
            }

            //var range = chars.Skip(start).Take((end + 1) - start).Reverse().ToList();
            newChars.AddRange(chars.Skip(start).Take( (end+1) - start).Reverse());

            newChars.AddRange(chars.Skip(end+1).Take(chars.Length - (end-1)));

            chars = newChars.ToArray();
            //var temp = (char[]) chars.Clone();
            //
            //for (int i = start; i <= end; i++)
            //{
            //    chars[(end - start) - i] = temp[i];
            //}
        }

        static void Move(ref char[] chars, int from, int to)
        {
            //Easier to just use a list - no need to manually shift around in the array.
            List<char> charList = chars.ToList();
            char fromChar = chars[from];
            char toChar = chars[to];
            charList.RemoveAt(from);
            charList.Insert(to, fromChar);
            chars = charList.ToArray();
        }

        private static string Input = @"fbgdceah";

        private static string InputInstruction = @"rotate right 3 steps
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
