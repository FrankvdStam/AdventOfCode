using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Years.Year2021.BeaconScanner;

namespace Tests
{
    [TestFixture]
    public class Year2021Day19
    {
        [Test]
        public void ExampleMap()
        {
            var map = Scanner.BuildMap(Example);
            //Assert.AreEqual(_exampleExpectedResult.Count, map.Count);

            foreach (var expected in _exampleExpectedResult)
            {
                //Assert.Contains(expected, map);
            }
        }
        
        
        
        [Test]
        public void Rotations()
        {
            var position = new Vector3(1, 2, 3);
            var rotations = Scanner.GetTransformations(position).ToList();


            foreach (var rot in rotations)
            {
                Console.WriteLine(rot);
            }
            
            CollectionAssert.Contains(rotations, new Vector3( 1,  2,  3));
            CollectionAssert.Contains(rotations, new Vector3(-2,  1,  3));
            CollectionAssert.Contains(rotations, new Vector3(-1, -2,  3));
            CollectionAssert.Contains(rotations, new Vector3( 2, -1,  3));
            CollectionAssert.Contains(rotations, new Vector3( 1, -3,  2));
            CollectionAssert.Contains(rotations, new Vector3(-2, -3,  1));
            CollectionAssert.Contains(rotations, new Vector3(-1, -3, -2));
            CollectionAssert.Contains(rotations, new Vector3( 2, -3, -1));
            CollectionAssert.Contains(rotations, new Vector3( 1, -2, -3));
            CollectionAssert.Contains(rotations, new Vector3(-2, -1, -3));
            CollectionAssert.Contains(rotations, new Vector3(-1,  2, -3));
            CollectionAssert.Contains(rotations, new Vector3( 2,  1, -3));
            CollectionAssert.Contains(rotations, new Vector3( 1,  3, -2));
            CollectionAssert.Contains(rotations, new Vector3(-2,  3, -1));
            CollectionAssert.Contains(rotations, new Vector3(-1,  3,  2));
            CollectionAssert.Contains(rotations, new Vector3( 2,  3,  1));
            CollectionAssert.Contains(rotations, new Vector3( 3,  2, -1));
            CollectionAssert.Contains(rotations, new Vector3( 3,  1,  2));
            CollectionAssert.Contains(rotations, new Vector3( 3, -2,  1));
            CollectionAssert.Contains(rotations, new Vector3( 3, -1, -2));
            CollectionAssert.Contains(rotations, new Vector3(-3, -2, -1));
            CollectionAssert.Contains(rotations, new Vector3(-3, -1,  2));
            CollectionAssert.Contains(rotations, new Vector3(-3,  2,  1));
            CollectionAssert.Contains(rotations, new Vector3(-3,  1, -2));
        }


