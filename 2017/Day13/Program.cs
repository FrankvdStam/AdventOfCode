using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = ParseInput(Input);

            Console.WriteLine($"Score: {CalculateScore(input, 0, out _)}");

            int delay = 0;
            while (true)
            {
                CalculateScore(input, delay, out bool detected);
                if (!detected)
                {
                    break;
                }

                if (delay % 1000 == 0)
                {
                    Console.Out.WriteLine(delay);
                }

                delay++;
            }
            Console.WriteLine($"Undected after waiting {delay} picoseconds");
            Console.ReadKey();
        }

        static int CalculateScore(int[] depts, int delay, out bool detected)
        {
            detected = false;
            int score = 0;
            for (int i = 0; i < depts.Length; i++)
            {
                int position = i;
                int time = i + delay;
                
                //See if we get detected at this position if there is a scanner active
                if (depts[position] > 0 && CalculatePosition(time, depts[position]) == 0)
                {
                    score += position * depts[position];
                    detected = true;
                }
                //Bonus 
                //Print(time, depts, position);
            }
            return score;
        }
        
        static int CalculatePosition(int time, int dept)
        {
            //The camera position is a sin-like function. We can calculate the position based on the dept and time.
            //We need to know the full cycle width/size corresponding to a given dept.
            //We go x-steps up and x-steps down except we ignore the peak because that is counted twice
            //and the lowest point because it is the start of the next cycle.
            //That means that the cycle is twice the dept - peak - start:
            int cycleSize = (dept * 2) - 2; //Potential performance increase is to memoize these, we can calculate them beforehand.
            
            //Maybe 400 cycles have passed; we don't care about this. We only care about the position in the current cycle.
            int cycle = time % cycleSize;

            //Now we need to find the find the position by either getting a portion of the height or by subtracting from the height (if the cycle step exceeds the height)
            int position;
            if (cycle < dept)
            {
                position = cycle;
            }
            else
            {
                int mod = cycle % dept;
                position = dept - mod - 2;
            }

            return position;
        }

        static void Print(int time, int[] state, int? position = null)
        {
            Console.WriteLine($"Picosecond {time}");
            if (position != null)
            {
                Console.WriteLine($"Position {position}");
            }

            for (int i = 0; i < state.GetLength(0); i++)
            {
                int x = Console.CursorLeft;
                int y = Console.CursorTop;

                Console.Write($" {i}");
                Console.CursorLeft = x;
                Console.CursorTop++;

                //If there is dept/height at this index
                if (state[i] > 0)
                {
                    int scannerPosition = CalculatePosition(time, state[i]);

                    for (int j = 0; j < state[i]; j++)
                    {
                        if (j == scannerPosition)
                        {
                            Console.Write($"[S]");
                        }
                        else
                        {
                            Console.Write($"[ ]");
                        }
                        Console.CursorLeft = x;
                        Console.CursorTop++;
                    }
                }

                if (i + 1 < state.GetLength(0))
                {
                    Console.CursorLeft = x + 4;
                    Console.CursorTop = y;
                }
                else
                {
                    Console.Write("\n\n");
                }
            }
        }
        
        static int[] ParseInput(string input)
        {

            var lines = input.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            //Get the size our array should be by parsing the last line
            int size = int.Parse(lines[lines.Length - 1].Split(' ')[0].Replace(":", string.Empty)) + 1;
            int[] things = new int[size];

            //1st pass: create instances in lookup table
            foreach (var line in lines)
            {
                var bits = line.Split(' ');
                int index = int.Parse(bits[0].Replace(":", string.Empty));
                int dept = int.Parse(bits[1]);

                things[index] = dept;
            }

            return things;
        }


        private static string Example = @"0: 3
1: 2
4: 4
6: 4";

        private static string Input = @"0: 4
1: 2
2: 3
4: 4
6: 8
8: 5
10: 8
12: 6
14: 6
16: 8
18: 6
20: 6
22: 12
24: 12
26: 10
28: 8
30: 12
32: 8
34: 12
36: 9
38: 12
40: 8
42: 12
44: 17
46: 14
48: 12
50: 10
52: 20
54: 12
56: 14
58: 14
60: 14
62: 12
64: 14
66: 14
68: 14
70: 14
72: 12
74: 14
76: 14
80: 14
84: 18
88: 14";
    }
}
