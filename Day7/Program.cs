using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            ProblemOne();
        }

        static void ProblemOne()
        {
            List<Step> steps = ParseInput(example);
            var firstSteps = FindFirstSteps(steps);
            Step nextStep = SelectNextStep(firstSteps);
            string order = "";
            while (nextStep != null)
            {
                order += nextStep.StepName;
                nextStep.Done = true;
                
                nextStep = SelectNextStep(firstSteps);
            }
        }
        
        static Step SelectNextStep(List<Step> steps)
        {
            return FindPosibleSteps(steps).OrderBy(i => i.StepName).FirstOrDefault();

        }

        static List<Step> FindPosibleSteps(List<Step> steps)
        {
            List<Step> result = new List<Step>();
            foreach (Step s in steps)
            {
                if (!s.Done)
                {
                    result.Add(s);
                }
                else
                {
                    result.AddRange(FindPosibleSteps(s.CompleteFirstSteps));
                }
            }

            return result;
        }

        static List<Step> ParseInput(string input)
        {
            Dictionary<string, Step> steps = new Dictionary<string, Step>();
            //for (int i = 0; i < 26; i++)
            //{
            //    string s = ((char)(65 + i)).ToString();
            //    steps[s] = new Step(){StepName = s};
            //}

            var split = input.Split(new string[]{"\r\n"}, StringSplitOptions.None).ToList();
            List<(string mustFinish, string canStart)> list = new List<(string mustFinish, string canStart)>();
            foreach (string s in split)
            {
                string mustFinish = s[5].ToString();
                string canStart = s[36].ToString();
                list.Add((mustFinish, canStart));

                if (!steps.ContainsKey(mustFinish))
                {
                    steps[mustFinish] = new Step(){StepName = mustFinish };
                }

                if (!steps.ContainsKey(canStart))
                {
                    steps[canStart] = new Step() { StepName = canStart };
                }
            }

            foreach (var s in list)
            {
                steps[s.canStart].CompleteFirstSteps.Add(steps[s.mustFinish]);
            }

            List<Step> result = new List<Step>();
            foreach (var s in steps)
            {
                result.Add(s.Value);
            }

            return result;
        }


        static List<Step> FindFirstSteps(List<Step> steps)
        {
            List<Step> result = new List<Step>();
            foreach (Step s in steps)
            {
                if (!IsContainedInOtherStep(s, steps))
                {
                    result.Add(s);
                }
            }
            return result;
        }

        static bool IsContainedInOtherStep(Step step, List<Step> steps)
        {
            foreach (Step s in steps)
            {
                if (s.CompleteFirstSteps.Contains(step))
                {
                    return true;
                }
            }
            return false;
        }

        public class Step
        {
            public bool Done = false;
            public string StepName;
            public List<Step> CompleteFirstSteps = new List<Step>();

            public override string ToString()
            {
                return $"{StepName} {CompleteFirstSteps.Count}";
            }
        }


        private static string example = @"Step C must be finished before step A can begin.
Step C must be finished before step F can begin.
Step A must be finished before step B can begin.
Step A must be finished before step D can begin.
Step B must be finished before step E can begin.
Step D must be finished before step E can begin.
Step F must be finished before step E can begin.";

        private static string input = @"Step P must be finished before step O can begin.
Step H must be finished before step X can begin.
Step M must be finished before step Q can begin.
Step E must be finished before step U can begin.
Step G must be finished before step O can begin.
Step W must be finished before step F can begin.
Step O must be finished before step F can begin.
Step B must be finished before step X can begin.
Step F must be finished before step C can begin.
Step A must be finished before step L can begin.
Step C must be finished before step D can begin.
Step D must be finished before step Y can begin.
Step V must be finished before step R can begin.
Step I must be finished before step Y can begin.
Step X must be finished before step K can begin.
Step T must be finished before step S can begin.
Step Y must be finished before step J can begin.
Step Z must be finished before step R can begin.
Step R must be finished before step K can begin.
Step K must be finished before step N can begin.
Step U must be finished before step N can begin.
Step Q must be finished before step N can begin.
Step N must be finished before step J can begin.
Step S must be finished before step J can begin.
Step L must be finished before step J can begin.
Step A must be finished before step C can begin.
Step S must be finished before step L can begin.
Step X must be finished before step S can begin.
Step T must be finished before step J can begin.
Step B must be finished before step C can begin.
Step G must be finished before step N can begin.
Step M must be finished before step O can begin.
Step Y must be finished before step K can begin.
Step B must be finished before step Y can begin.
Step Y must be finished before step U can begin.
Step F must be finished before step J can begin.
Step A must be finished before step N can begin.
Step W must be finished before step Y can begin.
Step C must be finished before step R can begin.
Step Q must be finished before step J can begin.
Step O must be finished before step L can begin.
Step Q must be finished before step S can begin.
Step H must be finished before step E can begin.
Step N must be finished before step S can begin.
Step A must be finished before step T can begin.
Step C must be finished before step K can begin.
Step Z must be finished before step J can begin.
Step U must be finished before step Q can begin.
Step B must be finished before step F can begin.
Step W must be finished before step X can begin.
Step H must be finished before step Q can begin.
Step B must be finished before step V can begin.
Step Z must be finished before step U can begin.
Step O must be finished before step A can begin.
Step C must be finished before step I can begin.
Step I must be finished before step T can begin.
Step E must be finished before step D can begin.
Step V must be finished before step S can begin.
Step F must be finished before step V can begin.
Step C must be finished before step S can begin.
Step I must be finished before step U can begin.
Step F must be finished before step Z can begin.
Step A must be finished before step X can begin.
Step C must be finished before step N can begin.
Step G must be finished before step F can begin.
Step O must be finished before step R can begin.
Step V must be finished before step X can begin.
Step E must be finished before step A can begin.
Step K must be finished before step Q can begin.
Step Z must be finished before step K can begin.
Step T must be finished before step K can begin.
Step Y must be finished before step Z can begin.
Step W must be finished before step B can begin.
Step E must be finished before step V can begin.
Step W must be finished before step J can begin.
Step I must be finished before step S can begin.
Step H must be finished before step L can begin.
Step G must be finished before step I can begin.
Step X must be finished before step L can begin.
Step H must be finished before step G can begin.
Step H must be finished before step Z can begin.
Step H must be finished before step N can begin.
Step D must be finished before step I can begin.
Step E must be finished before step J can begin.
Step X must be finished before step R can begin.
Step O must be finished before step J can begin.
Step N must be finished before step L can begin.
Step X must be finished before step N can begin.
Step V must be finished before step Q can begin.
Step P must be finished before step Y can begin.
Step H must be finished before step U can begin.
Step X must be finished before step Z can begin.
Step G must be finished before step Q can begin.
Step B must be finished before step Q can begin.
Step Y must be finished before step L can begin.
Step U must be finished before step J can begin.
Step W must be finished before step V can begin.
Step G must be finished before step C can begin.
Step G must be finished before step B can begin.
Step O must be finished before step B can begin.
Step R must be finished before step N can begin.";
    }
}
