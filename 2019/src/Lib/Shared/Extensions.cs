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
    }
}
