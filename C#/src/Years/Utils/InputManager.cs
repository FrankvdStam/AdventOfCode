using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading;
using System.IO;
using Newtonsoft.Json;

namespace Years.Utils
{
    public class InputManager
    {
        private static InputManager _instace;
        public static InputManager Instance
        {
            get
            {
                if (_instace == null)
                {
                    _instace = new InputManager();
                }
                return _instace;
            }
        }

        public InputManager()
        {
            if (File.Exists(_inputsFilePath))
            {
                _inputs = JsonConvert.DeserializeObject<List<(int Year, int Day, string input)>>(File.ReadAllText(_inputsFilePath));
            }

            if (File.Exists(_sessionFilePath))
            {
                _session = JsonConvert.DeserializeObject<Session>(File.ReadAllText(_sessionFilePath)).Value;
            }
        }

        private readonly string _sessionFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\AdventOfCodeSession.json";
        private readonly string _inputsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\AdventOfCodeInputs.json";

        private List<(int Year, int Day, string input)> _inputs = new List<(int Year, int Day, string input)>();
        private string _session = null;

        public string GetInput(int year, int day)
        {
            return _inputs.FirstOrDefault(i => i.Year == year && i.Day == day).input;
        }

        public void DownloadAllInputs()
        {
            _inputs.Clear();
            using (var client = SetupClient())
            {
                for (int year = 2015; year <= 2021; year++)
                {
                    for (int day = 1; day <= 25; day++)
                    {
                        var str = DownloadDayInput(client, year, day);
                        _inputs.Add((year, day, str));
                        Console.WriteLine($"{year} {day}\n{str}");
                        Console.Out.Flush();
                        Thread.Sleep(1000);
                        Console.Clear();
                    }
                }
            }
            SaveToFile();
        }

        public void AppendInput(int year, int day)
        {
            using (var client = SetupClient())
            {
                var str = DownloadDayInput(client, year, day);
                Console.WriteLine($"{year} {day}\n{str}");

                if(_inputs.Any(i => i.Year == year && i.Day == day))
                {
                    var input = _inputs.FirstOrDefault(i => i.Year == year && i.Day == day);
                    input.input = str;
                }
                else
                {
                    _inputs.Add((year, day, str));
                }
                SaveToFile();
            }
        }

        private void SaveToFile()
        {
            var json = JsonConvert.SerializeObject(_inputs, Formatting.Indented);
            File.WriteAllText(_inputsFilePath, json);
        }

        private HttpClient SetupClient()
        {
            if (string.IsNullOrWhiteSpace(_session))
            {
                throw new Exception($"Session not set. Make sure it is defined at {_sessionFilePath}");
            }

            var baseAddress = new Uri("https://adventofcode.com/");
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(baseAddress, new Cookie("session", _session));
            var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            return new HttpClient(handler) { BaseAddress = baseAddress };
        }

        private string DownloadDayInput(HttpClient client, int year, int day)
        {
            var result = client.GetAsync($"{year}/day/{day}/input").Result;
            result.EnsureSuccessStatusCode();
            return result.Content.ReadAsStringAsync().Result;
        }
    }

    public class Session
    {
        public string Value;
    }
}
