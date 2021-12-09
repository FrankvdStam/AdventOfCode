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
        private enum RunType
        {
            All,
            Year,
            Day
        }

        static void Main(string[] args)
        {
            Run(RunType.Year, 2017, 8);
        }

        static void Run(RunType runType, int year, int day)
        {
            //Initialize all IDays
            var type = typeof(IDay);
            var days = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p) && p.IsClass).Select(i => (IDay)Activator.CreateInstance(i)).ToList();
            
            //Filter
            switch (runType)
            {
                case RunType.All:
                    break;

                case RunType.Year:
                    days = days.Where(i => i.Year == year).ToList();
                    break;

                case RunType.Day:
                    days = days.Where(i => i.Year == year && i.Day == day).ToList();
                    break;

                
            }

            //sort
            days = days.OrderBy(i => i.Year).ThenBy(i => i.Day).ToList();

            //Run
            var stopwatch = new Stopwatch();
            foreach (var _day in days)
            {
                stopwatch.Start();
                _day.ProblemOne();
                stopwatch.Stop();
                Console.WriteLine($"{_day.Year}-{_day.Day.ToString().PadLeft(2, '0')} - part 1 in {stopwatch.ElapsedMilliseconds}ms");

                stopwatch.Restart();
                _day.ProblemTwo();
                stopwatch.Stop();
                Console.WriteLine($"{_day.Year}-{_day.Day.ToString().PadLeft(2, '0')} - part 2 in {stopwatch.ElapsedMilliseconds}ms");
            }

            Console.ReadKey();
        }
    }
}