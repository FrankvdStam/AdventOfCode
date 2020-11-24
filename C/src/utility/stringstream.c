//
// Created by Frank on 21/03/2020.
//

#include "stringstream.h"


//Helper method to clear a reallocated string
void clear_after_realloc(stringstream_t* stringstream)
{
	for (size_t i = stringstream->append_ptr; i < stringstream->size; i++)
	{
		stringstream->string[i] = ' ';
	}
}


//Initialization, growing, finalizing ==============================================================================================================
//Initialize the stringstream
void stringstream_init_with_size(stringstream_t* stringstream, size_t size)
{
    stringstream->string = malloc(sizeof(char) * size);
    stringstream->size = 4;
    stringstream->append_ptr = 0;
	clear_after_realloc(stringstream);
}

void stringstream_init(stringstream_t* stringstream)
{
    stringstream_init_with_size(stringstream, 4);
}

void stringstream_double_size(stringstream_t* stringstream)
{
    //printf("doubling stringstream size.\n");
    stringstream->size *= 2;
	void* temp = realloc(stringstream->string, sizeof(char) * stringstream->size);
	if(!temp)
	{
		stringstream->string[stringstream->size - 1] = '\0';
		fprintf(stderr, "Failed to reallocate stringstream. Contents: %s\n", stringstream->string);
		exit(1);
	}
	stringstream->string = temp;	
	clear_after_realloc(stringstream);
}

char* stringstream_finalize(stringstream_t* stringstream)
{
    stringstream_append_char(stringstream, '\0');
	//int len = strlen(stringstream->string);
	//printf("Realloced stringstream. length %d, value %s", len, stringstream->string);
    return stringstream->string;
}

void stringstream_print(stringstream_t* stringstream)
{
    printf("%.*s\n", (int)stringstream->size, stringstream->string);
}

//Appending ==============================================================================================================
void stringstream_append_char(stringstream_t* stringstream, char c)
{
    if(stringstream->append_ptr >= stringstream->size)
    {
        stringstream_double_size(stringstream);
    }

    stringstream->string[stringstream->append_ptr] = c;
    stringstream->append_ptr += 1;
}

void stringstream_append_char_ptr(stringstream_t* stringstream, char* c)
{
    size_t length = strlen(c);//Not taking null terminator into account
    //Grow in size until we can fit the given char ptr
    while(stringstream->size - stringstream->append_ptr < length)
    {
        stringstream_double_size(stringstream);
    }

    for(size_t i = 0; i < length; i++)
    {
        stringstream->string[stringstream->append_ptr] = c[i];
        stringstream->append_ptr++;
    }
}

void stringstream_append_long_long_int(stringstream_t* stringstream, long long int value)
{
    //This should be able to store whatever the number is converted to string, doubled it for good measure.
    char* temp = malloc(sizeof(char)*20);

    //convert to string
    sprintf(temp, "%lld", value);
    //printf("Appending long long int.\nTemp: %s\nSize: %zu\n", temp, strlen(temp));

    stringstream_append_char_ptr(stringstream, temp);
    free(temp);
}
