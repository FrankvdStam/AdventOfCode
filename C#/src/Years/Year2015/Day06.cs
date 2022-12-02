using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    public class Day06 : BaseDay
    {
        public Day06() : base(2015, 06) { }

        public override void ProblemOne()
        {
            var lines = Input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            int x1, x2, y1, y2;
            bool value;
            foreach (var line in lines)
            {
                if (line.StartsWith("toggle"))
                {
                    var split = line.Split(' ');
                    x1 = int.Parse(split[1].Split(',')[0]);
                    y1 = int.Parse(split[1].Split(',')[1]);

                    x2 = int.Parse(split[3].Split(',')[0]);
                    y2 = int.Parse(split[3].Split(',')[1]);

                    ToggleLights(x1, y1, x2, y2);
                }
                else
                {
                    var split = line.Split(' ');
                    value = true;
                    if (split[1] == "off")
                    {
                        value = false;
                    }

                    x1 = int.Parse(split[2].Split(',')[0]);
                    y1 = int.Parse(split[2].Split(',')[1]);

                    x2 = int.Parse(split[4].Split(',')[0]);
                    y2 = int.Parse(split[4].Split(',')[1]);

                    SetLights(x1, y1, x2, y2, value);
                }
            }

            var result = CountOnLights();
            Console.WriteLine(result);
        }

        public override void ProblemTwo()
        {
            var lines = Input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            int x1, x2, y1, y2;
            bool value;
            foreach (var line in lines)
            {
                if (line.StartsWith("toggle"))
                {
                    var split = line.Split(' ');
                    x1 = int.Parse(split[1].Split(',')[0]);
                    y1 = int.Parse(split[1].Split(',')[1]);

                    x2 = int.Parse(split[3].Split(',')[0]);
                    y2 = int.Parse(split[3].Split(',')[1]);

                    ToggleLightsTwo(x1, y1, x2, y2);
                }
                else
                {
                    var split = line.Split(' ');
                    value = true;
                    if (split[1] == "off")
                    {
                        value = false;
                    }

                    x1 = int.Parse(split[2].Split(',')[0]);
                    y1 = int.Parse(split[2].Split(',')[1]);

                    x2 = int.Parse(split[4].Split(',')[0]);
                    y2 = int.Parse(split[4].Split(',')[1]);

                    SetLightsTwo(x1, y1, x2, y2, value);
                }
            }

            var result = CountLightsBrightness();
            Console.WriteLine(result);
        }



        static void SetLightsTwo(int x1, int y1, int x2, int y2, bool value)
        {
            for (int x = x1; x <= x2; x++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    if (value)
                    {
                        lightsTwo[x, y]++;
                    }
                    else
                    {
                        if (lightsTwo[x, y] > 0)
                        {
                            lightsTwo[x, y]--;
                        }
                    }
                }
            }
        }

        static void ToggleLightsTwo(int x1, int y1, int x2, int y2)
        {
            for (int x = x1; x <= x2; x++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    lightsTwo[x, y] += 2;
                }
            }
        }

        static int CountLightsBrightness()
        {
            int count = 0;
            for (int x = 0; x < lights.GetLength(0); x++)
            {
                for (int y = 0; y < lights.GetLength(1); y++)
                {
                    count += lightsTwo[x, y];
                }
            }
            return count;
        }



        // var lines = input.Split(new string[] {"\r\n"}, StringSplitOptions.None);
        static bool[,] lights = new bool[1000, 1000];
        static int[,] lightsTwo = new int[1000, 1000];




        static void SetLights(int x1, int y1, int x2, int y2, bool value)
        {
            for (int x = x1; x <= x2; x++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    lights[x, y] = value;
                }
            }
        }

        static void ToggleLights(int x1, int y1, int x2, int y2)
        {
            for (int x = x1; x <= x2; x++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    lights[x, y] = !lights[x, y];
                }
            }
        }

        static int CountOnLights()
        {
            int count = 0;
            for (int x = 0; x < lights.GetLength(0); x++)
            {
                for (int y = 0; y < lights.GetLength(1); y++)
                {
                    if (lights[x, y])
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}