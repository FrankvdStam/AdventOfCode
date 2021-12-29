using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Years.Utils;

namespace Years.Year2021.BeaconScanner
{
    public class Scanner
    {
        #region Building the map ========================================================================================================

        public static (List<Vector3> beaconPositions, List<Vector3> scannerPositions) BuildMap(string input)
        {
            var scannerPositions = new List<Vector3>();
            scannerPositions.Add(new Vector3(0,0,0));

            var scanners = ParseInput(input);
            var outerScanner = scanners.First();
            scanners.RemoveAt(0);

            var unsolved = new Queue<Scanner>(scanners);



            //Foreach scanner except the first
            while (unsolved.Any())
            {
                var innerScanner = unsolved.Dequeue();

                //Console.WriteLine(innerScanner.Id);

                if (FindMatch(outerScanner, innerScanner, out Vector3 diff, out int innerTransformIndex))
                {
                    scannerPositions.Add(diff);

                    //Console.WriteLine($"Match for {innerScanner.Id} at {diff}");

                    //Rebuild scanner 0 with newly found positions
                    var currentPositions = outerScanner.Positions[0];
                    var transforms = innerScanner.Positions[innerTransformIndex];
                    var transformed = transforms.Select(i => Vector3.Add(i, diff));
                    foreach (var t in transformed)
                    {
                        if (!currentPositions.Contains(t))
                        {
                            currentPositions.Add(t);
                        }
                    }

                    var news = new Scanner(0, currentPositions);
                    outerScanner = news;

                    //Go to next scanner
                    continue;
                }
                else
                {
                    unsolved.Enqueue(innerScanner);
                    Console.WriteLine($"Unsolved: {unsolved.Count}");
                }
            }

            var outerPositions = outerScanner.Positions[0];

            return (outerPositions, scannerPositions);
        }


