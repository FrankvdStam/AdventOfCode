use crate::intcode_computer::program::Program;
use crate::intcode_computer::enums::*;
use crate::year2019::intcode_computer::opcode::Opcode;
use crate::year2019::intcode_computer::instruction::Instruction;

pub struct Computer
{
    memory: Vec<i64>,

    instruction_pointer: u64,
    state: State,
}

impl Computer
{
    pub fn new() -> Self
    {
        Computer
        {
            memory: Vec::new(),
            instruction_pointer: 0,
            state: State::Running,
        }
    }

    pub fn from_program(program: &Program) -> Self
    {
        let mut computer = Computer::new();
        computer.memory = program.clone_program_data();
        return computer;
    }

    pub fn step(&mut self)
    {
        let instruction = Instruction::parse(&self.memory, self.instruction_pointer);
    }
}





