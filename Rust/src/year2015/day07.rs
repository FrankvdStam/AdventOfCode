use std::str::FromStr;
use std::fmt::Error;
use std::collections::HashMap;

pub fn problem1()
{
    //parse
    let mut instructions = Vec::new();
    for line in EXAMPLE.split("\n")
    {
        let instruction = Instruction::from_str(line).unwrap();
        instructions.push(instruction);
    }

    //solve
    let mut wire_values = HashMap::new();
    let mut change = false;
    loop
    {
        for mut i in instructions
        {
            //Only look at unsolved wires
            if wire_values.contains_key(i.output_name.as_str())
            {
                match i.operator
                {
                    Operator::Value =>
                    {
                        wire_values.insert(i.output_name, i.output_value);
                        change = true;
                    }

                    Operator::And =>
                    {
                        if wire_values.contains_key(i.input_a_name.as_str()) && wire_values.contains_key(i.input_b_name.as_str())
                        {
                            i.output_value = wire_values.get(i.input_a_name.as_str()).unwrap() & wire_values.get(i.input_a_name.as_str()).unwrap();
                        }
                    }
                    Operator::Or =>
                    {

                    }
                    Operator::LeftShift=> {}
                    Operator::RightShift=> {}
                    Operator::Not=> {}
                }
            }
        }
        if !change
        {
            break;
        }
    }

}

pub fn problem2()
{

}

#[derive(PartialEq, Eq)]
enum Operator
{
    Value,
    And,
    Or,
    LeftShift,
    RightShift,
    Not,
}




struct Instruction
{
    input_value: u32,

    input_a_name: String,
    input_b_name: String,
    operator: Operator,

    output_name: String,
    output_value: u32,
}

impl FromStr for Instruction
{
    type Err = Error;

    fn from_str(s: &str) -> Result<Self, Self::Err>
    {
        let chars = s.chars().collect::<Vec<char>>();

        let mut instruction = Instruction
        {
            input_value: 0,

            input_a_name: String::new(),
            input_b_name: String::new(),

            operator: Operator::Value,

            output_name: String::new(),
            output_value: 0,
        };


        //Parse the type of operator
        if s.contains("AND")
        {
            instruction.operator = Operator::And;
        }
        else if s.contains("OR")
        {
            instruction.operator = Operator::Or;
        }
        else if s.contains("LSHIFT")
        {
            instruction.operator = Operator::LeftShift;
        }
        else if s.contains("RSHIFT")
        {
            instruction.operator = Operator::RightShift;
        }
        else if s.contains("NOT")
        {
            instruction.operator = Operator::Not;
        }
        else
        {
            instruction.operator = Operator::Value;
        }

        //===================================================================================================================
        //Are there 2 input names?
        if instruction.operator != Operator::Value && instruction.operator != Operator::Not
        {
            let mut word = 1;
            for i in 0..chars.len()
            {
                if chars[i] == ' '
                {
                    word += 1;
                    if word > 3
                    {
                        break;
                    }
                }
                else
                {
                    if word == 1
                    {
                        instruction.input_a_name.push(chars[i]);
                    }

                    if word == 3
                    {
                        instruction.input_b_name.push(chars[i]);
                    }
                }
            }
        }


        //Find the value in value cases
        if instruction.operator == Operator::Value
        {
            let mut number = String::new();
            let first_whitespace_position = s.find(' ').unwrap();
            for i in 0..first_whitespace_position
            {
                number.push(chars[i]);
            }
            instruction.input_value = number.parse::<u32>().unwrap();
        }


        //Find the input string in not cases
        //always starts at index 4
        if instruction.operator == Operator::Not
        {
            let mut index = 4;
            loop
            {
                if chars[index] == ' '
                {
                    break;
                }
                else
                {
                    instruction.input_a_name.push(chars[index]);
                }
                index += 1;
            }
        }



        //Output is required in all cases
        let last_whitespace_position = s.rfind(' ').unwrap();
        for i in last_whitespace_position..chars.len()
        {
            instruction.output_name.push(chars[i]);
        }

        return Ok(
            instruction
        );
    }
}


static EXAMPLE: &'static str = "123 -> x
456 -> y
x AND y -> d
x OR y -> e
x LSHIFT 2 -> f
y RSHIFT 2 -> g
NOT x -> h
NOT y -> i";




static INPUT: &'static str = "";