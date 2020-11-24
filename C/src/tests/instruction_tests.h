//
// Created by Frank on 02/03/2020.
//

//ABCDE
// 1002
//
//DE - two-digit opcode,      02 == opcode 2
// C - mode of 1st parameter,  0 == position mode
// B - mode of 2nd parameter,  1 == immediate mode
// A - mode of 3rd parameter,  0 == position mode,
//                                  omitted due to being a leading zero

#include <assert.h>
#include <stdbool.h>
#include "../intcode_computer/intcode_computer.h"
#include "../intcode_computer/instruction.h"
#include "custom_assert.h"


int test_parse_instruction()
{
    addressing_mode_t addressing_mode1 = no_addressing_mode;
    addressing_mode_t addressing_mode2 = no_addressing_mode;
    addressing_mode_t addressing_mode3 = no_addressing_mode;
    opcode_t opcode = no_opcode;

    instruction_parse_opcode(00002, &addressing_mode1, &addressing_mode2, &addressing_mode3, &opcode);
    custom_assert(opcode == multiply, "wrong opcode");
    assert(addressing_mode1 == position);
    assert(addressing_mode2 == position);
    assert(addressing_mode3 == position);

    instruction_parse_opcode(11199, &addressing_mode1, &addressing_mode2, &addressing_mode3, &opcode);
    custom_assert(opcode == halt, "wrong opcode");
    assert(addressing_mode1 == immediate);
    assert(addressing_mode2 == immediate);
    assert(addressing_mode3 == immediate);

    instruction_parse_opcode(3, &addressing_mode1, &addressing_mode2, &addressing_mode3, &opcode);
    custom_assert(opcode == input, "wrong opcode");
    assert(addressing_mode1 == position);
    assert(addressing_mode2 == position);
    assert(addressing_mode3 == position);



    return 0;
}