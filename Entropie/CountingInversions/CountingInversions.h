#pragma once
#include "stdafx.h"

struct ListItem
{
	int data;
	ListItem * next;

	ListItem(){ data = 0; next = NULL; }
};

extern ListItem * Data;
extern ListItem * TempRoot;
extern int Size;

unsigned long CountSplit(ListItem * left, int leftSize, ListItem * right, int rightSize, ListItem ** sorted);

unsigned long CountInversion(ListItem * source, int size, ListItem ** sortedSource);

void ReadArray(const char * fileName);

long long GetTimeMs64();
