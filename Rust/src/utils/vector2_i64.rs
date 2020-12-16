#![allow(non_camel_case_types)]
#![allow(unused_imports)]

use std::fmt::Display;
use core::fmt;
use crate::utils::math::difference;

pub struct Vector2_i64
{
    pub x: i64,
    pub y: i64,
}

impl Copy for Vector2_i64 { }

impl Clone for Vector2_i64
{
    fn clone(&self) -> Self
    {
        return Vector2_i64
        {
            x: self.x,
            y: self.y,
        };
    }
}

impl PartialEq for Vector2_i64
{
    fn eq(&self, vec: &Self) -> bool
    {
        return  self.x == vec.x && self.y == vec.y;
    }
}

impl fmt::Display for Vector2_i64
{
    fn fmt(&self, f: &mut fmt::Formatter<'_>) -> fmt::Result {
        write!(f, "{}", self.to_string())
    }
}

impl Vector2_i64
{
    pub fn new(x: i64, y: i64) -> Self
    {
        Vector2_i64
        {
            x,
            y
        }
    }

    pub fn add(&self, vec: &Vector2_i64) -> Vector2_i64
    {
        return Vector2_i64
        {
            x: self.x + vec.x,
            y: self.y + vec.y,
        }
    }

    pub fn to_string(&self) -> String
    {
        let mut string = String::new();
        string.push('(');
        string.push_str(self.x.to_string().as_str());
        string.push(',');
        string.push_str(self.y.to_string().as_str());
        string.push(')');
        return string;
    }

    pub fn from_str(str: &str) -> Self
    {
        let mut temp = String::from(str);
        temp.remove(0);
        let parts = temp.split(',').collect::<Vec<&str>>();
        let x = parts[0].parse::<i64>().unwrap();
        let mut temp2 = String::from(parts[1]);
        temp2.remove(temp2.len() - 1);
        let y = temp2.parse::<i64>().unwrap();

        return Vector2_i64::new(x, y);
    }

    pub fn rotate_right(&self) -> Vector2_i64
    {
        return Vector2_i64::new(self.y, -self.x);
    }

    pub fn rotate_left(&self) -> Vector2_i64
    {
        return Vector2_i64::new(-self.y, self.x);
    }
}
