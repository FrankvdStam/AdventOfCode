
pub fn problem1()
{
    let (eta, busses) = parse_busses(INPUT);

    let mut best_bus_id = 0;
    let mut best = usize::MAX;

    for b in busses.iter()
    {
        match b
        {
            None =>{},
            Some(bus_id) =>
            {
                let time = find_first_next(eta, *bus_id);
                let waiting_time = time - eta;
                if waiting_time < best
                {
                    best = waiting_time;
                    best_bus_id = *bus_id;
                }
            }
        }
    }
    println!("{}", best_bus_id * best);
}


pub fn problem2()
{
    //We can cheat a little bit - in the example and actual input, the first bus is always a valid number.
    let (_, busses) = parse_busses(INPUT);


    //Working with options isn't great. Lets get a list with the differences in the timestamps and a list of bus ids.
    let mut timestamps = Vec::new();
    let mut bus_ids = Vec::new();
    let mut time = 0;

    for i in 0..busses.len()
    {
        match busses[i]
        {
            None =>{},
            Some(bus_id) =>
            {
                bus_ids.push(bus_id);
                timestamps.push(time);
            }
        }
        time += 1;
    }


    //println!("{:?} {:?}", timestamps, bus_ids);

    //If planet a goes around in 10 cycles and b in 20, they line up at the 0th cycle, they line up again at 10 * 20 th cycle

    let mut increment = bus_ids[0];
    let mut time = find_first_next(0, bus_ids[0]);
    let mut found = 0;
    //let mut cycle = 0;

    loop
    {
        let bus_index = found + 1;
        let timestamp = time + timestamps[bus_index];
        let bus_next_time = find_first_next(timestamp, bus_ids[bus_index]);

        if timestamp == bus_next_time
        {
            increment *= bus_ids[bus_index];
            found += 1;
            //println!("{} of {} incr {} time {}", found, bus_ids.len(), increment, time);

            if found >= bus_ids.len() - 1
            {
                println!("{}", time);
                return;
            }
        }


        time += increment;

        //cycle += 1;
        //if cycle % 100000000 == 0
        //{
        //    println!("{} - {} {}", cycle, increment, time);
        //}
    }
}



fn recursive_find(bus_ids: &Vec<Option<usize>>, current_bus: usize, target: usize) -> bool
{
    if current_bus >= bus_ids.len()
    {
        return true;
    }

    return match bus_ids[current_bus]
    {
        None => recursive_find(bus_ids, current_bus + 1, target + 1),
        Some(bus_id) =>
        {
            let time = find_first_next(target - 1, bus_id);
            if target == time
            {
                //println!("rf: {} {}", bus_id, time);
                return recursive_find(bus_ids, current_bus + 1, target + 1);
            }
            false
        }
    }
}


fn find_first_next(eta: usize, bus_id: usize) -> usize
{
    let mut fraction = eta / bus_id;
    //If the division is a fraction, we round up to the first next whole number - which is the
    if eta % bus_id > 0
    {
        fraction += 1;
    }
    return fraction * bus_id;

    /////Never happens in my input at least.
    /////if (eta as f32 / *bus_id as f32).floor() as usize * *bus_id == eta
    /////{
    /////    panic!("");
    /////}
    ////return (((eta as f32 / bus_id as f32).floor() as usize) * bus_id) + bus_id;
}


pub fn parse_busses(input: &str) -> (usize, Vec<Option<usize>>)
{
    let split = input.split('\n').collect::<Vec<&str>>();
    let eta = split[0].parse::<usize>().unwrap();

    let mut numbers: Vec<Option<usize>> = Vec::new();
    let chars = split[1].split(',').collect::<Vec<&str>>();
    for s in chars.iter()
    {
        match *s
        {
            "x" => numbers.push(None),
            _ => numbers.push(Some(s.parse::<usize>().unwrap())),
        }
    }
    return (eta, numbers);
}



static EXAMPLE: &'static str = "939
7,13,x,x,59,x,31,19";

static INPUT: &'static str = "1008833
19,x,x,x,x,x,x,x,x,41,x,x,x,x,x,x,x,x,x,643,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,17,13,x,x,x,x,23,x,x,x,x,x,x,x,509,x,x,x,x,x,37,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,29";

