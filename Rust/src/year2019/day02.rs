use crate::year2019::intcode_computer::computer::Computer;
use crate::year2019::intcode_computer::instruction::Mode;

pub fn problem1()
{
    let mut computer = Computer::from_str(INPUT);

    //Replace position 1 with the value 12 and replace position 2 with the value 2.
    computer.memory_write(1, 12);
    computer.memory_write(2, 2);

    computer.print_disassembly = true;
    computer.run();

    let result = computer.memory_read(&Mode::Position, 0);
    println!("1202 program alarm: {}", result);
    println!("{}", computer.memory_to_string());
}

pub fn problem2()
{
    'outer: for noun in 0..100
    {
        for verb in 0..100
        {
            println!("{} {}", noun, verb);

            let mut computer = Computer::from_str(INPUT);
            computer.memory_write(1, noun);
            computer.memory_write(2, verb);
            computer.run();
            let result = computer.memory_read(&Mode::Position, 0);

            if result == 19690720
            {
                println!("Match! result: {}", 100 * noun + verb);
                println!("{}", computer.memory_to_string());
                break 'outer;
            }
        }
    }

}

static INPUT: &'static str = "1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,6,1,19,2,19,13,23,1,23,10,27,1,13,27,31,2,31,10,35,1,35,9,39,1,39,13,43,1,13,43,47,1,47,13,51,1,13,51,55,1,5,55,59,2,10,59,63,1,9,63,67,1,6,67,71,2,71,13,75,2,75,13,79,1,79,9,83,2,83,10,87,1,9,87,91,1,6,91,95,1,95,10,99,1,99,13,103,1,13,103,107,2,13,107,111,1,111,9,115,2,115,10,119,1,119,5,123,1,123,2,127,1,127,5,0,99,2,14,0,0";