﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    public class Node
    {
        public Node Up;
        public Node Down;
        public Node Left;
        public Node Right;


    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(160, 71);


            //Map m = new Map(ExampleInput);
            Map m = new Map(ExampleInput);
            Console.WriteLine(m);
            m.Tick();

            while (true)
            {
                Console.ReadKey();
                m.Tick();
                Console.WriteLine(m);
            }


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine(m.ToString());

            if (ExampleInput == m.ToString())
            {

            }
            //ProblemOne(ExampleInput);
            //ProblemOne(ActualInput);
        }

        static void ProblemOne(string input)
        {
        }

        public Node[,] ParseTrack(string input, out int width, out int height)
        {
            var split = input.Split(' ').ToList();

            //This can be done more memory efficient
            width = split[0].Length;
            height = split.Count;
            Node[,] nodes = new Node[width,height];

            //Convert input string to multidim char array
            char[,] chars = new char[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    chars[x, y] = split[y][x];
                }
            }


            /*             
            /->-\        
            |   |  /----\
            | /-+--+-\  |
            | | |  | v  |
            \-+-/  \-+--/
              \------/   

            If there is a non-whitespace char, there are connections. The connections are based on what char it is but aren't always consistent
            For instance, / can be a corner right-up or a corner left-down. Instead of using the chars to decide where the connections go, we should always use 
            the surrounding data (eg a char or not a char) to make these connections.

            That means we are no longer dealing with chars but with a binary array - either there is or isn't a char and that's all we need for the connections.

            Later, we will look back at the chars to find the positions.
            
            This is a possible screnario:
            /->-\        
            |   |  /----\
            |  /+--+-\  |
            |  ||  | v  |
            \--+/  \-+--/
               \-----/   
            
            On line 3, there are 2 lines right next to each other. If we just add all adjacent chars as connections, we will make a sideways T connection on both sides
            
            |  |
            |--|
            |  |

            This is illegal. A plus always has 4 connections, all other nodes have 2 connections.
            We NEED char data to decide in these types of scenarios wether to make bend connections or to make the strang connections

            ++
            || -> straight connection
            ++

            +--
            |/-  -> bend connection
            ||   -> straight connection            
             */



            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
 
                }
            }

            return nodes;
        }

        //In this example, the location of the first crash is 7,3.
        public static string ExampleInput = 

@"/->-\        
|   |  /----\
| /-+--+-\  |
| | |  | v  |
\-+-/  \-+--/
  \------/   ";

        /*

/->-\        
|   |  /----\
| /-+--+-\  |
| | |  | v  |
\-+-/  \-+--/
  \------/   

         */

        public static string ActualInput =
