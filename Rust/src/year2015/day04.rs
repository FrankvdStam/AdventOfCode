use md5::Digest;

static INPUT: &'static str = "bgvyzdsv";

pub fn problem1()
{
    let mut count = 0;
    loop
    {
        //Build the input and calculate the hash
        let mut input: String = String::from(INPUT);
        input.push_str(count.to_string().as_str());
        let hash: Digest = md5::compute(input);


        //Check if hex representation has 5 0's
        if hash[0] == 0 && hash[1] == 0 && hash[2] <= 16
        {
            println!("Found hash: {:?} at {}", hash, count);
            return;
        }

        //increment the count, print every 1000.
        count += 1;
        if count % 1000 == 0
        {
            println!("{}", count);
        }
    }
}


pub fn problem2()
{
    let mut count = 0;
    loop
    {
        //Build the input and calculate the hash
        let mut input: String = String::from(INPUT);
        input.push_str(count.to_string().as_str());
        let hash: Digest = md5::compute(input);


        //Check if hex representation has 5 0's
        if hash[0] == 0 && hash[1] == 0 && hash[2] == 0
        {
            println!("Found hash: {:?} at {}", hash, count);
            return;
        }

        //increment the count, print every 1000.
        count += 1;
        if count % 1000 == 0
        {
            println!("{}", count);
        }
    }
}