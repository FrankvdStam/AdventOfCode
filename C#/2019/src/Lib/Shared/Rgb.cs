using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Shared
{
    public class Rgb
    {
        public Rgb(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }

        public int R;
        public int G;
        public int B;

        public override string ToString()
        {
            return $"({R}, {G}, {B})";
        }
    }
}
