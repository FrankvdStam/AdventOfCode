use crate::year2019::intcode_computer::program::Program;
use std::str::FromStr;
use crate::year2019::intcode_computer::computer::Computer;


pub fn problem1()
{
    let program = Program::from_str(INPUT).unwrap();
    println!("{}", program.disassemble());

    let mut computer = Computer::from_program(&program);
    computer.step();


}

pub fn problem2()
{

}

static INPUT: &'static str = "1,0,0,3,99";