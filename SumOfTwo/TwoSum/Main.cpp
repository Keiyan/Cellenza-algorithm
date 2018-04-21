#include "stdafx.h"
#include "TwoSum.h"
#include <amp.h>

using namespace concurrency;

int _tmain(int argc, _TCHAR* argv [])
{
    int result = 0;
    auto start = GetTimeMs64();

    // Params
    int size = 1000000;
    char * fileName = "2sum.txt";
    bool useFast = true;

    // Compute
    if (useFast)
    {
        auto data = ReadFileAsArray(fileName, size, 10000);
        QuickSort(data, size);
        auto set = FindSums(-20000, data, size, 10000);
        result = set.size();
    }
    else
    {
        auto data = ReadFile(fileName);

        parallel_for(-10000, 10001, 1, [&data, &result](int i){
            if (FindIf2Sum(*data, i)) ++result;
        });

        delete data;
    }
    auto stop = GetTimeMs64();

    std::cout << "Answer is " << result << std::endl;
    std::cout << "Time is " << (stop - start) * 100 << " nanoseconds" << std::endl;

    return result;
}

