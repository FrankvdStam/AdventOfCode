
use crate::utils::math::digits;
use std::convert::From;
use crate::year2019::intcode_computer::opcode::Opcode;
use crate::year2019::intcode_computer::computer::Computer;

#[derive(PartialEq, Eq)]
pub enum Mode
{
    Position = 0,
    Immediate = 1,
    Relative = 2,
}

impl From<i64> for Mode
{
    fn from(num: i64) -> Self
    {
        match num
        {
            0 => return Mode::Position,
            1 => return Mode::Immediate,
            2 => return Mode::Relative,
            _ => panic!("Failed to parse mode {}", num)
        }
    }
}

pub struct Instruction
{
    pub opcode: Opcode,
    pub size: u8,
    pub argument_count: u8,
    pub arguments: Vec<i64>,
    pub argument_modes: Vec<Mode>,
    pub write_address: u64,
}

impl Instruction
{
    fn new() -> Self
    {
        Instruction
        {
            opcode: Opcode::Halt,
            size: 2,
            argument_count: 0,
            arguments: Vec::new(),
            argument_modes: Vec::new(),
            write_address: 0,
        }
    }

    pub fn parse(instruction_pointer: u64, program: &Vec<i64>) -> Self
    {
        let mut instruction = Instruction::new();

        //Parse the opcode by splitting the digits
        let num: i64 = program[instruction_pointer as usize];
        let mut digits: Vec<i64> = digits(num).collect();
        digits.reverse();

        //Opcode can be width 2 in case of the halt opcode.
        if digits.len() >= 2 && digits[0] == 9 && digits[1] == 9
        {
            //halt, no arguments
            instruction.opcode = Opcode::Halt;
            instruction.size = 2;
        }
        //else instruction data is always 1 while it might take 2 digits (trailing 0)
        else
        {
            instruction.opcode = Opcode::from(digits[0]);


            //Remove the opcode itself so that only arguments remain.
            //If there are any modes specified, we'll have to remove 2 digits.
            digits.remove(0);
            if digits.len() > 0
            {
                digits.remove(0);
            }


            //Figure out how many arguments to expect
            let argument_count = instruction.opcode.get_argument_count();
            instruction.size = (argument_count + 1) as u8;

            //Always fill in the blanks of the opcode
            while digits.len() < argument_count
            {
                digits.push(0);
            }

            //Parse the modes of the arguments and the numeric values
            instruction.argument_count = digits.len() as u8;
            for i in 0..digits.len()
            {
                instruction.argument_modes.push(Mode::from(digits[i]));
                let value = program[(instruction_pointer + i as u64 +1u64) as usize];
                instruction.arguments.push(value);
            }
        }

        return instruction;
    }

    #[allow(dead_code)]
    pub fn disassemble(&self, instruction_pointer: u64, relative_base_pointer: i64, computer: &Computer) -> String
    {
        let mut disassembly = String::new();
        disassembly.push_str(format!("{: <8}", instruction_pointer.to_string()).as_str());

        //disassemble
        disassembly.push_str(format!("{: <8}", self.opcode.to_string()).as_str());

        for i in 0..self.argument_count
        {
            let mut argument = String::new();
            match self.argument_modes[i as usize]
            {
                Mode::Position =>
                {
                    argument.push('[');
                    argument.push_str(self.arguments[i as usize].to_string().as_str());
                    argument.push(']');
                    argument.push_str( computer.memory_read(&Mode::Position, self.arguments[i as usize]).to_string().as_str());
                }
                Mode::Immediate =>
                {
                    argument.push_str(self.arguments[i as usize].to_string().as_str());
                }
                Mode::Relative =>
                {
                    argument.push_str(self.arguments[i as usize].to_string().as_str());
                    argument.push('+');
                    argument.push_str(relative_base_pointer.to_string().as_str());
                    argument.push('*');
                    argument.push(' ');
                    argument.push_str(computer.memory_read(&Mode::Position, self.arguments[i as usize] + relative_base_pointer as i64).to_string().as_str());
                }
            }
            disassembly.push_str(format!("{: <12}", argument).as_str());
            disassembly.push(' ');
        }

        return disassembly;
    }
}