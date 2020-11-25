use std::convert::From;
use core::fmt;

pub enum Mode
{
    Position = 0,
    Immediate = 1,
    Relative = 2,
}

impl From<i64> for Mode
{
    fn from(num: i64) -> Self
    {
        match num
        {
            0 => return Mode::Position,
            1 => return Mode::Immediate,
            2 => return Mode::Relative,

            _ => panic!("Failed to parse mode {}", num)
        }
    }
}

pub enum State
{
    Running,
    WaitingForInput,
    Output,
    Halt
}
