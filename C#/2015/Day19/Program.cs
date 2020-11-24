using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day19
{
    public class Branch
    {
        public Branch Parent;

        public int Steps;

        public Branch Copy()
        {
            Branch b = new Branch();
            b.Molecules = new List<string>(Molecules);
            b.Steps = Steps;
            return b;
        }

        public List<string> Molecules = new List<string>();

        public void ReplaceAt(int index, List<string> replacement)
        {
            var start = Molecules.Take(index).ToList();
            var end = Molecules.Skip(index + 1).ToList();
            Molecules.Clear();
            Molecules.AddRange(start);
            Molecules.AddRange(replacement);
            Molecules.AddRange(end);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (string s in Molecules)
            {
                builder.Append(s);
            }
            return builder.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var replacements = ParseReplacements(InputReplacements);
           var result =  Reddit.Search(Input, replacements);
        }
        
        
        static List<string[]> ParseReplacements(string input)
        {
            var replacements = new List<string[]>();

            var lines = input.Split(new string[] {"\r\n"}, StringSplitOptions.None);
            foreach (var line in lines)
            {
                var arr = new string[2];
                var bits = line.Split(' ');
                arr[0] = bits[0];
                arr[1] = bits[2];
                replacements.Add(arr);
            }
            return replacements;
        }

        private static string Input = @"CRnCaSiRnBSiRnFArTiBPTiTiBFArPBCaSiThSiRnTiBPBPMgArCaSiRnTiMgArCaSiThCaSiRnFArRnSiRnFArTiTiBFArCaCaSiRnSiThCaCaSiRnMgArFYSiRnFYCaFArSiThCaSiThPBPTiMgArCaPRnSiAlArPBCaCaSiRnFYSiThCaRnFArArCaCaSiRnPBSiRnFArMgYCaCaCaCaSiThCaCaSiAlArCaCaSiRnPBSiAlArBCaCaCaCaSiThCaPBSiThPBPBCaSiRnFYFArSiThCaSiRnFArBCaCaSiRnFYFArSiThCaPBSiThCaSiRnPMgArRnFArPTiBCaPRnFArCaCaCaCaSiRnCaCaSiRnFYFArFArBCaSiThFArThSiThSiRnTiRnPMgArFArCaSiThCaPBCaSiRnBFArCaCaPRnCaCaPMgArSiRnFYFArCaSiThRnPBPMgAr";

        private static string InputReplacements = @"Al => ThF
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
e => OMg";


        private static string Example = "HOH";
        private static string ExampleReplacements = @"H => HO
H => OH
O => HH";

        private static string Custom = "HOH";
        private static string CustomReplacements = @"e => A
e => AB
e => C
A => B
A => D
C => A
C => BD";
    }
}
