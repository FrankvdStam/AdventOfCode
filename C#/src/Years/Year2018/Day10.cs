using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2018
{
    public class Day10 : IDay
    {
        private class Light
        {
            public Vector2i Position;
            public Vector2i Velocity;

            public void Move()
            {
                Position = Position.Add(Velocity);
            }
        }


        public int Day => 10;
        public int Year => 2018;

        public void ProblemOne()
        {
            throw new Exception();

            ////var lights = ParseInput(Input);
            ////
            ////int seconds = 0;
            ////while (true)
            ////{
            ////    //Draw(Normalize(lights));
            ////    //int biggestDistance = FindBiggestDistance(lights);
            ////    var test = MoveToOrigin(lights);
            ////
            ////    Draw(test);
            ////    Console.ReadKey();
            ////
            ////    if (test.All(i => i.Position.X > 0 && i.Position.Y > 0))
            ////    {
            ////        Draw(test);
            ////    }
            ////    
            ////    //Distance is smallest at 10077
            ////    //if (biggestDistance < 1000)
            ////    //{
            ////
            ////    //}
            ////
            ////    //Console.WriteLine(biggestDistance);
            ////    ProgresSeconds(1, lights);
            ////    seconds += 1;
            ////    //Console.ReadKey();
            ////}
        }

        public void ProblemTwo()
        {
            throw new Exception();
        }


        private int FindBiggestDistance(List<Light> lights)
        {

            int biggest = 0;
            foreach (Light l1 in lights)
            {
                foreach (Light l2 in lights)
                {
                    if (l1 == l2)
                    {
                        continue;
                    }
                    int dist = l1.Position.ManhattanDistance(l2.Position);
                    if (dist > biggest)
                    {
                        biggest = dist;
                    }
                }
            }
            return biggest;
        }
        
        private void ProgresSeconds(int seconds, List<Light> lights)
        {
            for (int i = 0; i < seconds; i++)
            {
                lights.ForEach(j => j.Move());
            }
        }

        private List<Light> MoveToOrigin(List<Light> lights)
        {
            var result = new List<Light>();
            Vector2i correction = new Vector2i()
            {
                X = -lights.Min(i => i.Position.X),
                Y = -lights.Min(i => i.Position.Y)
            };

            foreach (Light l in lights)
            {
                result.Add(
                    new Light()
                    {
                        Position = l.Position.Add(correction),
                        Velocity = l.Velocity,
                    }
                );
            }

            return result;
        }

        //private List<Light> Normalize(List<Light> lights)
        //{
        //    var result = new List<Light>();
        //    Vector2i correction = new Vector2i()
        //    {
        //        X = Math.Abs(lights.Min(i => i.Position.X)),
        //        Y = Math.Abs(lights.Min(i => i.Position.Y))
        //    };
        //
        //    foreach (Light l in lights)
        //    {
        //        result.Add(
        //            new Light()
        //            {
        //                Position = l.Position.Add(correction),
        //                Velocity = l.Velocity,
        //            }
        //        );
        //    }
        //
        //    return result;
        //}

        private void Draw(List<Light> lights)
        {
            try
            {
                Console.Clear();
                foreach (Light l in lights)
                {
                    Console.CursorLeft = l.Position.X;
                    Console.CursorTop = l.Position.Y;
                    Console.Write('#');
                }
            }catch{}
            
        }
        
        private List<Light> ParseInput(string input)
        {
            var lights = new List<Light>();
            foreach (var line in input.SplitNewLine())
            {
                var commaSplit = line.Split(',');
                var velocitySplit = line.Split("velocity=<")[1].Split(',');

                var substr = commaSplit[1].Substring(0, commaSplit[1].IndexOf('>'));

                var position = new Vector2i(
                        int.Parse(commaSplit[0].Substring("position=<".Length)),
                        int.Parse(commaSplit[1].Substring(0, commaSplit[1].IndexOf('>')))
                    );

                var velocity = new Vector2i(
                    int.Parse(velocitySplit[0]),
                    int.Parse(commaSplit[2].Substring(0, commaSplit[2].IndexOf('>')))
                );

                lights.Add(new Light(){Position = position, Velocity = velocity});
            }

            return lights;
        }

        private const string Example = @"position=< 9,  1> velocity=< 0,  2>
position=< 7,  0> velocity=<-1,  0>
position=< 3, -2> velocity=<-1,  1>
position=< 6, 10> velocity=<-2, -1>
position=< 2, -4> velocity=< 2,  2>
position=<-6, 10> velocity=< 2, -2>
position=< 1,  8> velocity=< 1, -1>
position=< 1,  7> velocity=< 1,  0>
position=<-3, 11> velocity=< 1, -2>
position=< 7,  6> velocity=<-1, -1>
position=<-2,  3> velocity=< 1,  0>
position=<-4,  3> velocity=< 2,  0>
position=<10, -3> velocity=<-1,  1>
position=< 5, 11> velocity=< 1, -2>
position=< 4,  7> velocity=< 0, -1>
position=< 8, -2> velocity=< 0,  1>
position=<15,  0> velocity=<-2,  0>
position=< 1,  6> velocity=< 1,  0>
position=< 8,  9> velocity=< 0, -1>
position=< 3,  3> velocity=<-1,  1>
position=< 0,  5> velocity=< 0, -1>
position=<-2,  2> velocity=< 2,  0>
position=< 5, -2> velocity=< 1,  2>
position=< 1,  4> velocity=< 2,  1>
position=<-2,  7> velocity=< 2, -2>
position=< 3,  6> velocity=<-1, -1>
position=< 5,  0> velocity=< 1,  0>
position=<-6,  0> velocity=< 2,  0>
position=< 5,  9> velocity=< 1, -2>
position=<14,  7> velocity=<-2,  0>
position=<-3,  6> velocity=< 2, -1>";

        private const string Input = @"position=< 20316, -30055> velocity=<-2,  3>
position=<-30043, -30055> velocity=< 3,  3>
position=<-19955,  40468> velocity=< 2, -4>
position=<-19981, -30055> velocity=< 2,  3>
position=< 30386,  30399> velocity=<-3, -3>
position=< 30417,  20320> velocity=<-3, -2>
position=<-40135, -50209> velocity=< 4,  5>
position=< 50586,  -9911> velocity=<-5,  1>
position=<-50222,  30399> velocity=< 5, -3>
position=<-40108,  30396> velocity=< 4, -3>
position=< -9931, -19980> velocity=< 1,  2>
position=< 10237, -40139> velocity=<-1,  4>
position=<-40106,  10242> velocity=< 4, -1>
position=<-30080, -30059> velocity=< 3,  3>
position=< -9913,  10249> velocity=< 1, -1>
position=< -9895, -30063> velocity=< 1,  3>
position=< 30401,  40468> velocity=<-3, -4>
position=< 50586,  -9911> velocity=<-5,  1>
position=< 30432,  -9912> velocity=<-3,  1>
position=<-50223,  50553> velocity=< 5, -5>
position=< 30406, -19983> velocity=<-3,  2>
position=< 10245, -30064> velocity=<-1,  3>
position=<-19956,  50544> velocity=< 2, -5>
position=<-40130,  -9903> velocity=< 4,  1>
position=<-50179,  20319> velocity=< 5, -2>
position=< 50529,  40475> velocity=<-5, -4>
position=< 10224,  20316> velocity=<-1, -2>
position=< 10269,  40470> velocity=<-1, -4>
position=<-50224,  10249> velocity=< 5, -1>
position=<-40111, -19985> velocity=< 4,  2>
position=< -9894,  40472> velocity=< 1, -4>
position=< 30399,  10240> velocity=<-3, -1>
position=<-40122, -19983> velocity=< 4,  2>
position=< 40489,  10241> velocity=<-4, -1>
position=< -9889,  -9903> velocity=< 1,  1>
position=< 10282, -30061> velocity=<-1,  3>
position=< 20331,  -9907> velocity=<-2,  1>
position=<-19967, -50208> velocity=< 2,  5>
position=<-30046, -40131> velocity=< 3,  4>
position=<-50179, -40139> velocity=< 5,  4>
position=< 20301,  20322> velocity=<-2, -2>
position=<-50230,  -9910> velocity=< 5,  1>
position=<-40133,  40473> velocity=< 4, -4>
position=<-19979, -19985> velocity=< 2,  2>
position=< 40476, -19984> velocity=<-4,  2>
position=< 20341,  50544> velocity=<-2, -5>
position=< -9894,  40471> velocity=< 1, -4>
position=< 20329,  30398> velocity=<-2, -3>
position=<-30075,  50548> velocity=< 3, -5>
position=< 50550,  30392> velocity=<-5, -3>
position=<-50230, -30061> velocity=< 5,  3>
position=< 40498, -40140> velocity=<-4,  4>
position=<-19949,  50548> velocity=< 2, -5>
position=<-30042, -19988> velocity=< 3,  2>
position=<-40140,  20325> velocity=< 4, -2>
position=< 30401,  50553> velocity=<-3, -5>
position=<-30040,  -9903> velocity=< 3,  1>
position=< 30393, -19979> velocity=<-3,  2>
position=< 50546, -30063> velocity=<-5,  3>
position=< 20331, -50216> velocity=<-2,  5>
position=<-30081,  10244> velocity=< 3, -1>
position=<-40132,  -9908> velocity=< 4,  1>
position=< 10233, -40140> velocity=<-1,  4>
position=<-40127, -30061> velocity=< 4,  3>
position=< 30405,  50549> velocity=<-3, -5>
position=< 30389,  -9909> velocity=<-3,  1>
position=< -9882, -19984> velocity=< 1,  2>
position=<-20007,  10245> velocity=< 2, -1>
position=< 20305,  30401> velocity=<-2, -3>
position=< 50529, -19988> velocity=<-5,  2>
position=<-19951, -19985> velocity=< 2,  2>
position=< 40489,  20321> velocity=<-4, -2>
position=< 30415, -19984> velocity=<-3,  2>
position=<-50223,  50544> velocity=< 5, -5>
position=< 20326, -19988> velocity=<-2,  2>
position=< 30413,  20318> velocity=<-3, -2>
position=< 10246,  30398> velocity=<-1, -3>
position=< 40470,  20317> velocity=<-4, -2>
position=< 50526,  50548> velocity=<-5, -5>
position=< 50560,  30392> velocity=<-5, -3>
position=<-40102, -40140> velocity=< 4,  4>
position=<-19964,  50553> velocity=< 2, -5>
position=<-30042,  10240> velocity=< 3, -1>
position=<-40115,  30396> velocity=< 4, -3>
position=< -9920, -19988> velocity=< 1,  2>
position=< -9874,  10244> velocity=< 1, -1>
position=< 10247,  30397> velocity=<-1, -3>
position=<-30075,  -9909> velocity=< 3,  1>
position=<-19954,  -9910> velocity=< 2,  1>
position=< 50577,  30392> velocity=<-5, -3>
position=< 20347,  20316> velocity=<-2, -2>
position=< -9875, -40136> velocity=< 1,  4>
position=< 50573,  50547> velocity=<-5, -5>
position=<-30074, -19984> velocity=< 3,  2>
position=< -9902,  50553> velocity=< 1, -5>
position=<-40151,  40469> velocity=< 4, -4>
position=< 50581, -30064> velocity=<-5,  3>
position=< 30378, -50214> velocity=<-3,  5>
position=<-30083,  20318> velocity=< 3, -2>
position=<-30059,  20323> velocity=< 3, -2>
position=<-30072, -40136> velocity=< 3,  4>
position=< 20341,  -9908> velocity=<-2,  1>
position=<-30035, -30059> velocity=< 3,  3>
position=< -9889, -40140> velocity=< 1,  4>
position=< -9930,  20320> velocity=< 1, -2>
position=< 50557,  -9907> velocity=<-5,  1>
position=< 30373,  40469> velocity=<-3, -4>
position=< -9875, -50208> velocity=< 1,  5>
position=< -9923,  20320> velocity=< 1, -2>
position=<-30080,  30392> velocity=< 3, -3>
position=< 30414,  40477> velocity=<-3, -4>
position=<-40149, -50207> velocity=< 4,  5>
position=<-40118, -30055> velocity=< 4,  3>
position=< -9918,  40474> velocity=< 1, -4>
position=< 40454, -40131> velocity=<-4,  4>
position=<-20007,  50549> velocity=< 2, -5>
position=<-50179, -30059> velocity=< 5,  3>
position=<-19980,  20325> velocity=< 2, -2>
position=<-50227,  -9911> velocity=< 5,  1>
position=< 20297, -50216> velocity=<-2,  5>
position=<-30081,  -9908> velocity=< 3,  1>
position=< 40497, -30062> velocity=<-4,  3>
position=<-20007,  30393> velocity=< 2, -3>
position=< -9918,  10246> velocity=< 1, -1>
position=< 20305,  40468> velocity=<-2, -4>
position=<-30055,  40468> velocity=< 3, -4>
position=<-40130,  40468> velocity=< 4, -4>
position=< 30434,  -9909> velocity=<-3,  1>
position=<-30033, -40136> velocity=< 3,  4>
position=< 20353, -50216> velocity=<-2,  5>
position=< 20302, -30063> velocity=<-2,  3>
position=< 10238, -40131> velocity=<-1,  4>
position=< 50562, -30058> velocity=<-5,  3>
position=< 30400,  40468> velocity=<-3, -4>
position=<-40146,  20319> velocity=< 4, -2>
position=< 20332,  -9907> velocity=<-2,  1>
position=< 10280, -30060> velocity=<-1,  3>
position=< 10231,  -9912> velocity=<-1,  1>
position=< 50541, -50213> velocity=<-5,  5>
position=< 40470,  40476> velocity=<-4, -4>
position=<-50190, -40131> velocity=< 5,  4>
position=< 30413, -19986> velocity=<-3,  2>
position=< 30433, -19988> velocity=<-3,  2>
position=< 40452, -50212> velocity=<-4,  5>
position=< 50557, -30058> velocity=<-5,  3>
position=< 10269,  50550> velocity=<-1, -5>
position=< 40494,  -9903> velocity=<-4,  1>
position=< 50545,  20325> velocity=<-5, -2>
position=< 40502,  40469> velocity=<-4, -4>
position=< 30373,  10243> velocity=<-3, -1>
position=<-30043, -40134> velocity=< 3,  4>
position=<-40149, -30060> velocity=< 4,  3>
position=<-40103,  10246> velocity=< 4, -1>
position=<-40122,  20321> velocity=< 4, -2>
position=< 10223, -19988> velocity=<-1,  2>
position=< 10261,  10249> velocity=<-1, -1>
position=< 10242,  20317> velocity=<-1, -2>
position=<-30074,  50548> velocity=< 3, -5>
position=< 10253,  -9903> velocity=<-1,  1>
position=< 40462,  -9907> velocity=<-4,  1>
position=<-50210, -40134> velocity=< 5,  4>
position=< -9913,  10240> velocity=< 1, -1>
position=<-30048,  30397> velocity=< 3, -3>
position=< -9872,  10244> velocity=< 1, -1>
position=<-40142,  10249> velocity=< 4, -1>
position=< 50562, -50209> velocity=<-5,  5>
position=<-40140, -40131> velocity=< 4,  4>
position=< 40492,  30392> velocity=<-4, -3>
position=< 50581,  10247> velocity=<-5, -1>
position=<-19983,  40476> velocity=< 2, -4>
position=<-50235,  40477> velocity=< 5, -4>
position=< 20321, -30064> velocity=<-2,  3>
position=< 30415,  40468> velocity=<-3, -4>
position=<-50193, -19984> velocity=< 5,  2>
position=< 30389, -19987> velocity=<-3,  2>
position=<-50227,  30393> velocity=< 5, -3>
position=< 30389,  50551> velocity=<-3, -5>
position=< 40506, -40136> velocity=<-4,  4>
position=<-40143, -30056> velocity=< 4,  3>
position=<-19947, -30064> velocity=< 2,  3>
position=< 40489,  30397> velocity=<-4, -3>
position=< 50533, -30059> velocity=<-5,  3>
position=< 30426, -40137> velocity=<-3,  4>
position=< 50538,  40473> velocity=<-5, -4>
position=<-30058,  50544> velocity=< 3, -5>
position=< 50557, -30062> velocity=<-5,  3>
position=< 50577,  30396> velocity=<-5, -3>
position=<-19975,  10249> velocity=< 2, -1>
position=< 40473,  30399> velocity=<-4, -3>
position=< 30410, -19986> velocity=<-3,  2>
position=< 10255,  20321> velocity=<-1, -2>
position=< 10229,  20321> velocity=<-1, -2>
position=<-50235,  20323> velocity=< 5, -2>
position=<-40151,  10242> velocity=< 4, -1>
position=< 20317,  30392> velocity=<-2, -3>
position=<-19999, -50209> velocity=< 2,  5>
position=<-50195, -19985> velocity=< 5,  2>
position=< -9922,  10249> velocity=< 1, -1>
position=< 40467, -50207> velocity=<-4,  5>
position=< 30406, -50211> velocity=<-3,  5>
position=< 10238,  30392> velocity=<-1, -3>
position=< 50537, -30060> velocity=<-5,  3>
position=<-30055,  20319> velocity=< 3, -2>
position=< 20302,  40469> velocity=<-2, -4>
position=< 50565,  50546> velocity=<-5, -5>
position=< 40449, -19984> velocity=<-4,  2>
position=< -9875,  20318> velocity=< 1, -2>
position=< -9931,  10245> velocity=< 1, -1>
position=< 50573, -19982> velocity=<-5,  2>
position=<-50198, -19980> velocity=< 5,  2>
position=<-40119,  40472> velocity=< 4, -4>
position=< 20330,  -9911> velocity=<-2,  1>
position=< 30423, -30064> velocity=<-3,  3>
position=<-40151, -50208> velocity=< 4,  5>
position=< 40505,  30400> velocity=<-4, -3>
position=< 10266, -50216> velocity=<-1,  5>
position=<-40150, -30064> velocity=< 4,  3>
position=< -9915,  20322> velocity=< 1, -2>
position=< 50534,  10249> velocity=<-5, -1>
position=< -9923,  -9905> velocity=< 1,  1>
position=< 50581,  20323> velocity=<-5, -2>
position=<-40151,  30395> velocity=< 4, -3>
position=< -9906,  20322> velocity=< 1, -2>
position=<-40146,  50545> velocity=< 4, -5>
position=<-19974,  10245> velocity=< 2, -1>
position=<-30075, -40137> velocity=< 3,  4>
position=<-50185,  -9912> velocity=< 5,  1>
position=< -9899,  50552> velocity=< 1, -5>
position=< 30393, -19988> velocity=<-3,  2>
position=<-19970,  40472> velocity=< 2, -4>
position=< 50574,  -9908> velocity=<-5,  1>
position=<-20003,  40474> velocity=< 2, -4>
position=< 40492,  -9903> velocity=<-4,  1>
position=< -9891,  30392> velocity=< 1, -3>
position=< 20329,  -9905> velocity=<-2,  1>
position=< 30422, -30064> velocity=<-3,  3>
position=< 30402, -30063> velocity=<-3,  3>
position=< 50565,  20320> velocity=<-5, -2>
position=< 50551,  30397> velocity=<-5, -3>
position=< 50581,  30393> velocity=<-5, -3>
position=< -9883, -40137> velocity=< 1,  4>
position=<-20002, -19980> velocity=< 2,  2>
position=< 20316, -50216> velocity=<-2,  5>
position=< 10258, -40131> velocity=<-1,  4>
position=< 30429,  30397> velocity=<-3, -3>
position=< 30424, -30064> velocity=<-3,  3>
position=< 40505,  20325> velocity=<-4, -2>
position=< 40457, -40131> velocity=<-4,  4>
position=< 40497,  40472> velocity=<-4, -4>
position=< 20322,  40477> velocity=<-2, -4>
position=< -9929, -19988> velocity=< 1,  2>
position=< 20310, -30061> velocity=<-2,  3>
position=< -9871, -50216> velocity=< 1,  5>
position=<-30043,  10244> velocity=< 3, -1>
position=< 40505,  -9909> velocity=<-4,  1>
position=< 20345, -50209> velocity=<-2,  5>
position=< 10237,  30398> velocity=<-1, -3>
position=<-30058,  50553> velocity=< 3, -5>
position=<-40123, -50211> velocity=< 4,  5>
position=<-40150,  50553> velocity=< 4, -5>
position=< 10226,  20317> velocity=<-1, -2>
position=< 10269,  -9912> velocity=<-1,  1>
position=< 20340, -19988> velocity=<-2,  2>
position=<-50222,  40476> velocity=< 5, -4>
position=< -9883, -40140> velocity=< 1,  4>
position=< 50575, -40136> velocity=<-5,  4>
position=<-19972, -30064> velocity=< 2,  3>
position=<-40125, -30064> velocity=< 4,  3>
position=<-40151, -19982> velocity=< 4,  2>
position=< 30434, -30062> velocity=<-3,  3>
position=<-30042,  -9912> velocity=< 3,  1>
position=< 40465,  20324> velocity=<-4, -2>
position=< 30429,  30400> velocity=<-3, -3>
position=< 10229,  10245> velocity=<-1, -1>
position=< 20301, -19981> velocity=<-2,  2>
position=< 20331,  10240> velocity=<-2, -1>
position=<-30079,  30396> velocity=< 3, -3>
position=<-50191,  20325> velocity=< 5, -2>
position=<-50215,  40468> velocity=< 5, -4>
position=<-30046, -50208> velocity=< 3,  5>
position=< 30421,  40470> velocity=<-3, -4>
position=< -9931,  50550> velocity=< 1, -5>
position=<-19997,  20320> velocity=< 2, -2>
position=< 50573, -19983> velocity=<-5,  2>
position=<-50219, -40138> velocity=< 5,  4>
position=< 20358,  50545> velocity=<-2, -5>
position=< 50558,  -9911> velocity=<-5,  1>
position=< 40449, -50208> velocity=<-4,  5>
position=<-30082, -40140> velocity=< 3,  4>
position=<-40135, -30055> velocity=< 4,  3>
position=<-20007,  20323> velocity=< 2, -2>
position=<-19979,  -9903> velocity=< 2,  1>
position=< 50533, -50209> velocity=<-5,  5>
position=< -9915, -40139> velocity=< 1,  4>
position=< 10270, -19984> velocity=<-1,  2>
position=< 20353,  50546> velocity=<-2, -5>
position=<-19957,  -9908> velocity=< 2,  1>
position=< 30377, -30064> velocity=<-3,  3>
position=<-30075, -40140> velocity=< 3,  4>
position=< 40483,  40473> velocity=<-4, -4>
position=< 20300, -40135> velocity=<-2,  4>
position=< 40481,  30396> velocity=<-4, -3>
position=< 10234,  40470> velocity=<-1, -4>
position=< -9883, -50215> velocity=< 1,  5>
position=< 50568,  10244> velocity=<-5, -1>
position=< 40492,  20316> velocity=<-4, -2>
position=<-19994, -30062> velocity=< 2,  3>
position=<-50214,  10248> velocity=< 5, -1>
position=< -9923,  -9908> velocity=< 1,  1>
position=<-19951, -40134> velocity=< 2,  4>
position=< 10245,  20325> velocity=<-1, -2>
position=< 30415, -40131> velocity=<-3,  4>
position=< 20357,  10244> velocity=<-2, -1>
position=< 20337,  20323> velocity=<-2, -2>
position=< 10226, -40137> velocity=<-1,  4>
position=<-19990,  20325> velocity=< 2, -2>
position=<-40127, -50207> velocity=< 4,  5>
position=< 10221,  10240> velocity=<-1, -1>
position=< 40461,  -9908> velocity=<-4,  1>
position=< 30373, -30055> velocity=<-3,  3>
position=<-50219, -40135> velocity=< 5,  4>
position=< 10269,  20320> velocity=<-1, -2>
position=<-40159, -30057> velocity=< 4,  3>
position=<-40102,  30392> velocity=< 4, -3>
position=< 50525,  10241> velocity=<-5, -1>
position=<-50227, -30056> velocity=< 5,  3>
position=<-50191,  -9903> velocity=< 5,  1>
position=<-50194,  10244> velocity=< 5, -1>
position=< 40476,  50544> velocity=<-4, -5>
position=<-50187, -30056> velocity=< 5,  3>
position=< 30429, -40134> velocity=<-3,  4>
position=<-20002,  -9910> velocity=< 2,  1>
position=<-19994,  30395> velocity=< 2, -3>
position=<-40122,  -9908> velocity=< 4,  1>
position=<-30043,  50545> velocity=< 3, -5>
position=< 10221, -30062> velocity=<-1,  3>
position=< 30426,  40469> velocity=<-3, -4>
position=<-19999,  20325> velocity=< 2, -2>
position=< 30389,  40472> velocity=<-3, -4>
position=<-50187, -30055> velocity=< 5,  3>
position=<-30070,  50549> velocity=< 3, -5>
position=< 50537,  50553> velocity=<-5, -5>
position=<-30035,  20325> velocity=< 3, -2>
position=<-30056,  10249> velocity=< 3, -1>
position=< 30421,  -9912> velocity=<-3,  1>
position=<-19991, -50210> velocity=< 2,  5>
position=< 40507, -19988> velocity=<-4,  2>
position=< 50527,  30396> velocity=<-5, -3>
position=< -9928,  -9912> velocity=< 1,  1>
position=< -9883, -50211> velocity=< 1,  5>
position=< 40452, -40135> velocity=<-4,  4>
position=<-30035,  20325> velocity=< 3, -2>
position=< 30386,  -9906> velocity=<-3,  1>
position=<-40130,  40470> velocity=< 4, -4>
position=< 40449,  20325> velocity=<-4, -2>
position=< 20353,  40477> velocity=<-2, -4>
position=<-50179,  50544> velocity=< 5, -5>
position=< 50582,  10240> velocity=<-5, -1>
position=<-40106,  40469> velocity=< 4, -4>
position=<-30082,  40468> velocity=< 3, -4>
position=< 10237,  30395> velocity=<-1, -3>
position=< 30410,  50553> velocity=<-3, -5>";
    }
}