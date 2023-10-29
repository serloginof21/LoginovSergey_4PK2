// MathLibrary.cpp : ���������� �������������� ������� ��� ���������� DLL.
#include "pch.h" // ����������� stdafx.h � Visual Studio 2017 � ����� ������ �������
#include <utility>
#include <limits.h>
#include "Header.h"
// ���������� ���������� DLL:
static unsigned long long previous_; // ���������� ��������
static unsigned long long current_; // ������� ��������
static unsigned index_; // ������� �������
// ������������� ������������������ ���������
// ��������, ��� F(0) = a, F(1) = b.
// ������ ������� ����������� ���������� ����� ����������
void fibonacci_init(
	const unsigned long long a,
	const unsigned long long b)
{
	index_ = 0;
	current_ = a;
	previous_ = b;
}
// ���������� ���������� �������� ������������������
bool fibonacci_next()
{
	// �������� �� ������������
	if ((ULLONG_MAX - previous_ < current_) ||
		(UINT_MAX == index_))
	{
		return false;
	}
	// ����� index == 0, ������������ b
	if (index_ > 0)
	{
		previous_ += current_;
	}
	std::swap(current_, previous_);
	++index_;
	return true;
}
// ��������� �������� �������� ����.
unsigned long long fibonacci_current()
{
	return current_;
}
// ��������� ������� �������� ��������
unsigned fibonacci_index()
{
	return index_;
}