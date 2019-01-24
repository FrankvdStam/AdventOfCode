using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    class Program
    {
        private static string Input = @"################################
##############.#################
##########G##....###############
#########.....G.################
#########...........############
#########...........############
##########.....G...#############
###########.........############
########.#.#..#..G....##########
#######..........G......########
##..GG..................###.####
##G..........................###
####G.G.....G.#####...E.#.G..###
#....##......#######........####
#.GG.#####.G#########.......####
###..####...#########..E...#####
#...####....#########........###
#.G.###.....#########....E....##
#..####...G.#########E.....E..##
#..###G......#######E.........##
#..##.........#####..........###
#......................#..E....#
##...G........G.......#...E...##
##............#..........#..####
###.....#...#.##..#......#######
#####.###...#######...#..#######
#########...E######....#########
###########...######.###########
############..#####..###########
#############.E..##.############
################.#..############
################################";


        static void Main(string[] args)
        {
            Map map = new Map(Input);
            map.Draw();
            Console.ReadKey();
        }
    }


    public class Map
    {
        public Map(string map)
        {
            ParseMap(map);
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
        }

        public void ParseMap(string input)
        {
            //Assume the map is always rectangular
            var lines = input.Split(new string[] {"\r\n"}, StringSplitOptions.None);
            Height = lines.Length;
            Width = lines[0].Length;

            Terrain = new char[Width, Height];
            Units = new Unit[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    switch (lines[y][x])
                    {
                        case '#':
                            Terrain[x, y] = '#';
                            break;
                        case '.':
                            Terrain[x, y] = '.';
                            break;
                        case 'G':
                            Terrain[x, y] = '.';
                            Units[x, y] = new Unit('G');
                            break;
                        case 'E':
                            Terrain[x, y] = '.';
                            Units[x, y] = new Unit('E');
                            break;
                        default:
                            throw new Exception($"Unexpected input char: {lines[y][x]}");
                    }
                }
            }
        }


        public int Width;
        public int Height;

        public char[,] Terrain;
        public Unit[,] Units;

        #region

        public void ProgressTurn()
        {
            AssignReadOrder();

            foreach (var unit in Units)
            {
                
            }
        }

        private void AssignReadOrder()
        {
            int counter = 0;
            foreach (Unit u in UnitsReadOrder())
            {
                u.ReadOrder = counter;
                counter++;
            }
        }
        #endregion

        #region Drawing ====================================================================================
        public void Draw()
        {
            Console.Clear();
            Console.SetCursorPosition(0,0);
            DrawTerrain();
            DrawUnits();
            DrawUnitInfo();
        }


        private void DrawTerrain()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.Write(Terrain[x, y]);
                }
                Console.Write("\n");
            }
        }

        private void DrawUnits()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Units[x, y] != null)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(Units[x, y].Type);
                    }
                }
            }
        }

        private void DrawUnitInfo()
        {
            for (int y = 0; y < Height; y++)
            {
                Console.SetCursorPosition(Width+3, y);
                for (int x = 0; x < Width; x++)
                {
                    if (Units[x, y] != null)
                    {
                        Console.Write(Units[x, y].ToString());
                    }
                }
            }
        }
        #endregion

        #region Iterators ======================================================================================================
        private IEnumerable<char> TerrainReadOrder()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    yield return Terrain[x, y];
                }
            }
        }

        private IEnumerable<Unit> UnitsReadOrder()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Units[x, y] != null)
                    {
                        yield return Units[x, y];
                    }
                }
            }
        }
        #endregion
    }

    public class Unit
    {
        public Unit(char type)
        {
            Type = type;
            Health = 200;
            Damage = 3;
        }

        public int ReadOrder;
        public int Health;
        public int Damage;
        public char Type;

        public override string ToString()
        {
            return $"[{Type}:{Health}]";
        }
    }
}
