mod utils;
mod year2015;
mod year2019;

use crate::year2019::day09::problem1;
use crate::year2019::day09::problem2;
use std::time::Instant;

fn main()
{
    let mut start = Instant::now();
    problem1();
    println!("First problem executed in: {:?}", start.elapsed());


    start = Instant::now();
    problem2();
    println!("Second problem executed in: {:?}", start.elapsed());
}