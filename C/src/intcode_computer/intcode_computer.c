//
// Created by Frank on 02/03/2020.
//

#include "intcode_computer.h"
#include "instruction.h"
#include <stdlib.h>
#include <stdbool.h>
#include <stdio.h>
#include <string.h>


//=====================================================================================================
//Initialization, freeing




size_t calculate_memory_size(const char* program, size_t program_size)
{
    size_t size = 0;
    for(size_t i = 0; i < program_size; i++)
    {
        if(program[i] == ',')
        {
            size++;
        }
    }
    return size;
}


//Initializes a program from a string by allocating and parsing.
void icc_init_program(char* program, long long int** memory, size_t* memory_size)
{
    //First we need to know the size of the program. We can figure this out by counting comma's and adding one
    size_t program_length = strlen(program);
    *memory_size = 1;//initialize to 1 to account for trailing numbers
    for(size_t i = 0; i < program_length+1; i++)
    {
        if(program[i] == ',')
        {
            *memory_size += 1;
        }
    }

    //Now that we know the memory size, we can allocate
    *memory = malloc(sizeof(long long int) * *memory_size);

    //Need to copy the program in order to work with it, strtok will modify the string. strlen does not include the null terminator.
    char* program_copy = malloc((program_length + 1) * sizeof(char));
    strcpy(program_copy, program);

    //Split the string on comma's and parse the substrings to long long int
    char* substring = strtok(program_copy, ",");
    for(size_t i = 0; i < *memory_size; i++)
    {
        long long int n = strtoll(substring, NULL, 0);
        (*memory)[i] = n;
        //printf("%zu is %s\n", i, substring);
        substring = strtok(NULL, ",");
    }
    free(program_copy);
}


//Free all allocated memory for the given computer
void icc_free(icc_t* computer)
{
    free(computer->program);
    free(computer->memory);
    if(computer->simulate_input)
    {
        free(computer->input);
    }
    if(computer->output_size > 0)
    {
        free(computer->output);
    }
}

//Initializes a computer - allocates where necessary. Use icc_free to clear the memory allocated.
void icc_init(icc_t* computer, char* program, char* simulated_input)
{
    //Retain the original program
    size_t program_size = strlen(program);
    computer->program = malloc(sizeof(char) * (program_size+1)); //+1 for null terminator
    computer->program_size = program_size;
    strcpy(computer->program, program);

    //Setup input
    computer->input = NULL;
    computer->program_ptr = 0;
    computer->input_ptr = 0;
    computer->input_add_ptr = 0;
    computer->input_size = 0;
    computer->simulate_input = false;

    if(simulated_input != NULL)
    {
        //Simulated input is the exact format as a program and can be handled the same way.
        icc_init_program(simulated_input, &computer->input, &computer->input_size);
        computer->simulate_input = true;
        //Set the add pointer at the end of the given input
        computer->input_add_ptr = computer->input_size;
    }

    //Setup output, will be allocated if required
    computer->output = NULL;
    computer->output_size = 0;
    computer->output_ptr = 0;

    computer->relative_base_ptr = 0;

    computer->halted = false;
    computer->disassemble = false;
    computer->print_output = true;
    computer->halt_before_input = false;
    computer->halt_after_output = false;
    computer->halt_reason = halt_none;

    //Initialize the memory and its size
    icc_init_program(program, &computer->memory, &computer->memory_size);
}

//=====================================================================================================
//input/output

long long int icc_get_input(icc_t* computer)
{
    long long int input = 0;
    if(computer->simulate_input && computer->input_ptr < computer->input_size)
    {
        input = computer->input[computer->input_ptr];
        computer->input_ptr++;
    }
    else
    {
        char str[128];
        printf("in: ");
        scanf("%127s", str);
        input = strtoll(str, NULL, 0);
    }
    return input;
}

void icc_output(icc_t* computer, long long int output)
{
    if(computer->print_output)
    {
        printf("out: %lld\n", output);
    }

    //realloc if required, always double the size
    //If the pointer given to realloc is NULL, it will act like malloc. This can be an optimization later.
    if(computer->output_ptr >= computer->output_size)
    {
        if(computer->output_size <= 0)
        {
            computer->output_size = 2;//this is multiplied by 2 before allocating, starting size is 4.
        }

        computer->output_size *= 2;
        computer->output = realloc(computer->output, sizeof(long long int) * computer->output_size);
        //0 initialize elements
        for(size_t i = computer->output_ptr; i < computer->output_size; i++)
        {
            computer->output[i] = 0;
        }
    }

    computer->output[computer->output_ptr] = output;
    computer->output_ptr++;
}

//=====================================================================================================
//Stepping and running till halt

