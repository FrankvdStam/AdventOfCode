using System;
using System.Collections.Generic;
using System.Linq;
using Years.Utils;

namespace Years.Year2019
{
    public class Day01 : IDay
    {
        public int Day => 1;
        public int Year => 2019;

        public Day01()
        {

        }


        public void ProblemOne()
        {
            List<int> result = new List<int>();
            var lines = Input.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                decimal mass = decimal.Parse(line);
                int floor = (int) Math.Floor( (mass / 3) );
                int fuelRequired = floor - 2;
                result.Add(fuelRequired);
            }

            var total = result.Sum();
            Console.WriteLine("Total: " + total);
        }

        public void ProblemTwo()
        {
            List<int> result = new List<int>();
            var lines = Input.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                decimal mass = decimal.Parse(line);
                
                Stack<decimal> masses = new Stack<decimal>();
                masses.Push(mass);

                while (masses.Any())
                {
                    mass = masses.Pop();
                    int fuelRequired = CalculateFuelFromMass(mass);
                    if (fuelRequired != 0)
                    {
                        result.Add(fuelRequired);
                        masses.Push(fuelRequired);
                    }
                }


                //3263320
            }

            var total = result.Sum();
            Console.WriteLine("Total: " + total);
            Console.ReadKey();
        }

        private int CalculateFuelFromMass(decimal mass)
        {
            int floor = (int)Math.Floor((mass / 3));
            int fuelRequired = floor - 2;
            if (fuelRequired < 0)
            {
                fuelRequired = 0;
            }
            return fuelRequired;
        }


        private static string Test = @"12
14
1969
100756";

        private static string Input = @"140170
75120
75645
134664
124948
137630
146662
116881
120030
94332
50473
59361
128237
84894
51368
128802
57275
129235
113481
66378
55842
90548
107696
53603
130458
80306
120820
131313
100303
59224
123369
140584
60642
68184
103101
82278
51968
51048
98139
60498
127082
71197
109478
71286
84840
141305
51800
72352
93147
73549
122739
62363
58453
59000
63564
63424
51053
120826
123337
130824
59053
77983
68977
67126
96051
53024
145647
139343
113236
59396
146174
148622
83384
86938
100673
80757
107675
147417
124538
136463
104609
149559
136037
54997
139674
101638
65739
70029
143847
122035
66256
78087
105045
108867
99630
127173
139021
139759
134171
104869";
        private static string Example = @"";
    }
}
