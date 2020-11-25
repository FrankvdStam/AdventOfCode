use crate::intcode_computer::program::Program;
use crate::intcode_computer::enums::*;
use crate::year2019::intcode_computer::opcode::Opcode;
use crate::year2019::intcode_computer::instruction::Instruction;
use std::str::FromStr;

pub struct Computer
{
    memory: Vec<i64>,

    instruction_pointer: u64,
    state: State,

    //Settings:
    pub print_disassembly: bool,
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
            print_disassembly: false,
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
    #[allow(dead_code)]
    pub fn memory_to_string(&self) -> String
    {
        return self.memory.iter().map(|x| x.to_string()).collect::<Vec<String>>().join(",");
    }

    pub fn memory_read(&self, mode: &Mode, value: i64) -> i64
    {
        match mode
        {
            Mode::Immediate => return value,
            Mode::Position => return self.memory[value as usize],
            _ => panic!("Memory read for this mode not implemented.")
        }
    }

    pub fn memory_write(&mut self, location: i64, value: i64)
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

        if self.print_disassembly
        {
            println!("{}", instruction.disassemble());
        }

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

        //Note: when writing to memory, the raw value from the instruction is used.
        //That is because the output location should always be seen as immediate even if it's mode is position.
        //Take this for example: 1,0,0,0,99
        //Will try to read the 3rd 0 and write the result to location 1. The result should be in location 0.

        match instruction.opcode
        {
            Opcode::Add =>
            {
                self.memory_write(instruction.arguments[2], numbers[0] + numbers[1]);
            }
            Opcode::Multiply =>
            {
                self.memory_write(instruction.arguments[2], numbers[0] * numbers[1]);
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

    const AMOUNT_OF_TESTS: usize = 6;

    const PROGRAMS: [&'static str; AMOUNT_OF_TESTS] = [
        "1,0,0,3,99",
        "1,0,0,0,99",
        "2,3,0,3,99",
        "2,4,4,5,99,0",
        "1,1,1,4,99,5,6,0,99",
        "1,9,10,3,2,3,11,0,99,30,40,50",
    ];

    const RESULTS: [&'static str; AMOUNT_OF_TESTS] = [
        "1,0,0,2,99",
        "2,0,0,0,99",
        "2,3,0,6,99",
        "2,4,4,5,99,9801",
        "30,1,1,4,2,5,6,0,99",
        "3500,9,10,70,2,3,11,0,99,30,40,50",
    ];




    fn test_program(program: &str, expected: &str)
    {
        let mut computer = Computer::from_str(program);
        computer.run();
        let result = computer.memory_to_string();
        assert_eq!(expected, result.as_str(), "IntCodeComputer test failed.\nExpected: {}\nResult:   {}", expected, result);
    }


    #[test]
    fn program_outputs()
    {
        for i in 0..AMOUNT_OF_TESTS
        {
            test_program(PROGRAMS[i], RESULTS[i]);
        }
    }
}





