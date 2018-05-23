#include "stdafx.h"
#include "MedianMaintenance.h"

using namespace std;

template<class _Ty = void>
struct more
    : public binary_function<_Ty, _Ty, bool>
{
    // functor for operator<
    bool operator()(const _Ty& _Left, const _Ty& _Right) const
    {
        // apply operator< to operands
        return (_Left > _Right);
    }
};

int SumOfMedian(int * data, int size)
{
    Heap<int, more<int>> Bottom;
    Heap<int> Top;
    int result = data[0];

    if (data[0] > data[1])
    {
        Bottom.Insert(&data[1]);
        Top.Insert(&data[0]);
        result += data[1];
    }
    else
    {
        Top.Insert(&data[1]);
        Bottom.Insert(&data[0]);
        result += data[0];
    }

    for (size_t i = 2; i < size; i++)
    {
        if (data[i] < *Bottom.PeekTop())
        {
            Bottom.Insert(&data[i]);
        }
        else
        {
            Top.Insert(&data[i]);
        }

        if (Top.Size() > Bottom.Size())
        {
            Bottom.Insert(Top.ExtractTop());
        }
        else if (Bottom.Size() > Top.Size() + 1)
        {
            Top.Insert(Bottom.ExtractTop());
        }

        result += *Bottom.PeekTop();
    }

    return result % 10000;
}
