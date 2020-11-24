//
// Created by Frank on 03/03/2020.
//

#ifndef ADVENTOFC_TESTS_H
#define ADVENTOFC_TESTS_H

#include "instruction_tests.h"
#include "intcode_computer_tests.h"

void run_tests()
{
    printf("Running all tests.\n");

    custom_assert_init();

    test_parse_instruction();
    icc_tests();

    custom_assert_finish();
}

#endif //ADVENTOFC_TESTS_H
