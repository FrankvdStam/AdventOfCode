using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            //var sheet = ParseInput(Example, 4, 3);
            var sheet = ParseInput(Input, 16, 16);
            //ProblemOne(sheet);
            ProblemTwo(sheet);
        }

        static void ProblemOne(int[,] sheet)
        {
            var sum = 0;
            for (int y = 0; y < sheet.GetLength(1); y++)
            {
                int min = int.MaxValue;
                int max = int.MinValue;

                for (int x = 0; x < sheet.GetLength(0); x++)
                {
                    int val = sheet[x, y];

                    if (sheet[x, y] < min)
                    {
                        min = sheet[x, y];
                    }

                    if (sheet[x, y] > max)
                    {
                        max = sheet[x, y];
                    }
                }

                int diff = max - min;
                sum += diff;
            }

            Console.Out.WriteLine($"Res: {sum}");
        }


        private static bool AreEvenDivisible(decimal a, decimal b, out decimal division)
        {
            decimal max, min;

            if (a > b)
            {
                max = a;
                min = b;
            }
            else
            {
                max = b;
                min = a;
            }

            division = max / min;
            return max % min == 0;
        }

        //This is terrible. It's horrifying. It works. Should NEVER be used in production.
        static void ProblemTwo(int[,] sheet)
        {
            int sum = 0;

            for (int y = 0; y < sheet.GetLength(1); y++)
            {
                Console.WriteLine($"Row: {y}");

                for (int x = 0; x < sheet.GetLength(0); x++)
                {
                    for (int i = 0; i < sheet.GetLength(0); i++)
                    {
                        Console.WriteLine($"{sheet[x, y]} - {sheet[i, y]}");

                        if (i != x && AreEvenDivisible(sheet[x, y], sheet[i, y], out decimal division))
                        {
                            var num1 = sheet[x, y];
                            var num2 = sheet[i, y];
                            sum += (int)division;
                            break;
                        }
                    }
                }
            }

            sum = sum / 2;

            Console.Out.WriteLine($"Sum: {sum}");
        }

        static int[,] ParseInput(string input, int width, int length)
        {
            int[,] sheet = new int[width, length];

            var lines = input.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            for (int y = 0; y < lines.Length; y++)
            {
                var bits = lines[y].Split(new string[] { "\t" }, StringSplitOptions.None);

                for (int x = 0; x < bits.Length; x++)
                {
                    sheet[x, y] = int.Parse(bits[x]);
                }
            }

            return sheet;
        }


        private static string Input = @"62	1649	1731	76	51	1295	349	719	52	1984	2015	2171	981	1809	181	1715
161	99	1506	1658	84	78	533	242	1685	86	107	1548	670	960	1641	610
95	2420	2404	2293	542	2107	2198	121	109	209	2759	1373	1446	905	1837	111
552	186	751	527	696	164	114	530	558	307	252	200	481	142	205	479
581	1344	994	1413	120	112	656	1315	1249	193	1411	1280	110	103	74	1007
2536	5252	159	179	4701	1264	1400	2313	4237	161	142	4336	1061	3987	2268	4669
3270	1026	381	185	293	3520	1705	1610	3302	628	3420	524	3172	244	295	39
4142	1835	4137	3821	3730	2094	468	141	150	3982	147	4271	1741	2039	4410	179
1796	83	2039	1252	84	1641	2165	1218	1936	335	1807	2268	66	102	1977	2445
96	65	201	275	257	282	233	60	57	200	216	134	72	105	81	212
3218	5576	5616	5253	178	3317	6147	5973	2424	274	4878	234	200	4781	5372	276
4171	2436	134	3705	3831	3952	2603	115	660	125	610	152	4517	587	1554	619
2970	128	2877	1565	1001	167	254	2672	59	473	2086	181	1305	162	1663	2918
271	348	229	278	981	1785	2290	516	473	2037	737	2291	2521	1494	1121	244
2208	2236	1451	621	1937	1952	865	61	1934	49	1510	50	1767	59	194	1344
94	2312	2397	333	1192	106	2713	2351	2650	2663	703	157	89	510	1824	125";
        private static string Example = @"5	9	2	8
9	4	7	3
3	8	6	5";

    }
}