//Runs a single instruction for the given computer
void icc_step(icc_t* computer)
{
    if(computer->halted)
    {
        return;
    }

    addressing_mode_t addressing_mode1 = no_addressing_mode;
    addressing_mode_t addressing_mode2 = no_addressing_mode;
    addressing_mode_t addressing_mode3 = no_addressing_mode;
    opcode_t opcode = no_opcode;

    long long int opcode_numeric = icc_memory_read(computer, immediate, (long long int) computer->program_ptr);
    instruction_parse_opcode(opcode_numeric, &addressing_mode1, &addressing_mode2, &addressing_mode3, &opcode);


    long long int parameter1 = 0;
    long long int parameter2 = 0;
    long long int parameter3 = 0;

    //Only fetch memory (and potentially trigger a regrowth of memory) when it is required, based on the opcode width.
    switch(instruction_width(opcode))
    {
        default:
        case 1:
            break;

        case 2:
            parameter1 = icc_memory_read(computer, addressing_mode1, (long long int) computer->program_ptr + 1);
            break;
        case 3:
            parameter1 = icc_memory_read(computer, addressing_mode1, (long long int) computer->program_ptr + 1);
            parameter2 = icc_memory_read(computer, addressing_mode2, (long long int) computer->program_ptr + 2);
            break;
        case 4:
            parameter1 = icc_memory_read(computer, addressing_mode1, (long long int) computer->program_ptr + 1);
            parameter2 = icc_memory_read(computer, addressing_mode2, (long long int) computer->program_ptr + 2);
            parameter3 = icc_memory_read(computer, immediate,		 (long long int) computer->program_ptr + 3);
            break;
    }

    if(computer->disassemble)
    {
        char* disassembly = instruction_disassemble(opcode, addressing_mode1, addressing_mode2, addressing_mode3, parameter1, parameter2, parameter3);
        printf("%s\n", disassembly);
        free(disassembly);
    }

    switch(opcode)
    {
        default:
        case no_opcode:
            printf("Encountered unknown opcode %lld parsed to enum value %d - exiting,.\n", opcode_numeric, opcode);
            exit(1);

        case add:
            icc_memory_write(computer, parameter3, parameter1 + parameter2);
            computer->program_ptr += 4;
            break;

        case multiply:
            icc_memory_write(computer, parameter3, parameter1 * parameter2);
            computer->program_ptr += 4;
            break;

        case input:
            if(computer->halt_before_input)
            {
                computer->halted = true;
                computer->halt_reason = halt_input;
            }

			if(addressing_mode1 == relative)
			{				
				parameter1 = icc_memory_read(computer, relative, (long long int) computer->program_ptr + 1);
			}
			else
			{
				parameter1 = icc_memory_read(computer, immediate, (long long int) computer->program_ptr + 1);
			}
    	
            //In case of an input we use the next value as storage address, that's why we have to fetch it in immediate mode.
            icc_memory_write(computer, parameter1, icc_get_input(computer));
            computer->program_ptr += 2;
            break;

        case output:
            icc_output(computer, parameter1);
            computer->program_ptr += 2;

            if(computer->halt_after_output)
            {
                computer->halted = true;
                computer->halt_reason = halt_output;
            }

            break;

        case jump_if_true:
            if(parameter1 != 0)
            {
                computer->program_ptr = parameter2;
            }
            else
            {
                computer->program_ptr += 3;
            }
            break;

        case jump_if_false:
            if(parameter1 == 0)
            {
                computer->program_ptr = parameter2;
            }
            else
            {
                computer->program_ptr += 3;
            }
            break;

        case less_than:
            if(parameter1 < parameter2)
            {
                icc_memory_write(computer, parameter3, 1);
            }
            else
            {
                icc_memory_write(computer, parameter3, 0);
            }
            computer->program_ptr += 4;
            break;

        case equals:
            if(parameter1 == parameter2)
            {
                icc_memory_write(computer, parameter3, 1);
            }
            else
            {
                icc_memory_write(computer, parameter3, 0);
            }
            computer->program_ptr += 4;
            break;

        case adjust_relative_base:
            computer->relative_base_ptr += parameter1;
            computer->program_ptr += 2;
            break;

        case halt:
            computer->halted = true;
            computer->halt_reason = halt_end;
            break;
    }
}

void icc_run(icc_t* computer)
{
    while(!computer->halted)
    {
        icc_step(computer);
    }
}


//=====================================================================================================
//reading and printing memory

void icc_grow_memory(icc_t* computer, long long int address)
{
	//printf("Growing memory. Instruction ptr %lld, requested size %lld\n", computer->program_ptr, address);
	
    void* temp = realloc(computer->memory, sizeof(long long int) * address);
    if(!temp)
    {
        fprintf(stderr, "Growing memory failed for address %lld\n", address);
        exit(1);
    }
    else
    {
        computer->memory = temp;
    }

    //0 initialize
    for(long long int i = computer->memory_size; i < address; i++)
    {
        computer->memory[i] = 0;
    }
    computer->memory_size = address;
}

