using System;
using System.Collections.Generic;
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
            int year = 2015;
            int day = 13;

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


            if (runAll)
            {
                for (int y = 2019; y <= 2019; y++)
                {
                    for (int d = 1; d <= 25; d++)
                    {
                        var day_ = days.First(i => i.Year == y && i.Day == d);
                        day_.ProblemOne();
                        day_.ProblemTwo();
                    }
                   
                }
            }
            else
            {
                var activeDay = days.First(i => i.Year == year && i.Day == day);

                activeDay.ProblemOne();
                activeDay.ProblemTwo();
                Console.ReadKey();
            }
        }
    }
}