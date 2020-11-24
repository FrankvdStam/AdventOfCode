using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    //What-a-baller
    public class Reddit
    {
        IEnumerable<string> Mutate(string sq, IEnumerable<string[]> replacements)
        {
            return 
                from pos in Enumerable.Range(0, sq.Length)
                from rep in replacements
                let a = rep[0]
                let b = rep[1]
                where sq.Substring(pos).StartsWith(a)
                select sq.Substring(0, pos) + b + sq.Substring(pos + a.Length);
        }

        static IEnumerable<T> Shuffle<T>(IEnumerable<T> source)
        {
            Random random = new Random();
            return source.OrderBy<T, int>((item) => random.Next());
        }


        public static int Search(string molecule, IEnumerable<string[]> replacements)
        {
            var target = molecule;
            var mutations = 0;

            while (target != "e")
            {
                var tmp = target;
                foreach (var rep in replacements)
                {
                    var a = rep[0];
                    var b = rep[1];
                    var index = target.IndexOf(b);
                    if (index >= 0)
                    {
                        target = target.Substring(0, index) + a + target.Substring(index + b.Length);
                        mutations++;
                    }
                }

                if (tmp == target)
                {
                    target = molecule;
                    mutations = 0;
                }
            }

            return mutations;
        }

        //void Main(string input, string replacementsInput)
        //{
        //    Mutate(input)
        //}
    }
}
