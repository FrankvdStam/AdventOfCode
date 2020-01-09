using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Shared;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class IntCodeComputerTests
    {

        #region Examples
        [TestCase("1,9,10,3,2,3,11,0,99,30,40,50", "3500,9,10,70,2,3,11,0,99,30,40,50")]
        [TestCase("1,0,0,0,99", "2,0,0,0,99")]
        [TestCase("2,3,0,3,99", "2,3,0,6,99")]
        [TestCase("2,4,4,5,99,0", "2,4,4,5,99,9801")]
        [TestCase("1,1,1,4,99,5,6,0,99", "30,1,1,4,2,5,6,0,99")]
        public void TestExampleProgram(string input, string result)
        {
            IntCodeComputer computer = new IntCodeComputer();

            var inputProgram = IntCodeComputer.ParseProgram(input);
            var resultProgram = IntCodeComputer.ParseProgram(result);

            computer.Program = inputProgram;
            computer.Run();

            string error = "";

            if (inputProgram.Count != resultProgram.Count)
            {
                error += $"input: {input}\r\nresult:{result}\r\n\r\nResulting programs have different sizes: {inputProgram.Count} != {resultProgram.Count}\r\n";
            }

            for (int i = 0; i < inputProgram.Count; i++)
            {
                try//ignore out of bounds exception from previous check
                {

                    if (inputProgram[i] != resultProgram[i])
                    {
                        error += $"address {i}: {inputProgram[i]} != {resultProgram[i]}";
                    }
                }
                catch { }
            }

            if (error != "")
            {
                Assert.Fail(error);
            }
            Assert.Pass();
        }
        #endregion


        [Test]
        public void DecodeOpcode()
        {
            var opcode = IntCodeComputer.DecodeOpcode(1002);
            Assert.AreEqual(Instruction.Multiply, opcode.instruction);
            Assert.AreEqual(InstructionMode.Position, opcode.mode1);
            Assert.AreEqual(InstructionMode.Immediate, opcode.mode2);
            Assert.AreEqual(InstructionMode.Position, opcode.mode3);
        }


        

    }
}
