#include <iostream>
#include "Header.h"

int main()
{
	fibonacci_init(1, 1);
	do {
		std::cout << fibonacci_index() << ": " << fibonacci_current() << std::endl;
	} while (fibonacci_next());

	std::cout << fibonacci_index() + 1 << "Fibonacci sequence values fit in an " << "unsigned 64-bit integer." << std::endl;
}