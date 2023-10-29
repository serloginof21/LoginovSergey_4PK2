// MathLibrary.cpp : Определяет экспортируемые функции для библиотеки DLL.
#include "pch.h" // используйте stdafx.h в Visual Studio 2017 и более ранних версиях
#include <utility>
#include <limits.h>
#include "Header.h"
// внутренние переменные DLL:
static unsigned long long previous_; // предыдущее значение
static unsigned long long current_; // текущее значение
static unsigned index_; // текущая позиция
// инициализация последовательность Фибоначчи
// допустим, что F(0) = a, F(1) = b.
// данная функция обязательно вызывается перед остальными
void fibonacci_init(
	const unsigned long long a,
	const unsigned long long b)
{
	index_ = 0;
	current_ = a;
	previous_ = b;
}
// вычисление следующего значения последовательности
bool fibonacci_next()
{
	// проверка на переполнение
	if ((ULLONG_MAX - previous_ < current_) ||
		(UINT_MAX == index_))
	{
		return false;
	}
	// когда index == 0, возвращается b
	if (index_ > 0)
	{
		previous_ += current_;
	}
	std::swap(current_, previous_);
	++index_;
	return true;
}
// получение текущего значения посл.
unsigned long long fibonacci_current()
{
	return current_;
}
// получение индекса текущего элемента
unsigned fibonacci_index()
{
	return index_;
}