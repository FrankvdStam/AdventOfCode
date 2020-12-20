#![allow(dead_code)]
#![allow(non_camel_case_types)]
pub struct Vector2_usize
{
    pub x: usize,
    pub y: usize,
}

impl Copy for Vector2_usize { }

impl Clone for Vector2_usize
{
    fn clone(&self) -> Self
    {
        return Vector2_usize
        {
            x: self.x,
            y: self.y,
        };
    }
}

impl PartialEq for Vector2_usize
{
    fn eq(&self, vec: &Self) -> bool
    {
        return  self.x == vec.x && self.y == vec.y;
    }
}



impl Vector2_usize
{
    pub fn new(x: usize, y: usize) -> Self
    {
        Vector2_usize
        {
            x,
            y
        }
    }

    pub fn add(&self, vec: &Vector2_usize) -> Vector2_usize
    {
        return Vector2_usize
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
        let x = parts[0].parse::<usize>().unwrap();
        let mut temp2 = String::from(parts[1]);
        temp2.remove(temp2.len() - 1);
        let y = temp2.parse::<usize>().unwrap();

        return Vector2_usize::new(x, y);
    }
}
