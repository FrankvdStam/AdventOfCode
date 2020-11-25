use crate::intcode_computer::program::Program;
use crate::intcode_computer::enums::*;
use crate::year2019::intcode_computer::opcode::Opcode;
use crate::year2019::intcode_computer::instruction::Instruction;
use std::fs::read_to_string;
use std::str::FromStr;

pub struct Computer
{
    memory: Vec<i64>,

    instruction_pointer: u64,
    state: State,
}

impl Computer
{
    //Constructors ============================================================================================================================
    fn new() -> Self
    {
        Computer
        {
            memory: Vec::new(),
            instruction_pointer: 0,
            state: State::Running,
        }
    }

    pub fn from_str(str: &str) -> Self
    {
        let program = Program::from_str(str).unwrap();
        return Computer::from_program(&program);
    }

    pub fn from_program(program: &Program) -> Self
    {
        let mut computer = Computer::new();
        computer.memory = program.clone_program_data();
        return computer;
    }
    //============================================================================================================================

    //Helpers ============================================================================================================================
    pub fn memory_to_string(&self) -> String
    {
        return self.memory.iter().map(|x| x.to_string()).collect::<Vec<String>>().join(",");
    }

    fn memory_read(&self, mode: &Mode, value: i64) -> i64
    {
        match mode
        {
            Mode::Immediate => return value,
            Mode::Position => return self.memory[value as usize],
            _ => panic!("Memory read for this mode not implemented.")
        }
    }

    fn memory_write(&mut self, location: i64, value: i64)
    {
        self.memory[location as usize] = value;
        //match mode
        //{
        //    Mode::Position =>
        //    _ => panic!("Memory read for this mode not implemented.")
        //}
    }
    //============================================================================================================================

    //Code execution ============================================================================================================================
    pub fn run(&mut self)
    {
        while self.state == State::Running
        {
            self.step();
        }
    }

    pub fn step(&mut self)
    {
        let instruction = Instruction::parse(&self.memory, self.instruction_pointer);

        //Instead of fetching the arguments from memory at every twist and turn, we'll fetch them beforehand
        //we will only be fetching the correct amount else we might be reading outside of our memory
        //The variables must always exist
        let mut numbers: Vec<i64> = vec!{0,0,0};
        if instruction.opcode != Opcode::Halt
        {
            for i in 0..(instruction.size-1) as usize
            {
                numbers[i] = self.memory_read(&instruction.argument_modes[i], instruction.arguments[i]);
            }
        }



        match instruction.opcode
        {
            Opcode::Add =>
            {
                self.memory_write(numbers[2], numbers[0] + numbers[1]);
            }
            Opcode::Multiply =>
            {
                self.memory_write(numbers[2], numbers[0] * numbers[1]);
            }



            Opcode::Halt =>
            {
                self.state = State::Halt;
            }
            _ => panic!("Opcode not implemented."),
        }
        self.instruction_pointer += instruction.size as u64;
    }
    //============================================================================================================================
}





#[cfg(test)]
mod computer_tests
{
    use crate::year2019::intcode_computer::computer::Computer;

    static INPUT_01: &'static str = "1,0,0,3,99";
    static EXPECTED_01: &'static str = "1,0,0,2,99";

    fn test_program(program: &str, expected: &str)
    {
        let mut computer = Computer::from_str(program);
        computer.run();
        let result = computer.memory_to_string();
        assert_eq!(expected, result.as_str());
    }


    #[test]
    fn program_outputs()
    {
        test_program(INPUT_01, EXPECTED_01);
    }
}