        private readonly List<Vector3> _exampleExpectedResult = new List<Vector3>()
        {
            new Vector3(-892,524,684                     ),
            new Vector3(-876,649,763                     ),
            new Vector3(-838,591,734                     ),
            new Vector3(-789,900,-551                    ),
            new Vector3(-739,-1745,668                   ),
            new Vector3(-706,-3180,-659                  ),
            new Vector3(-697,-3072,-689                  ),
            new Vector3(-689,845,-530                    ),
            new Vector3(-687,-1600,576                   ),
            new Vector3(-661,-816,-575                   ),
            new Vector3(-654,-3158,-753                  ),
            new Vector3(-635,-1737,486                   ),
            new Vector3(-631,-672,1502                   ),
            new Vector3(-624,-1620,1868                  ),
            new Vector3(-620,-3212,371                   ),
            new Vector3(-618,-824,-621                   ),
            new Vector3(-612,-1695,1788                  ),
            new Vector3(-601,-1648,-643                  ),
            new Vector3(-584,868,-557                    ),
            new Vector3(-537,-823,-458                   ),
            new Vector3(-532,-1715,1894                  ),
            new Vector3(-518,-1681,-600                  ),
            new Vector3(-499,-1607,-770                  ),
            new Vector3(-485,-357,347                    ),
            new Vector3(-470,-3283,303                   ),
            new Vector3(-456,-621,1527                   ),
            new Vector3(-447,-329,318                    ),
            new Vector3(-430,-3130,366                   ),
            new Vector3(-413,-627,1469                   ),
            new Vector3(-345,-311,381                    ),
            new Vector3(-36,-1284,1171                   ),
            new Vector3(-27,-1108,-65                    ),
            new Vector3(7,-33,-71                        ),
            new Vector3(12,-2351,-103                    ),
            new Vector3(26,-1119,1091                    ),
            new Vector3(346,-2985,342                    ),
            new Vector3(366,-3059,397                    ),
            new Vector3(377,-2827,367                    ),
            new Vector3(390,-675,-793                    ),
            new Vector3(396,-1931,-563                   ),
            new Vector3(404,-588,-901                    ),
            new Vector3(408,-1815,803                    ),
            new Vector3(423,-701,434                     ),
            new Vector3(432,-2009,850                    ),
            new Vector3(443,580,662                      ),
            new Vector3(455,729,728                      ),
            new Vector3(456,-540,1869                    ),
            new Vector3(459,-707,401                     ),
            new Vector3(465,-695,1988                    ),
            new Vector3(474,580,667                      ),
            new Vector3(496,-1584,1900                   ),
            new Vector3(497,-1838,-617                   ),
            new Vector3(527,-524,1933                    ),
            new Vector3(528,-643,409                     ),
            new Vector3(534,-1912,768                    ),
            new Vector3(544,-627,-890                    ),
            new Vector3(553,345,-567                     ),
            new Vector3(564,392,-477                     ),
            new Vector3(568,-2007,-577                   ),
            new Vector3(605,-1665,1952                   ),
            new Vector3(612,-1593,1893                   ),
            new Vector3(630,319,-379                     ),
            new Vector3(686,-3108,-505                   ),
            new Vector3(776,-3184,-501                   ),
            new Vector3(846,-3110,-434                   ),
            new Vector3(1135,-1161,1235                  ),
            new Vector3(1243,-1093,1063                  ),
            new Vector3(1660,-552,429                    ),
            new Vector3(1693,-557,386                    ),
            new Vector3(1735,-437,1738                   ),
            new Vector3(1749,-1800,1813                  ),
            new Vector3(1772,-405,1572                   ),
            new Vector3(1776,-675,371                    ),
            new Vector3(1779,-442,1789                   ),
            new Vector3(1780,-1548,337                   ),
            new Vector3(1786,-1538,337                   ),
            new Vector3(1847,-1591,415                   ),
            new Vector3(1889,-1729,1762                  ),
            new Vector3(1994,-1805,1792                  ),
        };

        private const string Example = @"--- scanner 0 ---
404,-588,-901
528,-643,409
-838,591,734
390,-675,-793
-537,-823,-458
-485,-357,347
-345,-311,381
-661,-816,-575
-876,649,763
-618,-824,-621
553,345,-567
474,580,667
-447,-329,318
-584,868,-557
544,-627,-890
564,392,-477
455,729,728
-892,524,684
-689,845,-530
423,-701,434
7,-33,-71
630,319,-379
443,580,662
-789,900,-551
459,-707,401

--- scanner 1 ---
686,422,578
605,423,415
515,917,-361
-336,658,858
95,138,22
-476,619,847
-340,-569,-846
567,-361,727
-460,603,-452
669,-402,600
729,430,532
-500,-761,534
-322,571,750
-466,-666,-811
-429,-592,574
-355,545,-477
703,-491,-529
-328,-685,520
413,935,-424
-391,539,-444
586,-435,557
-364,-763,-893
807,-499,-711
755,-354,-619
553,889,-390

--- scanner 2 ---
649,640,665
682,-795,504
-784,533,-524
-644,584,-595
-588,-843,648
-30,6,44
-674,560,763
500,723,-460
609,671,-379
-555,-800,653
-675,-892,-343
697,-426,-610
578,704,681
493,664,-388
-671,-858,530
-667,343,800
571,-461,-707
-138,-166,112
-889,563,-600
646,-828,498
640,759,510
-630,509,768
-681,-892,-333
673,-379,-804
-742,-814,-386
577,-820,562

--- scanner 3 ---
-589,542,597
605,-692,669
-500,565,-823
-660,373,557
-458,-679,-417
-488,449,543
-626,468,-788
338,-750,-386
528,-832,-391
562,-778,733
-938,-730,414
543,643,-506
-524,371,-870
407,773,750
-104,29,83
378,-903,-323
-778,-728,485
426,699,580
-438,-605,-362
-469,-447,-387
509,732,623
647,635,-688
-868,-804,481
614,-800,639
595,780,-596

--- scanner 4 ---
727,592,562
-293,-554,779
441,611,-461
-714,465,-776
-743,427,-804
-660,-479,-426
832,-632,460
927,-485,-438
408,393,-506
466,436,-512
110,16,151
-258,-428,682
-393,719,612
-211,-452,876
808,-476,-593
-575,615,604
-485,667,467
-680,325,-822
-627,-443,-432
872,-547,-609
833,512,582
807,604,487
839,-516,451
891,-625,532
-652,-548,-490
30,-46,-14";
    }
}
