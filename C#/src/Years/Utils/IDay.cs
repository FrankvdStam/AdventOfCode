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
}
