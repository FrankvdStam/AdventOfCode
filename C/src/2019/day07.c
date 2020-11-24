//
// Created by Frank on 20/03/2020.
//

#include <stddef.h>
#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include "day07.h"
#include "../intcode_computer/intcode_computer.h"


//==========================================================================================================================================================================================================
//Permute

void swap(long long int* arr, int a, int b);

//https://www.quora.com/How-do-you-make-a-C-program-that-displays-all-possible-permutations-of-an-array
/* swap 2 values by index */
inline void swap(long long int* arr, int a, int b)
{
    int tmp = arr[a];
    arr[a] = arr[b];
    arr[b] = tmp;
}

typedef long long int (*permute_function_t)(long long int*);

void permute(long long int* arr, int start, int end, long long int* results, size_t* results_ptr, permute_function_t function)
{
    int i;
    if(start == end) /* this function is done */
    {
        results[*results_ptr] = function(arr);
        (*results_ptr)++;
        return;
    }
    permute(arr, start + 1, end, results, results_ptr, function); /* start at next element */

    /* permute remaining elements recursively */
    for(i = start + 1; i < end; i++)
    {
        if( arr[start] == arr[i] ) continue; /* skip */
        swap(arr, start, i);
        permute(arr, start + 1, end, results, results_ptr, function);
        swap(arr, start, i); /* restore element order */
    }
}


//==========================================================================================================================================================================================================
//Running with a phase setting


char* amplifier_controller_software = "3,8,1001,8,10,8,105,1,0,0,21,46,55,68,89,110,191,272,353,434,99999,3,9,1002,9,3,9,1001,9,3,9,102,4,9,9,101,4,9,9,1002,9,5,9,4,9,99,3,9,102,3,9,9,4,9,99,3,9,1001,9,5,9,102,4,9,9,4,9,99,3,9,1001,9,5,9,1002,9,2,9,1001,9,5,9,1002,9,3,9,4,9,99,3,9,101,3,9,9,102,3,9,9,101,3,9,9,1002,9,4,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,99,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,99,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,99";

//Expects a phase setting with size 5.
long long int run_phase_setting(long long int* phase_setting)
{
    printf("Running sequence for ");
    for(size_t i = 0; i < 5; i++)
    {
        printf("%lld", phase_setting[i]);
    }


    bool first = true;
    long long int previous_output = 0;
    //Init, run, capture output and free all amplifiers
    for(size_t i = 0; i < 5; i++)
    {
        icc_t amplifier;
        icc_init(&amplifier, amplifier_controller_software, NULL);
        icc_use_simulated_input(&amplifier, 2);
        //amplifier.disassemble = true;
        amplifier.print_output = false;

        amplifier.input[0] = phase_setting[i];
        if(first)
        {
            first = false;
            amplifier.input[1] = 0;
        }
        else
        {
            amplifier.input[1] = previous_output;
        }

        icc_run(&amplifier);
        previous_output = amplifier.output[0];
        //char* mem = icc_memory_to_string(amplifier.output, amplifier.output_size);
        //printf("output: %s\n", mem);
        //free(mem);
        icc_free(&amplifier);
    }

    printf(" - result: %lld\n", previous_output);
    return previous_output;
}

//Expects a phase setting with size 5.
long long int run_phase_setting_with_feedback_loop(long long int* phase_setting)
{
    printf("Running sequence for ");
    for(size_t i = 0; i < 5; i++)
    {
        printf("%lld", phase_setting[i]);
    }

    //Initialization
    icc_t* amplifiers = malloc(sizeof(icc_t) * 5);
    for(size_t i = 0; i < 5; i++)
    {
        icc_init(&amplifiers[i], amplifier_controller_software, NULL);
        amplifiers[i].print_output = true;
        amplifiers[i].disassemble = true;
        amplifiers[i].halt_after_output = true;
        amplifiers[i].simulate_input = true;
        icc_add_input(&amplifiers[i], phase_setting[i]);
    }

    //set the first amplifies second input to 0 to start the chain
    long long int signal = 0;
    bool running = true;
    size_t counter = 0;
    while(running)
    {
        for(size_t i = 0; i < 5; i++)
        {
            printf("===================================================================================================\n");
            printf("Running %zu:\n", i);
            if(amplifiers[i].halt_reason == halt_output)
            {
                amplifiers[i].halted = false;
                amplifiers[i].halted = halt_none;
            }
            icc_add_input(&amplifiers[i], signal);
            icc_run(&amplifiers[i]);

            if(amplifiers[i].halt_reason == halt_output)
            {
                signal = amplifiers[i].output[counter];//Fetch the last output
            }

            if(i == 4 && amplifiers[4].halted && amplifiers[4].halt_reason == halt_end)
            {
                running = false;
                break;
            }
        }
        counter++;
    }

    //Freeing
    for(size_t i = 0; i < 5; i++)
    {
        icc_free(&amplifiers[i]);
    }
    free(amplifiers);

    printf(" - result: %lld\n", signal);
    return signal;
}


//==========================================================================================================================================================================================================
//Problem 1 and 2

void day07_problem_1()
{
    long long int phase_setting[] = {0, 1, 2, 3, 4 };

    //since we have 5 different values, we know that we have 5^2 possible permutations giving us 120 results.
    //We can store all results and calculate the max. We could also calculate the max in-place but maybe we want to use all results later?
    long long int results[120];
    size_t results_ptr = 0;

    permute_function_t function = &run_phase_setting;

    permute(phase_setting, 0, 5, results, &results_ptr, function);
    long long int max = 0;
    for(size_t i = 0; i < 120; i++)
    {
        if(results[i] > max)
        {
            max = results[i];
        }
    }
    printf("max result: %lld\n", max);
}


void day07_problem_2()
{
    //icc computer;
    //icc_init(&computer, amplifier_controller_software2, "0,2");
    //computer.disassemble = true;
    //icc_run(&computer);
    //icc_free(&computer);

    long long int phase_settings[] = {9,8,7,6,5};

    //since we have 5 different values, we know that we have 5^2 possible permutations giving us 120 results.
    //We can store all results and calculate the max. We could also calculate the max in-place but maybe we want to use all results later?
    long long int results[120];
    size_t results_ptr = 0;

    permute_function_t function = &run_phase_setting_with_feedback_loop;
    permute(phase_settings, 0, 5, results, &results_ptr, function);
    long long int max = 0;
    for(size_t i = 0; i < 120; i++)
    {
        if(results[i] > max)
        {
            max = results[i];
        }
    }
    printf("max result: %lld\n", max);
}
