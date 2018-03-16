// CountingInversions.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "CountingInversions.h"

unsigned long CountSplit(ListItem * left, int leftSize, ListItem * right, int rightSize, ListItem ** sorted)
{
	ListItem * cLeft = left, *cRight = right, *current = TempRoot;
	unsigned long result = 0;
	while (leftSize > 0 && rightSize > 0)
	{
		if (cLeft->data < cRight->data)
		{
			current->next = cLeft;
			cLeft = cLeft->next;
			--leftSize;
		}
		else
		{
			current->next = cRight;
			cRight = cRight->next;
			--rightSize;
			result += leftSize;
		}

		current = current->next;
	}
	if (leftSize > 0) current->next = cLeft;
	if (rightSize > 0) current->next = cRight;

	*sorted = TempRoot->next;
	TempRoot->next = NULL;

	return result;
}

unsigned long CountInversion(ListItem * source, int size, ListItem ** sortedSource)
{
	if (size == 0)
	{
		*sortedSource = NULL;
		return 0;
	}
	if (size == 1)
	{
		*sortedSource = source;
		return 0;
	}

	ListItem * left = NULL, *right = NULL;
	int half = size >> 1, odd = size & 1;
	unsigned long result = CountInversion(source, half, &left);
	result += CountInversion(&(source[half]), half + odd, &right);
	result += CountSplit(left, half, right, half + odd, sortedSource);

	return result;
}