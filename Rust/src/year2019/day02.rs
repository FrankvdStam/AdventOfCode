use crate::year2019::intcode_computer::program::Program;
use std::str::FromStr;
use crate::year2019::intcode_computer::computer::Computer;


pub fn problem1()
{
    let mut computer = Computer::from_str(INPUT);
    computer.run();

}

pub fn problem2()
{

}

static INPUT: &'static str = "1,0,0,3,99";