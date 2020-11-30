using System;

namespace Years.Utils
{
    public struct Vector3i : IEquatable<Vector3i>
    {
        public Vector3i(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }



        public int X;
        public int Y;
        public int Z;


        public Vector3i Add(Vector3i vector)
        {
            return new Vector3i(X + vector.X, Y + vector.Y, Z + vector.Z);
        }

        //public bool Equals(Vector2i vector)
        //{
        //    return vector.X == X && vector.Y == Y;
        //}

        public static bool operator ==(Vector3i first, Vector3i second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(Vector3i first, Vector3i second)
        {
            return !first.Equals(second);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector3i v)
            {
                return v.X == X && v.Y == Y && v.Z == Z;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode();
        }

        //public static int ManhattanDistance(Vector3i vector1, Vector3i vector2)
        //{
        //    return Math.Abs(vector1.X - vector2.X) + Math.Abs(vector1.Y - vector2.Y);
        //}

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }

        public bool Equals(Vector3i other)
        {
            return other.X == X && other.Y == Y && other.Z == Z;
        }

        public static readonly Vector3i Zero = new Vector3i(0, 0, 0);
    }
}
