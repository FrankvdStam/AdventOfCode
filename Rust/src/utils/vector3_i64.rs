#![allow(dead_code)]
#![allow(non_camel_case_types)]

use core::fmt;

#[derive(Debug, Eq, Hash)]
pub struct Vector3_i64
{
    pub x: i64,
    pub y: i64,
    pub z: i64,
}

impl Copy for Vector3_i64 { }

impl Clone for Vector3_i64
{
    fn clone(&self) -> Self
    {
        return Vector3_i64
        {
            x: self.x,
            y: self.y,
            z: self.z,
        };
    }
}

impl PartialEq for Vector3_i64
{
    fn eq(&self, vec: &Self) -> bool
    {
        return  self.x == vec.x && self.y == vec.y && self.z == vec.z;
    }
}

impl fmt::Display for Vector3_i64
{
    fn fmt(&self, f: &mut fmt::Formatter<'_>) -> fmt::Result {
        write!(f, "{}", self.to_string())
    }
}

impl Vector3_i64
{
    pub fn new(x: i64, y: i64, z: i64) -> Self
    {
        Vector3_i64
        {
            x,
            y,
            z
        }
    }

    pub fn add(&self, vec: &Vector3_i64) -> Vector3_i64
    {
        return Vector3_i64
        {
            x: self.x + vec.x,
            y: self.y + vec.y,
            z: self.z + vec.z,
        }
    }


    pub fn max(&self, vector: &Vector3_i64) -> Vector3_i64
    {
        let mut result = self.clone();

        if vector.x > result.x
        {
            result.x = vector.x;
        }

        if vector.y > result.y
        {
            result.y = vector.y;
        }

        if vector.z > result.z
        {
            result.z = vector.z;
        }

        return result;
    }

    pub fn min(&self, vector: &Vector3_i64) -> Vector3_i64
    {
        let mut result = self.clone();

        if vector.x < result.x
        {
            result.x = vector.x;
        }

        if vector.y < result.y
        {
            result.y = vector.y;
        }

        if vector.z < result.z
        {
            result.z = vector.z;
        }

        return result;
    }

    pub fn to_string(&self) -> String
    {
        let mut string = String::new();
        string.push('(');
        string.push_str(self.x.to_string().as_str());
        string.push(',');
        string.push_str(self.y.to_string().as_str());
        string.push(',');
        string.push_str(self.z.to_string().as_str());
        string.push(')');
        return string;
    }

    ///assumed format: (x,y,z)
    pub fn from_str(str: &str) -> Self
    {
        let split = str.split(',').collect::<Vec<&str>>();

        let mut str_x = split[0].to_string();
        str_x.remove(0);
        let x = str_x.parse::<i64>().unwrap();

        let y = split[1].parse::<i64>().unwrap();

        let mut str_z = split[2].to_string();
        str_z.remove(str_z.len()-1);
        let z = str_z.parse::<i64>().unwrap();

        return Vector3_i64::new(x, y, z);
    }
}


///Lookup table containing all vectors with a difference of 1 to (0,0,0)
pub const VECTOR3_I64_NEIGHBOR_LOOKUP: [Vector3_i64; 26] = [
    //Above: y+1
    //Right: x+1
    Vector3_i64 { x:  1, y:  1, z: -1}, //up
    Vector3_i64 { x:  1, y:  0, z: -1}, //middle
    Vector3_i64 { x:  1, y: -1, z: -1}, //down

    //middle: x:0
    Vector3_i64 { x:  0, y:  1, z: -1}, //up
    Vector3_i64 { x:  0, y:  0, z: -1}, //middle
    Vector3_i64 { x:  0, y: -1, z: -1}, //down

    //left:  x-1
    Vector3_i64 { x: -1, y:  1, z: -1}, //up
    Vector3_i64 { x: -1, y:  0, z: -1}, //middle
    Vector3_i64 { x: -1, y: -1, z: -1}, //down


    //bellow: y-1
    //Right: x+1
    Vector3_i64 { x:  1, y:  1, z:  1}, //up
    Vector3_i64 { x:  1, y:  0, z:  1}, //middle
    Vector3_i64 { x:  1, y: -1, z:  1}, //down

    //middle: x:0
    Vector3_i64 { x:  0, y:  1, z:  1}, //up
    Vector3_i64 { x:  0, y:  0, z:  1}, //middle
    Vector3_i64 { x:  0, y: -1, z:  1}, //down

    //left:  x-1
    Vector3_i64 { x: -1, y:  1, z:  1}, //up
    Vector3_i64 { x: -1, y:  0, z:  1}, //middle
    Vector3_i64 { x: -1, y: -1, z:  1}, //down


    //same layer
    //Right: x+1
    Vector3_i64 { x:  1, y:  1, z:  0}, //up
    Vector3_i64 { x:  1, y:  0, z:  0}, //middle
    Vector3_i64 { x:  1, y: -1, z:  0}, //down

    //left:  x-1
    Vector3_i64 { x: -1, y:  1, z:  0}, //up
    Vector3_i64 { x: -1, y:  0, z:  0}, //middle
    Vector3_i64 { x: -1, y: -1, z:  0}, //down

    //middle: x:0
    Vector3_i64 { x:  0, y:  1, z:  0}, //up
    Vector3_i64 { x:  0, y: -1, z:  0}, //down
];
