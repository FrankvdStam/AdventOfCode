//
// Created by Frank on 23/11/2020.
//

#include <stdlib.h>
#include <memory.h>
#include "list.h"

void list_create(list_t* list, size_t element_size, size_t initial_size)
{
    list->data = malloc(initial_size * element_size);
    list->insert_index = 0;
    list->element_size = element_size;
    list->count = initial_size;
}

void list_add(list_t* list, char* data)
{
    if (list->insert_index == list->count) {
        list->count *= 2;
        list->data = realloc(list->data, list->count * sizeof(int));
    }
    memcpy(&list->data[list->insert_index++], data, list->element_size);
}

void list_free(list_t* list)
{
    free(list->data);
    list->data = NULL;
    list->insert_index = list->count = list->element_size = 0;
}
