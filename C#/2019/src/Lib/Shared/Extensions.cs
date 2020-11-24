using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Shared
{
    public static class Extensions
    {
        public static List<T> Clone<T>(this List<T> oldList)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, oldList);
            stream.Position = 0;
            return (List<T>)formatter.Deserialize(stream);
        }

        public static Direction RotateLeft(this Direction direction)
        {
            //  /U\
            //  L R
            //  \D/
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Left;

                case Direction.Left:
                    return Direction.Down;

                case Direction.Down:
                    return Direction.Right;

                case Direction.Right:
                    return Direction.Up;
                
            }
            throw new Exception($"Unsupported ");
        }

        public static Direction RotateRight(this Direction direction)
        {
            //  /U\
            //  L R
            //  \D/
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Right;

                case Direction.Right:
                    return Direction.Down;

                case Direction.Down:
                    return Direction.Left;

                case Direction.Left:
                    return Direction.Up;

            }
            throw new Exception($"Unsupported ");
        }

        public static void Draw(this List<Vector2i> vectors, char c)
        {
            int minx = vectors.Min(i => i.X);
            int maxx = vectors.Max(i => i.X);
            int width = maxx - minx;
            int miny = vectors.Min(i => i.Y);
            int maxy = vectors.Max(i => i.Y);
            int height = maxy - miny;

            int transformX = 0;
            if (minx < 0)
            {
                transformX = Math.Abs(minx);
            }

            int transformY = 0;
            if (miny < 0)
            {
                transformY = Math.Abs(miny);
            }

            //Console.SetWindowSize(width, height);

            foreach (Vector2i v in vectors)
            {
                Console.SetCursorPosition(v.X + transformX, v.Y + transformY);
                Console.Write(c);
            }
        }
    }
}
