use std::collections::HashMap;
use crate::utils::vector2_i64::Vector2_i64;
use crate::year2019::intcode_computer::computer::{Computer, State};
use crate::year2019::day11::OutputState::{PaintColor, TurnDirection};
use crate::utils::math::difference;


#[derive(PartialEq, Eq)]
enum OutputState
{
    PaintColor,
    TurnDirection,
}

#[derive(PartialEq, Eq, Clone, Copy)]
enum Direction
{
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3,
}

const DIRECTION_VECTORS: [Vector2_i64; 4] =
[
    Vector2_i64 { x:  0, y:  1 },
    Vector2_i64 { x:  1, y:  0 },
    Vector2_i64 { x:  0, y: -1 },
    Vector2_i64 { x: -1, y:  0 },
];


impl From<i8> for Direction
{
    fn from(num: i8) -> Self
    {
        match num
        {
            0 => return Direction::Up,
            1 => return Direction::Right,
            2 => return Direction::Down,
            3 => return Direction::Left,
            _ => panic!("Can't turn {} into Direction enum.", num)
        }
    }
}


fn turn(direction: Direction, mut number: i64) -> Direction
{
    if number == 0
    {
        number = -1
    }
    let mut new_direction = direction as i8 + number as i8;

    if new_direction < 0
    {
        new_direction = 3;
    }

    if new_direction > 3
    {
        new_direction = 0;
    }

    return Direction::from(new_direction);
}


pub fn problem1()
{
    let mut hull: HashMap<String, bool> = HashMap::new();
    let mut position = Vector2_i64::new(0, 0);

    let mut computer = Computer::from_str(INPUT);
    computer.print_output = false;
    computer.print_disassembly = false;
    //computer.input.push(1);

    let mut output_state = OutputState::PaintColor;
    let mut direction = Direction::Up;
    loop
    {
        match computer.step()
        {
            State::Running => {}
            State::WaitingForInput =>
            {
                //provide 0 if the robot is over a black panel or 1 if the robot is over a white panel

                let key = position.to_string();
                if !hull.contains_key(key.as_str())
                {
                    computer.input.push(0);
                }
                else
                {
                    let black_paint = hull.get(key.as_str()).unwrap();
                    if *black_paint
                    {
                        computer.input.push(0);
                    }
                    else
                    {
                        computer.input.push(1);
                    }
                }
            }
            State::PushedOutput =>
            {
                let output = computer.output[0];
                computer.output.remove(0);

                match output_state
                {
                    OutputState::PaintColor =>
                    {
                        //0 means to paint the panel black, and 1 means to paint the panel white.
                        let black_paint = output == 0;
                        let key = position.to_string();

                        //let stat = player_stats.entry("attack").or_insert(100);
                        // *stat += random_stat_buff();


                        let color = hull.entry(key).or_insert(black_paint);
                        *color = black_paint;

                        //if !hull.contains_key(key.as_str())
                        //{
                        //    count += 1;
                        //    hull.insert(key, black_paint);
                        //}
                        //else
                        //{
                        //
                        //
                        //    hull[key.as_str()] = black_paint;
                        //    count += 1;
                        //}
                    },
                    OutputState::TurnDirection =>
                    {
                        direction = turn(direction, output);
                        position = position.add(&DIRECTION_VECTORS[direction as usize]);
                    },
                };


                output_state = match output_state
                {
                    OutputState::TurnDirection => PaintColor,
                    OutputState::PaintColor => TurnDirection
                };
            }
            _ =>
            {
                break;
            }
        }
    }
    println!("Total unique panels painted: {}", hull.iter().count());
}

pub fn problem2()
{
    let mut hull: HashMap<String, bool> = HashMap::new();
    //set 0,0 to white.
    hull.insert(String::from("(0,0)"), false);

    let mut min = Vector2_i64::new(0, 0);
    let mut max = Vector2_i64::new(0, 0);

    let mut position = Vector2_i64::new(0, 0);

    let mut computer = Computer::from_str(INPUT);
    computer.print_output = false;
    computer.print_disassembly = false;
    //computer.input.push(1);

    let mut output_state = OutputState::PaintColor;
    let mut direction = Direction::Up;
    loop
    {
        match computer.step()
        {
            State::Running => {}
            State::WaitingForInput =>
                {
                    //provide 0 if the robot is over a black panel or 1 if the robot is over a white panel

                    let key = position.to_string();
                    if !hull.contains_key(key.as_str())
                    {
                        computer.input.push(0);
                    }
                    else
                    {
                        let black_paint = hull.get(key.as_str()).unwrap();
                        if *black_paint
                        {
                            computer.input.push(0);
                        }
                        else
                        {
                            computer.input.push(1);
                        }
                    }
                }
            State::PushedOutput =>
                {
                    let output = computer.output[0];
                    computer.output.remove(0);

                    match output_state
                    {
                        OutputState::PaintColor =>
                            {
                                //0 means to paint the panel black, and 1 means to paint the panel white.
                                let black_paint = output == 0;
                                let key = position.to_string();

                                //let stat = player_stats.entry("attack").or_insert(100);
                                // *stat += random_stat_buff();


                                let color = hull.entry(key).or_insert(black_paint);
                                *color = black_paint;
                            },
                        OutputState::TurnDirection =>
                        {
                            direction = turn(direction, output);
                            position = position.add(&DIRECTION_VECTORS[direction as usize]);

                            //Keep track of min & max
                            if position.x < min.x
                            {
                                min.x = position.x;
                            }

                            if position.y < min.y
                            {
                                min.y = position.y;
                            }

                            if position.x > max.x
                            {
                                max.x = position.x;
                            }

                            if position.y > max.y
                            {
                                max.y = position.y;
                            }
                        },
                    };


                    output_state = match output_state
                    {
                        OutputState::TurnDirection => PaintColor,
                        OutputState::PaintColor => TurnDirection
                    };
                }
                _ =>
                {
                    break;
                }
        }
    }
    println!("Total unique panels painted: {}", hull.iter().count());
    render(hull, min, max);
}

