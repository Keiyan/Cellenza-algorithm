#pragma once

#include "stdafx.h"

long long GetTimeMs64();

std::hash_set<long long> * ReadFile(const char * fileName);
long long * ReadFileAsArray(const char * fileName, int size, long long offset);

int QuickSort(long long * data, int size);
int Find(long long * data, int size, long long toFind);

bool FindIf2Sum(std::hash_set<long long> & data, int target);

std::hash_set<int> FindSums(long long min, long long * data, int size, long long offset);