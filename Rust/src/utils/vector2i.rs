
pub struct Vector2i
{
    pub(crate) x: i32,
    pub(crate) y: i32,
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
    pub fn add(&self, vec: &Vector2i) -> Vector2i
    {
        return Vector2i
        {
            x: self.x + vec.x,
            y: self.y + vec.y,
        }
    }
}