//Y axis is flipped
pub fn render(hull: HashMap<String, bool>, min: Vector2_i64, max: Vector2_i64)
{
    let width = difference(min.x, max.x) + 10;
    let height = difference(min.y, max.y) + 10;

    //Most high-tec framebuffer in history
    let mut frame_buffer = Vec::with_capacity((width * height) as usize);
    for _ in 0..width*height
    {
        frame_buffer.push(' ');
    }

    for i in hull
    {
        if !i.1
        {
            let mut vector = Vector2_i64::from_str(i.0.as_str());
            vector.x += min.x.abs();
            vector.y += min.y.abs();
            frame_buffer[(vector.x + vector.y  * width) as usize] = '#';
        }
    }
    println!();
    println!();
    for y in 0..height
    {
        for x in 0..width
        {
            print!("{}", frame_buffer[(x + y * width) as usize])
        }
        println!();
    }
}


static INPUT: &'static str = "3,8,1005,8,320,1106,0,11,0,0,0,104,1,104,0,3,8,1002,8,-1,10,101,1,10,10,4,10,1008,8,1,10,4,10,102,1,8,29,2,1005,1,10,1006,0,11,3,8,1002,8,-1,10,101,1,10,10,4,10,108,0,8,10,4,10,102,1,8,57,1,8,15,10,1006,0,79,1,6,3,10,3,8,102,-1,8,10,101,1,10,10,4,10,108,0,8,10,4,10,101,0,8,90,2,103,18,10,1006,0,3,2,105,14,10,3,8,102,-1,8,10,1001,10,1,10,4,10,108,0,8,10,4,10,101,0,8,123,2,9,2,10,3,8,102,-1,8,10,1001,10,1,10,4,10,1008,8,1,10,4,10,1001,8,0,150,1,2,2,10,2,1009,6,10,1,1006,12,10,1006,0,81,3,8,102,-1,8,10,1001,10,1,10,4,10,1008,8,1,10,4,10,102,1,8,187,3,8,102,-1,8,10,1001,10,1,10,4,10,1008,8,0,10,4,10,101,0,8,209,3,8,1002,8,-1,10,1001,10,1,10,4,10,1008,8,1,10,4,10,101,0,8,231,1,1008,11,10,1,1001,4,10,2,1104,18,10,3,8,102,-1,8,10,1001,10,1,10,4,10,108,1,8,10,4,10,1001,8,0,264,1,8,14,10,1006,0,36,3,8,1002,8,-1,10,1001,10,1,10,4,10,108,0,8,10,4,10,101,0,8,293,1006,0,80,1006,0,68,101,1,9,9,1007,9,960,10,1005,10,15,99,109,642,104,0,104,1,21102,1,846914232732,1,21102,1,337,0,1105,1,441,21102,1,387512115980,1,21101,348,0,0,1106,0,441,3,10,104,0,104,1,3,10,104,0,104,0,3,10,104,0,104,1,3,10,104,0,104,1,3,10,104,0,104,0,3,10,104,0,104,1,21102,209533824219,1,1,21102,1,395,0,1106,0,441,21101,0,21477985303,1,21102,406,1,0,1106,0,441,3,10,104,0,104,0,3,10,104,0,104,0,21101,868494234468,0,1,21101,429,0,0,1106,0,441,21102,838429471080,1,1,21102,1,440,0,1106,0,441,99,109,2,21201,-1,0,1,21101,0,40,2,21102,472,1,3,21101,0,462,0,1106,0,505,109,-2,2106,0,0,0,1,0,0,1,109,2,3,10,204,-1,1001,467,468,483,4,0,1001,467,1,467,108,4,467,10,1006,10,499,1102,1,0,467,109,-2,2106,0,0,0,109,4,2101,0,-1,504,1207,-3,0,10,1006,10,522,21101,0,0,-3,21202,-3,1,1,22101,0,-2,2,21102,1,1,3,21102,541,1,0,1106,0,546,109,-4,2105,1,0,109,5,1207,-3,1,10,1006,10,569,2207,-4,-2,10,1006,10,569,22102,1,-4,-4,1105,1,637,22102,1,-4,1,21201,-3,-1,2,21202,-2,2,3,21102,588,1,0,1105,1,546,22101,0,1,-4,21102,1,1,-1,2207,-4,-2,10,1006,10,607,21101,0,0,-1,22202,-2,-1,-2,2107,0,-3,10,1006,10,629,21201,-1,0,1,21102,629,1,0,105,1,504,21202,-2,-1,-2,22201,-4,-2,-4,109,-5,2105,1,0";
