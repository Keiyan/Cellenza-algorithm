#pragma once

#include "stdafx.h"

template < class T, class Compare = std::less<T>> class Heap
{
private:
    T** _Data;
    Compare _Comparer;
    size_t _DataSize;
    size_t _Amount;
public:
    Heap(size_t reservedSize = 64)
    {
        this->_Amount = 0;
        this->_DataSize = reservedSize;
        this->_Data = new T*[reservedSize];
        this->_Comparer = Compare();
    }

    virtual ~Heap()
    {
        delete [] this->_Data;
        this->_Amount = 0;
        this->_DataSize = 0;
    }

    void Insert(T* toInsert)
    {
        if (this->_Amount == this->_DataSize) this->expand();
        this->_Data[this->_Amount] = toInsert;
        this->bubbleUp(this->_Amount++);
    }

    T* PeekTop()
    {
        if (this->_Amount == 0) return NULL;
        return this->_Data[0];
    }

    T* ExtractTop()
    {
        if (this->_Amount == 0) return NULL;
        T* result = this->_Data[0];
        this->_Data[0] = this->_Data[--this->_Amount];
        this->bubbleDown(0);
        return result;
    }

    size_t const Size()
    {
        return this->_Amount;
    }

private:
    void bubbleUp(size_t index)
    {
        if (index == 0) return;
        size_t parentIndex = index >> 1;
        if (this->_Comparer(*this->_Data[index], *this->_Data[parentIndex]))
        {
            T* parent = this->_Data[parentIndex];
            this->_Data[parentIndex] = this->_Data[index];
            this->_Data[index] = parent;
            this->bubbleUp(parentIndex);
        }
    }

    void bubbleDown(size_t index)
    {
        size_t childIndex = index ? index << 1 : 1;
        if (childIndex >= this->_Amount) return;

        T* minChild = this->_Data[childIndex];
        if (childIndex + 1 < this->_Amount
            && this->_Comparer(*this->_Data[childIndex + 1], *minChild))
        {
            minChild = this->_Data[++childIndex];
        }

        if (!this->_Comparer(*this->_Data[index], *minChild))
        {
            T* child = this->_Data[childIndex];
            this->_Data[childIndex] = this->_Data[index];
            this->_Data[index] = child;
            this->bubbleDown(childIndex);
        }
    }

    void expand()
    {
        T** expanded = new T*[this->_DataSize * 2];
        for (size_t i = 0; i < this->_DataSize; i++)
        {
            expanded[i] = this->_Data[i];
        }

        delete [] this->_Data;
        this->_DataSize *= 2;
        this->_Data = expanded;
    }
};
