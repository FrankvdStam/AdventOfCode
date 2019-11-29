using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    /// <summary>
    /// Helper class to determine some constants about chars to make the rest of the code more readable.
    /// </summary>
    public static class CharExtensions
    {
        /// <summary>
        /// This class stores all the information we need about a track char
        /// </summary>
        internal class CharInfo
        {
            public bool HasUpConnection;
            public bool HasDownConnection;
            public bool HasLeftConnection;
            public bool HasRightConnection;
        }

        /// <summary>
        /// With this dictionary we memoize the important information about chars
        /// </summary>
        internal static Dictionary<char, CharInfo> _charInfo = new Dictionary<char, CharInfo>()
        {
            { '+', new CharInfo{ HasUpConnection = true,  HasDownConnection = true,  HasLeftConnection = true,  HasRightConnection = true  } },
            { '|', new CharInfo{ HasUpConnection = true,  HasDownConnection = true,  HasLeftConnection = false, HasRightConnection = false } },
            { '-', new CharInfo{ HasUpConnection = false, HasDownConnection = false, HasLeftConnection = true,  HasRightConnection = true  } },
        };

        internal static char[] _carts = { '<', '>', '^', 'v' };

        public static bool IsCart(this char c)
        {
            return _carts.Contains(c);
        }


        #region Connections =================================================================================================================
        public static bool HasUpConnection(this char c)
        {
            if (_charInfo.TryGetValue(c, out CharInfo result))
            {
                return result.HasUpConnection;
            }
            return false;
        }

        public static bool HasDownConnection(this char c)
        {
            if (_charInfo.TryGetValue(c, out CharInfo result))
            {
                return result.HasDownConnection;
            }
            return false;
        }

        public static bool HasLeftConnection(this char c)
        {
            if (_charInfo.TryGetValue(c, out CharInfo result))
            {
                return result.HasLeftConnection;
            }
            return false;
        }

        public static bool HasRightConnection(this char c)
        {
            if (_charInfo.TryGetValue(c, out CharInfo result))
            {
                return result.HasRightConnection;
            }
            return false;
        }
        #endregion

        #region find connections to make =============================================================================================================
        /// <summary>
        /// Find out what connection to make. We only need the char itself and the char above to make find out. The char above can be null if we are in the top row of the track - in this case we also know that no connection can be made up.
        /// This method does not handle carts (<>v^)
        /// </summary>
        public static (bool up, bool down, bool left, bool right) FindRequiredConnections(this char c, char? above)
        {
            bool up = false;
            bool down = false;
            bool left = false;
            bool right = false;

            switch (c)
            {
                //Whitespace - we're not part of the track. Do nothing.
                case ' ':
                    break;

                //Horizontal track: need to add left & right
                case '-':
                    left = true;
                    right = true;
                    break;
                case '|':
                    up = true;
                    down = true;
                    break;
                case '+':
                    up = true;
                    down = true;
                    left = true;
                    right = true;
                    break;

                //Corners are slightly more complicated because their connections depend on the chars they align with.
                //They have to attach to any char in charInfo for a valid connection. Using this dictionary, we can find out what way they connect.
                case '/':
                    if (above != null && above.Value.HasDownConnection())
                    {
                        //This corner is connected from above to the left
                        up = true;
                        left = true;
                    }
                    else
                    {
                        right = true;
                        down = true;
                    }

                    break;
                case '\\':
                    if (above != null && above.Value.HasDownConnection())
                    {
                        up = true;
                        right = true;
                    }
                    else
                    {
                        left = true;
                        down = true;
                    }
                    break;
                default:
                    throw new Exception($"Unrecognized char: {c}");
            }

            return (up, down, left, right);

        }
        #endregion

        #region decipher track underneath cart =====================================================================================================================
        public static char GetTrackUnderneathCart(char? up, char? down, char? left, char? right)
        {
            bool _up    = up    != null && up   .Value.HasUpConnection();
            bool _down  = down  != null && down .Value.HasDownConnection();
            bool _left  = left  != null && left .Value.HasLeftConnection();
            bool _right = right != null && right.Value.HasRightConnection();

            if (_up && _down && _left && _right)
            {
                return '+';
            }

            if (_up && _down && !_left && !_right)
            {
                return '|';
            }

            if (!_up && !_down && _left && _right)
            {
                return '-';
            }

            //If we have a corner - cart - straight situation, only 1 bool will be true because we don't know the orientation of corners yet.

            //Bug: we cant parse this if cart is on intersection:
            /*
                |||
                \+/
                 |
             
             */

            if(_up && !_down && !_left && !_right)
            {
                return '|';
            }

            if (!_up && _down && !_left && !_right)
            {
                return '|';
            }

            if (!_up && !_down && _left && !_right)
            {
                return '-';
            }

            if (!_up && !_down && !_left && _right)
            {
                return '-';
            }

            //Going to assume the input never generates carts on corners.
            throw new Exception("Corners aren't supported as starting positions.");
        }
        #endregion

        #region cart and direction ======================================================================================================
        private static Dictionary<char, Direction> _directions = new Dictionary<char, Direction>()
        {
            { '^', Direction.Up },
            { 'v', Direction.Down },
            { '<', Direction.Left },
            { '>', Direction.Right },
        };
        
        public static Direction CartToDirection(this char cart)
        {
            if(!cart.IsCart())
            {
                throw new Exception($"{cart} is not a cart.");
            }
            return _directions[cart];
        }

        public static char DirectionToChar(this Direction dir)
        {
            return _directions.First(i => i.Value == dir).Key;
        }
        #endregion
    }
}
