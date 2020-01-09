using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Shared
{
    public struct Vector2i : IEquatable<Vector2i>
    {
        public int X;
        public int Y;


        public Vector2i Add(Vector2i vector)
        {
            return new Vector2i
            {
                X = X + vector.X,
                Y = Y + vector.Y
            };
        }

        public bool Equals(Vector2i vector)
        {
            return vector.X == X && vector.Y == Y;
        }

        public static bool operator ==(Vector2i first, Vector2i second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Vector2i first, Vector2i second)
        {
            return !first.Equals(second);
        }

        public int ManhattanDistance(Vector2i vector)
        {
            return Math.Abs(X - vector.X) + Math.Abs(Y - vector.Y);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
