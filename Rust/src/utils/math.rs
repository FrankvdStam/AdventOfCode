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

pub fn digits(mut num: i64) -> impl Iterator<Item = i64> {
    let mut divisor = 1;
    while num >= divisor * 10 {
        divisor *= 10;
    }

    std::iter::from_fn(move || {
        if divisor == 0 {
            None
        } else {
            let v = num / divisor;
            num %= divisor;
            divisor /= 10;
            Some(v)
        }
    })
}

