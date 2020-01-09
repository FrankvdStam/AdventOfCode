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
        [TestCase("1,9,10,3,2,3,11,0,99,30,40,50"   , "3500,9,10,70,2,3,11,0,99,30,40,50"   )]
        [TestCase("1,0,0,0,99"                      , "2,0,0,0,99"                          )]
        [TestCase("2,3,0,3,99"                      , "2,3,0,6,99"                          )]
        [TestCase("2,4,4,5,99,0"                    , "2,4,4,5,99,9801"                     )]
        [TestCase("1,1,1,4,99,5,6,0,99"             , "30,1,1,4,2,5,6,0,99"                 )]
        public void TestExampleProgram(string input, string result)
        {
            IntCodeComputer computer = new IntCodeComputer(input);
            computer.Run();
            Assert.AreEqual(result, computer.ToString());
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
