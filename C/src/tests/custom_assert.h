//
// Created by Frank on 03/03/2020.
//

#ifndef ADVENTOFC_CUSTOM_ASSERT_H
#define ADVENTOFC_CUSTOM_ASSERT_H

#include <stdio.h>
#include <stdbool.h>

int custom_assert_errors = 0;

void custom_assert_init()
{
    custom_assert_errors = 0;
}

void custom_assert_increment_errors()
{
    custom_assert_errors++;
}

//TODO: Turn into macro to make file and line work.
void custom_assert(bool condition, char* message)
{
    if(!condition)
    {
        fprintf(stderr, "Test failed %s, line %d - %s\n", __FILE__, __LINE__, message);
        custom_assert_errors++;
    }
}

void custom_assert_finish()
{
    fprintf(stderr, "Ran with %i failed tests.\n", custom_assert_errors);
}

#endif //ADVENTOFC_CUSTOM_ASSERT_H
