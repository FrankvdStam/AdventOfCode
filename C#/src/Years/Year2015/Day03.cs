using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    class Program3
    {
        static void Main(string[] args)
        {
            ProblemTwo(input);
            Console.ReadKey();
        }

        static void ProblemTwo(string input)
        {
            GetRoboSize(input, out int width, out int height, out int x, out int y);
            int roboX = x;
            int roboY = y;
            bool robo = false;
            int[,] houses = new int[width, height];
            houses[x, y] += 2;

            foreach (char c in input)
            {
                if (robo)
                {
                    switch (c)
                    {
                        case '^':
                            roboX--;
                            break;
                        case '<':
                            roboY--;
                            break;
                        case '>':
                            roboY++;
                            break;
                        case 'v':
                            roboX++;
                            break;
                        default:
                            throw new Exception("" + c);
                    }
                    houses[roboX, roboY]++;
                    robo = false;
                }
                else
                {
                    switch (c)
                    {
                        case '^':
                            x--;
                            break;
                        case '<':
                            y--;
                            break;
                        case '>':
                            y++;
                            break;
                        case 'v':
                            x++;
                            break;
                        default:
                            throw new Exception("" + c);
                    }
                    houses[x, y]++;
                    robo = true;
                }

                //increment amount of presents at this house.

            }

            int count = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (houses[i, j] > 0)
                    {
                        count++;
                    }

                }
            }
            Console.WriteLine(count);
        }

        static void ProblemOne(string input)
        {
            GetSize(input, out int width, out int height, out int x, out int y);
            int[,] houses = new int[width, height];

            foreach (char c in input)
            {
                switch (c)
                {
                    case '^':
                        x--;
                        break;
                    case '<':
                        y--;
                        break;
                    case '>':
                        y++;
                        break;
                    case 'v':
                        x++;
                        break;
                    default:
                        throw new Exception("" + c);
                }
                //increment amount of presents at this house.
                houses[x, y]++;
            }

            int count = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (houses[i, j] > 0)
                    {
                        count++;
                    }

                }
            }
            Console.WriteLine(count);
        }

        static void GetRoboSize(string input, out int width, out int height, out int x, out int y)
        {
            width = 0;
            height = 0;
            x = 0;
            y = 0;
            int curX = 0, curY = 0, minX = 0, minY = 0, maxX = 0, maxY = 0;
            int roboX = x;
            int roboY = y;
            bool robo = false;


            foreach (char c in input)
            {
                if (robo)
                {
                    switch (c)
                    {
                        case '^':
                            roboX--;
                            if (roboX < minX)
                            {
                                minX = roboX;
                            }
                            break;
                        case '<':
                            roboY--;
                            if (roboY < minY)
                            {
                                minY = roboY;
                            }
                            break;
                        case '>':
                            roboY++;
                            if (roboY > maxY)
                            {
                                maxY = roboY;
                            }
                            break;
                        case 'v':
                            roboX++;
                            if (roboX > maxX)
                            {
                                maxX = roboX;
                            }
                            break;
                    }

                    robo = false;
                }
                else
                {
                    switch (c)
                    {
                        case '^':
                            curX--;
                            if (curX < minX)
                            {
                                minX = curX;
                            }
                            break;
                        case '<':
                            curY--;
                            if (curY < minY)
                            {
                                minY = curY;
                            }
                            break;
                        case '>':
                            curY++;
                            if (curY > maxY)
                            {
                                maxY = curY;
                            }
                            break;
                        case 'v':
                            curX++;
                            if (curX > maxX)
                            {
                                maxX = curX;
                            }
                            break;
                        default:
                            throw new Exception("" + c);
                    }

                    robo = true;
                }
            }

            foreach (char c in input)
            {
                switch (c)
                {
                    case '^':
                        curX--;
                        if (curX < minX)
                        {
                            minX = curX;
                        }
                        break;
                    case '<':
                        curY--;
                        if (curY < minY)
                        {
                            minY = curY;
                        }
                        break;
                    case '>':
                        curY++;
                        if (curY > maxY)
                        {
                            maxY = curY;
                        }
                        break;
                    case 'v':
                        curX++;
                        if (curX > maxX)
                        {
                            maxX = curX;
                        }
                        break;
                    default:
                        throw new Exception("" + c);
                }
            }

            x = Math.Abs(minX);
            y = Math.Abs(minY);

            width = maxX - minX + 1;
            height = maxY - minY + 1;
        }

        static void GetSize(string input, out int width, out int height, out int x, out int y)
        {
            width = 0;
            height = 0;
            x = 0;
            y = 0;
            int curX = 0, curY = 0, minX = 0, minY = 0, maxX = 0, maxY = 0;

            foreach (char c in input)
            {
                switch (c)
                {
                    case '^':
                        curX--;
                        if (curX < minX)
                        {
                            minX = curX;
                        }
                        break;
                    case '<':
                        curY--;
                        if (curY < minY)
                        {
                            minY = curY;
                        }
                        break;
                    case '>':
                        curY++;
                        if (curY > maxY)
                        {
                            maxY = curY;
                        }
                        break;
                    case 'v':
                        curX++;
                        if (curX > maxX)
                        {
                            maxX = curX;
                        }
                        break;
                    default:
                        throw new Exception("" + c);
                }
            }

            x = Math.Abs(minX);
            y = Math.Abs(minY);

            width = maxX - minX + 1;
            height = maxY - minY + 1;
        }

        private static string input =
            @"^^<<v<<v><v^^<><>^^<v<v^>>^^^><^>v^>v><><><<vv^^<^>^^<v^>v>v^v>>>^<>v<^<v^><^>>>>><<v>>^>>^>v^>><<^>v>v<>^v^v^vvv><>^^>v><v<><>^><^^<vv^v<v>^v>>^v^>v><>v^<vv>^><<v^>vv^<<>v>>><<<>>^<vv<^<>^^vv>>>^><<<<vv^v^>>><><^>v<>^>v<v^v<^vv><^v^><<<<>^<>v>^v>v<v<v<<>v<^<<<v>>>>>^^v>vv^^<>^<>^^^^<^^^v<v^^>v<^^v^^>v>^v^^^^>><<v<>v<>^v^<v<>><>^^><<^^<^^>vv<>v^<^v<vv<<<>^>^^>^<>v^^vv<>>v><<<>vvv<>v<>><^<^v<>^vv>^^v<v<v><^<>>vv<^>>^>>vv^v<vv^vv<^<<>>^v^<>^>>>>vv>^^>v>vv>v><^vv^<<v>^<<^^<v<v>vv<v^^<>^^v>^>>v><^<<vv<<v^vv^^^v>>v<<v^><vv^><vv<^vv<<vv^v<<^v<^^v>><<v^>>^^<>v>><<v<>>^^<v>>^^>>vvv^><<<<<^<^vv<^<><v<<>^^^<<<^>^^^<v<<vv>vv<>^<>v<^v>^<<<v<v<v>>^v<>>v<<^<<v<<>^<<<><><>^>>>>^>v^v<<v<v<<>>vv<^vvv^^^^<vv>vv>^v^^v^<v^v><^vv<^vv>v<^>vv<>>^>^><vv<><^>v>^v>vvv<>^>^v<><>vv>><^v^<><><v>>v^v^><^<^>vv>v<^>vvv>v<<<<<^<v<<vv<^^^<<>>^v<vv<^<>v>^<v<>><><>^<<v>v^>^<vv>><><>>^>^>><^<v>^^>^^>^^v^^<^v^^>v^^>>><<><v<v<<v^vv<><><>^<v>^<<^^v^>v>><>^^^><^vvv<^^^^^v><<><v<^^v><><>>^>vv<vvvv<<>>><v<^^^^v<<^><v>^vv<v^^v^vv<^^>^^<v>><<v^>v<^^>^<^<v<^^v>^<<v>^>>>^v<>v<^^^>vvv^v<<^><>>><vvv^<^^^<^>>v>>><v>^^vvv^vvv<^^^^v^v^<vv^<v>^<<^>v^v^<<><>><^v><v<><<>><<<>^v>v<>^<v^v>^vv>>^<>v^^<<v><^v>>v<>>^v^^>><^>v^<^v^^>><>v^>^v^v<<<v^<v^^v<^>v<><>vv>>>>^>v<>v<<<>^^>vv^v<><v^<>^<<<<>>^^>^v<v^v<<><>^v<>>^v^<<^<^>>>^vv<><v<^^<>v^>>v<^^v<v>>>^>><<><<<>><vv<v>>^v>><^<v><vv>^vv<v<>>><>v^><>vv<^^v^^^v<>><^vvv<<^<>v>>>v>><v><>>><>><v^><v^v<v>^v>v<v>>^^<^>^>v><>vv>^v><<>>>>>>>^<<^vv^^vvvv<^^><<<v<<>vvv<>^><<v<v^v^<<v>v<>>^<vv^<v<v>^<<^^vv>v>^<vv<<>v<v^<>v>>^v^^vvvv>^^>>v^v^^><<^>v>>^^>^<^^<>v<v>vv^vv>v<v>>^v<><^vv^<vv<v^^^v<^v^>>^v>>>^^<^<^>^v^>^>>>^v>^>^^^>>^<>v^^<>^v<<^^>^^<vv<>v<^v^>><^v^>^<>>^vv^vv^>v^<vvvvvv^>><^^<^v<^<v^<<^^<<v^<^>><>v><^v^v^^^v>v^<>^<<v<^^vvv<v>^^>^v^^<><vv^v^>v^<<>>vv<>>>>v>v<>^>>>v<>^^><v<v^^^<>^<^><>^><<v>><>^<<>>><<^<vvv<^><v>>^vv^v>><v<>vv^<<^^<<><v><<^<v<vv<<^v^vv>v^>>>v<<<<v<<>v>^vv<^v><v<v>v<^>^^vv>v><v>><<v<<v^v>>><>^<>><><<^<<^v^v<<v>v>v<v<^^>vv<^v^^^<v^<<<v<>v^><^v>^<^<v>>^<<<v>>v^<><>>^v<>vvv<vvvvv<^^><^>><^^>^>^v^vv<^><<^v>><^^v>^v<>^>vvvv><^>^<<v^^vv<v^^<><>v>^>>^<^<<<^v^^^>^>>^>><><<^>v^^<v>>v<<<<vvv<vvvv^<^<v^^<>^>vvv^<vv^v^v>^<<><v><^v^v^^^>^^>^vv<>v>>v^>vv^vv>v<^v^^>>^v^v<>>^^><<v<<>><>>>^>^<>^^v^^><^<>><<^<vv^^^^^>>vv^<v^<^>>>>v<<><<^>vv>vvv>^<><>>>>vv><<v^v<^^^<<^^^vv^<v<><><<<<>><<v^<>v>v^><>v^v^^><>v>v>^^v<^v<>>^^^^^<v>><v^>^^<v>><v^^>v<^<^>>>^><^^>><<>>^><>^^^>v^^^>^^v^<>^^><^>>><><^>>v<v^>v<^><v<v^<>v<^v>v^<^vv^^><<<><><^v^<v<^^>v>v^>>^^vv^<v>^v>^<^v<>^>^><^<v>^v><^<^<>v^^>^><>>><<v><<><>v<<^v^^<^><>^<><><v>v<^^<v<v>>^^<<>>^<v>><^><^<^>^^v<>v>>><><<>^>v><><<<<v^^^^v<>>^^^v>><<^v>^>>><vv^>>^vv<^<>>^<^^<^v>v<v<<<<<>^<<^<<<<<^<^>>^><<>><>v^v>^<^>v^<><vvv^>^v^v^v><^<v<>vv<<^<>^^^<>^v>^<v^^<v^v>v<>>^>v<<>v<>v^v>v<<<>>v>vv>>v<<>v<>v<^>^>^<v>>v>^>^^^<vv>v<<>>><v>^vvv^^>^^<^vv^^^^>v>^v^>v^^v^>>^v>^vv>^^v^<<<<>^<><^<^<<^^>v^^^v<>>vvv<v>>vv><v<v>^<^v>>^v<vv^<<v<vv><^^v^v>v<>^v<<<^^v^^^<^v>v^v^v>><vvv<<>v<>^v>vv^v>vv<<^v<v>^v>v>><^v<v<>v>>>><<<><vv><>^v^<^vvv>v<>><^v>^>><v>vv<><><>v><>>><^>vv>>^<>v^>>^><<<^><<>^v^>>><><>vv>^<>^>^v^^><^>>><<>v^<^vv>^<^vv>><v<>vv<v><><<^><>v<^^<^>vv^^^^vv<<v><>vv<><v>v<>>>>^><v><>^<><>v<>><<>^^vvv>^^^<><>>vvv^v>><>vv<vv>^^^v^<<>^^v<><<^^v<>^^>^<^^v>>v^v^^>>v>>>^<<^<>^>^^v>>>><vv<<>^v<<vv><<^^vv><^>vv<>>v<v>v^>v>>v^<vv<<<v><v^>vvv^^>vv^<<v>v^>>v^<>>><><<^^<^v>^>>>v>v>^v<>vv><vv<vvv<<v>v>^v<<<>><<><><>v^>>>v^>v^>>vv^^<v>^<>>><^>v^<>^^><v>v<><<<><v^v<<<v<v^>v^v>^>v<^<>v>v^^>>v>vv^v<>>^^^^<>v^>>>>>>>><v<^<<vvv<^v^>^v<^<<>>><<<^<<^>^>v^<>^<<<>v>><^vv^>^>^>>>^<vv><v^^^<v^<v<><v^vvv<>v<vvv^vv<<<v^<^<^vvvv^<<vv<^v><<>^>^<v^v^<^>v^><>>v^>v^>^>>v<>vv^v<<>^^>>vv<>vv>>^v<^vv>^v>v<v^vvv^<<^><>v^<><vv><>v^^><<<><>^>^v^<>><vv<^>v^v>v<>><v<<^>^<vv<^v>^<<v><^<^^vv^<>><v^>^vv^<>>^^^^v>v><^^^v^<<<>^<^<<>><>>v<<^v^>><><v^>>^vv^v>vv>>>>>>^^<<>v^>v^v>^^>>><vv^^^v>^v>>^^^<>><>v^<<<v<vv^^<v^<<<>v>v^^^<vv<>>^v>^v<^<<><>vv>^^^<^^vv<v<<vv>^^>vv>v<<^>^vv><^><v>^^^^v<<vv>v^<<^^>>^^vvvv^v^>vv>>v^<v>vvv<>>^><>>v^^>>^<>>vvvv^>><v^v<^^<^vv>>v<<^<<^><v^^><v^>v^>><<<v>v>v^>^v<v^vv<^^^v<^<vvvvv<<vvv>><>v<v<v<<^v<><<>vv>><v>><^>>^^v>^>><>vv^><<>>vv<<<^<^^>^<<^>>>><v<^v<<<>>v>vv<^>^v><>>v<v^v<>v^vvvv>v^>>v><<^<v>^^v>>vv^^>v>^v>^v^^>^<^vv<v<<^>vv<<^>>^<<^^>>^<^>v^><^vv>^^v><v^>>><>v^v>^v<^><<<>vv><v>v<><>>v^<>^^>^<>^<<^>>vv^><^<v<^^vvv>>v^>>v^>v>vv><>>v<^>><<<v<<vv><v<v<v>v<v>vv^vvv^vv^>^>v><vv<v^^<>>>>vv^>^<>v<^>^<^v>vv<^<<>>^<^<vv><^^<>^<<v^v^>v<<><v>v>><^v<<^vvv>v>v<<^^<^^>v<vv<v<v^v>^^^>^>vv<v<<^^v^<v<^>^^^vv>v<>>>vv>><><^><><<<vvv<<^^v^<v^<<^>>vv>vv^v^>>><v><<v^v>>v>>vv>^^vvv^>^^>^>^>^v<<^vv^>vvv^^vv><^>^v^>^><>v<^^vv<v><v^<><^<>><v>^^v^v>v^vv<>><^v>^<^v>^<>^v>>>><<vv^^^vv^>>><vv^v>>v><^v^vv><<^v<<>^^<v><^v>vvv<><^^><<^v><>^<^v<^^<^vvvv^^>>>>vv>v>>>v<v^><<<<v>>v^><v>>vv^v<vv<>vv<>vvv>>>><>>><>^v<v^v><vvv<<v^^v^v<>>><>>^vv<<v<><<vv<v^>^^vv><^v^v<v^vvv^v>v^^^vv>^><^vvv<<>^vvv^<v<v^v>>>>^<<<><<<<<^v<^^>>>>^>^<v^^^v<vvv<vv^<>v<<<^<^>>v^<v><<><<^^vvv^>v<>>^^>v>^v>>v<v><v>>>>^<^<^>v^v<vv<>^>><>^<<^vvv^^<>^<vvv<>v^>^^<<^>^vv><vvv>>v^v^>v><v>^<^^<>^>^>>>^^vvv^<<>v^<<>><>v<^<^>v^>^vv><v<^<<<^v>^>>^<^v^<<<<^v^><v^v>v^><<v<><<v^<<^<<v<<v><v><><^^^^>v>^^<v>>v<vvv<<<>><>>^><<><^<>>^^>vv<^><^v^><vvv>>>vvv<<vv^<^^^<^>^<>>^>>^v^<^^v>^<v<<>^^v<^vv^><vvv>>^v><<^<v^<><><>>^>vv<<>^^^v^^<v<>><>>vv>v^>vvv^^v<vv<^<^>>^>>^>>v^<<<v^>v^<^v^vv^><^<^v<<v<<>v>^v^<<<v^vv<v<<>^^<v>>>^<v<^>^^v<v>>>><vv<^^<<>><<v<v>^^v^>>^^>>^v^<^v>v^v^v^v^>v^vv<><>^^<>^><^^^<<<^<v>v<<>^<^^^^^v^<^<<^^>^vv<>v^>><>>^>v>v<>^>v<v^>>><>^<><v>>>^>^>>v^><v<>v><^vv^>v<<v>v<><<vv<<v>^><^<v^>v<<v^v<<><v><>v<v><>^^<v<>><<>v>vv<<v>^v<v>vv><><>vv^<<>^>^<^>>>^v>v<^v^^^vv<>>>^<<^>>><<^^v^>v^<^v>vvv>v^^vv>^^>>v<>^<<>^<><^^v^>><>^>v>>^^^<<^^v<>^^>^<>^>><^>^vvv><^>^<^>^>>vv<^>>^v>>^<>>^^>>>v^<v>>v<<v<^>>v^^vv>v><^v^^><vv^v<^>v<<>v^^<><>^>vvv><^^^>^v^>v>>^vvv<^vv>^^>^>>v<>><<^v<<v^>^><>vv^<<^^vv><v>>^<^><^<v>^v<v>^<<>^v^^>v^>>^^^<^vv>v^>>>vv<<>v>>>^>v^^<v^v^^v^>>v<v<<v>^<<>>vv<<^v>v<<vv<<^<^v<^<><^^>v>>v>v^>><vv<^v<^>^>>v>^><<^<<>^v<v>>><^^<^<<<v^^>^>vv<<>^<>^<v^<<^v>vv>^^^v<^v><v<<<<<vv>vv>^^^^>v>v><<^<<<^vv><^<<<><v>><v^v>v<<v^^<v^>v>^v^v^<^<^vv>vvv<^^v<>v<<<<>v<v^<vvv^^^<<^<^<<>^<<><<<>v<^>^^v<^^v^>vv>vvv>v><v^^<<>>^><^>>v<<vv>v<<^^^v<<^v^^><><<<><<>v>^<<>v<<<^v>><v^v<^v<v^vv>v>><<^<><^v^^v<v>^>^>vvvv<<><<>>^<vv>^^><v<>v>v<v^^>^><>>><^><<><<<^<>v^><vv^^^^>>^v^>v^<>>v>^^><^<^v^<v^>>v>^vvv<>>v<v^v><>^vvvv<v^<<v^<<^^vv>><<<<<<v><<<v<v^v^^<v^^<>v<<<<^v<<><<v^<^><v<vv<v^v^<v^^vv<v^v<<<>^<<>vv<v<^>^<<><vv<<vv<v<^<^<>><^^<<>>>vv>>>>>>^v<v<>>v^v^^<v^<<<<>><<^v^^^<>^<vv>>>><>v^v^vvv^>>v>><v^v<<<^v>>^^<<^^vv><<<^^^<<<v><^^>>>>vvv^v<^>^^>v<^<><vv<v<>v>>>^vv<<^<v>^v^>^>^v>v>v^v^>v<<v>><>><v^^<<^>>>><<^v^<>^v<vv><>vvv^>v>v<v<v^>^<><><>^>>><v<<<v^vv><>^>^^<<v^>>v^^>^<v>><>><>v^v^^v>>>>vv>>^v<<^v^<>^>v^^>^^<<vvvvvvv>^<v^<<^<<>><<<^^^v^^^^v<^<>v<^^<>vv^^v^<>^<<^>>v>v<<<^^^^vvv^<^<><>v<<v^<^<>>><<><<<v<v<v><vv>^^<vv<<vv<<<v<^>^^vv<v<>><<>>>^v<<>^>>>v^>v>^^<>^<vv<><^>v>^>>>><>^^>v^^v>^vv^^v^><<<>>v<>v<vv<vv^v^v<^v^<^^><<<><vv^^>^<^<<>v>>>>^<<v>v<v>vv<^><^<v><<^>v>>v><<v<<^v^<>>^>>>^v^v>v^^vv^>^<^^>>^><^vv^^vv^<>>^^^^<^^><><v<>>^>>^><vv^>^vvv<^<<v^^<<<>^><>>>^^<><v<v<><<v^^^^^<^<^<<>><<>>>>^<<>>>^<^v^>><<^>>>^<<v>^>><>^<v>^<><v>^v^^vv<><^>vv^^v^<^^^v^vvv^>><>>v<<vv<>>^<^vvv<<^^><vvv^^<v<>vv^^<<>><v>><^^vvv<<<^>^<><^>vv^><^<<>vv<<v>>vv>v>v^<vv><vv><<>^^^^v^^^^<v>^<<^><><^^v^>v>^>><^><<>v^<v>>>^vvv>>^<^<>^^v^vv^^v><<vv^<>>>v<<<>v>^<>v<<>v^>^<<><<><v<v<v<>v^>v<><^^>^<^v^^><^>vv>^>vv<v<^v>vv>^^><<>vv^>^v<<^<<^<<>v<v<^<v>v>>^><v^^v^v>>>><v^v^<<<vv<<^^<>>v^v<^v>v>^^^v<v><v^^^vv<>v^v<^<>v><><v^<>>vv>v><>v>^v<><<<<<<v<>>v^vv<<<<v<<v><^<>^>><>^^vv>^<^<<>vv>>vv<vvv>><><v<>><^<v>^><^<<v>><v><v>^<v>><>v^^^^v<v^^v<>^^vv<>v<>v>^vv^><v^<<^<>^<>^^^>v^>>>v><<^>>v<^v<>^^<v<><v^v<v>v<><v<vv><<>v<^<^>v<>v^>v>^^<<<^^vv^<><<<>>v>^^<>v>>>><v<v<^^^v<v<v^><<>v^v<>v>><<<<v^<><^<<^>^<vvv<v^^v>>v^vv^><^v^^<>^^><<v^>>vv>^<v^vv<^^v<>>vvv<^v^>>^<v<v>>^>^^<<^>^>^v><>>^<^^v>^>>^^<><>>>^^>^^vvv>v<^^<>v^v^^<v<<^<v^v^<<>v^v<v<<v<>>><<^^^>>v>^vv>^>^^v<>^^<>v^^<><v<v<vvv^<vv<<>v^><<><v<>vv<<^vvvv><<<v>v>v^>v^<>v^>^<v<vvv^>^<>^>^^v<>><<<><v<^^>^v<v>^^v^v<<<^v^<>^<>v>^^>v<v<v>v>^^<<<><<^>v<v<^vv^v><^^<<vv>^<<v><>^>>>>><v^v<<<^>^v^v<<v<>vvv<<>v>v>>^v^v^>><<<<>v^<v<><<>>>^>>^>><<v>";
    }



    public class Day03 : IDay
    {
        public int Day => 3;
        public int Year => 2015;

        public void ProblemOne()
        {
        }

        public void ProblemTwo()
        {
        }
    }
}