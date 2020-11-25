use crate::year2019::intcode_computer::instruction::{Instruction, Mode};
use crate::year2019::intcode_computer::opcode::Opcode;
use std::io::{self, Write, Read};

#[allow(dead_code)]
#[derive(PartialEq, Eq, Clone)]
pub enum State
{
    Running,
    WaitingForInput,
    PushedOutput,
    Break,
    Halt,
}

pub struct Computer
{
    memory: Vec<i64>,

    instruction_pointer: u64,
    relative_base_pointer: u64,
    state: State,

    //I/O
    pub input:  Vec<i64>,
    pub output: Vec<i64>,

    //Settings:
    pub print_disassembly: bool,
    pub print_output: bool,

    pub break_pointer: Option<u64>,
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
            relative_base_pointer: 0,
            state: State::Running,

            input: Vec::new(),
            output: Vec::new(),

            print_disassembly: true,
            print_output: true,
            break_pointer: None,
        }
    }

    pub fn reset_from_str(&mut self, str: &str)
    {
        self.memory.clear();
        for num in str.split(',')
        {
            self.memory.push(num.parse::<i64>().unwrap());
        }
        self.instruction_pointer = 0;
        self.relative_base_pointer = 0;
        self.state = State::Running;

        self.input.clear();
        self.output.clear();
    }

    pub fn from_str(str: &str) -> Self
    {
        let mut computer = Computer::new();
        for num in str.split(',')
        {
            computer.memory.push(num.parse::<i64>().unwrap());
        }
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
    }

    pub fn disassemble_program(&self)
    {
        let mut ptr = 0;
        loop
        {
            let instruction = Instruction::parse(ptr, &self.memory);
            println!("{}", instruction.disassemble(ptr, self.relative_base_pointer, &self.memory));
            ptr += instruction.size as u64;
        }
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
                State::Running => {/*just keep running*/}
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
                    if self.print_output
                    {
                        println!("out: {}", self.output[0]);
                        self.output.remove(0);
                    }
                }
                State::Break =>
                {
                    println!("computer entered break mode. Press any key to continue.");
                    io::stdout().flush().unwrap();

                    //Read so a keypress is required
                    let mut buffer = [0, 10];
                    io::stdin().read(&mut buffer).unwrap();

                    //Clear this breakpoint to prevent a loop
                    self.break_pointer = None;
                    self.state = State::Running;
                }
                State::Halt => return,
            }
        }
    }

    pub fn step(&mut self) -> State
    {
        //Clear any flags recieved previously
        self.state = State::Running;

        match self.break_pointer
        {
            Some(number) =>
            {
                 if self.instruction_pointer == number
                 {
                     self.state = State::Break;
                     return self.state.clone();
                 }
            }
            _ => {}
        }


        let instruction = Instruction::parse(self.instruction_pointer, &self.memory);
        if self.print_disassembly
        {
            println!("{}", instruction.disassemble(self.instruction_pointer, self.relative_base_pointer, &self.memory));
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
                //Notify consumers of step that we pushed an output, do increment the instruction pointer so that we don't loop.
                self.output.push(numbers[0]);
                self.state = State::PushedOutput;
            }
            Opcode::JumpIfTrue =>
            {
                if numbers[0] != 0
                {
                    self.instruction_pointer = numbers[1] as u64;
                    //Exit without incrementing the instruction pointer
                    return self.state.clone();
                }
            }
            Opcode::JumpIfFalse =>
            {
                if numbers[0] == 0
                {
                    self.instruction_pointer = numbers[1] as u64;
                    //Exit without incrementing the instruction pointer
                    return self.state.clone();
                }
            }
            Opcode::LessThan =>
            {
                let mut num = 0;
                if numbers[0] < numbers[1]
                {
                    num = 1;
                }
                self.memory_write(instruction.arguments[2], num);
            }
            Opcode::Equals =>
            {
                let mut num = 0;
                if numbers[0] == numbers[1]
                {
                    num = 1;
                }
                self.memory_write(instruction.arguments[2], num);
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

