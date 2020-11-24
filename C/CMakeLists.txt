# cmake_minimum_required(VERSION <specify CMake version here>)
project(AdventOfC)
#set (CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -Wall -Wxtra")
#set (CMAKE_C_FLAGS "${CMAKE_C_FLAGS} -Wall -Wextra -pedantic -Werror")
set(CMAKE_C_STANDARD 11)
cmake_minimum_required(VERSION 3.16)

if(MSVC)
  # Force to always compile with W4
  if(CMAKE_C_FLAGS MATCHES "/W[0-4]")
    string(REGEX REPLACE "/W[0-4]" "/W4" CMAKE_CXX_FLAGS "${CMAKE_C_FLAGS}")
  else()
    set(CMAKE_C_FLAGS "${CMAKE_C_FLAGS} /W4")
  endif()
elseif(CMAKE_COMPILER_IS_GNUCC OR CMAKE_COMPILER_IS_GNUCXX)
  # Update if necessary
  set(CMAKE_C_FLAGS "${CMAKE_C_FLAGS} -Wall -Wextra -pedantic -Werror -Wno-overlength-strings")
endif()

add_executable(AdventOfC

        src/utility/stringstream.c
        src/utility/stringstream.h

        src/intcode_computer/intcode_computer.c
        src/intcode_computer/intcode_computer.h
        src/intcode_computer/instruction.c
        src/intcode_computer/instruction.h

        src/tests/instruction_tests.h
        src/tests/tests.h
        src/tests/custom_assert.h
        src/tests/intcode_computer_tests.h

        src/2019/day01.h
        src/2019/day01.c
        src/2019/day02.c
        src/2019/day02.h
        src/2019/day05.c
        src/2019/day05.h
        src/2019/day07.c
        src/2019/day07.h
        src/2019/day09.c
        src/2019/day09.h

        src/2015/2015.h
        src/2015/2015_day01.h
        src/2015/2015_day01.c
        src/2015/2015_day02.h
        src/2015/2015_day02.c

        src/main.c
        src/utility/list.c)