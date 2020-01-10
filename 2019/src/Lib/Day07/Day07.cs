using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Day07
{
    public class Day07 : IDay
    {
        public int Day => 7;

        public void ProblemOne()
        {
            var permutations = GetPermutations(new List<int> { 0, 1, 2, 3, 4 }, 5).ToList();
        }

        public void ProblemTwo()
        {
            throw new NotImplementedException();
        }


        //https://stackoverflow.com/questions/756055/listing-all-permutations-of-a-string-integer
        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}
