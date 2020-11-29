use crate::utils::vector3i::Vector3i;


struct Moon
{
    position: Vector3i,
    velocity: Vector3i,
}

impl Moon
{
    fn new(position: Vector3i, velocity: Vector3i) -> Self
    {
        Moon
        {
            position,
            velocity
        }
    }

    fn apply_velocity(&mut self)
    {
        self.position = self.position.add(&self.velocity);
    }

    fn to_string(&self) -> String
    {
        let mut str = String::new();
        str.push_str(self.position.to_string().as_str());
        str.push_str(" - ");
        str.push_str(self.velocity.to_string().as_str());
        return str;
    }


    // A moon's potential energy is the sum of the
    //absolute values of its x, y, and z position coordinates.
    fn potential_energy(&self) -> i64
    {
        return self.position.x.abs() + self.position.y.abs() + self.position.z.abs();
    }


    //A moon's kinetic energy is the sum of the absolute values of its
    //velocity coordinates. Below, each line shows the calculations for
    //a moon's potential energy (pot), kinetic energy (kin), and total energy:
    fn kinetic_energy(&self) -> i64
    {
        return self.velocity.x.abs() + self.velocity.y.abs() + self.velocity.z.abs();
    }

    //The total energy for a single moon is its potential energy multiplied
    //by its kinetic energy.
    fn total_energy(&self) -> i64
    {
        return self.potential_energy() * self.kinetic_energy();
    }
}

//bit of a cheat, memoizing the pairs instead of working out the iterations
static PAIRS: [(usize, usize); 6] = [
    (0, 1),
    (0, 2),
    (0, 3),
    (1, 2),
    (1, 3),
    (2, 3),
];





//Simulate the motion of the moons in time steps. Within each time step,
//first update the velocity of every moon by applying gravity.
//Then, once all moons' velocities have been updated, update the
//position of every moon by applying velocity. Time progresses by
//one step once all of the positions are updated.

//To apply gravity, consider every pair of moons. On each axis (x, y, and z),
//the velocity of each moon changes by exactly +1 or -1 to pull the moons together.
//For example, if Ganymede has an x position of 3, and Callisto has a x position
//of 5, then Ganymede's x velocity changes by +1 (because 5 > 3) and Callisto's x
//velocity changes by -1 (because 3 < 5). However, if the positions on a given axis are the same,
//the velocity on that axis does not change for that pair of moons.
//
//Once all gravity has been applied, apply velocity: simply add the velocity of
//each moon to its own position. For example, if Europa has a position of
//x=1, y=2, z=3 and a velocity of x=-2, y=0,z=3, then its new position
//would be x=-1, y=2, z=6. This process does not modify the velocity of any moon.

//For example, if Ganymede has an x position of 3, and Callisto has a x position
//of 5, then Ganymede's x velocity changes by +1 (because 5 > 3) and Callisto's x
//velocity changes by -1 (because 3 < 5). However, if the positions on a given axis are the same,
//the velocity on that axis does not change for that pair of moons.

//position1  pos 3
//position2  pos 5
//position1  vel +1
//position2  vel -1

///Calculates the change in velocity for position1
fn calculate_velocity(position1: i64, position2: i64) -> i64
{
    if position1 < position2
    {
        return 1;
    }

    if position1 > position2
    {
        return -1;
    }

    return 0;
}




pub fn problem1()
{
    let mut step = 0;
    println!("step {}", step);
    step += 1;


    let mut moons = Vec::new();

    for str in INPUT.split("\n").collect::<Vec<&str>>()
    {
        moons.push(Moon::new(Vector3i::from_str(str), Vector3i::new(0, 0, 0)));
        println!("{}", moons[moons.len()-1].to_string());
    }

    loop
    {
        //To apply gravity, consider every pair of moons. On each axis (x, y, and z),
        //the velocity of each moon changes by exactly +1 or -1 to pull the moons together.
        //For example, if Ganymede has an x position of 3, and Callisto has a x position
        //of 5, then Ganymede's x velocity changes by +1 (because 5 > 3) and Callisto's x
        //velocity changes by -1 (because 3 < 5). However, if the positions on a given axis are the same,
        //the velocity on that axis does not change for that pair of moons.

        for pair in PAIRS.iter()
        {
            moons[pair.0].velocity.x += calculate_velocity(moons[pair.0].position.x, moons[pair.1].position.x);
            moons[pair.0].velocity.y += calculate_velocity(moons[pair.0].position.y, moons[pair.1].position.y);
            moons[pair.0].velocity.z += calculate_velocity(moons[pair.0].position.z, moons[pair.1].position.z);

            moons[pair.1].velocity.x += calculate_velocity(moons[pair.1].position.x, moons[pair.0].position.x);
            moons[pair.1].velocity.y += calculate_velocity(moons[pair.1].position.y, moons[pair.0].position.y);
            moons[pair.1].velocity.z += calculate_velocity(moons[pair.1].position.z, moons[pair.0].position.z);
        }

        //Once all gravity has been applied, apply velocity: simply add the velocity of
        //each moon to its own position. For example, if Europa has a position of
        //x=1, y=2, z=3 and a velocity of x=-2, y=0,z=3, then its new position
        //would be x=-1, y=2, z=6. This process does not modify the velocity of any moon.

        println!("step {}", step);
        let mut energy = 0;
        for moon in moons.iter_mut()
        {
            moon.apply_velocity();
            energy += moon.total_energy();
            println!("{}", moon.to_string());
        }

        println!("Total energy: {}", energy);

        step += 1;
        if step > 1000
        {
            return;
        }
    }
}

pub fn problem2()
{

}




//manually edited formatting of inputs so that I didn't have to change the parser
static EXAMPLE: &'static str = "(-1,0,2)
(2,-10,-7)
(4,-8,8)
(3,5,-1)";

static EXAMPLE2: &'static str = "(-8,-10,0)
(5,5,10)
(2,-7,3)
(9,-8,-3)";

static INPUT: &'static str = "(6,-2,-7)
(-6,-7,-4)
(-9,11,0)
(-3,-4,6)";