#![allow(dead_code)]

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


pub fn greatest_common_divisor(mut a: usize, mut b: usize) -> usize
{
    let mut temp;
    while b > 0
    {
        temp = b;
        b = a % b;
        a = temp;
    }
    return a;
}

pub fn greatest_common_divisor_vec(mut list: Vec<usize>) -> usize
{
    if list.len() < 2
    {
        panic!("List must have at least 2 elements.");
    }

    while list.len() > 1
    {
        let temp = greatest_common_divisor(list[0], list[1]);
        list.remove(0);
        list.remove(0);
        list.insert(0, temp);
    }

    return list[0];
}