void icc_memory_write(icc_t* computer, long long int address, long long int value)
{
    if(address < 0)
    {
        fprintf(stderr, "Memory out of bounds access at address %lld\n", address);
        exit(1);
    }

    if(address >= (long long int)computer->memory_size)
    {
        icc_grow_memory(computer, address+1);
    }

    computer->memory[address] = value;
}

long long int icc_memory_read(icc_t* computer, addressing_mode_t addressing_mode, long long int program_ptr)
{
    //First we need to determine what location in memory we need to read.
    long long int address = 0;
    switch (addressing_mode)
    {
        default:
        case no_addressing_mode:
            fprintf(stderr, "No addressing mode for address %lld\n", program_ptr);
            exit(1);

        case immediate:
            address = program_ptr;
            break;

        case position:
            //pointer->pointer construction: it could be out of bounds. In that case we can safely return 0 without growing and initializing memory (the memory we would otherwise return would be 0 anyway).
            if(program_ptr >= (long long int)computer->memory_size)
            {
                return 0;
            }
            address = computer->memory[program_ptr];
            break;

        case relative:
            //pointer->pointer construction: it could be out of bounds. In that case we can safely return 0 without growing and initializing memory (the memory we would otherwise return would be 0 anyway).
            if(program_ptr >= (long long int)computer->memory_size)
            {
                address = computer->relative_base_ptr;
            }
            else
            {
                address = computer->relative_base_ptr + computer->memory[program_ptr];
            }
            break;
    }

    //Now we can read and return the address we came up with.
    if(address < 0)
    {
        fprintf(stderr, "Memory out of bounds access at address %lld, program pointer %lld\n", address, program_ptr);
        exit(1);
    }

    if(address >= (long long int)computer->program_size)
    {
        icc_grow_memory(computer, address+1);
    }

    return computer->memory[address];


    //if(program_ptr >= 0 && program_ptr < (long long int)computer->memory_size)
    //{
    //    //First fetch the actual number in memory
    //    long long int num = computer->memory[program_ptr];
//
    //    switch (addressing_mode)
    //    {
    //        default:
    //        case no_addressing_mode:
    //            fprintf(stderr, "No addressing mode for address %lld\n", program_ptr);
    //            exit(1);
//
    //        case immediate:
    //            return num;
    //        case position:
    //            if(num < 0)
    //            {
    //                fprintf(stderr, "Memory read bellow 0 - %lld\n", num);
    //                exit(1);
    //            }
//
    //            if(num >= (long long int)computer->memory_size)
    //            {
    //                icc_grow_memory(computer, num);
    //            }
    //            return computer->memory[num];
//
    //        case relative:
    //        {
    //            long long int relative_address = computer->relative_base_ptr + num;
    //            if(relative_address < 0)
    //            {
    //                fprintf(stderr, "Memory read bellow 0 - %lld\n", num);
    //                exit(1);
    //            }
//
    //            if(relative_address >= (long long int)computer->memory_size)
    //            {
    //                icc_grow_memory(computer, relative_address);
    //            }
//
    //            return computer->memory[relative_address];
    //        }
    //    }
    //}
    //return -1;
}

void icc_print_memory(icc_t* computer)
{
    char* str = icc_computer_memory_to_string(computer);
    printf("%s\n", str);
    free(str);
}

char* icc_memory_to_string(long long int* memory, size_t memory_size)
{
    char* result = malloc(sizeof(long long int) * (memory_size * 2));
    int index = 0;
    for(size_t i = 0; i < memory_size; i++)
    {
        index += sprintf(&result[index], "%lld", memory[i]);
        if(i + 1  < memory_size)
        {
            index += sprintf(&result[index], ",");
        }
    }
    return result;
}

char* icc_computer_memory_to_string(icc_t* computer)
{
    return icc_memory_to_string(computer->memory, computer->memory_size);
}

void icc_use_simulated_input(icc_t* computer, size_t size)
{
    computer->simulate_input = true;
    computer->input = malloc(sizeof(long long int) * size);
    computer->input_size = size;
}

//Can be used before or afer icc_use_simulated_input to add an input at the next available position. Can also be used after initializing with simulated input.
void icc_add_input(icc_t* computer, long long int input)
{
    if(computer->input_add_ptr >= computer->input_size)
    {
        //always increment by 1, don't want to give a false 0 initialized input
        computer->input_size += 1;
        computer->input = realloc(computer->input, sizeof(long long int) * computer->input_size);
    }
    computer->input[computer->input_add_ptr] = input;
    computer->input_add_ptr++;
}
