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
    }
}
