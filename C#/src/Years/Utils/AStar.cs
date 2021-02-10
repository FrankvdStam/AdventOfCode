using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Years.Utils
{
    ///https://gigi.nullneuron.net/gigilabs/a-pathfinding-example-in-c/

    public class AStarNode
    {
        public Vector2i Position;

        public int DistanceFromStart;//G
        public int ManhattanDistanceToDestination;//H
        public int F => DistanceFromStart + ManhattanDistanceToDestination;

        public AStarNode Parent;
    }



    public class AStar
    {
        public delegate AStarNode AStarProgressDelegate();

        public static int CalculateAStar(List<AStarNode> nodes, AStarNode start, AStarNode target, Action<AStarNode> currentProgressReport = null, Action<AStarNode> parentProgressReport = null)
        {
            AStarNode current = null;

            var openList = new List<AStarNode>();
            var closedList = new List<AStarNode>();
            int distanceFromStart = 0;

            openList.Add(start);

            while (openList.Count > 0)
            {
                // algorithm's logic goes here
                var lowest = openList.Min(l => l.F);
                current = openList.First(l => l.F == lowest);

                // add the current square to the closed list
                closedList.Add(current);

                // remove it from the open list
                openList.Remove(current);

                currentProgressReport?.Invoke(current);

                // if we added the destination to the closed list, we've found a path
                if (closedList.FirstOrDefault(l => l.Position.X == target.Position.X && l.Position.Y == target.Position.Y) != null)
                    break;

                var adjacentSquares = GetWalkableAdjacentSquares(current, nodes);
                distanceFromStart++;

                foreach (var adjacentSquare in adjacentSquares)
                {
                    // if this adjacent square is already in the closed list, ignore it
                    if (closedList.FirstOrDefault(l => l.Position.X == adjacentSquare.Position.X
                                                       && l.Position.Y == adjacentSquare.Position.Y) != null)
                        continue;

                    // if it's not in the open list...
                    if (openList.FirstOrDefault(l => l.Position.X == adjacentSquare.Position.X
                                                     && l.Position.Y == adjacentSquare.Position.Y) == null)
                    {
                        // compute its score, set the parent
                        adjacentSquare.DistanceFromStart = distanceFromStart;
                        adjacentSquare.ManhattanDistanceToDestination = adjacentSquare.Position.ManhattanDistance(target.Position);
                        adjacentSquare.Parent = current;

                        // and add it to the open list
                        openList.Insert(0, adjacentSquare);
                    }
                    else
                    {
                        // test if using the current G score makes the adjacent square's F score
                        // lower, if yes update the parent because it means it's a better path
                        if (distanceFromStart + adjacentSquare.ManhattanDistanceToDestination < adjacentSquare.F)
                        {
                            adjacentSquare.DistanceFromStart = distanceFromStart;
                            adjacentSquare.Parent = current;
                        }
                    }
                }
            }

            // assume path was found; let's show it
            int distance = 0;
            while (current != null)
            {
                parentProgressReport?.Invoke(current);
                current = current.Parent;
                distance++;
            }

            return distance;
        }

        static List<AStarNode> GetWalkableAdjacentSquares(AStarNode node, List<AStarNode> nodes)
        {
            return nodes.Where(i =>
                i.Position == new Vector2i(node.Position.X    , node.Position.Y - 1)  ||
                i.Position == new Vector2i(node.Position.X    , node.Position.Y + 1)  ||
                i.Position == new Vector2i(node.Position.X - 1, node.Position.Y    )  ||
                i.Position == new Vector2i(node.Position.X + 1, node.Position.Y    )
            ).ToList();
        }






        string[] map = new string[]
        {
            "+------+",
            "|      |",
            "|A X   |",
            "|XXX   |",
            "|   X  |",
            "| B    |",
            "|      |",
            "+------+",
        };


        public void Run()
        {
            AStarNode current = null;
            var start = new AStarNode {  Position =  new Vector2i(1, 2) };
            var target = new AStarNode { Position = new Vector2i(2, 5) };

            var openList = new List<AStarNode>();
            var closedList = new List<AStarNode>();
            int distanceFromStart = 0;

            openList.Add(start);

            while (openList.Count > 0)
            {
                // algorithm's logic goes here
                var lowest = openList.Min(l => l.F);
                current = openList.First(l => l.F == lowest); 
                
                // add the current square to the closed list
                closedList.Add(current);

                // remove it from the open list
                openList.Remove(current);

                // show current square on the map
                Console.SetCursorPosition(current.Position.X, current.Position.Y);
                Console.Write('.');
                Console.SetCursorPosition(current.Position.X, current.Position.Y);
                System.Threading.Thread.Sleep(1000);

                // if we added the destination to the closed list, we've found a path
                if (closedList.FirstOrDefault(l => l.Position.X == target.Position.X && l.Position.Y == target.Position.Y) != null)
                    break;

                var adjacentSquares = GetWalkableAdjacentSquares(current.Position.X, current.Position.Y, map);
                distanceFromStart++;

                foreach (var adjacentSquare in adjacentSquares)
                {
                    // if this adjacent square is already in the closed list, ignore it
                    if (closedList.FirstOrDefault(l => l.Position.X == adjacentSquare.Position.X
                                                       && l.Position.Y == adjacentSquare.Position.Y) != null)
                        continue;

                    // if it's not in the open list...
                    if (openList.FirstOrDefault(l => l.Position.X == adjacentSquare.Position.X
                                                     && l.Position.Y == adjacentSquare.Position.Y) == null)
                    {
                        // compute its score, set the parent
                        adjacentSquare.DistanceFromStart = distanceFromStart;
                        adjacentSquare.ManhattanDistanceToDestination = adjacentSquare.Position.ManhattanDistance(target.Position);
                        adjacentSquare.Parent = current;

                        // and add it to the open list
                        openList.Insert(0, adjacentSquare);
                    }
                    else
                    {
                        // test if using the current G score makes the adjacent square's F score
                        // lower, if yes update the parent because it means it's a better path
                        if (distanceFromStart + adjacentSquare.ManhattanDistanceToDestination < adjacentSquare.F)
                        {
                            adjacentSquare.DistanceFromStart = distanceFromStart;
                            adjacentSquare.Parent = current;
                        }
                    }
                }
            }


            // assume path was found; let's show it
            while (current != null)
            {
                Console.SetCursorPosition(current.Position.X, current.Position.Y);
                Console.Write('_');
                Console.SetCursorPosition(current.Position.X, current.Position.Y);
                current = current.Parent;
                System.Threading.Thread.Sleep(1000);
            }
        }

        static List<AStarNode> GetWalkableAdjacentSquares(int x, int y, string[] map)
        {
            var proposedLocations = new List<AStarNode>()
            {
                new AStarNode { Position = new Vector2i(x    , y - 1) },
                new AStarNode { Position = new Vector2i(x    , y + 1) },
                new AStarNode { Position = new Vector2i(x - 1, y    ) },
                new AStarNode { Position = new Vector2i(x + 1, y    ) },
            };

            return proposedLocations.Where(l => map[l.Position.Y][l.Position.X] == ' ' || map[l.Position.Y][l.Position.X] == 'B').ToList();
        }
    }
}
