using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2015
{
    public class Day07 : BaseDay
    {
        public Day07() : base(2015, 07) { }

        private ushort Solve(string input)
        {
            Dictionary<string, Signal> wires = new Dictionary<string, Signal>();
            int constantCount = 0;
            
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            //Give all constants a unique name.
            foreach (var line in lines)
            {

                var split = line.Split(' ');

                if (line.Contains("SHIFT"))
                {
                    Signal s = GetOrCreateSignal(split[4], ref constantCount, ref wires);
                    s.Operation = split[1];
                    s.ShiftNum = int.Parse(split[2]);
                    s.Inputs.Add(
                        GetOrCreateSignal(split[0], ref constantCount, ref wires)
                    );
                    continue;
                }

                if (line.Contains("AND") || line.Contains("OR"))
                {
                    Signal s = GetOrCreateSignal(split[4], ref constantCount, ref wires);
                    s.Operation = split[1];
                    s.Inputs.Add(
                        GetOrCreateSignal(split[0], ref constantCount, ref wires)
                    );
                    s.Inputs.Add(
                        GetOrCreateSignal(split[2], ref constantCount, ref wires)
                    );
                    continue;
                }

                if (line.Contains("NOT"))
                {
                    Signal s = GetOrCreateSignal(split[3], ref constantCount, ref wires);
                    s.Operation = split[0];
                    s.Inputs.Add(
                        GetOrCreateSignal(split[1], ref constantCount, ref wires)
                    );
                    continue;
                }

                //Else: signal gets a value, doesn't have to be constant..
                //Example: "lx -> a"
                Signal sig = GetOrCreateSignal(split[2], ref constantCount, ref wires);
                sig.Operation = "EQUALS";
                sig.Inputs.Add(GetOrCreateSignal(split[0], ref constantCount, ref wires));
            }


            while (wires.Values.Any(i => i.Value == null))
            {
                foreach (var signal in wires.Values)
                {
                    if (signal.Value == null && signal.Inputs.All(i => i.Value != null))
                    {
                        CalculateValue(signal);
                    }
                }
            }

           return wires["a"].Value.Value;
        }

        
        
        public override void ProblemOne()
        {
            _problemOneResult = Solve(Input);
            Console.WriteLine(_problemOneResult);

        }

        private ushort? _problemOneResult = null;



        public override void ProblemTwo()
        {
            //Programmatically repace b in the input
            var lines = Input.Split('\n', StringSplitOptions.RemoveEmptyEntries).ToList();
            var line = lines.First(i => i.EndsWith("-> b"));
            int index = lines.IndexOf(line);
            lines[index] = $"{_problemOneResult} -> b";
            var newInput = string.Join('\n', lines);
            
            var result = Solve(newInput);
            Console.WriteLine(result);
        }



        static void CalculateValue(Signal signal)
        {
            unchecked
            {
                if (signal.Operation == "AND")
                {
                    signal.Value = (ushort)(signal.Inputs[0].Value & signal.Inputs[1].Value);
                    return;
                }

                if (signal.Operation == "OR")
                {
                    signal.Value = (ushort)(signal.Inputs[0].Value | signal.Inputs[1].Value);
                    return;
                }

                if (signal.Operation == "LSHIFT")
                {
                    signal.Value = (ushort)(signal.Inputs[0].Value << signal.ShiftNum);
                    return;
                }

                if (signal.Operation == "RSHIFT")
                {
                    signal.Value = (ushort)(signal.Inputs[0].Value >> signal.ShiftNum);
                    return;
                }

                if (signal.Operation == "NOT")
                {
                    signal.Value = (ushort)(~signal.Inputs[0].Value);
                    return;
                }

                if (signal.Operation == "EQUALS")
                {
                    signal.Value = signal.Inputs[0].Value;
                    return;
                }
            }

            throw new Exception("Huh?");
        }

        
        private Signal GetOrCreateSignal(string name, ref int constantCount, ref Dictionary<string, Signal> wires)
        {
            if (ushort.TryParse(name, out ushort result))
            {
                Signal s = new Signal();
                s.Name = constantCount.ToString();
                s.Value = result;
                s.Constant = true;
                wires[constantCount.ToString()] = s;
                constantCount++;
                return s;
            }

            if (!wires.ContainsKey(name))
            {
                Signal s = new Signal();
                s.Name = name;
                wires[name] = s;
                return s;
            }

            return wires[name];
        }

        public class Signal
        {
            public bool Constant = false;
            public string Name;
            public ushort? Value;
            public List<Signal> Inputs = new List<Signal>();
            public string Operation;
            public int ShiftNum;

            public override string ToString()
            {
                if (Constant)
                {
                    return $"Const {Value}";
                }
                else
                {
                    return $"{Name} {Operation} {Value}";
                }
            }
        }

#pragma warning disable CS0414
        private const string example = @"123 -> x
456 -> y
x AND y -> d
x OR y -> e
x LSHIFT 2 -> f
y RSHIFT 2 -> g
NOT x -> h
NOT y -> i";

    
        
    }
}