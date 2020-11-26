pub fn min2(a: i64, b: i64) -> i64
{
    return if a < b { a } else { b };
}

pub fn max2(a: i64, b: i64) -> i64
{
    return if a > b { a } else { b };
}

pub fn min3(a: i64, b: i64, c: i64) -> i64
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

///Returns the difference between two whole positive or negative numbers
pub fn difference(a: i64, b: i64) -> i64
{
    //negate the effect of negative numbers by adding the smallest negative to both numbers, keeping the ratio intact
    let min = min2(a, b);
    let min_abs = min.abs();
    let negated_a = a + min_abs;
    let negated_b = b + min_abs;

    //now subtract the smallest from the largest number
    let negated_min = min2(negated_a, negated_b);
    let negated_max = max2(negated_a, negated_b);
    return negated_max - negated_min;
}

