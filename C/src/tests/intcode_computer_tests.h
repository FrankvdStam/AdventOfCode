//
// Created by f.stam on 04/03/2020.
//

#ifndef ADVENTOFC_INTCODE_COMPUTER_TESTS_H
#define ADVENTOFC_INTCODE_COMPUTER_TESTS_H

#include <string.h>
#include <stdlib.h>
#include "custom_assert.h"
#include "../intcode_computer/intcode_computer.h"


//Runs a single test program and tests it against a given expected output
void icc_run_test(char* program, char* expected_memory, char* input, char* expected_output)
{
    icc_t computer;
    icc_init(&computer, program, input);
    computer.print_output = false;
    //computer.disassemble = true;
    icc_run(&computer);

    //The memory has to be converted to a string to compare it against the expected memory string
    char* actual_memory        = icc_memory_to_string(computer.memory, computer.memory_size);
    char* actual_output = icc_memory_to_string(computer.output, computer.output_size);


    //Do the comparison, notify errors.
    if(strcmp(expected_memory, actual_memory) != 0)
    {
        //Want some more specialized output for this test.
        printf("Program memory not as expected:\nExpected: %s\nActual  : %s\n", expected_memory, actual_memory);
        custom_assert_increment_errors();
    }

    if(expected_output != NULL && strcmp(expected_output, actual_output) != 0)
    {
        //Want some more specialized output for this test.
        printf("Program output not as expected:\nExpected: %s\nActual  : %s\n", expected_output, actual_output);
        custom_assert_increment_errors();
    }

    free(actual_memory);
    icc_free(&computer);
}

char* program_01          =  "1,9,10,3,2,3,11,0,99,30,40,50";
char* program_01_expected =  "3500,9,10,70,2,3,11,0,99,30,40,50";

char* program_02          =  "1,0,0,0,99";
char* program_02_expected =  "2,0,0,0,99";

char* program_03          =  "2,4,4,5,99,0";
char* program_03_expected =  "2,4,4,5,99,9801";

char* program_04          =  "1,1,1,4,99,5,6,0,99";
char* program_04_expected =  "30,1,1,4,2,5,6,0,99";

char* program_05          = "1,12,2,3,1,1,2,3,1,3,4,3,1,5,0,3,2,6,1,19,2,19,13,23,1,23,10,27,1,13,27,31,2,31,10,35,1,35,9,39,1,39,13,43,1,13,43,47,1,47,13,51,1,13,51,55,1,5,55,59,2,10,59,63,1,9,63,67,1,6,67,71,2,71,13,75,2,75,13,79,1,79,9,83,2,83,10,87,1,9,87,91,1,6,91,95,1,95,10,99,1,99,13,103,1,13,103,107,2,13,107,111,1,111,9,115,2,115,10,119,1,119,5,123,1,123,2,127,1,127,5,0,99,2,14,0,0";
char* program_05_expected = "4330636,12,2,2,1,1,2,3,1,3,4,3,1,5,0,3,2,6,1,24,2,19,13,120,1,23,10,124,1,13,27,129,2,31,10,516,1,35,9,519,1,39,13,524,1,13,43,529,1,47,13,534,1,13,51,539,1,5,55,540,2,10,59,2160,1,9,63,2163,1,6,67,2165,2,71,13,10825,2,75,13,54125,1,79,9,54128,2,83,10,216512,1,9,87,216515,1,6,91,216517,1,95,10,216521,1,99,13,216526,1,13,103,216531,2,13,107,1082655,1,111,9,1082658,2,115,10,4330632,1,119,5,4330633,1,123,2,4330635,1,127,5,0,99,2,14,0,0";

char* program_06          = "1002,4,3,4,33";
char* program_06_expected = "1002,4,3,4,99";

char* program_07          = "1101,100,-1,4,0";
char* program_07_expected = "1101,100,-1,4,99";

char* program_08          = "3,0,4,0,99";
char* program_08_expected = "12,0,4,0,99";
char* program_08_input    = "12";
char* program_08_output   = "12,0,0,0";

char* program_09          = "3,9,8,9,10,9,4,9,99,-1,8";
char* program_09_expected = "3,9,8,9,10,9,4,9,99,1,8";
char* program_09_input    = "8";
char* program_09_output   = "1,0,0,0";

char* program_10          = "3,9,7,9,10,9,4,9,99,-1,8";
char* program_10_expected = "3,9,7,9,10,9,4,9,99,1,8";
char* program_10_input    = "7";
char* program_10_output   = "1,0,0,0";

char* program_11          = "3,3,1108,-1,8,3,4,3,99";
char* program_11_expected = "3,3,1108,1,8,3,4,3,99";
char* program_11_input    = "8";
char* program_11_output   = "1,0,0,0";

char* program_12          = "3,3,1107,-1,8,3,4,3,99";
char* program_12_expected = "3,3,1107,1,8,3,4,3,99";
char* program_12_input    = "7";
char* program_12_output   = "1,0,0,0";

char* program_13          = "3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9";
char* program_13_expected = "3,12,6,12,15,1,13,14,13,4,13,99,0,0,1,9";
char* program_13_input    = "0";
char* program_13_output   = "0,0,0,0";

char* program_14          = "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99";
char* program_14_expected = "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,7,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99";
char* program_14_input    = "7";
char* program_14_output   = "999,0,0,0";

char* program_15          = "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99";
char* program_15_expected = "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,1000,8,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99";
char* program_15_input    = "8";
char* program_15_output   = "1000,0,0,0";

char* program_16          = "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99";
char* program_16_expected = "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,1001,9,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99";
char* program_16_input    = "9";
char* program_16_output   = "1001,0,0,0";

void icc_tests()
{
    icc_run_test(program_01, program_01_expected, NULL, NULL);
    icc_run_test(program_02, program_02_expected, NULL, NULL);
    icc_run_test(program_03, program_03_expected, NULL, NULL);
    icc_run_test(program_04, program_04_expected, NULL, NULL);
    icc_run_test(program_05, program_05_expected, NULL, NULL);
    icc_run_test(program_06, program_06_expected, NULL, NULL);
    icc_run_test(program_07, program_07_expected, NULL, NULL);
    icc_run_test(program_08, program_08_expected, program_08_input, program_08_output);
    icc_run_test(program_09, program_09_expected, program_09_input, program_09_output);
    icc_run_test(program_10, program_10_expected, program_10_input, program_10_output);
    icc_run_test(program_11, program_11_expected, program_11_input, program_11_output);
    icc_run_test(program_12, program_12_expected, program_12_input, program_12_output);
    icc_run_test(program_13, program_13_expected, program_13_input, program_13_output);
    icc_run_test(program_14, program_14_expected, program_14_input, program_14_output);
    icc_run_test(program_15, program_15_expected, program_15_input, program_15_output);
    icc_run_test(program_16, program_16_expected, program_16_input, program_16_output);
}

#endif //ADVENTOFC_INTCODE_COMPUTER_TESTS_H
