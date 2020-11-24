//
// Created by Frank on 02/03/2020.
//

#include "instruction.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "../utility/stringstream.h"

//=====================================================================================================
//Helper methods to turn numbers into enums


opcode_t int_to_opcode_t(int code)
{
    switch (code)
    {
        default:
            fprintf(stderr, "Failed to parse opcode %d\n", code);
            return no_opcode;
        case 1:
            return add;
        case 2:
            return multiply;
        case 3:
            return input;
        case 4:
            return output;
        case 5:
            return jump_if_true;
        case 6:
            return jump_if_false;
        case 7:
            return less_than;
        case 8:
            return equals;
        case 9:
            return adjust_relative_base;
        case 99:
            return halt;
    }
}

size_t instruction_width(opcode_t opcode)
{
    switch (opcode)
    {
        default:
        case no_opcode:
            fprintf(stderr, "Failed to parse opcode %d\n", opcode);
            exit(1);

        case add:
        case multiply:
        case less_than:
        case equals:
            return 4;

        case input:
        case output:
        case adjust_relative_base:
            return 2;

        case jump_if_true:
        case jump_if_false:
            return 3;

        case halt:
            return 1;
    }
}

addressing_mode_t int_to_addressing_mode_t(int mode)
{
    switch (mode)
    {
        default:
            return no_addressing_mode;
        case 0:
            return position;
        case 1:
            return immediate;
        case 2:
            return relative;
    }
}

//=====================================================================================================
//Parsing the opcode

//ABCDE
// 1002
//
//DE - two-digit opcode,      02 == opcode 2
// C - mode of 1st parameter,  0 == position mode
// B - mode of 2nd parameter,  1 == immediate mode
// A - mode of 3rd parameter,  0 == position mode,
//                                  omitted due to being a leading zer

//Parses a given opcode into enums, assigns to all given enums - no need to initialize them - does not allocate.
void instruction_parse_opcode(long long int code, addressing_mode_t* addressing_mode1, addressing_mode_t* addressing_mode2, addressing_mode_t* addressing_mode3, opcode_t* opcode)
{
	if(code > 99999)
	{
		fprintf(stderr, "Parsing out of bound opcode: %lld\n", code);
		exit(1);
	}
	
    //Leading zero's are omitted - we can assume all digits of the opcode are zero by default.
    //1. Need to count the digits in our number (can be 5, 4, 3, etc.).
    //2. Then split the digits up and use the count we did before to determine their position in a 0 initialized array.
    //Omitted leading zero's will then already be 0.

    int digits[] = { 0, 0, 0, 0, 0}; //explicit 0 initialization

    //1. Count digits
    int count = 0;
    //Have to copy the value of code since we'll modify it in our count.
    long long int code_count_copy = code;
    while(code_count_copy != 0)
    {
        count++;
        code_count_copy /= 10;
    }

    //2. split digits and set their values in the array
    int index = 0;
    while (code > 0) {
        int digit = (int)(code % 10);
        digits[index] = digit;
        code /= 10;
        index++;
    }

    //Result array will contain 2 digits of opcode and 0-3 addressing modes starting from index 0
    //[0      , 1      , 2  , 3  , 4  ]
    //[opcode0, opcode1, am0, am1, am2]
    int parsed_opcode = digits[0] +  (digits[1] * 10);
    *opcode = int_to_opcode_t(parsed_opcode);
    *addressing_mode1 = int_to_addressing_mode_t(digits[2]);
    *addressing_mode2 = int_to_addressing_mode_t(digits[3]);
    *addressing_mode3 = int_to_addressing_mode_t(digits[4]);
}


//=====================================================================================================
//Disassembling

void format_addressing_mode_disassembly(stringstream_t* stream, addressing_mode_t addressing_mode, long long int parameter)
{
    switch (addressing_mode)
    {
        default:
        case no_addressing_mode:
            stringstream_append_char_ptr(stream, "?    ");
            break;
        case position:
            stringstream_append_char(stream, '[');
            stringstream_append_long_long_int(stream, parameter);
            stringstream_append_char_ptr(stream, "]    ");
            break;
        case immediate:
            stringstream_append_long_long_int(stream, parameter);
            stringstream_append_char_ptr(stream, "    ");
            break;
        case relative:
            stringstream_append_char(stream, '*');
            stringstream_append_long_long_int(stream, parameter);
            stringstream_append_char_ptr(stream, "   ");
    }
}

char* instruction_disassemble(opcode_t opcode, addressing_mode_t addressing_mode1, addressing_mode_t addressing_mode2, addressing_mode_t addressing_mode3, long long int parameter1, long long int parameter2, long long int parameter3)
{
    stringstream_t stream;
    stringstream_init(&stream);

    switch(opcode)
    {
        default:
        case no_opcode:
            stringstream_append_char_ptr(&stream, "?       ");
            break;
        case add:
            stringstream_append_char_ptr(&stream, "add     ");
            break;
        case multiply:
            stringstream_append_char_ptr(&stream, "mult    ");
            break;
        case input:
            stringstream_append_char_ptr(&stream, "in      ");
            break;
        case output:
            stringstream_append_char_ptr(&stream, "out     ");
            break;
        case jump_if_true:
            stringstream_append_char_ptr(&stream, "jit     ");
            break;
        case jump_if_false:
            stringstream_append_char_ptr(&stream, "jif     ");
            break;
        case less_than:
            stringstream_append_char_ptr(&stream, "less    ");
            break;
        case equals:
            stringstream_append_char_ptr(&stream, "eq      ");
            break;
        case adjust_relative_base:
            stringstream_append_char_ptr(&stream, "arb     ");
            break;
        case halt:
            stringstream_append_char_ptr(&stream, "halt    ");
            break;
    }

    switch(instruction_width(opcode))
    {
        default:
        case 1:
            break;

        case 2:
            format_addressing_mode_disassembly(&stream, addressing_mode1, parameter1);
            break;
        case 3:
            format_addressing_mode_disassembly(&stream, addressing_mode1, parameter1);
            format_addressing_mode_disassembly(&stream, addressing_mode2, parameter2);
            break;
        case 4:
            format_addressing_mode_disassembly(&stream, addressing_mode1, parameter1);
            format_addressing_mode_disassembly(&stream, addressing_mode2, parameter2);
            format_addressing_mode_disassembly(&stream, addressing_mode3, parameter3);
            break;
    }
    return stringstream_finalize(&stream);
}

