using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using Microsoft.VisualBasic;
using Years.Utils;

namespace Years.Year2021
{
    public class Day18 : IDay
    {
        public class Pair
        {
            public Pair Clone()
            {
                var p = new Pair()
                {
                    Dept = Dept,
                    LeftValue = LeftValue,
                    RightValue = RightValue,

                };

                p.LeftPair = LeftPair?.Clone();
                p.RightPair = RightPair?.Clone();

                if (p.LeftPair != null)
                {
                    p.LeftPair.Parent = p;
                }

                if (p.RightPair != null)
                {
                    p.RightPair.Parent = p;
                }

                return p;
            }


            public int Dept;

            public int? LeftValue;
            public int? RightValue;

            public Pair LeftPair;
            public Pair RightPair;
            public Pair Parent;

            public bool TryFindNextLeftValue(out Pair pair, out int value)
            {
                pair = null;
                value = 0;


                //Travel all the way up
                var ptr = this;
                while (ptr.Parent != null)
                {
                    ptr = ptr.Parent;
                }

                var leftToRight = ptr.LeftToRightIterator().ToList();
                var index = leftToRight.FindIndex(i => i.containingPair == this);
                for (var i = index - 1; i >= 0; i--)
                {
                    if (leftToRight[i].containingPair != this)
                    {
                        pair = leftToRight[i].containingPair;
                        value = leftToRight[i].value;
                        return true;
                    }
                }
                return false;
            }

            public bool TryFindNextRightValue(out Pair pair, out int value)
            {
                pair = null;
                value = 0;


                //Travel all the way up
                var ptr = this;
                while (ptr.Parent != null)
                {
                    ptr = ptr.Parent;
                }

                var leftToRight = ptr.LeftToRightIterator().ToList();
                var index = leftToRight.FindIndex(i => i.containingPair == this);
                for (var i = index + 1; i < leftToRight.Count; i++)
                {
                    if (leftToRight[i].containingPair != this)
                    {
                        pair = leftToRight[i].containingPair;
                        value = leftToRight[i].value;
                        return true;
                    }
                }
                return false;
            }

            public int Magnitude()
            {
                var left = 0;
                if (LeftValue.HasValue)
                {
                    left = 3 * LeftValue.Value;
                }
                else
                {
                    left = 3 * LeftPair.Magnitude();
                }

                var right = 0;
                if (RightValue.HasValue)
                {
                    right = 2 * RightValue.Value;
                }
                else
                {
                    right = 2 * RightPair.Magnitude();
                }

                return left + right;
            }



            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.Append('[');
                if (LeftValue != null)
                {
                    sb.Append(LeftValue);
                }
                else
                {
                    sb.Append(LeftPair);
                }

                sb.Append(',');

                if (RightValue != null)
                {
                    sb.Append(RightValue);
                }
                else
                {
                    sb.Append(RightPair);
                }

                sb.Append(']');
                return sb.ToString();
            }

            public IEnumerable<(int value, Pair containingPair)> LeftToRightIterator()
            {
                if (LeftValue.HasValue)
                {
                    yield return (LeftValue.Value, this);
                }

                if (LeftPair != null)
                {
                    foreach (var pair in LeftPair.LeftToRightIterator())
                    {
                        yield return pair;
                    }
                }

                if (RightValue.HasValue)
                {
                    yield return (RightValue.Value, this);
                }

                if (RightPair != null)
                {
                    foreach (var pair in RightPair.LeftToRightIterator())
                    {
                        yield return pair;
                    }
                }
            }

            /// <summary>
            /// Returns a flattened list containing each pair once, in left to right order
            /// </summary>
            public List<Pair> FlattenLeftToRight()
            {
                var result = new List<Pair>();
                if (LeftPair != null)
                {
                    result.Add(LeftPair);
                    result.AddRange(LeftPair.FlattenLeftToRight());
                }
                if (RightPair != null)
                {
                    result.Add(RightPair);
                    result.AddRange(RightPair.FlattenLeftToRight());
                }

                return result;
            }
        }


        public int Day => 18;
        public int Year => 2021;

        public void ProblemOne()
        {
            var pairs = ParseInput(Input);
            var currentPair = pairs.First();
            for (int i = 1; i < pairs.Count; i++)
            {
                currentPair = Add(currentPair, pairs[i]);
                ReduceAll(currentPair);
            }

            var result = currentPair.Magnitude();
            Console.WriteLine(result);
        }

