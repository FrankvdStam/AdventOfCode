//
// Created by Frank on 02/03/2020.
//

#ifndef ADVENTOFC_INTCODE_COMPUTER_H
#define ADVENTOFC_INTCODE_COMPUTER_H

#include <stddef.h>
#include <stdbool.h>
#include "instruction.h"

typedef enum
{
    halt_none  ,
    halt_input ,
    halt_output,
    halt_end
} halt_reason_t;


typedef struct
{
    //Store original program
    char* program;
    size_t program_size;

    //Memory, size and program counter
    long long int* memory;
    size_t memory_size;
    size_t program_ptr;

    //input buffer, in case of simulated input
    long long int* input;
    size_t input_size;
    size_t input_ptr;//points to the next active input
    size_t input_add_ptr;

    long long int* output;
    size_t output_size;
    size_t output_ptr;

    size_t relative_base_ptr;

    //Flags
    bool halted;//True if computer is halted, find the reason in the halt reason flag
    bool disassemble;//enables disassembly while running to simplify debugging
    bool simulate_input;//enables simulated input - input will be fetched from the input buffer instead of the stdin steam
    bool print_output;//prints the output values, they are always stored in the output buffer
    bool halt_before_input;//enables halt before fetching an input
    bool halt_after_output;//enables halt after pushing an ouput
    halt_reason_t halt_reason;
} icc_t;



void icc_init_program(char* program, long long int** memory, size_t* memory_size);


void icc_free(icc_t* computer);
void icc_init(icc_t* computer, char* program, char* simulated_input);

long long int icc_memory_read(icc_t* computer, addressing_mode_t addressing_mode, long long int program_ptr);
void icc_memory_write(icc_t* computer, long long int address, long long int value);

void icc_step(icc_t* computer);
void icc_run(icc_t* computer);

void icc_print_memory(icc_t* computer);
char* icc_computer_memory_to_string(icc_t* computer);
char* icc_memory_to_string(long long int* memory, size_t memory_size);

void icc_use_simulated_input(icc_t* computer, size_t size);
void icc_add_input(icc_t* computer, long long int input);

#endif //ADVENTOFC_INTCODE_COMPUTER_H
