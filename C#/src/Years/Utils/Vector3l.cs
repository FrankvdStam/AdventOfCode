using System;

namespace Years.Utils
{
    public struct Vector3l : IEquatable<Vector3l>
    {
        public Vector3l(long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;
        }



        public long X;
        public long Y;
        public long Z;


        public Vector3l Add(Vector3l vector)
        {
            return new Vector3l(X + vector.X, Y + vector.Y, Z + vector.Z);
        }

        //public bool Equals(Vector2i vector)
        //{
        //    return vector.X == X && vector.Y == Y;
        //}

        public static bool operator ==(Vector3l first, Vector3l second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Vector3l first, Vector3l second)
        {
            return !first.Equals(second);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector3l v)
            {
                return v.X == X && v.Y == Y && v.Z == Z;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode();
        }

        //public static int ManhattanDistance(Vector3l vector1, Vector3l vector2)
        //{
        //    return Math.Abs(vector1.X - vector2.X) + Math.Abs(vector1.Y - vector2.Y);
        //}

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }

        public bool Equals(Vector3l other)
        {
            return other.X == X && other.Y == Y && other.Z == Z;
        }

        public static readonly Vector3l Zero = new Vector3l(0, 0, 0);
    }
}
