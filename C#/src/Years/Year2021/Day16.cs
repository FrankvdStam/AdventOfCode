using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Years.Utils;

namespace Years.Year2021
{
    

    public class Day16 : IDay
    {
        public enum PacketType
        {
            Sum = 0,
            Product = 1,
            Minimum = 2,
            Maximum = 3,
            Literal = 4,
            GreaterThan = 5,
            SmallerThan = 6,
            EqualTo = 7,
        }


        public class Packet
        {
            public int SumVersion()
            {
                return SubPackets.Sum(i => i.SumVersion()) + Version;
            }

            public long GetValue()
            {
                switch (Type)
                {
                    case PacketType.Sum:
                        return SubPackets.Sum(i => i.GetValue());

                    case PacketType.Product:
                        return SubPackets.Select(i => i.GetValue()).Aggregate((total, next) => total * next);

                    case PacketType.Minimum:
                        return SubPackets.Min(i => i.GetValue());

                    case PacketType.Maximum:
                        return SubPackets.Max(i => i.GetValue());

                    case PacketType.Literal:
                        return LiteralValue.Value;

                    case PacketType.GreaterThan:
                        if (SubPackets[0].GetValue() > SubPackets[1].GetValue())
                        {
                            return 1;
                        }
                        return 0;

                    case PacketType.SmallerThan:
                        if (SubPackets[0].GetValue() < SubPackets[1].GetValue())
                        {
                            return 1;
                        }
                        return 0;

                    case PacketType.EqualTo:
                        if (SubPackets[0].GetValue() == SubPackets[1].GetValue())
                        {
                            return 1;
                        }
                        return 0;
                }

                throw new Exception("Unsupported type");
            }


            public int Version;
            public PacketType Type;
            public long? LiteralValue;
            public readonly List<Packet> SubPackets = new List<Packet>();

            public override string ToString()
            {
                return $"v{Version} t{Type} l{LiteralValue} c{SubPackets.Count}";
            }
        }




        public int Day => 16;
        public int Year => 2021;

        public void ProblemOne()
        {
            var packet = Parse(Input.HexStringToBinaryString(), out _);
            var versionSum = packet.SumVersion();
            Console.WriteLine(versionSum);
        }


        public void ProblemTwo()
        {
            var packet = Parse(Input.HexStringToBinaryString(), out _);
            var value = packet.GetValue();
            Console.WriteLine(value);
        }


        #region Parsing ==================================================================================================================

        public static Packet Parse(string binary, out int length)
        {
            length = 6;
            var packet = new Packet();

            packet.Version = ParseVersion(binary);
            packet.Type = ParseType(binary);
            
            //string log = $"V{packet.Version} t{packet.Type}";

            if (packet.Type == PacketType.Literal)
            {
                packet.LiteralValue = ParseLiteral(binary, out int literalLength);
                //log += $" l{packet.LiteralValue}";
                //Console.WriteLine(log);
                length += literalLength;
            }
            else
            {
                length += 1;

                if (binary[6] == '0')
                {
                    length += 15;
                    var totalLength = Convert.ToInt32(binary.Substring(7, 15), 2);
                    //log += $" 0 length: {totalLength}";
                    //Console.WriteLine(log);

                    //Don't know how many packets there are, but we know the total packet length
                    var subPacketsBinary = binary.Substring(7 + 15);

                    var subPacketIndex = 0;
                    while (subPacketIndex < totalLength)
                    {
                        var str = subPacketsBinary.Substring(subPacketIndex);
                        packet.SubPackets.Add(Parse(subPacketsBinary.Substring(subPacketIndex), out int packetLength));
                        subPacketIndex += packetLength;
                    }
                    length += totalLength;
                }
                else
                {
                    length += 11;
                    var subpacketCount = Convert.ToInt32(binary.Substring(7, 11), 2);
                    //log += $" 1 count: {subpacketCount}";
                    //Console.WriteLine(log);
                    var subPacketsBinary = binary.Substring(7 + 11);

                    var subPacketIndex = 0;
                    for (var i = 0; i < subpacketCount; i++)
                    {
                        var str = subPacketsBinary.Substring(subPacketIndex);
                        packet.SubPackets.Add(Parse(subPacketsBinary.Substring(subPacketIndex), out int packetLength));
                        subPacketIndex += packetLength;
                    }
                    length += subPacketIndex;
                }
            }

            return packet;
        }

