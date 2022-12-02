using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Years;
using Years.Utils;

namespace Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            //InputManager.Instance.AppendInput(2022, 02);
            RunDay(2015, 5);

            //RunYear(2018);
        }


        static List<IDay> _days = new List<IDay>();
        static void InitDays()
        {
            var type = typeof(IDay);
            _days = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract).Select(i => (IDay)Activator.CreateInstance(i)).ToList();
        }

        static void RunDay(int year, int day)
        {
            InitDays();
            var dayInstance = _days.First(i => i.Year == year && i.Day == day);
            RunDay(dayInstance);
            Console.ReadKey();
        }

        static void RunYear(int year)
        {
            InitDays();
            var days = _days.Where(i => i.Year == year).OrderBy(i => i.Day);
            foreach(var d in days)
            {
                RunDay(d);
            }
            Console.ReadKey();
        }

        private static void RunDay(IDay day)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            day.ProblemOne();
            stopwatch.Stop();
            Console.WriteLine($"{day.Year}-{day.Day.ToString().PadLeft(2, '0')} - part 1 in {stopwatch.ElapsedMilliseconds}ms");

            stopwatch.Restart();
            day.ProblemTwo();
            stopwatch.Stop();
            Console.WriteLine($"{day.Year}-{day.Day.ToString().PadLeft(2, '0')} - part 2 in {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}