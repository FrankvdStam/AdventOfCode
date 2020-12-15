use crate::utils::vector2_i64::Vector2_i64;

#[derive(Clone, Copy, Eq, PartialEq)]
enum Seat
{
    None,
    SeatEmpty,
    SeatOccupied,
}



pub fn problem1()
{
    let mut width = 0;
    let mut height = 0;

    let mut seats = parse_seats(INPUT, &mut width, &mut height);
    //draw_grid(&seats, width, height);

    let mut change = true;
    while change
    {
        seats = occupy_seats(&seats, width, height, &mut change);
        //draw_grid(&seats, width, height);
    }

    let count = seats.iter().filter(|s| **s == Seat::SeatOccupied).count();
    println!("{}", count);
}

pub fn problem2()
{
    let mut width = 0;
    let mut height = 0;

    let mut seats = parse_seats(EXAMPLE, &mut width, &mut height);
    draw_grid(&seats, width, height);

    let mut change = true;
    while change
    {
        seats = occupy_seats_directional(&seats, width, height, &mut change);
        println!();
        draw_grid(&seats, width, height);
    }

    let count = seats.iter().filter(|s| **s == Seat::SeatOccupied).count();
    println!("{}", count);
}



//Lookup table to ease looking around specific seats
const VECTOR_LOOKUP: [Vector2_i64; 8] = [
    //Right
    Vector2_i64 { x:  1, y:  1, }, //up
    Vector2_i64 { x:  1, y:  0, }, //middle
    Vector2_i64 { x:  1, y: -1, }, //down
    //left
    Vector2_i64 { x: -1, y:  1, }, //up
    Vector2_i64 { x: -1, y:  0, }, //middle
    Vector2_i64 { x: -1, y: -1, }, //down
    //middle
    Vector2_i64 { x:  0, y:  1, }, //up
    Vector2_i64 { x:  0, y: -1, }, //down
];


fn occupy_seats(grid: &Vec<Seat>, width: usize, height: usize, change: &mut bool) -> Vec<Seat>
{
    *change = false;
    let mut new_grid = vec![Seat::None; width * height];
    for y in 0..height
    {
        for x in 0..width
        {
            //Only process seats
            if grid[x + y * width] != Seat::None
            {
                //Count the occupied seats around it
                let mut count = 0;

                for v in VECTOR_LOOKUP.iter()
                {
                    let _x = v.x + (x as i64);
                    let _y = v.y + (y as i64);
                    //println!("{},{} bounds? {},{}", x, y, _x, _y);

                    //Check if this coord is within bounds
                    if _x >= 0 && _x < width as i64 && _y >= 0 && _y < height as i64
                    {
                        //println!("      {},{} checking {},{}", x, y, v.x + (x as i64), v.y + (y as i64));

                        if grid[(_x + _y * width as i64) as usize] == Seat::SeatOccupied
                        {
                            count += 1;
                        }
                    }
                }

                //Otherwise, the seat's state does not change.
                new_grid[x + y * width] = grid[x + y * width];

                //If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
                if grid[x + y * width] == Seat::SeatEmpty && count == 0
                {
                    new_grid[x + y * width] = Seat::SeatOccupied;
                    *change = true;
                }

                //If a seat is occupied (#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
                if grid[x + y * width] == Seat::SeatOccupied && count >= 4
                {
                    new_grid[x + y * width] = Seat::SeatEmpty;
                    *change = true;
                }


            }
        }
    }
    return new_grid;
}



fn occupy_seats_directional(grid: &Vec<Seat>, width: usize, height: usize, change: &mut bool) -> Vec<Seat>
{
    *change = false;
    let mut new_grid = vec![Seat::None; width * height];
    for y in 0..height
    {
        for x in 0..width
        {
            //Only process seats
            if grid[x + y * width] != Seat::None
            {
                //Count the occupied seats around it
                let mut count = 0;

                for v in VECTOR_LOOKUP.iter()
                {
                    let mut direction = v.clone();
                    'directional_search: loop
                    {
                        let _x = direction.x + (x as i64);
                        let _y = direction.y + (y as i64);
                        //println!("{},{} bounds? {},{}", x, y, _x, _y);

                        //Check if this coord is within bounds
                        if _x >= 0 && _x < width as i64 && _y >= 0 && _y < height as i64
                        {
                            //println!("      {},{} checking {},{}", x, y, _x, _y);
                            if grid[(_x + _y * width as i64) as usize] == Seat::SeatOccupied
                            {
                                count += 1;
                                //println!("          {}", count);
                                break;
                            }
                        }
                        else
                        {
                            break 'directional_search;
                        }

                        direction = direction.add(v);
                    }
                }

                //Otherwise, the seat's state does not change.
                new_grid[x + y * width] = grid[x + y * width];

                //If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
                if grid[x + y * width] == Seat::SeatEmpty && count == 0
                {
                    new_grid[x + y * width] = Seat::SeatOccupied;
                    *change = true;
                }

                //If a seat is occupied (#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
                //Also, people seem to be more tolerant than you expected: it now takes five or more visible occupied seats for an occupied seat to become empty (rather than four or more from the previous rules).
                if grid[x + y * width] == Seat::SeatOccupied && count >= 5
                {
                    new_grid[x + y * width] = Seat::SeatEmpty;
                    *change = true;
                }


            }
        }
    }
    return new_grid;
}






