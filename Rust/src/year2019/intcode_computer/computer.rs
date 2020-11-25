use crate::year2019::intcode_computer::instruction::{Instruction, State, Mode};
use crate::year2019::intcode_computer::opcode::Opcode;
use std::io::{self, Write};


pub struct Computer
{
    memory: Vec<i64>,

    instruction_pointer: u64,
    state: State,

    //I/O
    pub input:  Vec<i64>,
    pub output: Vec<i64>,

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

            input: Vec::new(),
            output: Vec::new(),

            print_disassembly: true,
        }
    }

    pub fn from_str(str: &str) -> Self
    {
        let mut computer = Computer::new();

        let mut data = Vec::new();
        for num in str.split(',')
        {
            data.push(num.parse::<i64>().unwrap());
        }
        computer.memory = data;

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
        loop
        {
            let state = self.step();
            match state
            {
                State::WaitingForInput =>
                {
                    print!("in: ");
                    io::stdout().flush().unwrap();
                    let mut buffer = String::new();
                    io::stdin().read_line(&mut buffer).unwrap();
                    let num = buffer.trim().parse::<i64>().unwrap();
                    self.input.push(num);
                    println!();
                }
                State::PushedOutput =>
                {
                    println!("out: {}", self.output[0]);
                    self.output.remove(0);
                }
                State::Halt => return,

                //In all other cases just keep running
                _ => {}
            }
        }
    }

    //Breaks out on input
    pub fn step(&mut self) -> State
    {
        //Clear any flags recieved previously
        self.state = State::Running;

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
            Opcode::Input =>
            {
                if self.input.len() > 0
                {
                    let input = self.input[0];
                    //Erase the value we just used as input
                    self.input.remove(0);
                    self.memory_write(instruction.arguments[0], input);
                }
                else
                {
                    //We have no input: Do not increment instruction pointer, break out so that input can be supplied
                    self.state = State::WaitingForInput;
                    return self.state.clone();
                }
            }
            Opcode::Output =>
            {
                self.output.push(numbers[0]);
                //Notify consumers of step that we pushed an output, do increment the instruction pointer so that we don't loop.
                self.state = State::PushedOutput;
            }

            Opcode::Halt =>
            {
                self.state = State::Halt;
            }
            _ => panic!("Opcode not implemented."),
        }
        self.instruction_pointer += instruction.size as u64;
        return self.state.clone();
    }
    //============================================================================================================================
}





#[cfg(test)]
mod computer_tests
{
    use crate::year2019::intcode_computer::computer::Computer;

    const AMOUNT_OF_TESTS: usize = 10;

    const PROGRAMS: [&'static str; AMOUNT_OF_TESTS] = [
        "1,0,0,3,99",
        "1,0,0,0,99",
        "2,3,0,3,99",
        "2,4,4,5,99,0",
        "1,1,1,4,99,5,6,0,99",
        "1,9,10,3,2,3,11,0,99,30,40,50",
        "1002,4,3,4,33",
        "1101,100,-1,4,0",
        "1,12,2,3,1,1,2,3,1,3,4,3,1,5,0,3,2,6,1,19,2,19,13,23,1,23,10,27,1,13,27,31,2,31,10,35,1,35,9,39,1,39,13,43,1,13,43,47,1,47,13,51,1,13,51,55,1,5,55,59,2,10,59,63,1,9,63,67,1,6,67,71,2,71,13,75,2,75,13,79,1,79,9,83,2,83,10,87,1,9,87,91,1,6,91,95,1,95,10,99,1,99,13,103,1,13,103,107,2,13,107,111,1,111,9,115,2,115,10,119,1,119,5,123,1,123,2,127,1,127,5,0,99,2,14,0,0",
        "1,60,86,3,1,1,2,3,1,3,4,3,1,5,0,3,2,6,1,19,2,19,13,23,1,23,10,27,1,13,27,31,2,31,10,35,1,35,9,39,1,39,13,43,1,13,43,47,1,47,13,51,1,13,51,55,1,5,55,59,2,10,59,63,1,9,63,67,1,6,67,71,2,71,13,75,2,75,13,79,1,79,9,83,2,83,10,87,1,9,87,91,1,6,91,95,1,95,10,99,1,99,13,103,1,13,103,107,2,13,107,111,1,111,9,115,2,115,10,119,1,119,5,123,1,123,2,127,1,127,5,0,99,2,14,0,0",
    ];

    const RESULTS: [&'static str; AMOUNT_OF_TESTS] = [
        "1,0,0,2,99",
        "2,0,0,0,99",
        "2,3,0,6,99",
        "2,4,4,5,99,9801",
        "30,1,1,4,2,5,6,0,99",
        "3500,9,10,70,2,3,11,0,99,30,40,50",
        "1002,4,3,4,99",
        "1101,100,-1,4,99",
        "4330636,12,2,2,1,1,2,3,1,3,4,3,1,5,0,3,2,6,1,24,2,19,13,120,1,23,10,124,1,13,27,129,2,31,10,516,1,35,9,519,1,39,13,524,1,13,43,529,1,47,13,534,1,13,51,539,1,5,55,540,2,10,59,2160,1,9,63,2163,1,6,67,2165,2,71,13,10825,2,75,13,54125,1,79,9,54128,2,83,10,216512,1,9,87,216515,1,6,91,216517,1,95,10,216521,1,99,13,216526,1,13,103,216531,2,13,107,1082655,1,111,9,1082658,2,115,10,4330632,1,119,5,4330633,1,123,2,4330635,1,127,5,0,99,2,14,0,0",
        "19690720,60,86,2,1,1,2,3,1,3,4,3,1,5,0,3,2,6,1,120,2,19,13,600,1,23,10,604,1,13,27,609,2,31,10,2436,1,35,9,2439,1,39,13,2444,1,13,43,2449,1,47,13,2454,1,13,51,2459,1,5,55,2460,2,10,59,9840,1,9,63,9843,1,6,67,9845,2,71,13,49225,2,75,13,246125,1,79,9,246128,2,83,10,984512,1,9,87,984515,1,6,91,984517,1,95,10,984521,1,99,13,984526,1,13,103,984531,2,13,107,4922655,1,111,9,4922658,2,115,10,19690632,1,119,5,19690633,1,123,2,19690719,1,127,5,0,99,2,14,0,0",
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
            println!("Running int code computer test {}", i);
            test_program(PROGRAMS[i], RESULTS[i]);
        }
    }
}





