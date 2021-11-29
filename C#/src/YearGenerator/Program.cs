using System;
using System.IO;
using System.Threading;

namespace YearGenerator
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = @"C:\projects\AdventOfCode\C#\src\Years";
            int year = 2021;
            
            Console.WriteLine($"Generating 25 days for year {year} in {path}, press any key to continue.");
            Thread.Sleep(1000);
            Console.ReadKey();

            Generate(path, year);
        }



        static void Generate(string path, int year)
        {
            path += "\\Year" + year.ToString();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            for (int i = 1; i <= 25; i++)
            {
                string filename = "Day" + i.ToString().PadLeft(2, '0') + ".cs";
                string content = GenerateClass(year, i);


                Console.WriteLine(filename);
                Console.WriteLine(GenerateClass(year, i));

                File.WriteAllText(path + "\\" + filename, content);
            }
        }






        static string GenerateClass(int year, int day)
        {
            return $@"using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year{year}
{{
    public class Day{day.ToString().PadLeft(2, '0')} : IDay
    {{
        public int Day => {day};
        public int Year => {year};

        public void ProblemOne()
        {{
        }}

        public void ProblemTwo()
        {{
        }}
    }}
}}"; ;
        }
    }
}
