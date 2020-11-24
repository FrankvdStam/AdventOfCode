#include <stdio.h>
#include <stdlib.h>
#include "utility/stringstream.h"
#include "2019/day01.h"
#include "2019/day02.h"
#include "2019/day05.h"
#include "2019/day07.h"
#include "2019/day09.h"

#include "2015/2015.h"

#include "tests/tests.h"

int main()
{
	//stringstream_t stream;
	//stringstream_init(&stream);
	//stringstream_print(&stream);
	//stringstream_append_char(&stream, 'H');
	//stringstream_print(&stream);
	//stringstream_append_char(&stream, 'e');
	//stringstream_print(&stream);
	//stringstream_append_char(&stream, 'l');
	//stringstream_print(&stream);
	//stringstream_append_char(&stream, 'l');
	//stringstream_print(&stream);
	//stringstream_append_char(&stream, 'o');
	//stringstream_print(&stream);
	//
	//stringstream_append_char_ptr(&stream, " world!");
	//stringstream_print(&stream);
	//char* str = stringstream_finalize(&stream);
	//
	//printf("%s\n", str);
	//
	//free(str);
	//
	
   //char* prog = "101,0,100,0,99";
   //icc_t computer;
   //icc_init(&computer, prog, NULL);
   //computer.disassemble = true;
   //icc_run(&computer);
   //icc_free(&computer);

    run_tests();
    //day09_problem_1();

    p_2015_day02_01();

    return 0;
}


