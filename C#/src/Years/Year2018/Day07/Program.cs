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
            //ProblemOne();
            ProblemTwo();
        }

        static void ProblemOne()
        {
            var steps = ParseInput(input);
            string order = "";
            //CABDFE
            
            Step nextStep = FindNextStep(steps);
            while(nextStep != null)
            {
                order += nextStep.StepName;
                RemoveStepFromDictionary(nextStep, steps);
                nextStep = FindNextStep(steps);
            }
        }

        static void ProblemTwo(){
            var steps = ParseInput(input);

            int second = 0;
            string order = "";
            var workers = new (int duration, Step step)[numberOfWorkers];
            while(steps.Any()){
                for(int i = 0; i < workers.Count(); i++){
                    if (workers[i].duration > 0) {
                        workers[i].duration--;
                        if (workers[i].duration == 0 && workers[i].step != null) {
                            order += workers[i].step.StepName;
                            RemoveStepFromDictionary(workers[i].step, steps);
                            workers[i].step = null;
                        }
                    }
                }

                for(int i = 0; i < workers.Count(); i++){
                    if (workers[i].duration == 0) {
                        Step nextStep = FindNextStep(steps);
                        if (nextStep != null) {
                            nextStep.InProgress = true;
                            workers[i].duration = GetStepDuration(nextStep.StepName);
                            workers[i].step = nextStep;
                        }
                    }
                }
                second++;
            }
            second -= 1;
        }
        static int numberOfWorkers = 5;
        static int baseStepDuration = 60;
        static int GetStepDuration(string stepName)
        {
            return baseStepDuration + (stepName[0] - 'A') + 1;
        }
        
        static void RemoveStepFromDictionary(Step step, Dictionary<string, Step> steps)
        {
            steps.Remove(step.StepName);
            foreach(Step s in steps.Values){
                s.DependsOn.Remove(step);
            }
        }

        static Step FindNextStep(Dictionary<string, Step> steps)
        {
            return steps.Values.Where(i => !i.DependsOn.Any() && !i.InProgress).OrderBy(i => i.StepName).FirstOrDefault();
        }
        
        //input: Step C must be finished before step A can begin.
        static Dictionary<string, Step> ParseInput(string input)
        {            
            Dictionary<string, Step> steps = new Dictionary<string, Step>();
            var split = input.Split(new string[]{"\r\n"}, StringSplitOptions.None).ToList();
            List<(string mustFinish, string canStart)> list = new List<(string mustFinish, string canStart)>();

            foreach (string s in split)
            {
                string mustFinish = s[5].ToString();
                string canStart = s[36].ToString();

                if(!steps.ContainsKey(canStart)){
                    steps[canStart] = new Step() { StepName = canStart };
                }

                if (!steps.ContainsKey(mustFinish))
                {
                    steps[mustFinish] = new Step() { StepName = mustFinish };
                }

                steps[canStart].DependsOn.Add(steps[mustFinish]);
            }
            
            return steps;
        }

        public class Step
        {
            public bool InProgress = false;
            public string StepName;
            public List<Step> DependsOn = new List<Step>();

            public override string ToString()
            {
                return $"{StepName} {DependsOn.Count}";
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
