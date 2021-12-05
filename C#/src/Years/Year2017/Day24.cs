using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2017
{
    public class Day24 : IDay
    {

        public int Day => 24;
        public int Year => 2017;

        public void ProblemOne()
        {
            var parts = ParseInput(Input);
            var nodes = BuildTree(parts, out int max, out _cashedResult);
            Console.WriteLine(max);
        }

        private int _cashedResult = 0;

        public void ProblemTwo()
        {
            Console.WriteLine(_cashedResult);
        }

        private List<TreeNode<Vector2i>> BuildTree(List<Vector2i> parts, out int max, out int longestScore)
        {
            max = int.MinValue;
            longestScore = int.MinValue;
            int longest = int.MinValue;

            var nodes = parts.Where(i => i.X == 0).Select(i => new TreeNode<Vector2i>(i)).ToList(); //No input data exists where y == 0
            foreach (var treeNode in nodes)
            {
                BuildLeaves(parts, new List<Vector2i>(){ treeNode.Value }, treeNode, ref max, ref longest, ref longestScore);
            }
            return nodes;
        }

        private void BuildLeaves(List<Vector2i> parts, List<Vector2i> used, TreeNode<Vector2i> node, ref int max, ref int longest, ref int longestScore)
        {
            //Which side does the current node connect with?
            int connector = int.MinValue;
            if (node.Parent == null)
            {
                connector = node.Value.Y;
            }

            if (connector == int.MinValue)
            {
                if (node.Parent.Value.X == node.Value.X || node.Parent.Value.Y == node.Value.X)
                {
                    connector = node.Value.Y;
                }
                else
                {
                    connector = node.Value.X;
                }
            }

            
            //var debugBreakList = new List<Vector2i>();
            //debugBreakList.Add(new Vector2i(0, 4));
            //debugBreakList.Add(new Vector2i(32, 4));
            //debugBreakList.Add(new Vector2i(19, 32));
            //debugBreakList.Add(new Vector2i(49, 19));
            //debugBreakList.Add(new Vector2i(49, 49));
            //debugBreakList.Add(new Vector2i(49,39));
            //debugBreakList.Add(new Vector2i(39,44));
            //debugBreakList.Add(new Vector2i(44,1 ));
            //debugBreakList.Add(new Vector2i(1,18 ));
            //debugBreakList.Add(new Vector2i(18,20));
            //debugBreakList.Add(new Vector2i(32,20));
            //debugBreakList.Add(new Vector2i(32,30));
            //debugBreakList.Add(new Vector2i(37,30));
            //debugBreakList.Add(new Vector2i(32,37));
            //debugBreakList.Add(new Vector2i(32,10));
            //debugBreakList.Add(new Vector2i(10,11));
            //debugBreakList.Add(new Vector2i(11,7 ));
            //debugBreakList.Add(new Vector2i(7,41 ));
            //debugBreakList.Add(new Vector2i(41,48));
            //debugBreakList.Add(new Vector2i(45,48));//Hit
            //debugBreakList.Add(new Vector2i(45,45));//Hit
            //debugBreakList.Add(new Vector2i(47,45));//Hit
            //debugBreakList.Add(new Vector2i(0,47 ));//Hit
            //debugBreakList.Add(new Vector2i(0,29 ));//hit
            //debugBreakList.Add(new Vector2i(29,33));//hit
            //debugBreakList.Add(new Vector2i(33,33));//hit
            //debugBreakList.Add(new Vector2i(33,9 ));//hit
            //debugBreakList.Add(new Vector2i(9,24 ));//hit
            //debugBreakList.Add(new Vector2i(34,24));//hit
            //debugBreakList.Add(new Vector2i(34,4 ));
            //debugBreakList.Add(new Vector2i(35,4 ));
            //debugBreakList.Add(new Vector2i(28,35));//hit
            //debugBreakList.Add(new Vector2i(8,28 ));
            //debugBreakList.Add(new Vector2i(38,8 ));
            //debugBreakList.Add(new Vector2i(38,36));
            //
            //var _break = true;
            //
            //var ptr2 = node;
            //while (ptr2 != null)
            //{
            //    if (ptr2.Value != debugBreakList.Last())
            //    {
            //        _break = false;
            //        break;
            //    }
            //    else
            //    {
            //        debugBreakList.RemoveAt(debugBreakList.Count-1);
            //    }
            //    ptr2 = ptr2.Parent;
            //}
            //
            //if (debugBreakList.Count == 0 && _break)
            //{
            //
            //}
            
            foreach (var leaf in parts.Except(used))
            {
                if (leaf.X == connector || leaf.Y == connector)
                {
                    //Setup node relationship
                    var newNode = new TreeNode<Vector2i>(leaf);
                    newNode.Parent = node;
                    node.Children.Add(newNode);

                    //Find leaf subnodes
                    var temp = used.Clone();
                    temp.Add(leaf);

                    BuildLeaves(parts, temp, newNode, ref max, ref longest, ref longestScore);
                }
            }

            //Dead end from here. Calculate score
            var ptr = node;
            var score = 0;
            var txt = "";
            var length = 0;
            while (ptr != null)
            {
                score += ptr.Value.X;
                score += ptr.Value.Y;
                txt = $"{ptr.Value} {txt}";
                ptr = ptr.Parent;
                length++;
            }

            if (length > longest)
            {
                longest = length;
                longestScore = score;
            }

            if (length == longest)
            {
                if (score > longestScore)
                {
                    longestScore = score;
                }
            }

            if (score > max)
            {
                max = score;
                //Console.WriteLine($"{txt} - {score}");
            }
        }


        private List<Vector2i> ParseInput(string input)
        {
            var result = new List<Vector2i>();
            var lines = input.SplitNewLine();
            foreach (var l in lines)
            {
                var bits = l.Split('/');
                result.Add(new Vector2i(int.Parse(bits[0]), int.Parse(bits[1])));
            }
            return result;
        }

        private const string Example = @"0/2
2/2
2/3
3/4
3/5
0/1
10/1
9/10";

        private const string Input = @"31/13
34/4
49/49
23/37
47/45
32/4
12/35
37/30
41/48
0/47
32/30
12/5
37/31
7/41
10/28
35/4
28/35
20/29
32/20
31/43
48/14
10/11
27/6
9/24
8/28
45/48
8/1
16/19
45/45
0/4
29/33
2/5
33/9
11/7
32/10
44/1
40/32
2/45
16/16
1/18
38/36
34/24
39/44
32/37
26/46
25/33
9/10
0/29
38/8
33/33
49/19
18/20
49/39
18/39
26/13
19/32";
    }
}