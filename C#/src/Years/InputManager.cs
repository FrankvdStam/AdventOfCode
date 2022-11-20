using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Newtonsoft.Json;

namespace Years
{
    public class InputManager
    {
        public InputManager()
        {
            if(File.Exists(_inputsFilePath))
            {
                _inputs = JsonConvert.DeserializeObject<List<(int Year, int Day, string input)>>(File.ReadAllText(_inputsFilePath));
            }
        }

        private readonly string _inputsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\AdventOfCodeInputs.json";
        private List<(int Year, int Day, string input)> _inputs = new List<(int Year, int Day, string input)>();

        private void DownloadInputs(string session)
        {
            _inputs.Clear();

            var baseAddress = new Uri("https://adventofcode.com/");
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(baseAddress, new Cookie("session", session));
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                for(int year = 2015; year <= 2021; year++)
                {
                    for(int day = 1; day <= 25; day++)
                    {
                        var result = client.GetAsync($"{year}/day/{day}/input").Result;
                        result.EnsureSuccessStatusCode();
                        var str = result.Content.ReadAsStringAsync().Result;

                        _inputs.Add((year, day, str));
                        Console.WriteLine($"{year} {day}\n{str}");
                        Console.Out.Flush();
                        Thread.Sleep(1000);
                        Console.Clear();
                    }
                }
            }

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(_inputs);
            File.WriteAllText(_inputsFilePath, json);
        }


        public string GetInput(int year, int day)
        {
            return _inputs.FirstOrDefault(i => i.Year == year && i.Day == day).input;
        }
    }
}
