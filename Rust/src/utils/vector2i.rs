
pub struct Vector2i
{
    pub x: i64,
    pub y: i64,
}

impl Copy for Vector2i { }

impl Clone for Vector2i
{
    fn clone(&self) -> Self
    {
        return Vector2i
        {
            x: self.x,
            y: self.y,
        };
    }
}

impl PartialEq for Vector2i
{
    fn eq(&self, vec: &Self) -> bool
    {
        return  self.x == vec.x && self.y == vec.y;
    }
}



impl Vector2i
{
    pub fn new(x: i64, y: i64) -> Self
    {
        Vector2i
        {
            x,
            y
        }
    }

    pub fn add(&self, vec: &Vector2i) -> Vector2i
    {
        return Vector2i
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

        return Vector2i::new(x, y);
    }
}
