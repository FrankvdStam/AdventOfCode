use crate::utils::vector2_i64::Vector2_i64;

pub fn problem1()
{
    let directions = parse_directions(INPUT);
    let mut ship_direction = Direction::East;
    let mut ship_position = Vector2_i64::new(0,0);

    for d in directions.iter()
    {
        let (action, amount) = d;

        match action
        {
            //
            Action::Forward =>
            {
                ship_position = ship_position.add(&get_direction_vector(ship_direction, *amount as i64));
            }

            Action::Left  |
            Action::Right =>
            {
                ship_direction = change_direction(ship_direction, *action, *amount);

            }

            //If we simply move in any known direction, we can just add the amount in the correct direction to the current position
            Action::North  => ship_position = ship_position.add(&get_direction_vector(Direction::North, *amount as i64)),
            Action::East   => ship_position = ship_position.add(&get_direction_vector(Direction::East , *amount as i64)),
            Action::South  => ship_position = ship_position.add(&get_direction_vector(Direction::South, *amount as i64)),
            Action::West   => ship_position = ship_position.add(&get_direction_vector(Direction::West , *amount as i64)),
        }
    }

    println!("{}", ship_position.x.abs() + ship_position.y.abs());
}
// not -1533
// not 2512



pub fn problem2()
{
    let directions = parse_directions(INPUT);
    let mut ship_position = Vector2_i64::new(0,0);
    let mut waypoint = Vector2_i64::new(10, -1);

    for d in directions.iter()
    {
        let (action, amount) = d;

        match action
        {

            Action::Forward =>
            {
                let mut temp = amount.clone();
                while temp > 0
                {
                    ship_position = ship_position.add(&waypoint);
                    temp -= 1;
                }
            }

            Action::Left =>
            {
                let mut temp = amount.clone();
                while temp > 0
                {
                    //Somewhere along the way, the y axis got inverted
                    waypoint = waypoint.rotate_right();
                    temp -= 90;
                }
            }
            Action::Right =>
            {
                let mut temp = amount.clone();
                while temp > 0
                {
                    //Somewhere along the way, the y axis got inverted
                    waypoint = waypoint.rotate_left();
                    temp -= 90;
                }
            }

            //If we simply move in any known direction, we can just add the amount in the correct direction to the current position
            Action::North  => waypoint = waypoint.add(&get_direction_vector(Direction::North, *amount as i64)),
            Action::East   => waypoint = waypoint.add(&get_direction_vector(Direction::East , *amount as i64)),
            Action::South  => waypoint = waypoint.add(&get_direction_vector(Direction::South, *amount as i64)),
            Action::West   => waypoint = waypoint.add(&get_direction_vector(Direction::West , *amount as i64)),
        }

        //println!("{:?} {} {} {}", action, amount, waypoint, ship_position);
    }
    println!("{}", ship_position.x.abs() + ship_position.y.abs());
}

/////Rotates the other vector around this one, returning the results in a new vector.
//pub fn rotate_right(&self, other: &Vector2_i64) -> Vector2_i64
//{
//    let diff_x = difference(self.x, other.x);
//    let diff_y = difference(self.y, other.y);
//
//
//
//    let x = self.x - (self.y - other.y);
//    let y = self.y - (self.x - other.x);
//    return Vector2_i64::new(x, y);
//}


fn parse_directions(input: &str) -> Vec<(Action, usize)>
{
    let mut directions = Vec::new();

    for line in input.split('\n')
    {

        let action = match line.chars().next().unwrap()
        {
            'F' => Action::Forward,
            'L' => Action::Left,
            'R' => Action::Right,
            'N' => Action::North,
            'E' => Action::East,
            'S' => Action::South,
            'W' => Action::West,
            _ => panic!("Unsupported char."),
        };

        let mut num_string = line.to_string();
        num_string.remove(0);//remove action
        directions.push((action, num_string.parse::<usize>().unwrap()));
    }

    return directions;
}

#[derive(Clone, Copy, Debug)]
enum Action
{
    Forward,
    Left,
    Right,

    North,
    East,
    South,
    West,
}

#[derive(Clone, Copy, Debug)]
enum Direction
{
    North,
    East,
    South,
    West,
}

fn change_direction(mut current_direction: Direction, action: Action, mut amount: usize) -> Direction
{
    while amount > 0
    {
        current_direction = match action
        {
            Action::Left  =>
            {
                match current_direction
                {
                    Direction::North => { Direction::West }
                    Direction::West => {  Direction::South }
                    Direction::South => { Direction::East }
                    Direction::East => {  Direction::North }
                }
            },
            Action::Right =>
            {
                match current_direction
                {
                    Direction::North => { Direction::East }
                    Direction::East => {  Direction::South }
                    Direction::South => { Direction::West }
                    Direction::West => {  Direction::North }
                }
            },
            _ => panic!("Unsupported action"),
        };
        amount -= 90;
    }
    return current_direction;
}


///Gets a vector with the corresponding direction
fn get_direction_vector(direction: Direction, amount: i64) -> Vector2_i64
{
    return match direction
    {
        Direction::North => { Vector2_i64::new( 0,-amount) }
        Direction::East => {  Vector2_i64::new( amount, 0) }
        Direction::South => { Vector2_i64::new( 0, amount) }
        Direction::West => {  Vector2_i64::new(-amount, 0) }
    }
}


static CUSTOM_EXAMPLE: &'static str = "L90
F10
N3
F7
R90
F11";

static EXAMPLE: &'static str = "F10
N3
F7
R90
F11";

