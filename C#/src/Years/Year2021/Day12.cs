using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2021
{
    public class Day12 : IDay
    {
        public int Day => 12;
        public int Year => 2021;

        public void ProblemOne()
        {
            var map = ParseInput(Input);
            var result = CountPart1(map);
            Console.WriteLine(result);
        }

        public void ProblemTwo()
        {
            var map = ParseInput(Input);
            var result = CountPart2(map);
            Console.WriteLine(result);
        }

        private int CountPart1(Dictionary<string, List<string>> map)
        {
            var count = 0;
            //var paths = new List<List<string>>();
            var stack = new Stack<List<string>>();
            stack.Push(new List<string>() { "start" });

            while (stack.Any())
            {
                var path = stack.Pop();
                var currentPosition = path.Last();
                
                foreach (var potentialNextStep in map[currentPosition])
                {
                    if (potentialNextStep == "end")
                    {
                        //Add finished path
                        //path.Add(potentialNextStep);
                        //paths.Add(path);
                        count++;
                        continue;
                    }

                    //Large cave
                    if (potentialNextStep.IsAllCaps())
                    {
                        //We can visit big caves more than once, so we can add it without questioning
                        var newPath = path.Clone();
                        newPath.Add(potentialNextStep);
                        stack.Push(newPath);
                    }
                    //small cave
                    else
                    {
                        //Only take this step if we've not already taken it before
                        if (!path.Contains(potentialNextStep))
                        {
                            var newPath = path.Clone();
                            newPath.Add(potentialNextStep);
                            stack.Push(newPath);
                        }
                    }
                }
            }
            return count;
        }


        private int CountPart2(Dictionary<string, List<string>> map)
        {
            var count = 0;
            var paths = new List<List<string>>();
            var stack = new Stack<List<string>>();
            stack.Push(new List<string>() { "start" });

            while (stack.Any())
            {
                var path = stack.Pop();
                var currentPosition = path.Last();


                
                foreach (var potentialNextStep in map[currentPosition])
                {
                    if (potentialNextStep != "start")
                    {
                        if (potentialNextStep == "end")
                        {
                            //Add finished path
                            //var newPath = path.Clone();
                            //newPath.Add(potentialNextStep);
                            //paths.Add(newPath);
                            count++;
                            continue;
                        }


                        //Large cave
                        if (potentialNextStep.IsAllCaps())
                        {
                            //We can visit big caves more than once, so we can add it without questioning
                            var newPath = path.Clone();
                            newPath.Add(potentialNextStep);
                            stack.Push(newPath);
                        }
                        //small cave
                        else
                        {
                            var smallCaves = path.Where(i => !i.IsAllCaps()).ToList();

                            if (!path.Contains(potentialNextStep) || /*True if no duplicates exist yet*/ smallCaves.Count == smallCaves.Distinct().Count())
                            {
                                var newPath = path.Clone();
                                newPath.Add(potentialNextStep);
                                stack.Push(newPath);
                            }
                        }
                    }
                }
            }
            return count;
        }

        private Dictionary<string, List<string>> ParseInput(string input)
        {
            var result = new Dictionary<string, List<string>>();
            var lines = input.SplitNewLine();
            foreach(var line in lines)
            {
                var split = line.Split('-');
                var first = split[0];
                var second = split[1];

                //Make sure both keys are initialized
                if (!result.ContainsKey(first))
                {
                    result[first] = new List<string>();
                }
                if (!result.ContainsKey(second))
                {
                    result[second] = new List<string>();
                }

                //Add relationship in both directions
                result[first].Add(second);
                result[second].Add(first);
            }
            return result;
        }

        private const string Example1 = @"start-A
start-b
A-c
A-b
b-d
A-end
b-end";

        private const string Example2 = @"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc";

        private const string Example3 = @"fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW";

        private const string Input = @"RT-start
bp-sq
em-bp
end-em
to-MW
to-VK
RT-bp
start-MW
to-hr
sq-AR
RT-hr
bp-to
hr-VK
st-VK
sq-end
MW-sq
to-RT
em-er
bp-hr
MW-em
st-bp
to-start
em-st
st-end
VK-sq
hr-st";
    }
}