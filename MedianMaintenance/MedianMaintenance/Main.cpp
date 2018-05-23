#include "stdafx.h"
#include "MedianMaintenance.h"

int _tmain(int argc, _TCHAR* argv [])
{
    auto start = GetTimeMs64();

    // Params
    int size = 10000;
    char * fileName = "Median.txt";

    // Compute
    auto data = ReadFile(fileName, size);
    int result = SumOfMedian(data, size);
    delete [] data;

    auto stop = GetTimeMs64();

    std::cout << "Answer is " << result << std::endl;
    std::cout << "Time is " << (stop - start) * 100 << " nanoseconds" << std::endl;

    return result;
}
