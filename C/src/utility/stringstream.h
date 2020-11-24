//
// Created by Frank on 21/03/2020.
//

#ifndef ADVENTOFC_STRINGSTREAM_H
#define ADVENTOFC_STRINGSTREAM_H

#include <stddef.h>
#include <stdlib.h>
#include <stdio.h>
#include <string.h>

typedef struct
{
    char* string;
    size_t size;
    size_t append_ptr;

} stringstream_t;

//Initialization, growing, finalizing ==============================================================================================================
void stringstream_init_with_size(stringstream_t* stringstream, size_t size);
void stringstream_init(stringstream_t* stringstream);
char* stringstream_finalize(stringstream_t* stringstream);

void stringstream_print(stringstream_t* stringstream);

//Appending ==============================================================================================================
void stringstream_append_char(stringstream_t* stringstream, char c);
void stringstream_append_char_ptr(stringstream_t* stringstream, char* c);
void stringstream_append_long_long_int(stringstream_t* stringstream, long long int value);



#endif //ADVENTOFC_STRINGSTREAM_H
