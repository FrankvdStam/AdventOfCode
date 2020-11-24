pub fn min2(a: i32, b: i32) -> i32
{
    if a < b
    {
        return a;
    }
    return b;
}

pub fn min3(a: i32, b: i32, c: i32) -> i32
{
    return min2(a, min2(b, c));
}