        public void ProblemTwo()
        {
            var pairs = ParseInput(Input);
            var max = int.MinValue;

            foreach (var pair in pairs.PermutePairs())
            {
                //Previously we only needed each pair once, and it was safe to modify. This time around, we'll use each pair multiple times.
                //Hence the need to clone them.
                var left = pair.Item1.Clone();
                var right = pair.Item2.Clone();
                
                var addded = Add(left, right);
                ReduceAll(addded);

                var magnitude = addded.Magnitude();
                if (magnitude > max)
                {
                    max = magnitude;
                }

                //Again, the other way around
                left = pair.Item2.Clone();
                right = pair.Item1.Clone();

                addded = Add(left, right);
                ReduceAll(addded);

                magnitude = addded.Magnitude();
                if (magnitude > max)
                {
                    max = magnitude;
                }
            }
            Console.WriteLine(max);
        }

        public static bool TryExplode(Pair pair)
        {
            var flat = pair.FlattenLeftToRight();
            var explodeAble = flat.FirstOrDefault(i => i.Dept == 4);

            if (explodeAble != null)
            {
                if (explodeAble.TryFindNextLeftValue(out Pair leftPair, out int leftValue))
                {
                    var newValue = leftValue + explodeAble.LeftValue;
                    if (leftPair.RightValue == leftValue) leftPair.RightValue = newValue;
                    else if (leftPair.LeftValue == leftValue) leftPair.LeftValue = newValue;
                }

                if (explodeAble.TryFindNextRightValue(out Pair rightPair, out int rightValue))
                {
                    var newValue = rightValue + explodeAble.RightValue;
                    if (rightPair.LeftValue == rightValue) rightPair.LeftValue = newValue; 
                    else if (rightPair.RightValue == rightValue) rightPair.RightValue = newValue;
                }

                if (explodeAble.Parent.LeftPair == explodeAble)
                {
                    explodeAble.Parent.LeftValue = 0;
                    explodeAble.Parent.LeftPair = null;
                }
                
                if (explodeAble.Parent.RightPair == explodeAble)
                {
                    explodeAble.Parent.RightValue = 0;
                    explodeAble.Parent.RightPair = null;
                }
                return true;
            }
            return false;
        }

        public static bool TrySplit(Pair pair)
        {
            var traversable = pair.LeftToRightIterator().ToList();
            var splitAble = traversable.FirstOrDefault(i => i.value >= 10).containingPair;

            if (traversable.Any(i => i.value >= 10))
            {
                if (splitAble.LeftValue >= 10)
                {
                    splitAble.LeftPair = new Pair();
                    splitAble.LeftPair.Dept = splitAble.Dept + 1;
                    splitAble.LeftPair.Parent = splitAble;
                    splitAble.LeftPair.LeftValue = (int)Math.Floor((decimal)splitAble.LeftValue / 2);
                    splitAble.LeftPair.RightValue = (int)Math.Ceiling((decimal)splitAble.LeftValue / 2);
                    splitAble.LeftValue = null;
                }
                else
                {
                    splitAble.RightPair = new Pair();
                    splitAble.RightPair.Dept = splitAble.Dept + 1;
                    splitAble.RightPair.Parent = splitAble;
                    splitAble.RightPair.LeftValue = (int)Math.Floor((decimal)splitAble.RightValue / 2);
                    splitAble.RightPair.RightValue = (int)Math.Ceiling((decimal)splitAble.RightValue / 2);
                    splitAble.RightValue = null;
                }

                return true;
            }


            return false;
        }

        public static bool TryReduce(Pair pair)
        {
            bool change = TryExplode(pair);
            if (!change)
            {
                if (TrySplit(pair))
                {
                    change = true;
                }
            }
            return change;
        }

        public static void ReduceAll(Pair pair)
        {
            while(TryReduce(pair)){}
        }

        public static Pair Add(Pair left, Pair right)
        {
            //Increment the depts of all current pairs
            var leftFlat = left.FlattenLeftToRight();
            leftFlat.Add(left);
            foreach (var p in leftFlat)
            {
                p.Dept++;
            }

            var rightFlat = right.FlattenLeftToRight();
            rightFlat.Add(right);
            foreach (var p in rightFlat)
            {
                p.Dept++;
            }

            var pair = new Pair();
            pair.Dept = 0;
            pair.LeftPair = left;
            pair.RightPair = right;
            left.Parent = pair;
            right.Parent = pair;
            return pair;
        }



