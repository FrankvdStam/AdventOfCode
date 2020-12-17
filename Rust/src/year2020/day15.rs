use std::collections::HashMap;

pub fn problem1()
{
    let mut numbers = parse_numbers(INPUT);
    let mut i = numbers.len();

    loop {
        let previous_number = numbers[i-1];

        match try_rfind(&numbers, previous_number)
        {
            None =>
            {
                //println!("{}={}", i+1, 0);
                numbers.push(0);
            }
            Some(index) =>
            {
                let num = i - (index+1);
                //println!("{}={}", i+1, num);
                numbers.push(num);
            }
        }
        //println!("{}",numbers[i]);

        if i+1 == 2020
        {
            println!("{}",numbers[i]);
            return;
        }

        i += 1;
    }

}

pub fn problem2()
{
    let mut numbers = parse_numbers(INPUT);
    //Use a hashmap with a number as key and the last seen index as value

    let mut map = HashMap::new();
    for i in 0..numbers.len()-1
    {
        map.insert(numbers[i], i+1);
    }

    let mut turn = numbers.len();
    let mut next_number = numbers[turn-1];

    loop
    {
        //println!("turn {}", turn);
        //copy the current next number
        let previous_number = next_number;

        //See if we have this number already
        let entry = map.get(&next_number);

        //Update the next number
        match entry
        {
            None =>
            {
                next_number = 0;
                //println!("next is 0");
            }
            Some(index) =>
            {
                //println!("next number before: {}", next_number);
                next_number = turn - index;
                //println!("next via index: {} = {} - {}", next_number, turn, index+1);
            }
        }

        //add the previous number to the list
        let value = map.entry(previous_number).or_insert_with(|| 0);
        *value = turn;

        //println!("{}", previous_number);

        if turn == 30000000
        {
            println!("{}", previous_number);
            return;
        }



        turn += 1;
        //if turn % 100000 == 0
        //{
        //    println!("{}", turn);
        //}
    }
}

///Returns the index the number is found at
fn try_rfind(numbers: &Vec<usize>, value: usize) -> Option<usize>
{
    let mut i: i64 = numbers.len() as i64 -2;
    while i >= 0
    {
        if numbers[i as usize] == value
        {
            return Some(i as usize);
        }
        i -= 1;
    }
    return None
}



fn parse_numbers(input: &str) -> Vec<usize>
{
    let mut result = Vec::with_capacity(2020);
    let split = input.split(',').collect::<Vec<&str>>();
    for i in 0..split.len()
    {
        result.push(split[i].parse::<usize>().unwrap());
    }
    return result;
}


static EXAMPLE1: &'static str = "0,3,6";
static EXAMPLE2: &'static str = "1,3,2";
static EXAMPLE3: &'static str = "2,1,3";
static EXAMPLE4: &'static str = "1,2,3";
static EXAMPLE5: &'static str = "2,3,1";
static EXAMPLE6: &'static str = "3,2,1";
static EXAMPLE7: &'static str = "3,1,2";

static INPUT: &'static str = "2,15,0,9,1,20";