        private static int ParseVersion(string binary)
        {
            var sub = binary.Substring(0, 3);
            return Convert.ToInt32(sub, 2);
        }

        private static PacketType ParseType(string binary)
        {
            var sub = binary.Substring(3, 3);
            return (PacketType)Convert.ToInt32(sub, 2);
        }

        private static long ParseLiteral(string binary, out int length)
        {
            var literal = binary.Substring(6);

            //Find the batch of 5 bits that starts with a 0, signaling the last batch
            var index = 0;
            while (literal[index] != '0')
            {
                index += 5;
            }
            index += 5;//off by one error

            var amount = index / 5;
            var result = new StringBuilder();
            for (var i = 0; i < amount; i++)
            {
                var part = literal.Substring((5 * i) + 1, 4);
                result.Append(part);
            }

            length = index;
            return Convert.ToInt64(result.ToString(), 2);
        }

        #endregion
        

        

        private const string Example1 = @"D2FE28";
        private const string Example2 = @"38006F45291200";
        private const string Example3 = @"EE00D40C823060";
        private const string Example4 = @"8A004A801A8002F478";
        private const string Example5 = @"620080001611562C8802118E34";
        private const string Example6 = @"C0015000016115A2E0802F182340";
        private const string Example7 = @"A0016C880162017C3686B18A3D4780";

        private const string Input = @"4054460802532B12FEE8B180213B19FA5AA77601C010E4EC2571A9EDFE356C7008E7B141898C1F4E50DA7438C011D005E4F6E727B738FC40180CB3ED802323A8C3FED8C4E8844297D88C578C26008E004373BCA6B1C1C99945423798025800D0CFF7DC199C9094E35980253FB50A00D4C401B87104A0C8002171CE31C41201062C01393AE2F5BCF7B6E969F3C553F2F0A10091F2D719C00CD0401A8FB1C6340803308A0947B30056803361006615C468E4200E47E8411D26697FC3F91740094E164DFA0453F46899015002A6E39F3B9802B800D04A24CC763EDBB4AFF923A96ED4BDC01F87329FA491E08180253A4DE0084C5B7F5B978CC410012F9CFA84C93900A5135BD739835F00540010F8BF1D22A0803706E0A47B3009A587E7D5E4D3A59B4C00E9567300AE791E0DCA3C4A32CDBDC4830056639D57C00D4C401C8791162380021108E26C6D991D10082549218CDC671479A97233D43993D70056663FAC630CB44D2E380592FB93C4F40CA7D1A60FE64348039CE0069E5F565697D59424B92AF246AC065DB01812805AD901552004FDB801E200738016403CC000DD2E0053801E600700091A801ED20065E60071801A800AEB00151316450014388010B86105E13980350423F447200436164688A4001E0488AC90FCDF31074929452E7612B151803A200EC398670E8401B82D04E31880390463446520040A44AA71C25653B6F2FE80124C9FF18EDFCA109275A140289CDF7B3AEEB0C954F4B5FC7CD2623E859726FB6E57DA499EA77B6B68E0401D996D9C4292A881803926FB26232A133598A118023400FA4ADADD5A97CEEC0D37696FC0E6009D002A937B459BDA3CC7FFD65200F2E531581AD80230326E11F52DFAEAAA11DCC01091D8BE0039B296AB9CE5B576130053001529BE38CDF1D22C100509298B9950020B309B3098C002F419100226DC";
    }
}