        public static Pair ParsePair(string input, Pair parent = null, int pairDept = 0)
        {
            var p = new Pair();
            p.Dept = pairDept;
            p.Parent = parent;

            //Remove outer brackets
            input = input.Remove(input.Length-1).Remove(0, 1);

            if (input[0] == '[')
            {
                //Find index of middle , - seperating the 2 parts
                var index = 0;
                var dept = 1;
                for (var i = 1; i < input.Length; i++) //start passed first char
                {
                    if (dept == 0)
                    {
                        index = i;
                        break;
                    }

                    if (input[i] == '[')
                    {
                        dept++;
                    }

                    if (input[i] == ']')
                    {
                        dept--;
                    }
                }
                //Parse contents
                p.LeftPair = ParsePair(input.Substring(0, index), p, pairDept+1);

                if (input[index + 1] == '[')
                {
                    p.RightPair = ParsePair(input.Substring(index + 1), p, pairDept + 1);
                }
                else
                {
                    var thing = input.Substring(index + 1);
                    p.RightValue = int.Parse(input.Substring(index + 1));
                }
            }
            else
            {
                var split = input.Split(',');
                p.LeftValue = int.Parse(split[0]);

                if (split[1][0] != '[')
                {
                    p.RightValue = int.Parse(split[1]);
                }
                else
                {
                    p.RightPair = ParsePair(input.Substring(split[0].Length + 1), p, pairDept + 1);
                }
            }

            return p;
        }

        public static List<Pair> ParseInput(string input)
        {
            var result = new List<Pair>();
            foreach (var line in input.SplitNewLine())
            {
                result.Add(ParsePair(line));
            }
            return result;
        }

        private const string Example = @"[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]
[[[5,[2,8]],4],[5,[[9,9],0]]]
[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]
[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]
[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]
[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]
[[[[5,4],[7,7]],8],[[8,3],8]]
[[9,3],[[9,9],[6,[4,9]]]]
[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]
[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]";


