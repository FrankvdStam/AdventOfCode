
pub fn problem1()
{
    let com = parse(EXAMPLE);
    com.print();

    let mut stack = Vec::new();
    stack.push(&com);

    let mut orbit_count = 0;
    let mut dept = 0;

    while stack.len() > 0
    {
        let planet = stack.pop().unwrap();

        orbit_count += planet.children.len();
        orbit_count += dept;

       // indirect_orbitters += planet.children.len();

        for i in 0..planet.children.len()
        {
            stack.push(&planet.children[i]);
        }

        dept += 1;
    }
    println!("orbits: {}", orbit_count);
}

pub fn problem2()
{

}

struct Planet
{
    name: String,
    children: Vec<Planet>
}

impl Planet{
    fn new(name: String) ->Self
    {
        return Planet
        {
            name: name.clone(),
            children: Vec::new(),
        }
    }

    fn add_to_tree(&mut self, parent_name: String, child_name: String) -> bool
    {
        if self.name == parent_name
        {
            self.children.push(Planet::new(child_name));
            return true;
        }

        for i in 0..self.children.len()
        {
            if self.children[i].add_to_tree(parent_name.clone(), child_name.clone())
            {
                return true;
            }
        }
        return false;
    }

    fn print(&self)
    {
        self.print_level(1, Vec::new());
    }

    fn print_level(&self, level: usize, leaves: Vec<usize>)
    {
        //build leading string
        let mut leave: String;
        if leaves.len() > 0
        {
            leave = String::new();
            let mut index = 0;
            let total = 4 * level;
            let mut leave_index = 0;

            'outer: while index < total
            {
                while index < leaves[leave_index]
                {
                    leave.push(' ');
                    index += 1;
                }
                leave.push('|');
                index += 1;
                leave_index += 1;

                if leave_index >= leaves.len()
                {
                    while index < total
                    {
                        leave.push(' ');
                        index += 1;
                    }
                    break 'outer;
                }
            }
        }
        else
        {
            leave = " ".repeat(4 * level).to_string();
        }

        println!("{}{}, {}", leave, self.name, self.children.len());

        //no copy trait so manual copy.
        let mut leaves_children = Vec::new();
        for i in 0..leaves.len()
        {
            leaves_children.push(leaves[i]);
        }
        if self.children.len() > 1
        {
            leaves_children.push(4 * level + 1);
        }

        for i in 0..self.children.len()
        {
            self.children[i].print_level(level + 1, leaves_children.clone());
        }
    }
}


fn parse(string: &str) -> Planet
{
    let mut pairs: Vec<(&str, &str)> = Vec::new();
    for line in string.split('\n').collect::<Vec<&str>>()
    {
        let parts = line.split(')').collect::<Vec<&str>>();
        let orbittee = parts[0];
        let orbitter = parts[1];
        pairs.push((orbittee, orbitter));
    }

    let mut com = Planet::new(String::from("COM"));

    while pairs.len() > 0
    {
        'inner: for i in 0..pairs.len()
        {
            if com.add_to_tree(pairs[i].0.to_string(), pairs[i].1.to_string())
            {
                pairs.remove(i);
                //size changes so we should break
                break 'inner;
            }
        }
    }
    return com;
}


static EXAMPLE: &'static str = "COM)B
B)C
C)D
D)E
E)F
B)G
G)H
D)I
E)J
J)K
K)L";


static INPUT: &'static str = "";