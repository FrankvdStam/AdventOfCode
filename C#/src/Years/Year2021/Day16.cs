using System;
using System.Collections.Generic;
using System.Text;
using Years.Utils;

namespace Years.Year2021
{
    public class Day16 : IDay
    {

        public int Day => 16;
        public int Year => 2021;

        public void ProblemOne()
        {
            var binary = Convert.ToString(Convert.ToInt32(Example1, 16), 2);
            var type = ParseType(binary);
            var literal = ParseLiteral(binary);

            ParseOperator(Example3.HexStringToBinaryString());
            var bin = Example4.HexStringToBinaryString();

            Console.WriteLine(literal);
        }

        private int ParseType(string binary)
        {
            var sub = binary.Substring(3, 3);
            return Convert.ToInt32(sub, 2);
        }

        private int ParseLiteral(string binary)
        {
            var literal = binary.Substring(6);

            var amount = literal.Length / 5;
            var result = new StringBuilder();
            for (var i = 0; i < amount; i++)
            {
                var part = literal.Substring((5 * i) + 1, 4);
                result.Append(part);
            }

            return Convert.ToInt32(result.ToString(), 2);
        }

        private void ParseOperator(string binary)
        {
            var subpacketStartIndex = 0;
            if (binary[6] == '0')
            {
                var total_length = Convert.ToInt32(binary.Substring(7, 15), 2);
                subpacketStartIndex = 7 + 15;
            }
            else
            {
                var subpacketCount = Convert.ToInt32(binary.Substring(7, 11), 2);
                subpacketStartIndex = 7 + 11;

                var totalSubpacketLength = binary.Length - 7 - 11;
                var thing = (decimal)totalSubpacketLength / subpacketCount;
                 var packetLength = (int)Math.Floor((decimal) totalSubpacketLength / subpacketCount);
            }
            
        }


        public void ProblemTwo()
        {
        }

        private const string Example1 = @"D2FE28";
        private const string Example2 = @"38006F45291200";
        private const string Example3 = @"EE00D40C823060";
        private const string Example4 = @"8A004A801A8002F478";

        private const string Input = @"4054460802532B12FEE8B180213B19FA5AA77601C010E4EC2571A9EDFE356C7008E7B141898C1F4E50DA7438C011D005E4F6E727B738FC40180CB3ED802323A8C3FED8C4E8844297D88C578C26008E004373BCA6B1C1C99945423798025800D0CFF7DC199C9094E35980253FB50A00D4C401B87104A0C8002171CE31C41201062C01393AE2F5BCF7B6E969F3C553F2F0A10091F2D719C00CD0401A8FB1C6340803308A0947B30056803361006615C468E4200E47E8411D26697FC3F91740094E164DFA0453F46899015002A6E39F3B9802B800D04A24CC763EDBB4AFF923A96ED4BDC01F87329FA491E08180253A4DE0084C5B7F5B978CC410012F9CFA84C93900A5135BD739835F00540010F8BF1D22A0803706E0A47B3009A587E7D5E4D3A59B4C00E9567300AE791E0DCA3C4A32CDBDC4830056639D57C00D4C401C8791162380021108E26C6D991D10082549218CDC671479A97233D43993D70056663FAC630CB44D2E380592FB93C4F40CA7D1A60FE64348039CE0069E5F565697D59424B92AF246AC065DB01812805AD901552004FDB801E200738016403CC000DD2E0053801E600700091A801ED20065E60071801A800AEB00151316450014388010B86105E13980350423F447200436164688A4001E0488AC90FCDF31074929452E7612B151803A200EC398670E8401B82D04E31880390463446520040A44AA71C25653B6F2FE80124C9FF18EDFCA109275A140289CDF7B3AEEB0C954F4B5FC7CD2623E859726FB6E57DA499EA77B6B68E0401D996D9C4292A881803926FB26232A133598A118023400FA4ADADD5A97CEEC0D37696FC0E6009D002A937B459BDA3CC7FFD65200F2E531581AD80230326E11F52DFAEAAA11DCC01091D8BE0039B296AB9CE5B576130053001529BE38CDF1D22C100509298B9950020B309B3098C002F419100226DC";
    }
}