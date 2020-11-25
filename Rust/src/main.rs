#[allow(dead_code)]
#[allow(unused_imports)]

mod utils;
mod year2015;
mod year2019;

//use crate::utils::*;
#[allow(unused_imports)]
use crate::year2015::*;
#[allow(unused_imports)]
use crate::year2019::*;

fn main()
{
    year2019::day02::problem1();
    year2019::day02::problem2();
}