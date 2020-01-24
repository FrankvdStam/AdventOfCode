using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Shared
{
    public struct Vector2i : IEquatable<Vector2i>
    {
        public Vector2i(int x, int y)
        {
            X = x;
            Y = y;
        }



        public int X;
        public int Y;


        public Vector2i Add(Vector2i vector)
        {
            return new Vector2i(X + vector.X, Y + vector.Y);
        }

        //public bool Equals(Vector2i vector)
        //{
        //    return vector.X == X && vector.Y == Y;
        //}

        public static bool operator ==(Vector2i first, Vector2i second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Vector2i first, Vector2i second)
        {
            return !first.Equals(second);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector2i v)
            {
                return v.X == X && v.Y == Y;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }

        public static int ManhattanDistance(Vector2i vector1, Vector2i vector2)
        {
            return Math.Abs(vector1.X - vector2.X) + Math.Abs(vector1.Y - vector2.Y);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public bool Equals(Vector2i other)
        {
            return other.X == X && other.Y == Y;
        }

        public static readonly Vector2i Zero = new Vector2i(0, 0);




        #region Plot line ========================================================================================================
        //https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm

        private void plot(int x, int y) { }

        private List<Vector2i> PlotLineLow(int x0, int y0, int x1, int y1)
        {
            List<Vector2i> line = new List<Vector2i>();

            int dx = x1 - x0;
            int dy = y1 - y0;
            int yi = 1;
            if (dy < 0)
            {
                yi = -1;
                dy = -dy;
            }

            int D = 2 * dy - dx;
            int y = y0;

            for (int x = x0; x < x1; x++)
            {
                line.Add(new Vector2i(x, y));
                if (D > 0)
                {
                    y = y + yi;
                    D = D - 2 * dx;
                }
                D = D + 2 * dy;
            }

            return line;
        }

        private List<Vector2i> PlotLineHigh(int x0, int y0, int x1, int y1)
        {
            List<Vector2i> line = new List<Vector2i>();

            int dx = x1 - x0;
            int dy = y1 - y0;
            int xi = 1;
            if (dx < 0)
            {
                xi = -1;
                dx = -dx;
            }

            int D = 2 * dx - dy;
            int x = x0;

            for (int y = y0; y < y1; y++)
            {
                line.Add(new Vector2i(x, y));
                if (D > 0)
                {
                    x = x + xi;
                    D = D - 2 * dy;
                }
                D = D + 2 * dx;
            }

            return line;
        }

        private List<Vector2i> PlotLine(int x0, int y0, int x1, int y1)
        {
            if (Math.Abs(y1 - y0) < Math.Abs(x1 - x0))
            {
                if (x0 > x1)
                {
                    return PlotLineLow(x1, y1, x0, y0);
                }
                else
                {
                    return PlotLineLow(x0, y0, x1, y1);
                }
            }
            else
            {
                if (y0 > y1)
                {
                    return PlotLineHigh(x1, y1, x0, y0);
                }
                else
                {
                    return PlotLineHigh(x0, y0, x1, y1);
                }
            }
        }

        public List<Vector2i> PlotLine(Vector2i vec)
        {
            var line = PlotLine(X, Y, vec.X, vec.Y);
            line.Add(vec);
            return line;
        }

        #endregion
    }
}
