//
// Created by Frank on 02/03/2020.
//

#ifndef ADVENTOFC_INSTRUCTION_H
#define ADVENTOFC_INSTRUCTION_H

#include <stddef.h>

typedef enum
{
    no_addressing_mode = 0,
    position = 1,
    immediate = 2,
    relative = 3
} addressing_mode_t;

typedef enum
{
    no_opcode = -1,
    add = 1,
    multiply = 2,
    input = 3,
    output = 4,
    jump_if_true = 5,
    jump_if_false = 6,
    less_than = 7,
    equals = 8,
    adjust_relative_base = 9,
    halt = 99,
} opcode_t;


void instruction_parse_opcode(long long int code, addressing_mode_t* addressing_mode1, addressing_mode_t* addressing_mode2, addressing_mode_t* addressing_mode3, opcode_t* opcode);
size_t instruction_width(opcode_t opcode);
char* instruction_disassemble(opcode_t opcode, addressing_mode_t addressing_mode1, addressing_mode_t addressing_mode2, addressing_mode_t addressing_mode3, long long int parameter1, long long int parameter2, long long int parameter3);

#endif //ADVENTOFC_INSTRUCTION_H
