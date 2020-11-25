//use std::ops::{Index, IndexMut};
//
//pub struct Memory
//{
//
//}
//
//impl Memory
//{
//    pub fn new(initial_size: i64)
//    {
//
//    }
//}
//
//
//
//impl Index<&'_ i64> for Memory
//{
//    type Output = i32;
//    fn index(&self, s: &str) -> &i32 {
//        match s {
//            "x" => &self.x,
//            "y" => &self.y,
//            _ => panic!("unknown field: {}", s),
//        }
//    }
//}
//
//impl IndexMut<&'_ str> for Foo {
//    fn index_mut(&mut self, s: &str) -> &mut i32 {
//        match s {
//            "x" => &mut self.x,
//            "y" => &mut self.y,
//            _ => panic!("unknown field: {}", s),
//        }
//    }
//}
//