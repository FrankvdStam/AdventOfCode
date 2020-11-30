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
            var type = typeof(IDay);
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p)).ToList();

            List<IDay> days = new List<IDay>();
            foreach (var t in types)
            {
                try
                {
                    var obj = (IDay)Activator.CreateInstance(t);
                    days.Add(obj);
                }
                catch { }
            }


            int year = 2015;
            int day = 7;

            var activeDay = days.First(i => i.Year == year && i.Day == day);

            activeDay.ProblemOne();
            activeDay.ProblemTwo();
            Console.ReadKey();
        }
    }
}