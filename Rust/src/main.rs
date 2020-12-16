mod utils;
mod year2015;
mod year2019;
mod year2020;

use crate::year2020::day12::problem1;
use crate::year2020::day12::problem2;
use std::time::Instant;
use crate::utils::vector2_i64::Vector2_i64;


fn main()
{
    let mut a = Vector2_i64::new(10, 10);
    let mut b = Vector2_i64::new(20, 20);
    let mut c = a.rotate_right(&b);
    println!("{}", c);










    let mut start = Instant::now();
    problem1();
    println!("First problem executed in: {:?}", start.elapsed());


    start = Instant::now();
    problem2();
    println!("Second problem executed in: {:?}", start.elapsed());
}