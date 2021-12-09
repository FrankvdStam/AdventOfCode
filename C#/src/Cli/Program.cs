using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Years.Utils;

namespace Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            bool runAll = false;
            int year = 2018;
            int day = 12;

            Run(runAll, year, day);
        }

        static void Run(bool runAll, int year, int day)
        {

            //Initialize all IDays
            var type = typeof(IDay);
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p)).ToList();

            List<IDay> days = new List<IDay>();
            foreach (var t in types)
            {
                if (t.IsClass)
                {
                    var obj = (IDay)Activator.CreateInstance(t);
                    days.Add(obj);
                }
            }

            Stopwatch stopwatch = new Stopwatch();

            if (runAll)
            {
                for (int y = 2015; y <= 2019; y++)
                {
                    for (int d = 1; d <= 25; d++)
                    {
                        var day_ = days.First(i => i.Year == y && i.Day == d);

                        stopwatch.Start();
                        day_.ProblemOne();
                        stopwatch.Stop();
                        Console.WriteLine($"{y}-{d.ToString().PadLeft(2, '0')}-{01} in {stopwatch.ElapsedMilliseconds}ms");

                        stopwatch.Restart();
                        day_.ProblemTwo();
                        stopwatch.Stop();
                        Console.WriteLine($"{y}-{d.ToString().PadLeft(2, '0')}-{02} in {stopwatch.ElapsedMilliseconds}ms");
                    }
                }
            }
            else
            {
                var activeDay = days.First(i => i.Year == year && i.Day == day);

                stopwatch.Start();
                activeDay.ProblemOne();
                stopwatch.Stop();
                Console.WriteLine($"{year}-{day}-{01} in {stopwatch.ElapsedMilliseconds}ms");

                stopwatch.Restart();
                activeDay.ProblemTwo();
                stopwatch.Stop();
                Console.WriteLine($"{year}-{day}-{02} in {stopwatch.ElapsedMilliseconds}ms");
            }

            Console.ReadKey();
        }
    }
}