static INPUT: &'static str = "W5
N3
W4
F2
N3
R180
E2
S4
E5
N4
E2
L90
F81
E5
R180
E2
F88
L90
N4
E1
F90
S3
W3
R90
F80
E4
F28
R180
S1
F80
E4
F18
S5
W4
F13
R90
S3
W5
N2
F76
L90
N4
F49
L180
E5
R90
F51
E4
L180
F86
S5
L180
F3
E5
W1
R90
F54
N4
E5
R90
E4
N3
W1
E4
R180
W2
F2
S5
W4
E3
R90
F49
R90
W5
N3
F47
E1
L90
E2
F86
E3
R90
F100
F84
N2
F12
L90
N4
W5
F40
N5
F68
W3
R90
W4
N1
F63
W5
S3
F52
R180
W1
W4
L90
N4
R90
S4
L90
F77
R90
E5
F20
N3
W5
R90
E5
R180
F63
S1
W1
W5
F12
E5
L90
N4
F83
W4
F92
W2
F41
E3
S5
R90
S5
W2
N1
F4
N1
F50
L180
F73
N2
L90
E2
S5
F19
L90
E1
N4
L90
S2
L90
F90
L90
N3
E1
F32
E1
F66
R180
E1
S2
F72
R90
S2
E1
N3
F24
W4
F32
W5
S3
E2
F52
W5
F54
E4
F97
N1
R90
W3
R180
E2
S3
R90
N1
N3
F76
R90
F43
E3
N4
L90
E4
F32
E4
S3
F46
R90
N4
E5
L90
F33
W1
W1
N1
E3
S1
R90
W3
L90
F59
W3
S1
F7
W3
F85
R90
F61
E5
S5
F25
F8
L90
N3
F80
W4
F89
N3
E5
N5
L90
F50
F19
L90
N2
R90
F8
W5
S5
L90
F63
S5
S2
F44
N1
W1
L90
R90
W2
F24
W3
S4
R90
F69
E5
F77
W4
F38
R180
W3
S5
N2
F91
F44
N2
W2
R90
W5
F48
W3
R90
F74
E4
S3
R90
E3
L90
F81
W1
F69
W2
N3
R90
E1
R90
L90
E5
S4
E2
S5
F58
N3
F50
L90
N1
L90
N4
L180
W2
L90
F61
L90
S5
E1
S4
W1
S3
E4
F62
S2
L270
F97
R90
S5
L180
F66
E1
R90
E3
L180
F98
R90
F37
R90
F18
N3
R90
E2
S3
L90
S4
F82
W3
F72
N1
E4
F67
L90
W1
S2
F94
R90
F62
N4
W2
S5
R180
F41
W5
F9
W2
F34
L90
N3
R90
F1
N4
E4
R90
F39
S5
W5
N5
R180
F32
W5
F97
R90
N4
W3
R90
N4
W2
N5
W5
R90
S4
L90
F99
N2
R90
E4
N5
E1
F67
R180
W3
S2
E2
F95
E1
S1
W5
S3
E2
F64
L90
F29
S3
F33
F46
S2
R90
N4
E1
F11
F50
L90
E2
F72
L180
N2
E4
N1
E3
S1
F37
W1
R90
N3
L90
W3
F62
R90
F88
W1
S4
E3
L90
S4
R90
E4
S2
F81
W5
F82
L90
F19
R90
R270
E4
F27
R90
N1
W3
R90
W1
S4
L90
W1
F24
L180
R90
S1
E3
S4
L90
E3
F71
R180
S1
F33
S1
F49
S1
R180
E4
L180
F44
R90
W2
F26
R90
L180
L180
F31
S3
E4
R90
W1
L90
W1
N5
F25
N3
L180
F4
N3
S5
E4
R90
S2
L90
F28
E4
N3
L90
S1
R90
N4
W1
N2
R180
E4
L90
S5
R180
S5
F14
E3
F38
S2
F1
E1
F46
R270
F69
L180
N1
R90
W5
N4
F22
R90
N1
L180
F16
N2
E1
N4
F68
L90
E2
F6
E2
F2
E4
R90
W4
E2
R90
S1
W1
S5
F87
S5
F9
W5
F91
L90
S2
R270
F73
L90
F17
L90
E4
W1
R90
F40
E5
F7
R180
R90
R180
E5
F89
R180
W2
L180
F31
E2
S1
W2
F11
L180
E1
F55
E5
S4
L90
S5
W4
R180
F23
E3
R90
F12
E3
F3
S3
W3
L90
W2
N5
E2
F77
E4
S3
F11
W4
F23
E1
R90
F61
E3
L90
S3
N5
W2
R180
W2
S2
N5
E1
S2
L90
W3
R90
F89
R270
N3
L90
R90
W4
S2
W4
L90
S1
E4
E4
S3
R270
F47
L90
E1
F10
L180
W4
R90
N2
F97
L180
F82
N5
L90
S1
E3
F14
R90
F23
N2
F34
L270
E2
F77
E5
L90
S3
R270
F12
N1
E3
R90
E2
F4
W3
E3
F33
S4
R180
S5
R90
E1
R270
F53
N4
L90
N1
W2
S5
E2
R180
W2
S3
L90
N4
W3
N3
F84
E5
N3
L90
F48
W4
F18
L90
W3
S4
E2
S4
F64
N1
F96
S3
E5
N4
W2
F22
W5
L90
F23
E2
N1
F92
F16
L180
E4
N1
F75
L90
W4
L270
W3
S4
L90
F29
W4
S2
F47
R90
N3
L90
S3
W3
N1
F45
N2
L90
E4
N1
L90
E3
R90
N3
F86
W1
N5
W3
S5
L90
S4
W2
F44";