@"                                    /----------------------------------------------------------------------\                  /------------\          
        /----------\  /-------------+------------------------------------------------\                     |                  |            |          
      /-+----------+--+-------------+--\                                             |  /-----------\      |                  |            |          
      | |          |  |             |  |                     /-----------------------+--+-----------+------+------------------+------------+-------\  
      | |          |  |             |  |                     |          /------------+--+-----------+------+---------------\  |            |       |  
    /-+-+----------+--+-------------+--+---------------------+-\        |     /------+--+-----------+------+---------------+\ |            |       |  
 /--+-+-+----------+--+-------------+--+---\                 | |        |     |      |  |           |      |               || |            |       |  
 |  | | | /--------+--+-------------+--+\  |                 | |        |     |      |  |           |      |               || |  /---------+-----\ |  
 |  | | | |     /--+--+----->-------+--++--+----------------\| |        |     |      |  |           |      |      /--------++-+--+---------+-\   | |  
 |  | | | |     |  |  |             |  ||  | /<-------------++-+-\ /----+-----+------+--+-----------+------+------+--------++-+--+\        | |   | |  
 |  | | | |     |  |  |            /+--++--+-+--------------++-+-+-+----+---->+------+--+-----------+------+------+--------++-+--++-----\  | |   | |  
 |  | | | |     |  |  |            ||  ||  | |              || |/+-+----+-----+------+--+-----------+------+\     |        || |  ||     |  | |   | |  
 |  | | | |     |  | /+------------++--++--+-+--------------++-+++-+-\  |     |      |  |           |      ||     |        || |  ||     |  | |   | |  
 | /+-+-+-+-----+--+-++------------++--++--+-+--------------++-+++-+-+--+-----+--\   |  |           |      ||     |        || |  ||     |  | |   | |  
 | || | | |     |  | ||            ||  ||  | |              ||/+++-+-+--+-----+--+---+--+-----------+------++-----+--------++-+-\||     |  | |   | |  
 \-++-+-+-+-----+--+-++------------++--++--/ |              |||||| | |/-+-----+--+---+--+-----------+------++-----+-----\  || | |||     |  | |   | |  
   || | | |     |  | ||   /--------++--++----+--------------++++++-+-++-+-----+--+---+--+-----------+------++--\  |     |  || | |||     |  | |   | |  
   || | | |     |  | ||   |        ||  ||    |    /---------++++++-+-++-+-----+--+---+--+-----------+------++--+--+-\   |  || | |||     |  | |   | |  
   || | | |     |/-+-++---+--------++--++----+----+\ /------++++++-+-++-+-----+--+---+--+-----\     |      ||  |  | |   |  || | |||     |  | |   | |  
   || |/+-+-----++-+<++---+--------++--++----+\   || |      |||||| | || |     |  |   |  |     |     |      ||  |  | |   |  || | |||     |  | |   | |  
   || ||| |     || | ||   |        ||  ||    ||   || |      |||||| \-++-+-----+--+---+--+-----+-----+------++--+--+-+---+--++-+-++/     |  | |   | |  
   || ||| |     || | ||   |        ||  ||    ||   || |      ||||||   || |     |  |   |  |     |     |     /++--+--+\|   |  || | ||      |  | |   | |  
   || |||/+-----++-+-++---+--------++--++----++---++-+------++++++-\ || |     |  |   |  |     |     |    /+++--+--+++---+->++-+\||      |  | |   | |  
   || |||||     || | ||   | /------++--++----++---++-+------++++++-+-++-+-----+--+---+--+-----+-----+---\||||  |  |||   |  || ||||      |  | |   | |  
   || ||||\-----++-+-++---+-+------++--+/    ||   || |  /---++++++-+-++-+-----+--+---+--+-----+----\|   |||||  |  |||   |  || ||||      |  | |   | |  
   || ||||    /-++-+-++---+-+------++--+-----++---++-+--+---++++++-+-++-+--\  |  |/--+--+-----+----++---+++++--+--+++---+--++-++++------+--+-+---+-+-\
   || ||||    | || | ||   | |      ||  |     ||   || |  |   |\++++-+-++-+--+--+--++--+--+-----+----++---+++++--+--+++---+--++-++++------+--+-+---+-/ |
   || ||||    | || | ||/--+-+------++-\| /---++---++-+--+--\| |||| | || |  |  \--++--+--+-----+----++---+++++--+--+++---+--+/ ||||      |  | |   |   |
   || ||||    | || | |\+--+-+------++-++-+---++---++-+--+--++-++++-+-++-+--+-----++--/  | /---+----++---+++++--+--+++---+--+--++++------+--+-+\  |   |
   || ||||    | || | | |  | |      || || |   ||   || |  |  || |||| | || |  |     ||   /-+-+---+----++---+++++--+--+++---+--+\ ||||      |  | |^  |   |
   || ||||    | || | | |  | |      || || |   ||   || |  |  || ||||/+-++-+--+-----++---+-+-+---+----++---+++++--+\ |||   |  || ||||      |  | ||  |   |
   || ||||    | || | | |  | |      |\-++-+---++---++-+--+--++-++++++-++-+--+-----++---+-+-+---+----++---+++/|  || |||   |  || ||||      |  | ||  |   |
   || ||||    | \+-+-+-+--+-+------+--++-+---++---++-+--+--+/ |||||| || |  | /---++---+-+-+---+----++---+++-+--++-+++---+--++-++++------+\ | ||  |   |
   || ||||    |  | | | |  | |      |  || |   ||   ||/+--+--+--++++++-++-+--+-+---++---+-+-+---+----++---+++-+--++-+++---+--++-++++------++-+-++-\|   |
   |\-++++----+--+-+-+-+--+-+------+--++-+---++---++++--+--+--+/|||| || |  | |   ||   | | |   |    ||   ||| |  || |||   |  || ||||      || | || ||   |
   |  |||\----+--+-+-+-+--+-+------+--++-+---++---++++--+--+--+-+++/ || |  | |   ||   | | |   |    ||   ||\-+--++-+/|   |  || ||||      || | || ||   |
   |  |||    /+--+-+-+-+--+-+------+--++-+---++---++++--+--+--+-+++--++-+--+-+---++---+-+-+---+----++---++\ |  || | |   |  || ||||      || | || ||   |
   |  |||    ||  | | | |  | |  /---+--++-+---++---++++--+--+--+-+++--++-+--+-+---++---+-+-+---+----++---+++-+--++-+-+---+--++-++++---\  || | || ||   |
   |  ||| /--++--+-+-+-+--+-+--+---+--++-+---++---++++--+--+--+-+++--++-+--+-+---++\  | | |   |    ||   ||| |  || | |   |  || ||||   |  || | || ||   |
   |  ||| |  ||  | | | |  | | /+---+--++-+---++---++++--+--+--+-+++--++-+--+-+-\ |||  | | |  /+----++---+++-+--++-+-+---+--++-++++---+\ || | || ||   |
   |  ||| |  ||  | | | |  | | ||   |  ||/+---++---++++--+--+--+-+++--++-+--+-+-+-+++--+-+-+--++----++---+++-+-\|| |/+---+--++-++++---++\|| | || ||   |
   |  ||| |  ||  | | | |  | | ||   |  ||||   ||   ||||  |  |  | |||  |\-+--+-+-+-+++--+-+-+--++----++---+++-+-+++-+++---/  || ||||   ||||| | || ||   |
   |  ||| |  ||  | | | |  | | ||   |  |||\---++---++++--+--/  | |||  |  |  | | | |||  | |/+--++----++---+++-+-+++-+++------++-++++-\ ||||| | || ||   |
  /+--+++-+--++--+-+-+-+--+-+-++---+--+++----++---++++--+-----+-+++--+--+--+-+-+\|||  | |||  ||  /-++---+++-+-+++-+++------++-++++-+-+++++-+-++\||   |
  ||  ||| |  ||  | | | |  | | ||   |  |||    ||   |||| /+-----+-+++--+--+--+-+-+++++--+-+++-\|| /+-++---+++-+-+++-+++------++-++++-+-+++++-+-+++++--\|
  ||  ||\-+--++--+-/ | |  | | ||   |  |||   /++---++++-++-----+-+++--+--+--+-+-+++++--+\||| ||| || ||   ||| | ||| |||      || |||| | ||||| | |||||  ||
  ||  ||  |  || /+---+-+--+-+-++---+--+++---+++---++++-++-----+-+++--+--+--+-+-+++++--+++++-+++-++-++---+++-+-+++-+++-----\|| |||| | ||||| | |||||  ||
  ||  ||  |  || ||   | |  | | ||   |  |||   |||   |||| ||     | |||  |  |  | | |||||  ||||| ||| || ||   ||| | ||| |||     ||| |||| | ||||| | |||||  ||
  |\--++--+--++-++---+-+--+-+-++---+--+++---+++---++++-++-----+-+++--+--+--+-+-++/||  ||||| ||| || ||   ||| | ||| |||     ||| |||| | ||||| | |||||  ||
  |   ||/-+--++-++---+-+--+-+-++---+--+++---+++---++++-++-----+-+++--+--+-\| | || ||  ||||| ||| || ||   |||/+-+++-+++-----+++-++++-+-+++++\| |||||  ||
  |   ||| | /++-++---+-+--+-+-++---+--+++---+++---++++-++-----+-+++--+--+-++-+-++-++--+++++-+++-++-++--\||||| ||| |||     ||| |||| | ||||||| |||||  ||
  |   ||| | ||| |\---+-+--+-+-++---+--+++---+++---+/|| ||     | |||  |  | ||/+-++-++--+++++-+++-++-++--++++++-+++-+++-----+++-++++-+\||||||| |||||  ||
  |   ||| | ||| |  /-+-+--+-+-++---+--+++---+++---+-++-++-----+-+++--+--+-++++-++-++--+++++-+++\|| ||  |||||| ||| |\+-----+++-++++-++++/|||| |||||  ||
  |   ||| | ||| |  | | |  | |/++---+--+++---+++---+-++-++-----+-+++\ |  | |||| || ||  ||||| |||||| ||  |||||| ||| | |     ||| ||||/++++-++++-+++++-\||
  |   ||| | ||v |  | | |  | ||||   |  |||   |||   | || ||     | |||| |  | |||| || ||  ||||| |||||| ||  |||||| ||| | |     ||| ||||||||| |||| ||||| |||
  |   ||| | ||| |  | | | /+-++++---+--+++---+++>--+-++-++-----+-++++\|  | |||| || ||  ||||| |||||| ||  |||||| ||| | |     ||| ||||||||| |||| ||||| |||
  |   ||| | ||| |  | | | || ||||   |  |||   |||   | || ||  /--+-++++++--+-++++-++-++--+++++-++++++-++--++++++-+++-+\|     ||| ||||||||| |||| ||||| |||
  |   ||| | ||| |  | | | || ||||   |  |||   |||   | || ||  |  | ||||||  | |||| || ||  ||||| |||||| ||  |||||| ||| |||     ||| ||||||||| |||| ||||| |||
  |   ||| | ||| |  | | | || ||||   |  |||   |||   | || ||  |  | ||||||  | |||| || ||  ||||| |||||| ||  |||||| ||| |||     ||| ||||||||| |||| ||||| |||
  |   ||| | ||| |  | | | || ||||   |  |||   |||   | || ||  |  | ||||||  | |||| || ||  ||||| |||||| ||  |||||| ||| |||     ||| ||||||||| |||| ||||| |||
  |   ||| | ||| |  |/+-+-++-++++---+--+++---+++---+-++-++--+--+-++++++--+-++++-++-++--+++++-++++++-++--++++++\||| |||     ||| ||||||||| |||| ||||| |||
  |   ||| | |v| |  ||| | || ||||   |  |||   |||   | || ||  |  | ||||||  | |||| || ||  ||||| |||||| ||  |||||||||| |||     ||| ||||||||| |||| ||||| |||
 /+---+++-+-+++-+--+++-+-++\||||   |  |||   |||   | || ||  |  | ||||||  | |||| || ||  ||||| |||||| ||  |||||||||| |||     ||| ||||||||| |||| ||||| |||
 ||   ||| | |||/+--+++-+-+++++++---+--+++---+++---+-++-++--+--+-++++++--+-++++-++-++--+++++-++++++-++\ |||||||||| |||     ||| ||||||||| |||| ||||| |||
 ||   ||| | |||||  ||| | |||||||   |  |||   |||   | || ||  |  | ||||||  | |||| || ||  ||||| |||||| ||| |||||||||| |||     ||| ||||||||| |||| ||||| |||
 ||   ||| |/+++++--+++-+-+++++++---+--+++---+++---+-++-++--+--+-++++++--+-++++-++-++--+++++-++++++-+++-++++++++++-+++-\   ||| ||||||||| |||| ||||| |||
 ||   ||| |||||||  ||| |/+++++++---+--+++--\|||   | || ||  |  | ||||||  | |||| || ||  ||||| |||||| ||| |||||||||| ||| |   ||| ||||||||| |||| |||v| |||
 ||   ||| ||||||\--+++-+++++++++---+--+++--++++---+-++-++--+--+-++++++--+-++++-++-++--+++++-++++++-+++-++++++++++-+++-+---/|| ||||||||| |||| ||||| |||
 ||   ||| ||||||   ||| |||||||||  /+--+++--++++---+-++-++--+--+-++++++--+\|||| || ||  ||||| |||||| ||| |||||||||| \++-+----++-+++++++++-++++-/|||| |||
 ||   ||| ||||||   ||| |||||||||  ||  |||  ||||   | || ||  \--+-++++++--++++++-++-++--+++++-++++++-+++-++++++++++--/| |    || ||||||||| ||||  |||| |||
 ||   ||| ||||||   ||\-+++++++++--++--+++--++++---+-++-++-----+-+++++/  |||||| || ^|  ||||| |||||| ||| ||||||||||   | |    || ||||||||| ||||  |||| |||
 ||/--+++-++++++---++--+++++++++--++--+++--++++---+-++-++-----+-+++++---++++++-++-++-\||||| |||||| ||| ||||||||||   | |    || ||||||||| ||||  |||| |||
 |||  ||| ||||||   ||  |||||||||  ||  |||  ||||   |/++-++-----+-+++++---++++++-++-++-++++++-++++++-+++-++++++++++\  | |    || ||||||||| ||||  |||| |||
 |||  \++-++++++---++--+++++++++--++--+/|  ||||/--++++-++-----+-+++++---++++++-++-++-++++++-++++++\||| |||||||||||  | |    || ||||||||| ||||  |||| |||
 |||   || ||||||  /++--+++++++++--++--+-+--+++++--++++-++-----+-+++++--<++++++-++-++-++++++\|||||||||| |||||||||||  | |    || ||||||||| ||||  |||| |||
 |||   || ||||||  ||| /+++++++++--++--+-+--+++++--++++-++-----+-+++++-\ |||||| || || |||\+++++++++++/| |||||||||||  | |    || ||||||||| ||||  |||| |||
 |||   || ||||||  ||| ||||||||||  ||  | |  ||\++--++++-++-----+-+/||| | |||||| || || ||| ||||||||||| | |||||||||||  | |    || ||||||||| ||||  |||| |||
 |||   \+-++++++--+++-++++++++++--++--+-+--++-/|  \+++-++-----+-+-+++-+-++++++-++-++-+++-+++++++++++-+-+++++++++++--/ |    || ||||||||| ||||  |||| |||
 |||    | ||||||  ||| ||||||||||  ||  | |  ||  |   ||| ||     | | ||| | |||||| || || ||| \++++++++++-+-+++++++++++----+----++-+++++/||| ||||  |||| |||
 |||    | ||||||  ||| ||||||||||  ||  | |  ||  |   ||| ||     | | ||| | |||||\-++-++-+++--++++++++++-+-+++++++++++----+----++-+++++-+++-+/||  |||| |||
 |||    | ||||||  ||| ||||||||||  ||  | |  ||  |   ||| ||     | | ||| |/+++++--++-++-+++--++++++++++-+-+++++++++++----+--\ || ||||| ||| | ||  |||| |||
 |||    | ||||||  ||\-++++++++++--++--+-+--++--+---+++-++-----+-+-+++-+++++++--++-++-+++--++++++++++-+-++++++/||||    |  | || ||||| ||| | ||  |||| |||
 |||    | ||||||  ||  ||||||||||  || /+-+--++--+--\||| ||     | | \++-+++++++--++-++-+++--++++++++++-+-++++++-++/|  /-+--+-++\||||| ||| | ||  |||| |||
 |||    | ||||||  ||  ||||||||||  || || |  |\--+--++++-++-----+-+--++-+++++++--++-++-++/  |||||||||| | |||||| || |  | |  | |||||||| ||| | ||  |||| |||
 |||    | ||||\+--++--++++++++++--++-++-+--+---+--++++-++-----+-+--++-+++++/|  || || ||   |||||||||| | |||||| || |  | |  | |||||||| ||| v ||  |||| |||
 |||    | |||| |  ||  ||||||||||  || || |  |   |  |||| ||   /-+-+--++-+++++-+--++-++-++---++++++++++-+-++++++-++-+--+-+-\| |||||||| ||| | ||  |||| |||
 |||    | |||| |/-++--++++++++++--++-++-+--+---+\ |||| ||   | | | /++-+++++-+--++-++-++--\|||||||||| | |||||| || |  | | || |||||||| ||| | ||  |||| |||
 |||   /+-++++-++-++--++++++++++--++\|| |  |   || |\++-++---+-+-+-+++-+++++-+--++-++-++--+++++++++++-+-++++++-++-/  | | || |||\++++-+++-+-+/  |||v |||
 |||   || |||| || ||  ||||||||||  ||||| |  |   || | || ||   | | \-+++-+++++-+--++-++-++--+++++++++++-+-+++++/ ||    | | || ||| |||| ||| | |   |||| |||
 |\+---++-++++-++-++--++++++++++--+++++-+--+---++-+-++-++---+-+---+++-+++++-+--+/ || ||  ||||||||||| | |||||  ||    | | || ||| |||| ||| | |   |||| |||
 | |   || |||| || ||  ||||||||||  ||||| |  |   || | || \+---+-+---+++-+++++-+--+--++-++--+++/||||||| | |||||  ||    | | || ||| |||| ||| | |   |||| |||
 | |   || |||| || ||  ||||||||||  ||||| |  |   || | ||  |   | |   ||| ||||| |  |  \+-++--+++-+++++++-+-+++++--++----+-+-++-+++-++++-+++-+-+---++++-++/
 | |   || |||| || ||  ||||||||||  ||||| |  |   || | \+--+---+-+---+++-+++++-+--+---+-++--+++-+++++++-+-+++++--++----+-+-++-+++-++++-+++-+-+---++/| || 
 | |   || |||| || ||  ||||\+++++--+++++-+--+---++-+--+--+---+-+---+++-+++++-+--+---+-++--+++-+++++++-+-+++++--+/    | | || ||| |||| ||| | |   || | || 
