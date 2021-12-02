using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2017
{
    public class Day06 : IDay
    {
        public int Day => 6;
        public int Year => 2017;

        public void ProblemOne()
        {
            int count = 0;
            List<string> configurations = new List<string>();

            List<int> memory = new List<int>()
            {
                //0, 2, 7, 0
                4, 1, 15, 12, 0, 9, 9, 5, 5, 8, 7, 3, 14, 5, 12, 3
            };
            string config = ListToString(memory);
            //Console.Out.WriteLine(config);
            configurations.Add(config);

            while (!configurations.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).Any())
            {
                Redist(memory);
                config = ListToString(memory);
                //Console.Out.WriteLine(config);
                configurations.Add(config);
                count++;
            }

            int first = configurations.IndexOf(config);
            _cashedResult = count - first;

            Console.WriteLine(count);
        }

        private int _cashedResult;

        public void ProblemTwo()
        {
            Console.WriteLine(_cashedResult);
        }
        

        //Redist a given index
        static void Redist(List<int> memory)
        {
            int max = memory.Max();
            int index = memory.IndexOf(max);

            int value = memory[index];
            decimal div = (decimal)value / (decimal)memory.Count;
            int largestRedist = (int)Math.Ceiling(div);

            memory[index] = 0;
            //Increment/looparound index
            index = index + 1 == memory.Count ? 0 : index + 1;

            //Redist
            while (value > 0)
            {
                int increment = value - largestRedist > 0 ? largestRedist : value;
                value -= increment;
                memory[index] += increment;

                //Loop index around if we surpass the list
                index = index + 1 == memory.Count ? 0 : index + 1;
            }
        }

        private string ListToString(List<int> ints)
        {
            StringBuilder s = new StringBuilder();
            foreach (var i in ints)
            {
                s.Append(i);
            }
            return s.ToString();
        }


        private void ParseInput(string input)
        {
            var lines = input.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                var bits = line.Split(' ');
            }
        }
    }
}