use std::str::FromStr;
use std::fmt::Error;
use crate::year2019::intcode_computer::instruction::Instruction;
use crate::year2019::intcode_computer::enums::Mode;
use crate::year2019::intcode_computer::opcode::Opcode;

#[allow(dead_code)]
pub struct Program
{
    original_data: String,
    data: Vec<i64>,
}

impl FromStr for Program
{
    type Err = Error;

    fn from_str(str: &str) -> Result<Self, Self::Err>
    {
        let mut data = Vec::new();
        for num in str.split(',')
        {
            data.push(num.parse::<i64>().unwrap());
        }

        return Ok(
            Program
            {
                original_data: String::from(str),
                data,
            }
        )
    }
}

impl Program
{
    pub fn clone_program_data(&self) -> Vec<i64>
    {
        return self.data.clone();
    }

    #[allow(dead_code)]
    pub fn disassemble(&self) -> String
    {
        let mut disassembly = String::new();

        let mut instruction_pointer = 0;
        loop
        {
            let instruction = Instruction::parse(&self.data, instruction_pointer);

            //disassemble
            disassembly.push_str(instruction.opcode.to_string().as_str());
            disassembly.push(' ');

            for i in 0..instruction.argument_count
            {
                match instruction.argument_modes[i as usize]
                {
                    Mode::Position =>
                    {
                        disassembly.push('P');
                        disassembly.push_str(instruction.arguments[i as usize].to_string().as_str());
                    }
                    Mode::Immediate =>
                    {
                        disassembly.push('I');
                        disassembly.push_str(instruction.arguments[i as usize].to_string().as_str());
                    }
                    Mode::Relative =>
                    {
                        disassembly.push('R');
                        disassembly.push_str(instruction.arguments[i as usize].to_string().as_str());
                    }
                }
                disassembly.push(' ');
            }
            disassembly.push('\n');
            instruction_pointer += instruction.size as u64;

            //check exit condition
            if instruction.opcode == Opcode::Halt || instruction_pointer as usize >= self.data.len()
            {
                break
            }
        }

        return disassembly;
    }
}