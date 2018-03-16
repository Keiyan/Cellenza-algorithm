#include "stdafx.h"
#include "CountingInversions.h"

int _tmain(int argc, _TCHAR* argv [])
{
	auto start = GetTimeMs64();
	ListItem * sorted = NULL;

	ReadArray("IntegerArray.txt");
	unsigned long result = CountInversion(Data, Size, &sorted);
	delete [] Data;
	delete TempRoot;

	auto stop = GetTimeMs64();

	std::cout << "Inversion count is " << result << std::endl;
	std::cout << "Time is " << (stop - start) * 100 << " nanoseconds" << std::endl;

	return result;
}