use crate::year2019::intcode_computer::opcode::Opcode;
use crate::year2019::intcode_computer::enums::Mode;
use crate::utils::math::digits;

pub struct Instruction
{
    pub opcode: Opcode,
    pub size: u8,
    pub argument_count: u8,
    pub arguments: Vec<i64>,
    pub argument_modes: Vec<Mode>,
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
        }
    }

    pub fn parse(program: &Vec<i64>, instruction_pointer: u64) -> Self
    {
        let mut instruction = Instruction::new();

        //Parse the opcode by splitting the digits
        let mut num: i64 = program[instruction_pointer as usize];
        let mut digits: Vec<i64> = digits(num).collect();
        digits.reverse();

        //Opcode can be width 2 in case of the halt opcode.
        if digits.len() >= 2 && digits[0] == 9 && digits[1] == 9
        {
            //halt, no arguments
            instruction.opcode = Opcode::Halt;
            instruction.size = 2;
        }
        //else instruction width is always 1
        else
        {
            instruction.opcode = Opcode::from(digits[0]);
            digits.remove(0);

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
}