        private const string Input = @"[[[9,2],[[2,9],0]],[1,[[2,3],0]]]
[[[[2,0],2],[[6,4],[7,3]]],[0,[[3,0],[0,6]]]]
[[[[7,2],2],[9,[6,5]]],[[2,4],5]]
[[[[7,8],2],1],[[[5,4],[2,9]],[7,8]]]
[[[0,7],[1,[6,6]]],[[[0,7],9],4]]
[[[3,[9,6]],[5,1]],[[[0,1],6],[[7,6],0]]]
[[[3,0],[7,[4,0]]],[4,[[6,6],[5,3]]]]
[[[1,[4,8]],[2,[5,8]]],[[[3,6],[2,2]],[[3,8],[7,0]]]]
[9,[[[5,0],[0,3]],[2,[2,6]]]]
[[[3,[8,2]],[[8,0],5]],[[[7,6],[4,9]],[7,5]]]
[[7,[[4,1],9]],[5,1]]
[[[5,[7,5]],1],[8,[5,8]]]
[[[[0,2],7],[[1,4],[9,8]]],[[3,[0,3]],7]]
[[[[4,3],[7,4]],[6,[6,4]]],[8,0]]
[[[1,1],1],[[5,[2,7]],7]]
[[[5,4],5],[[7,[6,3]],[[8,4],6]]]
[[[7,9],[[4,4],[0,0]]],[[[8,6],6],[2,[6,4]]]]
[[[[4,7],[4,9]],3],[[[7,1],[8,6]],[9,[8,2]]]]
[6,[6,[2,9]]]
[[4,[[5,5],[5,0]]],[[[3,4],[9,5]],[8,6]]]
[2,[0,[2,5]]]
[[[4,[7,1]],[2,8]],[[7,0],[[1,6],1]]]
[[[3,4],[[7,8],[6,7]]],[[[6,2],[1,2]],5]]
[[[8,[0,8]],[[9,9],0]],[[[3,5],[4,2]],7]]
[[0,[[0,3],2]],[4,1]]
[[[[0,4],6],7],[[4,[9,1]],3]]
[[0,[[7,0],8]],[2,[8,[8,2]]]]
[[[[3,6],2],[9,4]],[6,[[7,9],[4,5]]]]
[[[[4,9],1],[[9,6],[8,8]]],[[7,[7,6]],[[8,3],[9,0]]]]
[2,0]
[[[[8,2],0],[3,5]],[[7,2],0]]
[[[[1,9],9],6],[9,[[9,3],[8,7]]]]
[[[9,[4,0]],[[7,1],[4,4]]],[[4,[2,3]],[8,7]]]
[[[[9,7],[5,6]],[4,[6,7]]],7]
[5,[[[8,2],8],[6,[7,9]]]]
[0,[[9,[0,1]],[[8,3],7]]]
[[[[4,5],[4,2]],[[5,2],[3,1]]],[[[3,1],[8,5]],8]]
[[0,4],[[2,[2,6]],[[1,1],3]]]
[[[0,8],[7,[5,8]]],7]
[[[7,2],[[6,6],[2,7]]],[[0,[9,3]],2]]
[[[[0,9],2],[[6,0],4]],3]
[[5,[[9,6],9]],[[6,[1,2]],[1,[6,2]]]]
[[[[3,9],5],[9,[7,2]]],[5,[[3,4],[0,6]]]]
[[2,[6,7]],[0,[[2,0],7]]]
[[2,[[5,4],[2,1]]],[2,[[8,7],[5,3]]]]
[[[[0,4],[2,5]],[1,2]],[5,[8,[0,3]]]]
[[[[9,2],[3,2]],[[2,9],4]],5]
[[[[8,9],5],1],[9,3]]
[[5,2],[3,[[8,5],2]]]
[[[0,1],[7,8]],[[[6,2],4],[[6,2],[9,5]]]]
[[[[9,6],5],2],2]
[[[[3,2],3],3],[[[0,1],1],[[8,4],8]]]
[[4,[2,[3,0]]],[[6,[7,0]],6]]
[[6,[[7,8],3]],[[[2,7],4],9]]
[0,2]
[[[9,1],[[3,7],[6,0]]],[[0,[4,1]],[[5,4],7]]]
[[[3,[9,4]],8],[[5,3],2]]
[[6,6],[[[0,5],[0,9]],[[5,5],4]]]
[[[[1,2],4],[[2,4],[8,0]]],[0,[[4,4],[5,8]]]]
[0,[[[9,0],3],[8,4]]]
[[4,5],[[[9,9],[3,5]],[8,[1,4]]]]
[[7,8],[[[3,1],[7,0]],[[4,7],[9,1]]]]
[[4,[2,[1,9]]],[[6,[6,1]],[[0,3],3]]]
[[[5,[0,9]],6],[[[3,4],[9,6]],[[4,0],[0,4]]]]
[[[1,5],[8,[2,8]]],[[5,[0,8]],[[0,7],[4,6]]]]
[[9,[0,2]],[[3,3],[3,1]]]
[[[[2,8],[5,9]],[2,[1,5]]],9]
[[3,[[8,9],[3,1]]],[[[9,0],7],[[0,4],3]]]
[[[[1,5],2],[5,[5,9]]],[5,[[0,1],[0,2]]]]
[6,[[[0,4],8],[[8,2],[5,5]]]]
[[[[7,7],5],[[8,2],7]],[2,5]]
[[[1,1],[[7,8],0]],3]
[[6,[[4,2],9]],[[[5,4],4],3]]
[[[[5,8],3],[[0,4],9]],[[[2,9],2],[3,4]]]
[[0,[4,8]],6]
[[[[9,5],[1,9]],[[3,7],[5,5]]],8]
[[1,9],6]
[[[4,[1,5]],3],0]
[[[2,[6,9]],5],[[5,7],[5,[7,1]]]]
[[[[3,1],[7,3]],[[1,0],[4,6]]],[[[4,9],[4,1]],[9,[2,0]]]]
[[[5,0],[[9,4],6]],[1,[[0,4],[9,9]]]]
[[[[9,8],3],[7,5]],[[[9,5],2],[9,9]]]
[[8,[[8,0],[2,3]]],[[[3,8],[2,6]],[[1,0],0]]]
[[[7,[7,1]],[[6,6],[2,9]]],[[5,[2,0]],[[3,9],[7,4]]]]
[1,[4,[[9,7],[1,3]]]]
[[0,3],[[[4,1],7],[[4,1],[3,0]]]]
[[0,[[7,7],6]],[[4,9],2]]
[[0,8],[4,[4,5]]]
[[[8,[0,5]],[[1,3],[0,5]]],[[2,6],[1,5]]]
[[[[7,6],8],[0,[2,7]]],8]
[8,[[[5,4],8],[[2,1],[7,5]]]]
[[[[7,3],[7,1]],0],[[[7,9],2],3]]
[[8,5],[6,6]]
[[[[5,2],8],7],[[[6,8],[1,0]],[[0,0],1]]]
[[[[1,0],1],6],[9,8]]
[[[[1,2],7],[1,[2,8]]],[[8,1],[[7,5],2]]]
[[0,6],[[2,8],[9,0]]]
[[[0,[7,7]],[2,[0,8]]],[[[7,4],4],[7,[4,0]]]]
[[[2,[9,3]],[[3,7],3]],[[[9,7],[5,6]],8]]
[[2,[[8,7],2]],[[8,[1,8]],[[7,2],1]]]";

    }
}