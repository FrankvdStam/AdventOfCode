using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2021
{
    public class Day14 : IDay
    {
        public int Day => 14;
        public int Year => 2021;

        public void ProblemOne()
        {
            Console.WriteLine(Run(Input, 10));
        }

        public void ProblemTwo()
        {
            Console.WriteLine(Run(Input, 40));
        }



        private long Run(string input, int maxSteps)
        {
            //NNCB
            //Pairs + count:
            //NN 1
            //NC 1
            //CB 1
            //NN ALWAYS results in NCN. We always subtract 1 NN, we add 1 NC and 1 CN
            //
            //If we have NN 10 -> subtract 10 NN, add 10 NC and 10 CN
            //NCNBCHB

            ParseInput(input, out string initial, out Dictionary<(char first, char second), char> insertions);

            //Memoize all the pairs and their resulting pairs, like NN turns into NC and CN
            var lookupTable = new Dictionary<(char first, char second), ((char first, char second) left, (char first, char second) right)>();
            foreach (var insertion in insertions)
            {
                lookupTable[insertion.Key] = ((insertion.Key.first, insertion.Value), (insertion.Value, insertion.Key.second));
            }

            //Keep track of the pairs we have, and their count
            //Looks like all pairs in my initial state also occur as insert pair - we can assume the keys always exists for this dict after initializing them all to 0
            var pairs = new Dictionary<(char first, char second), long>();
            foreach (var p in lookupTable.Keys)
            {
                pairs[p] = 0;
            }

            for (var i = 0; i < initial.Length - 1; i++)
            {
                pairs[(initial[i], initial[i + 1])]++;
            }


            //Now we're all setup and can run the damn thing.
            for (var i = 0; i < maxSteps; i++)
            {
                //Copy current state
                var newPairs = pairs.ToDictionary(j => j.Key, j => j.Value);
                foreach (var pair in pairs)
                {
                    if (pair.Value > 0)
                    {
                        ((char first, char second) left, (char first, char second) right) = lookupTable[pair.Key];
                        newPairs[left] += pair.Value;
                        newPairs[right] += pair.Value;
                        newPairs[pair.Key] -= pair.Value;
                    }
                }
                pairs = newPairs;
            }

            //Count parts
            var result = new Dictionary<char, long>();
            foreach (var pair in pairs)
            {
                if (!result.ContainsKey(pair.Key.first))
                {
                    result[pair.Key.first] = 0;
                }
                if (!result.ContainsKey(pair.Key.second))
                {
                    result[pair.Key.second] = 0;
                }

                result[pair.Key.first] += pair.Value;
                result[pair.Key.second] += pair.Value;
            }

            //Considering that they are all pairs, they occur twice...
            foreach (var key in result.Keys)
            {
                result[key] /= 2;
            }

            //The first and last character of our inital state will never move - they will always form the first and last character.. 
            //These should actually be counted
            result[initial[0]]++;
            result[initial[initial.Length - 1]]++;


            return result.Values.Max() - result.Values.Min();
        }


        private void ParseInput(string input, out string initial, out Dictionary<(char, char), char> insertions)
        {
            var lines = input.SplitNewLine();
            initial = lines[0];

            insertions = new Dictionary<(char, char), char>();
            for (var i = 2; i < lines.Length; i++)
            {
                var split = lines[i].Split(" -> ");
                insertions[(split[0][0], split[0][1])] = split[1][0];
            }
        }
        
        private const string Example = @"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C";

        private const string Input = @"FSKBVOSKPCPPHVOPVFPC

BV -> O
OS -> P
KP -> P
VK -> S
FS -> C
OK -> P
KC -> S
HV -> F
HC -> K
PF -> N
NK -> F
SC -> V
CO -> K
PO -> F
FB -> P
CN -> K
KF -> N
NH -> S
SF -> P
HP -> P
NP -> F
OV -> O
OP -> P
HH -> C
FP -> P
CS -> O
SK -> O
NS -> F
SN -> S
SP -> H
BH -> B
NO -> O
CB -> N
FO -> N
NC -> C
VF -> N
CK -> C
PC -> H
BP -> B
NF -> O
BB -> C
VN -> K
OH -> K
CH -> F
VB -> N
HO -> P
FH -> K
PK -> H
CC -> B
VH -> B
BF -> N
KS -> V
PV -> B
CP -> N
PB -> S
VP -> V
BO -> B
HS -> H
BS -> F
ON -> B
HB -> K
KH -> B
PP -> H
BN -> C
BC -> F
KV -> K
VO -> P
SO -> V
OF -> O
BK -> S
PH -> V
SV -> F
CV -> H
OB -> N
SS -> H
VV -> B
OO -> V
CF -> H
KB -> F
NV -> B
FV -> V
HK -> P
VS -> P
FF -> P
HN -> N
FN -> F
OC -> K
SH -> V
KO -> C
HF -> B
PN -> N
SB -> F
VC -> B
FK -> S
KK -> N
FC -> F
NN -> P
NB -> V
PS -> S
KN -> S";
    }
}