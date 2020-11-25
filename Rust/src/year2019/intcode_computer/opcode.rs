use std::convert::From;
use core::fmt;

#[derive(PartialEq, Eq)]
pub enum Opcode
{
    Add = 1,
    Multiply = 2,
    Input = 3,
    Output = 4,
    JumpIfTrue = 5,
    JumpIfFalse = 6,
    LessThan = 7,
    Equals = 8,
    AdjustRelativeBase = 9,
    Halt = 99,
}

impl fmt::Display for Opcode {
    fn fmt(&self, f: &mut fmt::Formatter<'_>) -> fmt::Result
    {
        match *self {
            Opcode::Add                => write!(f, "ADD"),
            Opcode::Multiply           => write!(f, "MUL"),
            Opcode::Input              => write!(f, "INP"),
            Opcode::Output             => write!(f, "OUT"),
            Opcode::JumpIfTrue         => write!(f, "JIT"),
            Opcode::JumpIfFalse        => write!(f, "JIF"),
            Opcode::LessThan           => write!(f, "LTN"),
            Opcode::Equals             => write!(f, "EQL"),
            Opcode::AdjustRelativeBase => write!(f, "ARB"),
            Opcode::Halt               => write!(f, "HLT"),
        }
    }
}


impl Opcode
{
    pub fn get_argument_count(&self) -> usize
    {
        return match self
        {
            Opcode::Add                 => 3,
            Opcode::Multiply            => 3,
            Opcode::Input               => 1,
            Opcode::Output              => 1,
            Opcode::JumpIfTrue          => 2,
            Opcode::JumpIfFalse         => 2,
            Opcode::LessThan            => 3,
            Opcode::Equals              => 3,
            Opcode::AdjustRelativeBase  => 1,
            Opcode::Halt                => 0,
        }
    }
}


impl From<i64> for Opcode
{
    fn from(num: i64) -> Self
    {
        match num
        {
            1  => return Opcode::Add,
            2  => return Opcode::Multiply,
            3  => return Opcode::Input,
            4  => return Opcode::Output,
            5  => return Opcode::JumpIfTrue,
            6  => return Opcode::JumpIfFalse,
            7  => return Opcode::LessThan,
            8  => return Opcode::Equals,
            9  => return Opcode::AdjustRelativeBase,
            99 => return Opcode::Halt,

            _ => panic!("Failed to parse instruction {}", num)
        }
    }
}