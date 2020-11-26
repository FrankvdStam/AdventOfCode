//phase setting (an integer from 0 to 4). Each phase setting is used exactly once, but the Elves can't remember which amplifier needs which phase setting.

use permutohedron::LexicalPermutation;
use crate::year2019::intcode_computer::computer::{Computer, State};

pub fn problem1()
{
    //Generate all the permutations.
    //Could do this without storing them in memory but its nice to keep this problem seperate.
    let mut inputs = vec!{0, 1, 2, 3, 4};
    let mut permutations = Vec::new();

    loop {
        permutations.push(inputs.to_vec());
        if !inputs.next_permutation()
        {
            break;
        }
    }


    //Initialising the amplifiers
    let mut computers: Vec<Computer> = Vec::with_capacity(5);
    for _i in 0..5
    {
        computers.push(Computer::from_str(INPUT));
    }
    for i in 0..5
    {
        computers[i].print_disassembly = false;
        computers[i].print_output = false;
    }



    let mut highest = 0;

    for permutation in permutations
    {
        //Setup
        //Set the phase setting
        for i in 0..5
        {
            computers[i].input.push(permutation[i]);
        }

        //run

        let mut output = 0;
        for i in 0..5
        {
            computers[i].input.push(output);
            computers[i].run();
            output = computers[i].output[0];
        }

        if output > highest
        {
            highest = output;
        }

        //cleanup
        for i in 0..5
        {
            computers[i].reset_from_str(INPUT);
        }
    }
    println!("Highest signal: {}", highest);
}







pub fn problem2()
{
    //Generate all the permutations.
    //Could do this without storing them in memory but its nice to keep this problem seperate.
    let mut inputs = vec!{5, 6, 7, 8, 9};
    let mut permutations = Vec::new();

    loop {
        permutations.push(inputs.to_vec());
        if !inputs.next_permutation()
        {
            break;
        }
    }


    //Initialising the amplifiers
    let mut computers: Vec<Computer> = Vec::with_capacity(5);
    for _i in 0..5
    {
        computers.push(Computer::from_str(INPUT));
    }
    //computers[0].disassemble_program();
    for i in 0..5
    {
        computers[i].print_disassembly = false;
        computers[i].print_output = false;
    }

    let mut highest = 0;

    for permutation in permutations
    {
        //Setup
        //Set the phase setting
        for i in 0..5
        {
            computers[i].input.push(permutation[i]);
        }

        //run

        let mut output = 0;
        loop
        {
            //v.iter().filter(|&n| *n == 91).count());
            //Check if all computers have halted
            if computers.iter().filter(|x| x.get_state() == State::Halt).count() == computers.iter().count()
            {
                break;
            }

            for i in 0..5
            {
                if computers[i].get_state() != State::Halt
                {
                    computers[i].input.push(output);
                    run_custom(&mut computers[i]);
                    if computers[i].output.len() > 0
                    {
                        output = computers[i].output[0];
                        computers[i].output.remove(0);
                    }
                }
            }
        }


        if output > highest
        {
            highest = output;
        }

        //cleanup
        for i in 0..5
        {
            computers[i].reset_from_str(INPUT);
            computers[i].input.clear();
            computers[i].output.clear();
        }
    }
    println!("Highest signal: {}", highest);
}



pub fn run_custom(computer: &mut Computer)
{
    loop
    {
        let state = computer.step();
        match state
        {
            State::Running => {/*just keep running*/}
            //Always return on in or output.
            _ => return
            //State::WaitingForInput =>
            //    {
            //        print!("in: ");
            //        io::stdout().flush().unwrap();
            //        let mut buffer = String::new();
            //        io::stdin().read_line(&mut buffer).unwrap();
            //        let num = buffer.trim().parse::<i64>().unwrap();
            //        self.input.push(num);
            //        println!();
            //    }
            //State::PushedOutput =>
            //    {
            //        if self.print_output
            //        {
            //            println!("out: {}", self.output[0]);
            //            self.output.remove(0);
            //        }
            //    }
            //State::Break =>
            //    {
            //        return
            //    }
            //State::Halt => return,
        }
    }
}


static EXAMPLE: &'static str = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";
static INPUT: &'static str = "3,8,1001,8,10,8,105,1,0,0,21,46,55,68,89,110,191,272,353,434,99999,3,9,1002,9,3,9,1001,9,3,9,102,4,9,9,101,4,9,9,1002,9,5,9,4,9,99,3,9,102,3,9,9,4,9,99,3,9,1001,9,5,9,102,4,9,9,4,9,99,3,9,1001,9,5,9,1002,9,2,9,1001,9,5,9,1002,9,3,9,4,9,99,3,9,101,3,9,9,102,3,9,9,101,3,9,9,1002,9,4,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,99,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,99,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,99";