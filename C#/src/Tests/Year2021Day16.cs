using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Years.Utils;

namespace Tests
{
    [TestFixture]
    public class Year2021Day16
    {
        [TestCase("8A004A801A8002F478"            , 16)]
        [TestCase("620080001611562C8802118E34"    , 12)]
        [TestCase("C0015000016115A2E0802F182340"  , 23)]
        [TestCase("A0016C880162017C3686B18A3D4780", 31)]
        public void SumVersion(string hex, int expectedSum)
        {
            var packet = Years.Year2021.Day16.Parse(hex.HexStringToBinaryString(), out _);
            var versionSum = packet.SumVersion();
            Assert.AreEqual(expectedSum, versionSum);
        }


        [TestCase("C200B40A82"                  , 3)]
        [TestCase("04005AC33890"                , 54)]
        [TestCase("880086C3E88112"              , 7)]
        [TestCase("CE00C43D881120"              , 9)]
        [TestCase("D8005AC2A8F0"                , 1)]
        [TestCase("F600BC2D8F"                  , 0)]
        [TestCase("9C005AC2F8F0"                , 0)]
        [TestCase("9C0141080250320F1802104A08"  , 1)]
        public void FindOuterValue(string hex, long expectedValue)
        {
            var packet = Years.Year2021.Day16.Parse(hex.HexStringToBinaryString(), out _);
            var value = packet.GetValue();
            Assert.AreEqual(expectedValue, value);
        }
    }
}
