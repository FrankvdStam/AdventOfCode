using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Years.Utils
{
    public interface IDay
    {
        int Day { get; }
        int Year { get; }
        void ProblemOne();
        void ProblemTwo();
    }

    public abstract class BaseDay : IDay
    {
        public BaseDay(int year, int day)
        {
            Year = year;
            Day = day;
            Input = InputManager.Instance.GetInput(Year, Day);
        }

        public int Year { get; }
        public int Day { get; }
        public string Input { get; }

        public abstract void ProblemOne();
        public abstract void ProblemTwo();
    }    
}
