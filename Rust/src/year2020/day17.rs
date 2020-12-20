use crate::utils::vector3_i64::{Vector3_i64, VECTOR3_I64_NEIGHBOR_LOOKUP};
use std::collections::HashMap;

pub fn problem1()
{
    let cubes = parse_cubes(EXAMPLE);

    let (_min, _max) = get_min_max(&cubes);

    cycle(&cubes);
    println!("{:?}", cubes);
}

pub fn problem2()
{

}



fn cycle(cubes: &HashMap<Vector3_i64, bool>) -> HashMap<Vector3_i64, bool>
{
    let new_cubes:  HashMap<Vector3_i64, bool> = HashMap::new();

    for (vector, _) in cubes.into_iter()
    {
        let mut count = 0;
        for i in 0..VECTOR3_I64_NEIGHBOR_LOOKUP.len()
        {
            let neighbor = vector.add(&VECTOR3_I64_NEIGHBOR_LOOKUP[i]);
            if cubes.contains_key(&neighbor)
            {
                if *cubes.get(&neighbor).unwrap()
                {
                    count += 1;
                }
            }
        }
        println!("checking {} - count: {}", vector, count);
    }

    return new_cubes;
}


fn get_min_max(cubes: &HashMap<Vector3_i64, bool>) -> (Vector3_i64, Vector3_i64)
{
    let mut max = Vector3_i64::new(i64::MIN,i64::MIN,i64::MIN);
    let mut min = Vector3_i64::new(i64::MAX,i64::MAX,i64::MAX);

    //Find the dimensions
    for (vector, _) in cubes.into_iter()
    {
        max = max.max(vector);
        min = min.min(vector);
    }

    return (min, max);
}







fn parse_cubes(input: &str) -> HashMap<Vector3_i64, bool>
{
    let mut cubes = HashMap::new();

    let lines = input.split('\n').collect::<Vec<&str>>();
    for y in 0..lines.len()
    {
        let chars = lines[y].chars().collect::<Vec<char>>();
        for x in 0..chars.len()
        {
            let vector = Vector3_i64::new(x as i64, y as i64, 0);
            let active = chars[x] == '#';

            cubes.insert(vector, active);
        }
    }
    return cubes;
}








static EXAMPLE: &'static str = ".#.
..#
###";

static INPUT: &'static str = ".###.#.#
####.#.#
#.....#.
####....
#...##.#
########
..#####.
######.#";

