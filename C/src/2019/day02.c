//
// Created by Frank on 02/03/2020.
//

#include "day02.h"
#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>

char* program = "1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,6,1,19,2,19,13,23,1,23,10,27,1,13,27,31,2,31,10,35,1,35,9,39,1,39,13,43,1,13,43,47,1,47,13,51,1,13,51,55,1,5,55,59,2,10,59,63,1,9,63,67,1,6,67,71,2,71,13,75,2,75,13,79,1,79,9,83,2,83,10,87,1,9,87,91,1,6,91,95,1,95,10,99,1,99,13,103,1,13,103,107,2,13,107,111,1,111,9,115,2,115,10,119,1,119,5,123,1,123,2,127,1,127,5,0,99,2,14,0,0";

void day02_problem_1()
{
    icc_t computer;
    icc_init(&computer, program, NULL);

    computer.memory[1] = 12;
    computer.memory[2] = 2;

    icc_print_memory(&computer);

    computer.disassemble = true;
    icc_run(&computer);
    icc_print_memory(&computer);

    icc_free(&computer);
}

void day02_problem_2()
{
    //Setup timer.
    clock_t t;
    t = clock();

    long long int magic_output = 19690720;

    for(long long int noun = 0; noun <= 99; noun++)
    {
        for(long long int verb = 0; verb <= 99; verb++)
        {
            printf("%lld%lld\n", noun, verb);

            icc_t computer;
            icc_init(&computer, program, NULL);
            //computer.disassemble = true;
            computer.memory[1] = noun;
            computer.memory[2] = verb;

            icc_run(&computer);

            if(magic_output == computer.memory[0])
            {
                printf("Found magic input!\n");

                t = clock() - t;
                double time_taken = ((double)t)/CLOCKS_PER_SEC; // calculate the elapsed time
                printf("Ran %lld instances of icc in %f seconds", noun * verb, time_taken);

                exit(0);
            }

            icc_free(&computer);
        }
    }

    t = clock() - t;
    double time_taken = ((double)t)/CLOCKS_PER_SEC; // calculate the elapsed time
    printf("Ran %d instances of icc in %f seconds", 99 * 99, time_taken);
}