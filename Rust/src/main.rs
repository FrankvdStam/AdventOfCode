mod utils;
mod year2015;
mod year2019;

use crate::year2019::day07::problem1;
use crate::year2019::day07::problem2;
use std::time::Instant;

fn main()
{
    let start = Instant::now();

    problem1();
    problem2();

    println!("Ran main in: {:?}", start.elapsed());
}