/+-+---++-++++-++-++--++++-+++++--+++++\|  |   || |  |  \---+-+---+++-+++++-+--+---+-++--+++-++++++/ | |||||  |     | | || ||| |||| ||| | |   || | || 
|| |   || |||| || ||  |||| |||||  |||\+++--+---++-/  |      | |  /+++-+++++-+--+---+\||  ||| ||||||  |/+++++--+-----+-+-++-+++-++++-+++-+\|   || | || 
|| |   || |||| \+-++--++++-+++++--+++-+++--+---++----+------+-+--++++-+++++-+--+---++++--+++-++++++--/|||\++--+-----+-+-++-+++-/|\+-+++-+++---++-/ || 
|| |   |\-++++--+-++--++++-+++++--+++-+++--+---++----+------+-+--++++-++++/ |  |   ||||  ||| ||||||   ||| ||  |     | | || |||  | | ||| |||   ||   || 
|| |   |  ||||  | || /++++-+++++--+++-+++--+---++----+-----\| |  |||| ||||  |  |   ||||  ||| ||||\+---+++-++--+-----+-+-++-+++--+-+-+++-+++---+/   || 
|| |   |  ||||  | || ||||| |||||  ||| |||  |   ||    |     || |  |||| ||||  |  |  /++++--+++-++++-+---+++-++--+-----+-+-++-+++--+-+-+++-+++---+----++\
|| |   |  ||||  | || |||\+-+++++--+++-+++--/   ||    |     |\-+--++++-++++--+--+--+++++--+++-++++-+---+++-++--+-----+-+-/| |||  | | ||| |||   |    |||
|| |   |  ||||  | || ||| | |||||  ||| |||      ||    |     |  |  |||| ||||  |  |  |||||  ||| |||| |   ||| ||  |     | |  | |||  | | ||| |||   |    |||
|| |   |  |||| /+-++-+++-+-+++++--+++-+++------++----+-----+--+--++++-++++--+--+--+++++--+++-++++-+---+++-++--+-----+-+\ | |||  | | ||| |||   |    |||
|| |   |  |||| || \+-+++-+-+++++--+++-+++------++----+-----+--+--++++-++++--+--+--+++++--++/ |||| |   ||| ||  |     | || | |||  | | ||| |||   |    |||
|| |   |  |||| ||  | ||| | |||||  ||| |||   /--++----+-----+--+--++++-++++--+--+--+++++--++--++++-+---+++-++--+-----+-++-+-+++--+\| ||| |||   |    |||
|| |   |  |||| ||  | ||| | |||||  ||| |||   |  ||    |     |  |  |||| ||||  |  |  |||||  ||  |||| |   ||| ||  |     | || | |||  ||| ||| |||   |    |||
|| |   |  |||| ||  | ||| | |||||  |\+-+++---+--++----+-----+--+--++++-++++--+--+--+++++--++--++++-+---+++-++--+-----+-++-+-+++--+++-+++-/||   |    |||
|| |   |  |||| ||  | ||| | |||||  | | |||   |  ||    |     |  \--++++-++++--+--+--+++++--++--++++-+---+++-++--+-----+-++-+-+++--/|| |||  ||   |    |||
|| | /-+--++++-++--+-+++-+-+++++-\| | |||   |  ||    |     |   /-++++-++++--+--+--+++++--++--++++-+---+++-++--+----\| || | |||   || |||  ||   |    |||
|| | | |  ||\+-++--+-+++-+-+++++-++-+-+++---+--++----+-----+---+-++++-++++--+--+--+++++--++--++++-+---+/| ||  |    || || | |||   || |||  ||   |    |||
|| | | |  || | ||  | ||| | ||||| || | ||\---+--++----+-----+---+-++++-++++--+--+--+++++--++--++++-+---+-+-++--/    || || | |||   || |||  ||   |    |||
|| | | |  || | ||  | ||| | ||||| || | ||    |  ||    |     |   | |||| ||||  |  |  |||||  ||  |||| |   | | ||       || || | |||   || |||  ||   |    |||
|| | | |  || | ||  | ||| | ||||| || | ||    |  ||    |     |   | |||| ||\+--+--+--+++++--++--++++-+---+-+-++-------++-++-+-/||   || |||  ||   |    |||
|| | | |  || | ||  | \++-+-+++++-++-+-++----+--++----+-----/   | |||| || |  |  |  |||||/-++--++++-+---+-+-++-------++-++-+--++---++-+++--++--\|    |||
|| | | |  || | ||/-+--++-+-+++++-++-+-++----+--++----+\        | |||| || |  | /+--++++++-++--++++-+---+-+-++-------++-++-+--++---++-+++--++--++--\ |||
|| | | |  || | ||| |  || | ||||| || | ||    |  ||    ||        | |||| || |  | ||  |||||| ||  |||| |   | | ||       || || |  ||   || |||  ||  ||  | |||
|| | | |  || | ||| |  || | ||||| || | ||    |  ||    ||        | \+++-++-+--+-++--++/||| ||  |||| |   | | |\-------++-++-+--++---++-+++--+/  ||  | |||
|| | | |  || | ||| |  || | ||||| || | ||    | /++----++--------+--+++-++-+\ | ||  || ||| ||  |||| | /-+-+-+--------++-++-+--++\  || |||  |   ||  | |||
|| | | |  || | ||| |  || | |||||/++-+-++----+-+++----++-----\  |  ||| || || | ||  || ||| ||  |||\-+-+-+-+-+--------++-++-+--+++--++<+++--+---++--+-+/|
|| | | |  || | ||| |  || | |||||||| | ||    | |||    ||     |  |  ||| || || | ||  || |\+-++--+++--+-+-+-+-+--------++-++-+--/||  || |||  |   ||  | | |
|| | | |  || | ||| |  || \-++++++++-+-++----+-+++----++-----+--+--++/ || || | ||  || | | ||  |||  | | | | |   /----++-++-+---++--++-+++--+---++--+-+\|
|| | | |  || | ||| |  ||   |\++++++-+-++----+-+++----++-----+--+--++--++-++-+-++--++-+-+-++--+++--+-+-+-/ |   |    || || |   ||  || |||  |   ||  | |||
|| | | |  || | ||| |  ||   | |||||| | ||    | |||    ||     |  |  \+--++-++-+-++--++-+-+-/|  |||  | | |   |   |    || || |   ||  || |||  |   ||  | |||
|| | | | /++-+-+++-+--++---+-++++++-+-++----+\|||    ||     |  |   |  || || | \+--++-+-+--+--+++--+-+-+---+---+----++-++-+---++--++-+++--+---++--/ |||
|| | | | ||| | ||| |  ||   | ||\+++-+-++----+++++----++-----+--+---+--++-++-+--+--++-+-+--+--+++--+-+-+---+---+----++-++-+---++--++-+/|  |   ||    |||
|| | | | ||| | ||| |  |\---+-++-+++-+-/|    |||||    \+-----+--+---+--++-++-+--+--++-+-+--+--+/|  | | |   |   |    || || |   ||  |\-+-+--+---++----/||
|| | | | ||| | ||| |  |    | || ||| |  |    |||||     |     |  |   |  || || |/-+--++-+-+--+--+-+--+-+-+---+--\|    || || |   ||  |  | |  |   ||     ||
|| | \-+-+++-+-+++-+--+----+-++-+/| |  |    |||||     |     |  |   |  || || || |  \+-+-+--+--+-+--+-+-+---+--++----++-++-+---++--+--+-+--+---++-----+/
|| |   | ||| | ||| |  |    | || | | |  |    |||||     |     |  |   |  || || || |   | | |  |  | |  | | |   |  |v    || || |   ||  |  | |  |   ||     | 
|| |   | ||| \-+++-+--+----+-++-+-+-+--+----+++++-----+-----+--+---+--++-++-++-+---+-+-+--+--+-+--+-+-+---/  ||    |\-++-+---/|  |  | |  |   ||     | 
|\-+---+-+++---+++-+--+----/ \+-+-+-+--+----+++++-----+-----+--+---/  || || || |   | | |  |  | |  | | |      ||    |  || |    |  |  | |  |   ||     | 
|  |   | |||   ||| |  |       | | | |  |    |||||     |     |  |      |^ || || |   | | |  |  \-+--+-+-+------++----+--++-+----+--+--+-/  |   ||     | 
|  |   | \++---+++-+--+-------+-+-+-+--+----+/|\+-----+-----+--+------++-++-++-+---+-+-+--+----+--/ | |      ||    |  || |    |  |  |    |   ||     | 
|  |   |  ||   ||| |  |       | \-+-+--+----+-+-+-----+-----/  |      |\-++-++-+---+-+-+--+----+----+-+------++----+--++-/    |  |  |    |   ||     | 
|  |   \--++---+++-+--+-------+---+-/  |    | | |     |        |      |  || || |   | | |  \----+----+-+------++----+--++------+--+--+----+---+/     | 
|  |      ||   ||| |  \-------+---+----+----+-+-+-----+--------+------/  || || |   | | |       |    \-+------++----+--++------/  |  |    |   |      | 
|  |      ||   ||\-+----------+---+----+----+-+-+-----/        |         || || |   | | |       |      |      |\----+--++---------+--+----+---+------/ 
|  |      ||   ||  |          |   \----+----+-+-+--------------+---------/| || |   | | |       |      |      |     |  ||         |  |    |   |        
|  \------++---++--+----------+--------+----+-+-+--------------+----------+-++-+---+-/ |       |      |      |     |  ||         |  |    |   |        
|         |\---++--+----------+--------+----+-+-+--------------+----------+-++-+---+---+-------+------+------+-----+--/|         |  |    |   |        
|         |    ||  |          |        |    | | |              |          | || |   |   |       |      |      |     |   |         |  |    |   |        
|         |    ||  |          |        |    | | |              |          | |\-+---+---+-------+------+------/     |   |         |  |    |   |        
\---------+----++--+----------+--------/    \-+-+--------------+----------+-+--+---+---+-------+------+------------+---+---------/  |    |   |        
          |    |\--+----------+---------------+-/              |          | \--+---+---+-------+------+------------+---+------------/    |   |        
          |    |   |          |               \----------------+----------/    |   |   |       |      |            |   |                 |   |        
          \----+---+----------+--------------------------------+---------------+---/   |       |      |            |   |                 |   |        
               |   |          |                                \---------------+-------+-------+------+------------/   |                 |   |        
               |   |          \------------------------------------------------/       \-------+------+----------------+-----------------+---/        
               \---+---------------------------------------------------------------------------+------+----------------/                 |            
                   \---------------------------------------------------------------------------/      \----------------------------------/            ";
    }
}