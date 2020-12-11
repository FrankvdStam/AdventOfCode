
pub fn problem1()
{
    let mut jolts = parse_jolts(INPUT);
    let device_jolts = jolts.iter().max().unwrap() + 3;
    jolts.push(device_jolts);


    //Assuming we don't have to check for paths that don't add up to the device's jolts
    //Always picking the next smallest adapter. (parse function sorts the jolts)

    let mut one_jolt_diff = 0;
    let mut three_jolt_diff = 0;

    let mut current_jolts = 0;

    for i in 0..jolts.len()
    {
        let diff = jolts[i] - current_jolts;

        match diff{
            1 => one_jolt_diff += 1,
            3 => three_jolt_diff += 1,
            _ => {}
        }

        current_jolts += diff;
    }


    println!("{}", one_jolt_diff * three_jolt_diff);
}








///Example 1:
///(0), 1, 4, 5, |(6), 7, 10, |11, 12, 15, 16, 19, (22)
///|    12, 15, 16, 19, (22)
///
///|(7), 10, | 11, 12, 15, 16, 19, (22)
///|     12, 15, 16, 19, (22)
///
///
///(0), 1, 4, (6, 7), 10,| 11, 12, 15, 16, 19, (22)
///|     12, 15, 16, 19, (22)
///
///(0), 1, 4,   (7), 10,| 11, 12, 15, 16, 19, (22)
///|     12, 15, 16, 19, (22)
///
///
///
///
///
///Possible jumps at each step
///(0), 1, 4,   5, 6, 7, 10, 11, 12, 15, 16, 19, (22)
///1    4  567  67 7  10 1112    15  16  19  22
///1	 1	3    2  1  1  2       1   1   1   1
///
///
///
///2 paths lead to 2 paths
///1 path leads to 1 path
///1 ------|----|-----
///|	 |-----
///|
///|----|-----
///|	 |-----
///|
///|----|-----
///
///however, we can also go 4 - 6 - 7
///
///if we find patterns in the amount of jumps at each step:
///11321121111
///
///
///running this on my input (programmatically) gives this:
///[3, 3, 2, 1, 1, 3, 2, 1, 1, 3, 3, 2, 1, 1, 1, 3, 3, 2, 1, 1, 3, 3, 2, 1, 1, 3, 3, 2, 1, 1, 3, 3, 2, 1, 1, 3, 3, 2, 1, 1, 2, 1, 1, 3, 2, 1, 1, 3, 2, 1, 1, 2, 1, 1, 1, 3, 3, 2, 1, 1, 1, 1, 3, 3, 2, 1, 1, 1, 1, 1, 3, 3, 2, 1, 1, 3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 1, 3, 3, 2, 1, 1, 2, 1, 1, 0]
///
///What we can do is use each pattern to find how many different paths spawn
///There are only 3 patterns
///
///==========================================
///3321 spawns 7 new paths
///
///1    2   3   4    5    8
///234  345 45  5
///3    3   2   1
///
///
///1 - 2 - 3 - 4 - 5 - 8
///- 5 - 8
///- 4 - 5 - 8
///- 5 - 8
///
///1 - 3 - 4 - 5 - 8
///- 5 - 8
///
///1 - 4 - 8
///
///==========================================
///321 leads to 4 paths
///
///2   3   4    5    8
///345 45  5
///3   2   1
///
///
///2 - 3 - 4 - 5 - 8
///- 5 - 8
///
///2 - 4 - 5 - 8
///
///2 - 5 - 8
///
///==========================================
///21 leads to 4 paths
///
///3   4    5    8
///45  5
///2   1
///
///3 - 4 - 5 - 8
///3 - 5 - 8
///
///
///paths = 5

pub fn problem2()
{
    let mut jolts = parse_jolts(INPUT);
    let device_jolts = jolts.iter().max().unwrap() + 3;
    jolts.insert(0, 0);
    jolts.push(device_jolts);

    //Find the number of possible jumps for each "node"

    let mut possible_jumps = Vec::new();

    for i in 0..jolts.len()
    {
        //Consider the next 3 jolts
        let mut count = 0;
        for j in i+1..i+4
        {
            if j < jolts.len() && jolts[j] <= jolts[i] + 3
            {
                count += 1;
            }
        }
        possible_jumps.push(count);
    }

    let mut index = 0;
    let mut count: u64 = 1;
    while index < possible_jumps.len()
    {
        if possible_jumps[index] != 1 && index + 1 < possible_jumps.len()
        {
            //Get 33/32/21 - could compare this differently but a match is very readable
            let mut pattern = String::from(possible_jumps[index].to_string());
            pattern.push_str(possible_jumps[index + 1].to_string().as_str());

            match pattern.as_str()
            {
                "33" =>
                {
                    count *= 7;
                    index += 4;
                }
                "32" =>
                {
                    count *= 4;
                    index += 3;
                }
                "21" =>
                {
                    count *= 2;
                    index += 2;
                }
                _ =>
                {
                    panic!("unsupported pattern: {}", pattern);
                }
            }
        }
        else
        {
            index += 1;
        }
    }
    println!("{}", count);
}



fn parse_jolts(input: &str) -> Vec<i64>
{
    let mut jolts = Vec::new();
    for s in input.split('\n').into_iter()
    {
        jolts.push(s.parse::<i64>().unwrap());
    }
    jolts.sort();
    return jolts;
}


static EXAMPLE1: &'static str = "16
10
15
5
1
11
7
19
6
12
4";


static EXAMPLE2: &'static str = "28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
10
3";


static INPUT: &'static str = "151
94
14
118
25
143
33
23
80
95
87
44
150
39
148
51
138
121
70
69
90
155
144
40
77
8
97
45
152
58
65
63
128
101
31
112
140
86
30
55
104
135
115
16
26
60
96
85
84
48
4
131
54
52
139
76
91
46
15
17
37
156
134
98
83
111
72
34
7
108
149
116
32
110
47
157
75
13
10
145
1
127
41
53
2
3
117
71
109
105
64
27
38
59
24
20
124
9
66";