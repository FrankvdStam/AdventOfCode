using System;
using System.Collections.Generic;

namespace Years.Utils
{
    [Serializable]
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

        public int ManhattanDistance(Vector2i other)
        {
            return ManhattanDistance(this, other);
        }

        public int Distance(Vector2i other)
        {
            return (int)Math.Sqrt(Math.Pow((other.X - X), 2) + Math.Pow((other.Y - Y), 2));
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

        public Vector2i Clone()
        {
            return new Vector2i(X, Y);
        }


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
            List<Vector2i> line = new List<Vector2i>();
            if (Math.Abs(y1 - y0) < Math.Abs(x1 - x0))
            {
                if (x0 > x1)
                {
                    line.Add(new Vector2i(x0, y0));//Origin
                    var temp = PlotLineLow(x1, y1, x0, y0);
                    temp.Reverse();
                    line.AddRange(temp);
                }
                else
                {
                    line.AddRange(PlotLineLow(x0, y0, x1, y1));
                    line.Add(new Vector2i(x1, y1)); //Add end
                }
            }
            else
            {
                if (y0 > y1)
                {
                    line.Add(new Vector2i(x0, y0));//Origin
                    var temp = PlotLineHigh(x1, y1, x0, y0);
                    temp.Reverse();
                    line.AddRange(temp);
                }
                else
                {
                    line.AddRange(PlotLineHigh(x0, y0, x1, y1));
                    line.Add(new Vector2i(x1, y1)); //Add end
                }
            }

            return line;
        }

        public List<Vector2i> PlotLine(Vector2i vec)
        {
            return PlotLine(X, Y, vec.X, vec.Y);
        }

        #endregion
    }
}
