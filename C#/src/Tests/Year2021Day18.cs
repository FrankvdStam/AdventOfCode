using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Years.Year2017;
using static Years.Year2021.Day18;

namespace Tests
{
    [TestFixture]
    public class Year2021Day18
    {

        [TestCase("[[[[[9,8],1],2],3],4]", "[[[[0, 9],2],3],4]")]
        [TestCase("[7,[6,[5,[4,[3,2]]]]]", "[7,[6,[5,[7, 0]]]]")]
        [TestCase("[[6,[5,[4,[3,2]]]],1]", "[[6,[5,[7, 0]]],3]")]
        [TestCase("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]", "[[3,[2,[8, 0]]],[9,[5,[4,[3,2]]]]]")]
        [TestCase("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]", "[[3,[2,[8, 0]]],[9,[5,[7,0]]]]")]
        public void Explode(string input, string expected)
        {
            var pair = Years.Year2021.Day18.ParsePair(input);
            var exploded = Years.Year2021.Day18.TryExplode(pair);

            Assert.That(pair.ToString(), Is.EqualTo(expected.Replace(" ", string.Empty)));
        }
        
        [TestCase("[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]", "[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]", "[[[[7,8],[6,6]],[[6,0],[7,7]]],[[[7,8],[8,8]],[[7,9],[0,6]]]]")]
        [TestCase("[[[[4,3],4],4],[7,[[8,4],9]]]", "[1,1]", "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]")]
        public void AddReduce(string left, string right, string expected)
        {
            var leftPair = Years.Year2021.Day18.ParsePair(left);
            var rightPair = Years.Year2021.Day18.ParsePair(right);
            var result = Years.Year2021.Day18.Add(leftPair, rightPair);
            Years.Year2021.Day18.ReduceAll(result);

            Assert.That(result.ToString(), Is.EqualTo(expected.Replace(" ", string.Empty)));
        }



        [TestCase(@"[1,1]
[2,2]
[3,3]
[4,4]", "[[[[1,1],[2,2]],[3,3]],[4,4]]")]
        [TestCase(@"[1,1]
[2,2]
[3,3]
[4,4]
[5,5]", "[[[[3,0],[5,3]],[4,4]],[5,5]]")]
        [TestCase(@"[1,1]
[2,2]
[3,3]
[4,4]
[5,5]
[6,6]", "[[[[5,0],[7,4]],[5,5]],[6,6]]")]
        [TestCase(@"[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]
[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]
[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]
[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]
[7,[5,[[3,8],[1,4]]]]
[[2,[2,2]],[8,[8,1]]]
[2,9]
[1,[[[9,3],9],[[9,0],[0,7]]]]
[[[5,[7,4]],7],1]
[[[[4,2],2],6],[8,7]]", "[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]")]
        public void AddReduceList(string list, string expected)
        {
            var pairs = Years.Year2021.Day18.ParseInput(list);
            var currentPair = pairs.First();
            for (int i = 1; i < pairs.Count; i++)
            {
                //Console.WriteLine(currentPair);
                //Console.WriteLine(pairs[i]);

                currentPair = Years.Year2021.Day18.Add(currentPair, pairs[i]);
                Years.Year2021.Day18.ReduceAll(currentPair);
                //Console.WriteLine(currentPair);
            }
            Assert.That(currentPair.ToString(), Is.EqualTo(expected.Replace(" ", string.Empty)));
        }


        [TestCase("[9,1]", 29)]
        [TestCase("[1,9]", 21)]
        [TestCase("[[9,1],[1,9]]", 129)]
        [TestCase("[[1,2],[[3,4],5]]", 143)]
        [TestCase("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", 1384)]
        [TestCase("[[[[1,1],[2,2]],[3,3]],[4,4]]", 445)]
        [TestCase("[[[[3,0],[5,3]],[4,4]],[5,5]]", 791)]
        [TestCase("[[[[5,0],[7,4]],[5,5]],[6,6]]", 1137)]
        [TestCase("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", 3488)]
        public void Magnitude(string pair, int expected)
        {
            var pair_ = Years.Year2021.Day18.ParsePair(pair);

            Assert.That(pair_.Magnitude(), Is.EqualTo(expected));
        }
    }
}
