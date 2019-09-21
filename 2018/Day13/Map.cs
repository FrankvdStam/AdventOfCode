using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    public class TrackNode
    {
        public TrackNode Up;
        public TrackNode Down;
        public TrackNode Left;
        public TrackNode Right;

        public bool IsIntersection => Up != null && Down != null && Left != null && Right != null;

        public void ConnectUp(TrackNode n)
        {
            this.Up = n;
            n.Down = this;
        }

        public void ConnectDown(TrackNode n)
        {
            this.Down = n;
            n.Up = this;
        }

        public void ConnectLeft(TrackNode n)
        {
            this.Left = n;
            n.Right = this;
        }

        public void ConnectRight(TrackNode n)
        {
            this.Right = n;
            n.Left = this;
        }
    }
    
    public class Map
    {
        public Map(string input)
        {
            _tracks = Parse(input, out _width, out _height);
        }

        private int _width;
        private int _height;
        private TrackNode[,] _tracks;

        /*

        /->-\        
        |   |  /----\
        | /-+--+-\  |
        | | |  | v  |
        \-+-/  \-+--/
          \------/   

         */

        #region Parsing the map ==============================================================================================
        /// <summary>
        /// Parses everything. Outputs the width and height.
        /// </summary>
        private TrackNode[,] Parse(string input, out int width, out int height)
        {
            char[,] chars = InputToCharArray(input, out width, out height);
            PrintChars(chars, width, height);
            chars = CleanCartsFromTrack(chars, width, height);
            PrintChars(chars, width, height);
            TrackNode[,] nodes = CreateTrackNodes(chars, width, height);

            return nodes;
        }
               

        private void PrintChars(char[,] chars, int width, int height)
        {
            Console.Clear();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write(chars[x, y]);
                }
                Console.Write('\n');
            }
        }


        /// <summary>
        /// Converting the input string to a chararray just clears things up a lot.
        /// </summary>
        private char[,] InputToCharArray(string input, out int width, out int height)
        {
            var lines = input.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            width = lines[0].Length;
            height = lines.Length;

            char[,] chars = new char[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    chars[x, y] = lines[y][x];
                }
            }

            return chars;
        }


        /// <summary>
        /// To parse the track I do not want to have to deal with carts right away, parsing the track is complicated enough on it's own.
        /// </summary>
        private char[,] CleanCartsFromTrack(char[,] chars, int width, int height)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if(chars[x, y].IsCart())
                    {
                        char c = chars[x, y];

                        char? up = null, down = null, left = null, right = null;
                        
                        if(y-1 >= 0)
                        {
                            up = chars[x, y - 1];
                        }

                        if(y+1 < height)
                        {
                            down = chars[x, y + 1];
                        }

                        if(x - 1 >= 0)
                        {
                            left = chars[x - 1, y];
                        }

                        if (x + 1 < width)
                        {
                            right = chars[x + 1, y];
                        }

                        //TODO: remember cart position in an out value
                        try
                        {

                        chars[x, y] = CharExtensions.GetTrackUnderneathCart(up, down, left, right);
                        }
                        catch
                        {
                        }
                    }
                }
            }

            return chars;
        }


        /// <summary>
        /// Create tracknodes and their connection from an input char array without carts on it.
        /// </summary>
        private TrackNode[,] CreateTrackNodes(char[,] chars, int width, int height)
        {
            TrackNode[,] track = new TrackNode[width, height];

            //Local helper method, creates and returns or finds and returns tracknodes.
            TrackNode GetNodeAt(int x, int y)
            {
                if (track[x, y] == null)
                {
                    track[x, y] = new TrackNode();
                }
                return track[x, y];
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if(chars[x, y] != ' ')
                    {
                        char? above = null;
                        if(y-1 >= 0)
                        {
                            above = chars[x, y - 1];
                        }

                        (bool up, bool down, bool left, bool right) connectionsToMake = chars[x, y].FindRequiredConnections(above);

                        TrackNode middle = GetNodeAt(x, y);

                        if (connectionsToMake.up)
                        {
                            middle.ConnectUp(GetNodeAt(x, y - 1));
                        }

                        if (connectionsToMake.down)
                        {
                            middle.ConnectDown(GetNodeAt(x, y + 1));
                        }

                        if (connectionsToMake.left)
                        {
                            middle.ConnectLeft(GetNodeAt(x - 1, y));
                        }

                        if (connectionsToMake.right)
                        {
                            middle.ConnectRight(GetNodeAt(x + 1, y));
                        }
                    }
                }
            }
            return track;
        }

        #endregion

    }
}