        private static bool TryFindTwelveMatches(int firstIndex, int secondIndex, Scanner left, Scanner right, out Vector3 diff)
        {
            diff = new Vector3();

            var firstTransforms = left.Positions[firstIndex];// Scanner.GetTransformations(first).ToList();
            var secondTransforms = right.Positions[secondIndex];// Scanner.GetTransformations(second).ToList();

            for (var outerPositionIndex = 0; outerPositionIndex < firstTransforms.Count; outerPositionIndex++)
            {
                for (var innerPositionIndex = 0; innerPositionIndex < secondTransforms.Count; innerPositionIndex++)
                {
                    var outerPosition = firstTransforms[outerPositionIndex];
                    var innerPosition = secondTransforms[innerPositionIndex];

                    // if (outerPosition == new Vector3(-618, -824, -621))
                    // {
                    //
                    // }

                    diff = Vector3.Subtract(outerPosition, innerPosition);

                    if (diff == new Vector3(-92, -2380, -20))
                    {

                    }

                    var copy = new Vector3(diff.X, diff.Y, diff.Z);//Can't use out param in lambda
                    //var innerTransformPlusDiff = right.Positions[secondIndex].Select(i => Vector3.Add(i, copy));
                    //var duplicates = left.Positions[firstIndex].Count(outer => innerTransformPlusDiff.Any(inner => outer == inner));

                    var innerTransformPlusDiff = new HashSet<Vector3>(right.Positions[secondIndex].Select(i => Vector3.Add(i, copy)));
                    var duplicates = left.Positions[firstIndex].Count(outer => innerTransformPlusDiff.Contains(outer));

                    if (duplicates >= 12)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool FindMatch(Scanner outerScanner, Scanner innerScanner, out Vector3 diff, out int innerTransformIndex)
        {
            diff = new Vector3();
            innerTransformIndex = 0;

            //Foreach transformation of the outer scanner
            for (var outerTransformIndex = 0; outerTransformIndex < 24; outerTransformIndex++)
            {
                var outerTransforms = outerScanner.Positions[outerTransformIndex];

                //Foreach transformation of the inner scanner
                for (innerTransformIndex = 0; innerTransformIndex < 24; innerTransformIndex++)
                {
                    var innerTransforms = innerScanner.Positions[innerTransformIndex];



                    if (TryFindTwelveMatches(outerTransformIndex, innerTransformIndex, outerScanner, innerScanner, out diff))
                    {
                        return true;
                        //Console.WriteLine("match!");
                    }

                    //Console.WriteLine($"{innerScanner.Id} {outerTransformIndex} {innerTransformIndex}");
                }
            }
            return false;
        }


        private static List<Scanner> ParseInput(string input)
        {
            var result = new List<Scanner>();
            List<Vector3> currentList = null;
            var currentId = 0;
            foreach (var line in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
            {
                if (line.StartsWith("---"))
                {
                    if (currentList == null)
                    {
                        currentList = new List<Vector3>();
                    }
                    else
                    {
                        result.Add(new Scanner(currentId, currentList));

                        var split = line.Split(' ');
                        currentId = int.Parse(split[2]);
                        currentList = new List<Vector3>();
                    }
                }
                else
                {
                    var split = line.Split(',');
                    currentList.Add(new Vector3(
                        int.Parse(split[0]),
                        int.Parse(split[1]),
                        int.Parse(split[2])));
                }
            }
            result.Add(new Scanner(currentId, currentList));


            return result;
        }



        #endregion



        





        public override string ToString()
        {
            return $"{Id}";
        }

        public Scanner(int id, List<Vector3> positions)
        {
            //First calculate all transforms
            var lookup = new Dictionary<Vector3, List<Vector3>>();
            foreach (var position in positions)
            {
                var transforms = GetTransformations(position).ToList();
                lookup.Add(position, transforms);
            }

            //Initialize all lists
            Positions = new List<List<Vector3>>();
            for (var i = 0; i < 24; i++)
            {
                Positions.Add(new List<Vector3>());
            }


            foreach (var position in positions)
            {
                for (int i = 0; i < 24; i++)
                {
                    var transforms = lookup[position];
                    Positions[i].Add(transforms[i]);
                }
            }

            Id = id;
        }


        public int Id;
        public List<List<Vector3>> Positions;

        public void AddPositions(List<Vector3> positions)
        {
            foreach (var position in positions)
            {
                //index 0 is not transformed - contains the original list
                if (!Positions[0].Contains(position))
                {


                }
            }
        }





        #region Rotations
        //The 24 different rotations and orientations can be calculated using quaternation math
        //The transformations can be pre-calculated and then applied to each vector.

        private static List<Quaternion> _transformations;
        public static List<Quaternion> Transformations
        {
            get
            {
                if (_transformations == null)
                {
                    InitTransformations();
                }
                return _transformations;
            }
        }

        private static void InitTransformations()
        {
            float Rads(int degr)
            {
                return (float)((double)degr).DegreesToRadians();
            }

            //No magic, these just happen to be all the unique rotations of a cube.
            _transformations = new List<Quaternion>();
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(0 ), Rads(0  ), Rads(0  )));
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(0 ), Rads(0  ), Rads(90 )));
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(0 ), Rads(0  ), Rads(180)));
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(0 ), Rads(0  ), Rads(270)));
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(0 ), Rads(90 ), Rads(0  )));
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(0 ), Rads(90 ), Rads(90 )));
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(0 ), Rads(90 ), Rads(180)));
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(0 ), Rads(90 ), Rads(270)));
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(0 ), Rads(180), Rads(0  )));
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(0 ), Rads(180), Rads(90 )));
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(0 ), Rads(180), Rads(180)));
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(0 ), Rads(180), Rads(270)));
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(0 ), Rads(270), Rads(0  )));
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(0 ), Rads(270), Rads(90 )));
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(0 ), Rads(270), Rads(180)));
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(0 ), Rads(270), Rads(270)));
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(90), Rads(0  ), Rads(0  )));
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(90), Rads(0  ), Rads(90 )));
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(90), Rads(0  ), Rads(180)));
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(90), Rads(0  ), Rads(270)));
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(90), Rads(180), Rads(0  )));
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(90), Rads(180), Rads(90 )));
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(90), Rads(180), Rads(180)));
            _transformations.Add(Quaternion.CreateFromYawPitchRoll(Rads(90), Rads(180), Rads(270)));
        }


        //Using the pre-calculated transforms, calculate all transformations of a given vector
        public static IEnumerable<Vector3> GetTransformations(Vector3 vector)
        {
            foreach (var quat in Transformations)
            {
                var result = Vector3.Transform(vector, quat);
                result.X = (float)Math.Round(result.X);
                result.Y = (float)Math.Round(result.Y);
                result.Z = (float)Math.Round(result.Z);
                yield return result;
            }
        }

        public static Vector3 GetDeltas(Vector3 vector)
        {
            var x = Math.Abs(vector.X);
            var y = Math.Abs(vector.Y);
            var z = Math.Abs(vector.Z);

            return new Vector3(x.Difference(y), y.Difference(z), z.Difference(x));
        }

        #endregion
    }
}
