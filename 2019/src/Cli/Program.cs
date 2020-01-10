using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib;

namespace Cli
{
    class Program
    {
        private static List<IDay> Days = new List<IDay>();

        static void Main(string[] args)
        {
            var type = typeof(IDay);
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p)).ToList();
            foreach (var t in types)
            {
                try
                {
                    var obj = (IDay)Activator.CreateInstance(t);
                    Days.Add(obj);
                }
                catch { }
            }
            

            int day = 11;

            var activeDay = Days.FirstOrDefault(i => i.Day == day);

            activeDay.ProblemOne();
            activeDay.ProblemTwo();
        }
    }
}
