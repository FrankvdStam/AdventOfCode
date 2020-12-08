using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Years.Utils;

namespace Years.Year2015
{
    public class Day19 : IDay
    {
        //From reddit user _Le1_


        public int Day => 19;
        public int Year => 2015;



        List<string> _instKey = new List<string>();
        List<string> _instVal = new List<string>();
        List<string> _result = new List<string>();


        List<string> _replacements = new List<string>();
        private string _molecule = "";

        public void ProblemOne()
        {
            var split = Input.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
            
            //Step 1: separate the replacements and input chemical
            for (int i = 0; i < split.Count() - 2; i++)
            {
                _replacements.Add(split[i]);
            }
            _molecule = split[split.Count - 1];

            

            foreach (string s in _replacements)
                ParseLines(s);

            for (int i = 0; i < _instKey.Count; i++)
            {
                string key = _instKey[i];
                string val = _instVal[i];

                ChangeString(key, val);
            }

            int count = _result.Distinct().ToList().Count();
            Console.WriteLine(count);

        }

        public void ProblemTwo()
        {
            int cnt = 0;
            while (!_molecule.Equals("e"))
            {
                for (int i = 0; i < _instKey.Count; i++)
                {
                    string key = _instKey[i];
                    string val = _instVal[i];

                    if (_replacements.Contains(val))
                    {
                        Regex regex = new Regex(val);
                        _molecule = regex.Replace(_molecule, key, 1);
                        cnt++;
                    }

                }
            }
            Console.WriteLine(cnt);
        }

        
       
            
        private void ChangeString(string key, string val)
        {
            foreach (Match m in Regex.Matches(_molecule, key, RegexOptions.Compiled))
            {
                string s = _molecule.Remove(m.Index, key.Length).Insert(m.Index, val);
                _result.Add(s);
            }
        }

        private void ParseLines(string s)
        {
            Match match = Regex.Match(s, @"(\w+)\s=\>\s(\w+)");

            _instKey.Add(match.Groups[1].Value);
            _instVal.Add(match.Groups[2].Value);
        }
    






























        /////        private void ParseInput(string input)
        /////        {
        /////            var split = input.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
        /////
        /////            //Step 1: separate the replacements and input chemical
        /////            var replacements = new List<string>();
        /////            for (int i = 0; i < split.Count() - 2; i++)
        /////            {
        /////                replacements.Add(split[i]);
        /////            }
        /////            var molecule = split[split.Count - 1];
        /////
        /////            //Step 2: Find all unique chemicals and assign them a unique number.
        /////            Dictionary<string,  int> chemicals = new Dictionary<string, int>();
        /////            int chemicalNum = 0;
        /////            foreach (var line in replacements)
        /////            {
        /////                var left = line.Substring(0, line.IndexOf(' '));
        /////                var right = line.Substring(line.LastIndexOf(' ')+1);
        /////
        /////                int startIndex = 0;
        /////                int endIndex = 0;
        /////
        /////                var chems = Regex.Split(right, @"(?<!^)(?=[A-Z])").ToList();
        /////                chems.Add(left);
        /////
        /////                foreach (var chem in chems)
        /////                {
        /////                    if(!chemicals.ContainsKey(chem))
        /////                    {
        /////                        chemicals.Add(chem, chemicalNum++);
        /////                    }
        /////                }
        /////            }
        /////
        /////            //We should also add the input molecule to the chemicals
        /////            foreach (var chem in Regex.Split(molecule, @"(?<!^)(?=[A-Z])").ToList())
        /////            {
        /////                if (!chemicals.ContainsKey(chem))
        /////                {
        /////                    chemicals.Add(chem, chemicalNum++);
        /////                }
        /////            }
        /////
        /////
        /////            //Now that we have a complete model of chems mapped to integers, we can replace them all.
        /////            foreach (var line in replacements)
        /////            {
        /////                var left = line.Substring(0, line.IndexOf(' '));
        /////                var right = line.Substring(line.LastIndexOf(' ') + 1);
        /////
        /////                int startIndex = 0;
        /////                int endIndex = 0;
        /////
        /////                var chems = Regex.Split(right, @"(?<!^)(?=[A-Z])").ToList();
        /////                chems.Add(left);
        /////
        /////                foreach (var chem in chems)
        /////                {
        /////                    if (!chemicals.ContainsKey(chem))
        /////                    {
        /////                        chemicals.Add(chem, chemicalNum++);
        /////                    }
        /////                }
        /////            }
        /////        }


        private const string Example = @"e => H
e => O
H => HO
H => OH
O => HH

HOH";


        private const string Input = @"Al => ThF
Al => ThRnFAr
B => BCa
B => TiB
B => TiRnFAr
Ca => CaCa
Ca => PB
Ca => PRnFAr
Ca => SiRnFYFAr
Ca => SiRnMgAr
Ca => SiTh
F => CaF
F => PMg
F => SiAl
H => CRnAlAr
H => CRnFYFYFAr
H => CRnFYMgAr
H => CRnMgYFAr
H => HCa
H => NRnFYFAr
H => NRnMgAr
H => NTh
H => OB
H => ORnFAr
Mg => BF
Mg => TiMg
N => CRnFAr
N => HSi
O => CRnFYFAr
O => CRnMgAr
O => HP
O => NRnFAr
O => OTi
P => CaP
P => PTi
P => SiRnFAr
Si => CaSi
Th => ThCa
Ti => BP
Ti => TiTi
e => HF
e => NAl
e => OMg

CRnCaSiRnBSiRnFArTiBPTiTiBFArPBCaSiThSiRnTiBPBPMgArCaSiRnTiMgArCaSiThCaSiRnFArRnSiRnFArTiTiBFArCaCaSiRnSiThCaCaSiRnMgArFYSiRnFYCaFArSiThCaSiThPBPTiMgArCaPRnSiAlArPBCaCaSiRnFYSiThCaRnFArArCaCaSiRnPBSiRnFArMgYCaCaCaCaSiThCaCaSiAlArCaCaSiRnPBSiAlArBCaCaCaCaSiThCaPBSiThPBPBCaSiRnFYFArSiThCaSiRnFArBCaCaSiRnFYFArSiThCaPBSiThCaSiRnPMgArRnFArPTiBCaPRnFArCaCaCaCaSiRnCaCaSiRnFYFArFArBCaSiThFArThSiThSiRnTiRnPMgArFArCaSiThCaPBCaSiRnBFArCaCaPRnCaCaPMgArSiRnFYFArCaSiThRnPBPMgAr";
    }
}