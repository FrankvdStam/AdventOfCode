
pub struct Vector3i
{
    pub x: i64,
    pub y: i64,
    pub z: i64,
}

impl Copy for Vector3i { }

impl Clone for Vector3i
{
    fn clone(&self) -> Self
    {
        return Vector3i
        {
            x: self.x,
            y: self.y,
            z: self.z,
        };
    }
}

impl PartialEq for Vector3i
{
    fn eq(&self, vec: &Self) -> bool
    {
        return  self.x == vec.x && self.y == vec.y && self.z == vec.z;
    }
}



impl Vector3i
{
    pub fn new(x: i64, y: i64, z: i64) -> Self
    {
        Vector3i
        {
            x,
            y,
            z
        }
    }

    pub fn add(&self, vec: &Vector3i) -> Vector3i
    {
        return Vector3i
        {
            x: self.x + vec.x,
            y: self.y + vec.y,
            z: self.z + vec.z,
        }
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

        return Vector3i::new(x, y, z);
    }
}
