using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Years.Utils;

namespace Years.Year2022
{
    public class Day09 : BaseDay
    {
        public Day09() : base(2022, 9) 
        {
            _movements = ParseInput(Example);
        }

        private readonly List<(Direction direction, int steps)> _movements;
        private readonly Dictionary<Direction, Vector2i> _directions = new Dictionary<Direction, Vector2i>()
        {
            { Direction.Right,  Extensions.DirectionRight   },
            { Direction.Left,   Extensions.DirectionLeft    },
            { Direction.Up,     Extensions.DirectionUp      },
            { Direction.Down,   Extensions.DirectionDown    },
        };


        public override void ProblemOne()
        {
            var rope = new LinkedList<Vector2i>();
            rope.AddLast(new Vector2i(0, 0));
            rope.AddLast(new Vector2i(0, 0));

            var positions = new List<Vector2i>();
            foreach(var m in _movements)
            {
                var move = _directions[m.direction];
                for(int i = 0; i < m.steps; i++)
                {
                    var head = rope.First;
                    head.Value = head.Value.Add(move);
                    MoveRope(head, move);

                    var tail = rope.Last.Value;
                    if(!positions.Contains(tail))
                    {
                        positions.Add(tail);
                    }

                    var drawable = new List<(Vector2i, char)>()
                    {
                        (new Vector2i(0,0), 's'),
                        (tail, 'T'),
                        (head.Value, 'H'),
                    };
                    Console.Clear();
                    drawable.Draw();
                    Console.ReadKey();
                }
            }
            Console.WriteLine(positions.Count);
        }

        public override void ProblemTwo()
        {
            //var ropeLength = 3;
            //var rope = new LinkedList<Vector2i>();
            //for(int i = 0; i < ropeLength; i++)
            //{
            //    rope.AddLast(new Vector2i(0, 0));
            //}
            //
            //var positions = new List<Vector2i>();
            //foreach (var m in _movements)
            //{
            //    var move = _directions[m.direction];
            //    for (int i = 0; i < m.steps; i++)
            //    {
            //        //Move the head
            //        var head = rope.First;
            //                                               
            //        rope[0] = rope[0].Add(move);
            //
            //        //Move each tail
            //        for(int j = 0; j < rope.Count-1; j++)
            //        {
            //            rope[j + 1] = MoveTail(rope[j], rope[j+1], move);
            //
            //            var drawable = new List<(Vector2i, char)>();
            //
            //            for (int a = 0; a < ropeLength; a++)
            //            {
            //                drawable.Add((rope[a], a.ToString()[0]));
            //            }
            //
            //            Console.Clear();
            //            drawable.Draw();
            //            Console.WriteLine();
            //            Console.WriteLine(j);
            //            Console.ReadKey();
            //        }
            //
            //        var tail = rope[ropeLength-1];
            //        if (!positions.Contains(tail))
            //        {
            //            positions.Add(tail);
            //        }
            //
            //        
            //    }
            //}
            //Console.WriteLine(positions.Count);
        }
        //4323 too high


        private void MoveRope(LinkedListNode<Vector2i> headNode, Vector2i step)
        {
            var tailNode = headNode.Next;

            var head = headNode.Value;
            var tail = tailNode.Value;


            var xDiff = head.X.Difference(tail.X);
            //var yDiff = head.Y.Difference(tail.Y);
            var distance = head.Distance(tail);

            if (distance > 1)
            {
                tail = tail.Add(step);
                tailNode.Value = tail;
                
                //Move next tail
                //if(tailNode.Next != null)
                //{
                //    MoveRope(tailNode.Next, step);
                //}

                //Check if a sideways adjustment is required
                if (head.X != tail.X && head.Y != tail.Y)
                {
                    if (xDiff > 1)
                    {
                        var moveY = head.Y - tail.Y;
                        tail.Y = head.Y;
                        var tailStep = new Vector2i(0, moveY);

                        if (tailNode.Next != null)
                        {
                            MoveRope(tailNode.Next, tailStep);
                        }
                    }
                    else
                    {
                        var moveX = head.X - tail.X;
                        tail.X = head.X;
                        var tailStep = new Vector2i(moveX, 0);

                        if (tailNode.Next != null)
                        {
                            MoveRope(tailNode.Next, tailStep);
                        }
                    }

                    //if(step.X != 0)
                    //{
                    //    tail.Y = head.Y;
                    //}
                    //else
                    //{
                    //    tail.X = head.X;
                    //}
                }
            }
            //Console.Clear();
            //new List<(Vector2i, char)>()
            //{
            //    (new Vector2i(0,0), 's'),
            //    (tail, 'T'),
            //    (head, 'H')
            //}.Draw();
        }

        private Vector2i MoveTail(Vector2i head, Vector2i tail, Vector2i step)
        {
            var xDiff = head.X.Difference(tail.X);
            //var yDiff = head.Y.Difference(tail.Y);
            var distance = head.Distance(tail);

            if (distance > 1)
            {
                tail = tail.Add(step);

                //Check if a sideways adjustment is required
                if (head.X != tail.X && head.Y != tail.Y)
                {
                    if (xDiff > 1)
                    {
                        tail.Y = head.Y;
                    }
                    else
                    {
                        tail.X = head.X;
                    }

                    //if(step.X != 0)
                    //{
                    //    tail.Y = head.Y;
                    //}
                    //else
                    //{
                    //    tail.X = head.X;
                    //}
                }
            }
            //Console.Clear();
            //new List<(Vector2i, char)>()
            //{
            //    (new Vector2i(0,0), 's'),
            //    (tail, 'T'),
            //    (head, 'H')
            //}.Draw();
            return tail;
        }


        private List<(Direction direction, int steps)> ParseInput(string input)
        {
            var result = new List<(Direction direction, int steps)>();
            foreach(var line in input.SplitNewLine())
            {
                var split = line.Split(' ');
                var direction = split[0] switch
                {
                    "R" => Direction.Right,
                    "L" => Direction.Left,
                    "U" => Direction.Up,
                    "D" => Direction.Down,
                    _ => throw new NotImplementedException(),
                };
                var steps = int.Parse(split[1]);
                result.Add((direction, steps));
            }
            return result;
        }

        private const string LargeExample = @"R 5
U 8
L 8
D 3
R 17
D 10
L 25
U 20
";

        private const string Example = @"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2
";
    }
}