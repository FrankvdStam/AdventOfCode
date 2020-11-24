using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Lib.Shared;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class VectorTests
    {
        //Moves of 1

        //1 right of origin
        [TestCase(
            0, 0, //start
            1, 0,  //end
            0, 0, //result
            1, 0
        )]

        //1 Left of origin
        [TestCase( 
            0, 0, //start
           -1, 0,  //end
            0, 0, //result
           -1, 0
        )]
        
        //1 bellow origin
        [TestCase( 
            0, 0, //start
            0, 1,  //end
            0, 0, //result
            0, 1
        )]

        //1 above origin
        [TestCase(
            0, 0, //start
            0, -1,  //end
            0, 0, //result
            0, -1
        )]

        //Sideways


        //1 right+bellow of origin
        [TestCase(
            0, 0, //start
            1, 1,  //end
            0, 0, //result
            1, 1
        )]

        //1 left+bellow of origin
        [TestCase(
            0, 0, //start
           -1, 1,  //end
            0, 0, //result
           -1, 1
        )]

        //1 right+above of origin
        [TestCase(
            0, 0, //start
            1,-1,  //end
            0, 0, //result
            1,-1
        )]

        //1 left+above of origin
        [TestCase(
            0, 0, //start
            -1,-1,  //end
            0, 0, //result
            -1,-1
        )]


        //Now restarting with the first set but doing a line of 5 instead of 1

        //1 right of origin
        [TestCase(
            0, 0, //start
            5, 0, //end
            0, 0, //result
            1, 0,
            2, 0,
            3, 0,
            4, 0,
            5, 0
        )]

        //5 Left of origin
        [TestCase(
            0, 0, //start
           -5, 0, //end
            0, 0, //result
           -1, 0,
           -2, 0,
           -3, 0,
           -4, 0,
           -5, 0
        )]

        //5 bellow origin
        [TestCase(
            0, 0, //start
            0, 5, //end
            0, 0, //result
            0, 1,
            0, 2,
            0, 3,
            0, 4,
            0, 5
        )]

        //5 above origin
        [TestCase(
            0,  0, //start
            0, -5, //end
            0,  0, //result
            0, -1,
            0, -2,
            0, -3,
            0, -4,
            0, -5
        )]





        public void TestPlotLine(int x1, int y1, int x2, int y2, params int[] resultInts)
        {
            var start = new Vector2i(x1, y1);
            var end = new Vector2i(x2, y2);
            var line = start.PlotLine(end);

            List<Vector2i> resultVectors = new List<Vector2i>();
            for (int i = 0; i < resultInts.Length; i += 2)
            {
                resultVectors.Add(new Vector2i(resultInts[i], resultInts[i+1]));
            }

            Assert.AreEqual(line.ToArray(), resultVectors.ToArray());
        }

    }
}
