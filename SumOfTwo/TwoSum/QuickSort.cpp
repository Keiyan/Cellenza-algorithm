#include "stdafx.h"
#include "TwoSum.h"

int GetPivot(long long* data, int size)
{
    if (size <= 2) return 0;
    int i = 0, j = size - 1, k = (size - 1) / 2;
    int a = data[i], b = data[j], c = data[k];
    if (a < b)
    {
        if (c < a)
        {
            return i;
        }
        if (b < c)
        {
            return j;
        }
        return k;
    }
    else
    {
        if (c < b)
        {
            return j;
        }
        if (a < c)
        {
            return i;
        }
        return k;
    }
}

int Partition(long long * data, int size)
{
    int frontier = 1;
    int pivot = GetPivot(data, size);
    if (pivot != 0)
    {
        auto tmp = data[pivot];
        data[pivot] = data[0];
        data[0] = tmp;
    }

    auto pValue = data[0];
    auto end = &data[size], current = data + 1;
    for (; current != end; current++)
    {
        if (*current < pValue)
        {
            auto tmp = data[frontier];
            data[frontier++] = *current;
            *current = tmp;
        }
    }

    data[0] = data[--frontier];
    data[frontier] = pValue;

    return frontier;
}

int QuickSort(long long * data, int size)
{
    if (size <= 1)return 0;

    int pivot = Partition(data, size);
    int result = QuickSort(data, pivot)
        + QuickSort(&data[pivot + 1], size - pivot - 1);

    return result + size - 1;
}