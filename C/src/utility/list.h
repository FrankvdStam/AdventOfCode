//
// Created by Frank on 23/11/2020.
//

typedef struct
{
    char* data;
    size_t element_size;
    size_t count;
    size_t insert_index;
} list_t;

void list_create(list_t* list, size_t element_size, size_t initial_size);
void list_add(list_t* list, char* data);
void list_free(list_t* list);
