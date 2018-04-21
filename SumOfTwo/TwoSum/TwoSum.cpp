#include "stdafx.h"
#include "TwoSum.h"

using namespace std;

bool FindIf2Sum(std::hash_set<long long> & data, int target)
{
    auto iter = data.begin();
    while (iter != data.end())
    {
        if (target - *iter != *iter && data.find(target - *iter) != data.end())
        {
            return true;
        }
        ++iter;
    }

    return false;
}

hash_set<int> FindSums(long long min, long long * data, int size, long long offset)
{
    hash_set<int> result;
    size_t cmin = size - 1, cmax = size - 1;
    auto current = data[0], past = current, dmin = data[cmin], dmax = data[cmax];

    for (size_t i = 0; i < size; i++)
    {
        current = data[i];

        dmin = min - current - offset;
        dmax = 0 - current - offset;
        while (data[cmin] > dmin) --cmin;
        while (data[cmax] > dmax) --cmax;

        if (cmax < i) break;

        for (size_t j = cmin; j <= cmax; j++)
        {
            if (i == j) continue;
            auto val = current + data[j] + offset;
            if (val >= min && val <= 0)
            {
                result.insert(val + offset);
            }
        }
    }


    return result;
}