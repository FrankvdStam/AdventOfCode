using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right,
    }

    public enum Heading
    {
        Left,
        Straight,
        Right
    }

    public class Cart
    {
        public Direction Direction = Direction.None;

        private Heading _previousHeading = Heading.Right;
        public Heading GetHeading()
        {
            switch (_previousHeading)
            {
                case Heading.Right:
                    _previousHeading = Heading.Left;
                    break;
                case Heading.Left:
                    _previousHeading = Heading.Straight;
                    break;
                case Heading.Straight:
                    _previousHeading = Heading.Right;
                    break;
            }
            return _previousHeading;
        }
    }

    public class TrackNode
    {
        public TrackNode Up;
        public TrackNode Down;
        public TrackNode Left;
        public TrackNode Right;

        public Cart Cart;

        public bool IsIntersection => Up != null && Down != null && Left != null && Right != null;

        #region connections
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
        #endregion

        #region ToChar
        //This is messy. Just look the other way :)
        public char ToChar()
        {
            if(Cart != null)
            {
                return Cart.Direction.DirectionToChar();
            }

            if(Up != null && Down != null && Left != null && Right != null)
            {
                return '+';
            }

            if(Up != null && Down != null && Left == null && Right == null)
            {
                return '|';
            }

            if (Up == null && Down == null && Left != null && Right != null)
            {
                return '-';
            }

            if ((Up != null && Down == null && Left != null && Right == null) || (Up == null && Down != null && Left == null && Right != null))
            {
                return '/';
            }

            if ((Up != null && Down == null && Left == null && Right != null) || (Up == null && Down != null && Left != null && Right == null))
            {
                return '\\';
            }

            throw new Exception("Unsuported config.");
        }
        #endregion
    }

    public class Map
    {
        public Map(string input)
        {
            _tracks = Parse(input, out _width, out _height);
            ValidateMap(input);
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

        #region Moving the carts ==========================================================================================

        private static readonly Dictionary<Direction, (int x, int y)> DirectionToVectorLookup = new Dictionary<Direction, (int x, int y)>()
        {
            { Direction.Up      , ( 0, -1) },
            { Direction.Down    , ( 0,  1) },
            { Direction.Left    , (-1,  0) },
            { Direction.Right   , ( 1,  0) },
        };

        public void Tick()
        {
            List<Cart> movedCarts = new List<Cart>();
            
            var node = _tracks[2, 0];


            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (_tracks[x, y] != null && _tracks[x, y].Cart != null && !movedCarts.Contains(_tracks[x, y].Cart))
                    {
                        TrackNode currentNode = _tracks[x, y];

                        //Dont want to move it twice.
                        movedCarts.Add(currentNode.Cart);
                        
                        //Find our destination node
                        (int x, int y) vec = DirectionToVectorLookup[currentNode.Cart.Direction];
                        TrackNode destination = _tracks[x + vec.x, y + vec.y];
                        if (destination == null)
                        {

                            throw new Exception("Bug or invalid map.");
                        }

                        if (destination.IsIntersection)
                        {
                            //TODO.
                            //destination.Cart = currentNode.Cart;
                        }
                        else
                        {
                            destination.Cart = currentNode.Cart;
                        }
                        currentNode.Cart = null;
                    }
                }
            }
        }
        #endregion

        #region Parsing the map ==============================================================================================
        /// <summary>
        /// Parses everything. Outputs the width and height.
        /// </summary>
        private TrackNode[,] Parse(string input, out int width, out int height)
        {
            char[,] chars = InputToCharArray(input, out width, out height);
            char[,] charsWithCarts = InputToCharArray(input, out width, out height);
            
            CleanCartsFromTrack(chars, width, height);
            TrackNode[,] nodes = CreateTrackNodes(chars, width, height);
            AddCartsToTrackNodes(charsWithCarts, nodes, width, height);
            return nodes;
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

        private void ValidateMap(string input)
        {
            char[,] fromInput = InputToCharArray(input, out int inputWidth, out int inputHeight);
            char[,] fromParse = InputToCharArray(ToString(), out int parseWidth, out int parseHeight);

            if (inputWidth != parseWidth || inputHeight != parseHeight)
            {
                throw new Exception("Validating map failed, width and height don't match.");
            }

            List<(int x, int y, char input, char parse)> mismatchedChars = new List<(int x, int y, char input, char parse)> ();
            for (int y = 0; y < inputHeight; y++)
            {
                for (int x = 0; x < inputWidth; x++)
                {
                    if (fromInput[x, y] != fromParse[x, y])
                    {
                        mismatchedChars.Add((x, y, fromInput[x, y], fromParse[x, y]));
                    }
                }
            }

            if (mismatchedChars.Any())
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("Validating map failed, mismatched chars at: ");
                foreach (var chars in mismatchedChars)
                {
                    builder.Append($"({chars.x}, {chars.y}): '{chars.input}' - '{chars.parse}' ");
                }

                throw new Exception(builder.ToString());
            }
        }


        /// <summary>
        /// To parse the track I do not want to have to deal with carts right away, parsing the track is complicated enough on it's own.
        /// </summary>
        private void CleanCartsFromTrack(char[,] chars, int width, int height)
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

                        chars[x, y] = CharExtensions.GetTrackUnderneathCart(up, down, left, right);
                    }
                }
            }
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
        

        private void AddCartsToTrackNodes(char[,] chars, TrackNode[,] nodes, int width, int height)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (chars[x, y].IsCart())
                    {
                        nodes[x, y].Cart = new Cart { Direction = chars[x, y].CartToDirection() };
                    }
                }
            }
        }
        #endregion

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (_tracks[x, y] != null)
                    {
                        builder.Append(_tracks[x, y].ToChar());
                    }
                    else
                    {
                        builder.Append(' ');
                    }
                }
                builder.Append("\r\n");
            }
            //Remove the last \r\n we append
            builder.Remove(builder.Length - 2, 2);
            return builder.ToString();
        }
    }
}