fn draw_grid(grid: &Vec<Seat>, width: usize, height: usize)
{
    for y in 0..height
    {
        for x in 0..width
        {
            match grid[x + y * width]
            {
                Seat::None          => print!(" "),
                Seat::SeatEmpty     => print!("L"),
                Seat::SeatOccupied  => print!("#"),
            }
        }
        println!();
    }
}

fn parse_seats(input: &str, width: &mut usize, height: &mut usize) -> Vec<Seat>
{

    let split = input.split('\n').collect::<Vec<&str>>();
    //Output the width and height of the grid
    *width = split[0].len();
    *height = split.len();

    let mut seat_grid = vec![Seat::None; *width * *height];

    for y in 0..*height
    {
        let mut chars = split[y].chars();
        for x in 0..*width
        {
            match chars.next().unwrap()
            {
                'L' => seat_grid[x + y * *width] = Seat::SeatEmpty,
                '#' => seat_grid[x + y * *width] = Seat::SeatOccupied,
                _ => {}
            }
        }
    }
    return seat_grid;
}


static EXAMPLE: &'static str = "L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL";

static INPUT: &'static str = "LLLLLL.LLLLL.LLLL..LLLLLLLLL.LLLLLLLLLLLLLLLLL.LLLLLLL.LLLLL..LLLLLLLLLLLLLLLLLLLL.LLLLLL.LLLLLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLLLLLLLLL.LLLLLLLLLLLLLL.LLLLL.LLLLLL.LLLLLLL
LLLLLLLLLLLLLLLLLL.LLLLLLLLL.LLLLLLLLLLLLLLLLL.LLLLLLLLLLLLLL.LLLLLLLLLLLLLL.LLLLLLLLLLLLLLLLLLLL
LLLLLL.LLLLL.LLLLLLLLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLL.LLLLLLLLLLLLLLLLLLLL.LLLLLL.LLLLLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLL.LLLLLL.LLLLL.LLLLLLLLLLLLLLLLLLLLL.LLLLLLL
LLLLLLLLLLLL.LLLLL.LLLLLLLLLLLLLLLLLLL.LLLLLLL.LLLLLLLLLLLLLL.LLLLL.LLLLLLLL.LLLLLLLLLLLL.LLLLLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLL.L.LLLLLLL.LLLLLLLLLLLLLLL.LLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL
LLLLLLLLLLLL.LLLLL.LLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLLLLLLLLLLLLLLL.LLLLLLLL.LLLLLLLLLLLL.LLLLLLL
...L....L.LLL.L.L....LL..LL....L.L..L.....L....LLL..L....LL....LLLL.L.........L..LL..............
LLLLLL.LLLLLLLLLLLLLLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLL.LLLLL.LLLLLLLLLLLLLL.LLLLLLLLLLLLLL
LLLLLLLLLLLL.LLLLLLLLLLLLLLLLLLLLLLLL..LLLLLLL.LLLLLL..LLLLLLLLLLLL.LLLLLLLL..LLLL.LLLLLL.LLLLLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLLLLLLLLLLLL.LLLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLLLLLLLLLLL
LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL.LLLLLLL.LLLLLL.LLLLL.LLLLLLLLLLLLLL.LLLLLLLLLLLLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLL.LLLLLLL
LLL...L.L..L.LL.L.L...L....L..L.LLL.L..L....L.L.L...LLL..L...LLL..L..LL.L.L.LL.LL..L...L......L.L
LLLLLL.LLLL.LLLLLLLLLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLLLLLLLL.LLLLLLLL.LLLL.LLLLLLL.LLLLLLL
LLLLLL.LLLLL.LLLLLLLLLLLLLLL.LLLLLLLLL.LLLLLL..LLLLLLLLLLLLLL..LLLL.LLLLLLLLLLLLLL.LLLLLLLLLLLLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLL.LL.LLLLLLLLLLLLLL.LLLLLLL.LLLLLLLLLLLL.LLLLLLLL.LLLLLLLLLLLL.LLLLLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLLLLL.LLLLLL.LLLLL.LLLLLLLLLLLLLL.LLLLLLLLLLLLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLL.LLLLL.LLLLLLLL.LLLLL.LLLLLL.LLLLLLL
LLLLLLLLLLLL.LLLLL.LLLLLLLLLLLLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLLLLLLLL.LLLLLLLL.LLLLLLLLLLLL.LLLLLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL.LLLLL.LLLLLL.LLLLLLL
LLLLLL.LLLLLLLLLLL.LLLLLLLLLLLLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLL.LLLLLLLLLLLLLL.LLLLL.LLLLLL.LLLLLLL
......L.L.L...LLLL.L....L...LL....LL.LL..L.L..L..L..LL.....L..LL...LLL...L.LL.L.L...L.L.......L..
LLLLLL.LLLLLLLLLLL.LLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLLLLLLLLL.LLLLLLLLLLLLLL.LLLLLLLLLLLLLLLLLLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL.LLLLL..LLLLLLL.LLLLLLLLLLLL.LLLLLLL
LLLLLL.LLLLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLL.LLLLLLLLLLLLLL.LLLLLLLLLLLLLL
LLLLLL.LLLLL.LLLLLLLLLLLLLLL.LLLLLLLLL.LLLLLLLLLLLLLLL.LLLLLL.LLLLL.LLLLLLLL.LLLLL.LLLL.LLLLLLLLL
..L...LL..L.LL..L.L.LL.LLLL......L...LLL..L.L.L..L...LL.LLL..L..L....L.L.LLL..L..L...LL...L.L..LL
LLLLLLLLLLLL.LLLLL.LLLLLLLLLLLLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLL.L.LLL.LLLLLLLL.LLLLL.LLLLLL.LLLLLLL
LLLLLL.LLLLL.LLLLLLLLLLLLLLLLLLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLLLLLLLL.LLLLLLLL.LLLLL.LLLLLL.LLLLLLL
LLLLLLLLLLLLLLLLLL.LLLLLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLL.LLLLLL.LLLLL.LLLLLLLL.LLLLL.LLLLLL.LLLLLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLL.LLLLLLLLLLLLLL.LLLLLLLLLLLL.LLLLLLL
LLLLLL.LLLLLLLLLLL.LLLLLLLLL.LLLLLLLLL.LLLL.LL.LLLLLLL.LLLLLL.LLLLL.LLLLLLLL.LLLLLLLLLLLL.LLLLLLL
..L.......L.LL..LL..L.L....LLL.L....LL...L.L...L.....L...LL..LL....LL.....LL.L....L..............
LLLLLLLLLLLL.LLLLL.LLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLL.LLLLLLLLLLLLLL.LLLLL.LLLLLL.LL.LLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLLLLLLLLLLLL.LLLLLLL.LLLLLLLLLLLLLLLLLLLL.LLLLLLLL.LLLLLLLLLLLL.LLLL.LL
LLLLLL.LLLLL.LLLLL.LLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLLLL.LLLLLLL
LLLLLL.LLLLLLLLLLLLLLLLLLLLL.LLLLLLLLL.LLLLLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLLLL.LLLLL.LLLLLL.LLLLLLL
LL.......L...L..L.L.LL...LL...........LL.L...L.....LL.......LL....LLL.L.LLL..L.L.L.L...LL..LL....
LLLLLLLLLLLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLLLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLL.LLLLLLL
LLLLLL.LLLLL.LLLL..LLLLLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLL.LLLLL.LLLLLL.LLLLLLLL.LLLLLLLLLLLL.LLLLLLL
LLLLLLLLLLLLLLLLLL.LLLLLLLLLLLLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLL.LLLLL.LLLLLLLL.LLLLL.LLLLLL.LLLLLLL
LLLLLL.LLLLL.LLLLLLLLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLLLLLLLLLLLLLLLLL.LLLLL.LLLLLLLLLLLLLL
LLLLLL.LLLLLLLLLLL.LLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLL.LLLLL.LLLLLLLL.LLLLLLLLLLLLLLLLLLLL
LLLLLL.LLLLL.LLLLLLLLLLLLLLL.LLLLLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLL.LLLLLLL
L....L.L.L...LL...LL.....L..L.LLLLLL.L...........L..LL.L....L.....LL..LL.L...L..LLL........L.LLL.
LLLLLL.LLLLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLLLLLLL.LLLLLLL.LLLLLL.LLLLL.LLLLLLLLLLLLLL.LLLLLLLLLLLLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLLLLLLLLL.LLLLL.LLLLLLLL.LLLLLLLLLLLL.LLLLLLL
LLLLLLLLLLLL.LLLLLLLLLLLLLLL.LLLLLL.LLLLLLLLLL.LLLLLLLLLLLLLL.LLLLLLLLLLLLLLLLLLLL.LLLLLL.LLLLLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLL..LLLLLLLLLLLLLLLLLLLLLLLL.LLLLLL.LLLLL.LLLLLLLL.LLLLL.LLLLLL.LLLLLLL
LLLLLL.LLLLLLLLLLLLLLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLL.LLLLLLL
LL.L..................L..L..L..L..L...LL....LL.L......L..LL...LL...........LL....LL..L...L.......
LLLLLL.LLLLL.LLLLLLLLLLLLLLL.LLLLLLLLLLLLLLLLL.LLLLLLLLLLLLLL.LLLLL.LLLLLLLL.LLLLL.LLLLLL.LL.LLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLLLLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLLLLLL
LLLLLLLLLLLL.LLLLL.LLLLLLLLL.LLLLLLLLL.LLLLLLL.L.LLLLL.LLLLLL.LLLLL.LLLLLLLL.LLLLLLLLLLLL.LLLLLLL
LLLLLLLLLLLL.LLLLL.LL.LLLLLL.LLLLLLLLL.LLLLLLLLLLLLLLLLLLLLLL.LLLLL.LLLLLLLLLLLLLLLLLLLL..L.LLLLL
L...L.LL.....L...L....L...L...L..L.L..L..L..LL.L.LL..L....L.L.L..L.L.L.LL......LL.L.L.LLLLL...L.L
LLLL.L.LLLLL.LLLLL.LLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLLLLLLLLL.LLLLLLLLLLLLLLLL.LLL.LLLLLL.LLLLLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLL.LLLLLLLLLLLLLLLLLLLLLLL.L.LLLLLLLLLLLL.LLLLLLLL.LLLLL.LLLLLL.LLLLLLL
LLLLLLLLLLLL.LLLLL.LLLLLLLLL.LLLLLLLLL.LLLLLLLLLLLLLLL.LLLLLLLLLLLL.LLLLLLLL.LLLLL.LLLLLL.LLLLLLL
LLLLLLLLLLLL.LLLLL.LLLLLLLLL.LLLLLLLLL..LLLLLLLLLLLLLL.LLLLLLLLLLLL.LLLLLLLL.LLLLL.LLLLLLLLLLLLLL
LLLLLL.LLLLL.LLLLLLLLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLLLLLLLLLLLL.LLLLLLL.LLLLLLLLLLLLLL.LLLLLLLLLLLLLL.LLLLLLLLLLLLLLLLLLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL.LLLLL.LLLLLLLL.LLLLL.LLLLLLLLLLLLLL
LLLLLLLLLLLL.LLLLL.LLLLLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLL.LLLLLL.LLLLLLLLLLLLLL.LLLLL.LLLLLLLLLLLLLL
..L..L.....LLL.......LL....L...L..LLL..L.......L......L.L....L..L....LLL.LL.L.....LL...L..LLLL...
LLLLLL.LLLLL.LLLLL.LL.LLLLLL.LLLLLLLLLLLLLLLL..LLLLLLLLLLLLLL.LLLLL.LLLLLLLL.LLLLL.L.LLLL.LLLLLLL
LLL.LL.LLLLL.LLLLL.LLLLLLLLLLLLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLL.LLLLLLLLLLLLLL.LLLLLLLLLLLL.LLLLLLL
LLLLLL.LLLLLLLLLLL.LLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLLLLLLLLLLLL.LLLLL.LLLLLLLL.LLLLL.LLLLLL.LLLLLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLL.LLLLLLLLLLLLLLLLL.LLL.LLL.LLLLLL.LLLLL.LLLLLLLL.LLLLL.LLLLLL.LLLLLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLLLLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLLLLLL
LLLLLLLLLLLL.LLLLL.LLLLLLLLL.LLLLLLLLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLL.LL.LLLLLLLLLLLLLLLLLL.LLLLLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLL.LL.LLLLLL..LLLLLLLLLLLLLL.LLLLLLLLLLLL.LLLLLLLL.LLLL.LLLLLLL.LLLLLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLL.L.LLLLLL.LLLLLLLL.LLLLLLL.LLLLLL.LLLLL.LLLLLLLLLLLLLL.LLLLLL.LLLLLLL
LLLLLLLLLLLL.LLLLL.LLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLLLLLLLL.LLLLLLLLLLLLLLLLLLLLL.LLLLLLL
...LLLL.LL.L.L........LLL...L.LLL....L.L..LLL.L..L..L...L.L..L...LL..LL.L.LL..LLL.L....L....L.L..
LLLLLL.LLLLL.LLLLL.LLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLLLLLLLLL.LLLLL.LLLLLL...LLLLL.LLLLLL.LLLLLLL
LLLLLL.LLLLL.LLLLLLLLLLLLLLL.LLLLLLLLL.LLLL.LLLLL.LLLL.LLLLLL.LLLLLLLLLLLLLL.LLLLL.LLLLLL.LLLLLLL
LLLLLLLLLLLL.LLLLLLLLLLLLLLL.LLLLLLLLLLLLLLLLL.LLLLLLL.LLLLLLLLLLLL.LLLLLLLLLLLLLL.LLLLLLLLLLLLLL
LLLLLL.LLLLL.LLLLLLLLLLLLLLL.LLLLLLLLL.LLL.LLL.LLLLLLLLLLLLLL.LLLLL.LLLLLLLL.LLLLL.LLLLLL.LLLLLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLL.LLLLLLLLLLLLLLLLL.LLLLLLL.LLLLLL.LLLLL.LLLLLLLLLLLLLLLLLLLLL.LLLLLLL
LLLLLL.LLLLLLLL.LL.LLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLLLLLLLL.LLLLLLLL.LLLLL.LLLLLL.LLLLLLL
LLLLLL.LLLLLLLLLLL.LLLLLLLLL.LLLLLL.LLLLLLLLLLLLLLLLLL.LLLLLLLLLLLL.LLL..LLL.LLLLLLLLLLLLLLLLLLLL
.....L.L.LL.....L.L.LL.L...L....LLL........LL.L....LLL..LLL.L..LL..L......L..L...L.L.L.....L.L...
LLLLLLLLLLLLLLLLLL.LL.LLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLL.LLLLL.LLLLLLLLLLLLLL.LLLLL..LLLLLLL
LLLLLL.LLLLL.LLLLLLLLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLL.LLLLL.LLLLLLLLLLLLLLLLLLLLL.LLLLLLL
LLLLL..LLLLL.LLLLL.LLLLLLLLLLLLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLLLLLLLL.LLLLLLLLLLLLLL.LLLLLLLLLLLLLL
LLLLLLLLLLLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLLLLLLL.LLLLLLL.LLLLLL.LLLLL.LLLLLL.L.LL.LL.LLLLLLLLLLLLLL
LLLLLL.LLLLLLLLLLL.LLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLLLLL.LLLLLL.LLLLLLLLLLLLLL.LLLLL.LLLLLLLLLLLLLL
LLLLLLLLLLLL.LLLLL.LLLLLLLLL.LL.LLLLLL.LLLLLLL.LLLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLLLLLLLLLLL.LLLLLLL
LLLLLL.LLLLL..LLLL.LLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLLLLLLLLLLLLLLLLL.LLLLL.LLLLLL.LLLLLLL
LLLLLLLLLLLL.LLLLLLLLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLLLLLLLL.LLLLLLLL.LLLLL.LLLLLL.LLLLLLL
LLLLLLLLLLLL.LLLL.LLLLLLLLLL.LLLLLLLLLLLLLLLLL.LLLLLLL.LLLLLL.LLLLL.LLLLLLLL.LLLLL.LLLLLLLLLLLLLL
LLLLLL.LLLLL.LLLLL.LLLLLLLLLLLLLLLLLLL.LLLLLLL.L.LLLLLLLLLLLLLLLLLLLLLLLLLLL.LLLLLLLLLLLL.LLLLLLL
LLLLLL.LL.LLLLLLLLLLLLLLLLLL.LLLLLLLLL.LLLLLLLLLLLLLLL.LLLLLL.LLLLL.LLLLLLLL.LLLLLLLLLLLL.LLLLLLL
LLLLL.LLLLLL.LLLLL.LLLLLLLLL.LLLLLLLLL.LLLLLLL.LLLLLLL.LLLLLL.LLLLL.LLLLLLLL.LLLLL.LLLLLL.LLLLLLL";

