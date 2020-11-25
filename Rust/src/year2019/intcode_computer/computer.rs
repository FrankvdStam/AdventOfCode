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
                    self.instruction_pointer = instruction.arguments[1] as u64;
                    //Exit without incrementing the instruction pointer
                    return self.state.clone();
                }
            }
            Opcode::JumpIfFalse =>
            {
                if numbers[0] == 0
                {
                    self.instruction_pointer = instruction.arguments[1] as u64;
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





#[cfg(test)]
mod computer_tests
{
    use crate::year2019::intcode_computer::computer::Computer;



    struct TestCase
    {
        pub program: String,
        pub result_memory: String,
        pub inputs: String,
        pub outputs: String,
    }

    impl TestCase
    {
        //These names are padded on the right so that they show up nicely in bellow list.
        pub fn new(program_aaaaa: &str, result_memory: &str, inputs_: &str, outputs: &str) -> Self
        {
            TestCase
            {
                program: program_aaaaa.to_string(),
                result_memory: result_memory.to_string(),
                inputs: inputs_.to_string(),
                outputs: outputs.to_string(),
            }
        }
    }




    fn test_program(program: &str, expected: &str, inputs: &str, outputs: &str)
    {
        let mut computer = Computer::from_str(program);
        computer.print_output = false;
        computer.print_disassembly = false;

        //Add any potential inputs
        if inputs.len() > 0
        {
            for num in inputs.split(",")
            {
                computer.input.push(num.parse::<i64>().unwrap());
            }
        }

        computer.run();
        let result = computer.memory_to_string();

        let output_str = computer.output.iter().map(|x| x.to_string()).collect::<Vec<String>>().join(",");



        assert_eq!(expected, result, "ICC memory comparison failed.\nExpected: {}\nResult:   {}", expected, result);
        assert_eq!(outputs, output_str, "ICC output comparison failed.\nExpected: {}\nResult:   {}", expected, result);
    }


    #[test]
    fn program_tests()
    {
        let test_cases = vec! {
            //basic tests
            TestCase::new(
                "1,0,0,3,99",
                "1,0,0,2,99",
                "",
                ""
            ),
            TestCase::new(
                "1,0,0,0,99",
                "2,0,0,0,99",
                "",
                ""
            ),
            TestCase::new(
                "2,3,0,3,99",
                "2,3,0,6,99",
                "",
                ""
            ),
            TestCase::new(
                "2,4,4,5,99,0",
                "2,4,4,5,99,9801",
                "",
                ""
            ),
            TestCase::new(
                "1,1,1,4,99,5,6,0,99",
                "30,1,1,4,2,5,6,0,99",
                "",
                ""
            ),
            TestCase::new(
                "1,9,10,3,2,3,11,0,99,30,40,50",
                "3500,9,10,70,2,3,11,0,99,30,40,50",
                "",
                ""
            ),
            TestCase::new(
                "1002,4,3,4,33",
                "1002,4,3,4,99",
                "",
                ""
            ),
            TestCase::new(
                "1101,100,-1,4,0",
                "1101,100,-1,4,99",
                "",
                ""
            ),

            //Testing day02
            TestCase::new(
                "1,12,2,3,1,1,2,3,1,3,4,3,1,5,0,3,2,6,1,19,2,19,13,23,1,23,10,27,1,13,27,31,2,31,10,35,1,35,9,39,1,39,13,43,1,13,43,47,1,47,13,51,1,13,51,55,1,5,55,59,2,10,59,63,1,9,63,67,1,6,67,71,2,71,13,75,2,75,13,79,1,79,9,83,2,83,10,87,1,9,87,91,1,6,91,95,1,95,10,99,1,99,13,103,1,13,103,107,2,13,107,111,1,111,9,115,2,115,10,119,1,119,5,123,1,123,2,127,1,127,5,0,99,2,14,0,0",
                "4330636,12,2,2,1,1,2,3,1,3,4,3,1,5,0,3,2,6,1,24,2,19,13,120,1,23,10,124,1,13,27,129,2,31,10,516,1,35,9,519,1,39,13,524,1,13,43,529,1,47,13,534,1,13,51,539,1,5,55,540,2,10,59,2160,1,9,63,2163,1,6,67,2165,2,71,13,10825,2,75,13,54125,1,79,9,54128,2,83,10,216512,1,9,87,216515,1,6,91,216517,1,95,10,216521,1,99,13,216526,1,13,103,216531,2,13,107,1082655,1,111,9,1082658,2,115,10,4330632,1,119,5,4330633,1,123,2,4330635,1,127,5,0,99,2,14,0,0",
                "",
                ""
            ),
            TestCase::new(
                "1,60,86,3,1,1,2,3,1,3,4,3,1,5,0,3,2,6,1,19,2,19,13,23,1,23,10,27,1,13,27,31,2,31,10,35,1,35,9,39,1,39,13,43,1,13,43,47,1,47,13,51,1,13,51,55,1,5,55,59,2,10,59,63,1,9,63,67,1,6,67,71,2,71,13,75,2,75,13,79,1,79,9,83,2,83,10,87,1,9,87,91,1,6,91,95,1,95,10,99,1,99,13,103,1,13,103,107,2,13,107,111,1,111,9,115,2,115,10,119,1,119,5,123,1,123,2,127,1,127,5,0,99,2,14,0,0",
                "19690720,60,86,2,1,1,2,3,1,3,4,3,1,5,0,3,2,6,1,120,2,19,13,600,1,23,10,604,1,13,27,609,2,31,10,2436,1,35,9,2439,1,39,13,2444,1,13,43,2449,1,47,13,2454,1,13,51,2459,1,5,55,2460,2,10,59,9840,1,9,63,9843,1,6,67,9845,2,71,13,49225,2,75,13,246125,1,79,9,246128,2,83,10,984512,1,9,87,984515,1,6,91,984517,1,95,10,984521,1,99,13,984526,1,13,103,984531,2,13,107,4922655,1,111,9,4922658,2,115,10,19690632,1,119,5,19690633,1,123,2,19690719,1,127,5,0,99,2,14,0,0",
                "",
                ""
            ),

            //Testing jumps
            TestCase::new(
                "3,9,8,9,10,9,4,9,99,-1,8",
                "3,9,8,9,10,9,4,9,99,0,8",
                "1",
                "0"
            ),
            TestCase::new(
                "3,9,8,9,10,9,4,9,99,-1,8",
                "3,9,8,9,10,9,4,9,99,1,8",
                "8",
                "1"
            ),
            TestCase::new(
                "3,9,7,9,10,9,4,9,99,-1,8",
                "3,9,7,9,10,9,4,9,99,1,8",
                "1",
                "1"
            ),
            TestCase::new(
                "3,9,7,9,10,9,4,9,99,-1,8",
                "3,9,7,9,10,9,4,9,99,0,8",
                "8",
                "0"
            ),
            TestCase::new(
                "3,3,1108,-1,8,3,4,3,99",
                "3,3,1108,0,8,3,4,3,99",
                "1",
                "0"
            ),
            TestCase::new(
                "3,3,1108,-1,8,3,4,3,99",
                "3,3,1108,1,8,3,4,3,99",
                "8",
                "1"
            ),
            TestCase::new(
                "3,3,1107,-1,8,3,4,3,99",
                "3,3,1107,1,8,3,4,3,99",
                "1",
                "1"
            ),
            TestCase::new(
                "3,3,1107,-1,8,3,4,3,99",
                "3,3,1107,0,8,3,4,3,99",
                "8",
                "0"
            ),

            //Testing day05
            TestCase::new(
                "3,225,1,225,6,6,1100,1,238,225,104,0,1101,9,90,224,1001,224,-99,224,4,224,102,8,223,223,1001,224,6,224,1,223,224,223,1102,26,62,225,1101,11,75,225,1101,90,43,225,2,70,35,224,101,-1716,224,224,4,224,1002,223,8,223,101,4,224,224,1,223,224,223,1101,94,66,225,1102,65,89,225,101,53,144,224,101,-134,224,224,4,224,1002,223,8,223,1001,224,5,224,1,224,223,223,1102,16,32,224,101,-512,224,224,4,224,102,8,223,223,101,5,224,224,1,224,223,223,1001,43,57,224,101,-147,224,224,4,224,102,8,223,223,101,4,224,224,1,223,224,223,1101,36,81,225,1002,39,9,224,1001,224,-99,224,4,224,1002,223,8,223,101,2,224,224,1,223,224,223,1,213,218,224,1001,224,-98,224,4,224,102,8,223,223,101,2,224,224,1,224,223,223,102,21,74,224,101,-1869,224,224,4,224,102,8,223,223,1001,224,7,224,1,224,223,223,1101,25,15,225,1101,64,73,225,4,223,99,0,0,0,677,0,0,0,0,0,0,0,0,0,0,0,1105,0,99999,1105,227,247,1105,1,99999,1005,227,99999,1005,0,256,1105,1,99999,1106,227,99999,1106,0,265,1105,1,99999,1006,0,99999,1006,227,274,1105,1,99999,1105,1,280,1105,1,99999,1,225,225,225,1101,294,0,0,105,1,0,1105,1,99999,1106,0,300,1105,1,99999,1,225,225,225,1101,314,0,0,106,0,0,1105,1,99999,1008,226,677,224,1002,223,2,223,1005,224,329,1001,223,1,223,1007,677,677,224,102,2,223,223,1005,224,344,101,1,223,223,108,226,677,224,102,2,223,223,1006,224,359,101,1,223,223,108,226,226,224,1002,223,2,223,1005,224,374,1001,223,1,223,7,226,226,224,1002,223,2,223,1006,224,389,1001,223,1,223,8,226,677,224,1002,223,2,223,1006,224,404,1001,223,1,223,107,677,677,224,1002,223,2,223,1006,224,419,101,1,223,223,1008,677,677,224,102,2,223,223,1006,224,434,101,1,223,223,1107,226,677,224,102,2,223,223,1005,224,449,1001,223,1,223,107,226,226,224,102,2,223,223,1006,224,464,101,1,223,223,107,226,677,224,102,2,223,223,1005,224,479,1001,223,1,223,8,677,226,224,102,2,223,223,1005,224,494,1001,223,1,223,1108,226,677,224,102,2,223,223,1006,224,509,101,1,223,223,1107,677,226,224,1002,223,2,223,1005,224,524,101,1,223,223,1008,226,226,224,1002,223,2,223,1005,224,539,101,1,223,223,7,226,677,224,1002,223,2,223,1005,224,554,101,1,223,223,1107,677,677,224,1002,223,2,223,1006,224,569,1001,223,1,223,8,226,226,224,1002,223,2,223,1006,224,584,101,1,223,223,1108,677,677,224,102,2,223,223,1005,224,599,101,1,223,223,108,677,677,224,1002,223,2,223,1006,224,614,101,1,223,223,1007,226,226,224,102,2,223,223,1005,224,629,1001,223,1,223,7,677,226,224,1002,223,2,223,1005,224,644,101,1,223,223,1007,226,677,224,102,2,223,223,1005,224,659,1001,223,1,223,1108,677,226,224,102,2,223,223,1006,224,674,101,1,223,223,4,223,99,226",
                "3,225,1,225,6,6,1101,1,238,225,104,0,1101,9,90,224,1001,224,-99,224,4,224,102,8,223,223,1001,224,6,224,1,223,224,223,1102,26,62,225,1101,11,75,225,1101,90,43,225,2,70,35,224,101,-1716,224,224,4,224,1002,223,8,223,101,4,224,224,1,223,224,223,1101,94,66,225,1102,65,89,225,101,53,144,224,101,-134,224,224,4,224,1002,223,8,223,1001,224,5,224,1,224,223,223,1102,16,32,224,101,-512,224,224,4,224,102,8,223,223,101,5,224,224,1,224,223,223,1001,43,57,224,101,-147,224,224,4,224,102,8,223,223,101,4,224,224,1,223,224,223,1101,36,81,225,1002,39,9,224,1001,224,-99,224,4,224,1002,223,8,223,101,2,224,224,1,223,224,223,1,213,218,224,1001,224,-98,224,4,224,102,8,223,223,101,2,224,224,1,224,223,223,102,21,74,224,101,-1869,224,224,4,224,102,8,223,223,1001,224,7,224,1,224,223,223,1101,25,15,225,1101,64,73,225,4,223,99,13818007,7,137,677,0,0,0,0,0,0,0,0,0,0,0,1105,0,99999,1105,227,247,1105,1,99999,1005,227,99999,1005,0,256,1105,1,99999,1106,227,99999,1106,0,265,1105,1,99999,1006,0,99999,1006,227,274,1105,1,99999,1105,1,280,1105,1,99999,1,225,225,225,1101,294,0,0,105,1,0,1105,1,99999,1106,0,300,1105,1,99999,1,225,225,225,1101,314,0,0,106,0,0,1105,1,99999,1008,226,677,224,1002,223,2,223,1005,224,329,1001,223,1,223,1007,677,677,224,102,2,223,223,1005,224,344,101,1,223,223,108,226,677,224,102,2,223,223,1006,224,359,101,1,223,223,108,226,226,224,1002,223,2,223,1005,224,374,1001,223,1,223,7,226,226,224,1002,223,2,223,1006,224,389,1001,223,1,223,8,226,677,224,1002,223,2,223,1006,224,404,1001,223,1,223,107,677,677,224,1002,223,2,223,1006,224,419,101,1,223,223,1008,677,677,224,102,2,223,223,1006,224,434,101,1,223,223,1107,226,677,224,102,2,223,223,1005,224,449,1001,223,1,223,107,226,226,224,102,2,223,223,1006,224,464,101,1,223,223,107,226,677,224,102,2,223,223,1005,224,479,1001,223,1,223,8,677,226,224,102,2,223,223,1005,224,494,1001,223,1,223,1108,226,677,224,102,2,223,223,1006,224,509,101,1,223,223,1107,677,226,224,1002,223,2,223,1005,224,524,101,1,223,223,1008,226,226,224,1002,223,2,223,1005,224,539,101,1,223,223,7,226,677,224,1002,223,2,223,1005,224,554,101,1,223,223,1107,677,677,224,1002,223,2,223,1006,224,569,1001,223,1,223,8,226,226,224,1002,223,2,223,1006,224,584,101,1,223,223,1108,677,677,224,102,2,223,223,1005,224,599,101,1,223,223,108,677,677,224,1002,223,2,223,1006,224,614,101,1,223,223,1007,226,226,224,102,2,223,223,1005,224,629,1001,223,1,223,7,677,226,224,1002,223,2,223,1005,224,644,101,1,223,223,1007,226,677,224,102,2,223,223,1005,224,659,1001,223,1,223,1108,677,226,224,102,2,223,223,1006,224,674,101,1,223,223,4,223,99,226",
                "1",
                "0,0,0,0,0,0,0,0,0,13818007"
            ),
        };

        let mut counter = 0;
        for t in test_cases
        {
            println!("Running int code computer test {}", counter);
            test_program(t.program.as_str(), t.result_memory.as_str(), t.inputs.as_str(), t.outputs.as_str());
            counter += 1;
        }
    }
}





