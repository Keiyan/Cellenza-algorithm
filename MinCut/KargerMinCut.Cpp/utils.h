#pragma once

#include "stdafx.h"

using namespace concurrency;

std::vector<std::vector<int>> ReadFile(std::string & fileName, int & width)
{
	FILE * file = NULL;
	fopen_s(&file, fileName.c_str(), "r");

	std::vector<std::vector<int>> result;
	if (!file) return result;

	char c = fgetc(file);
	int readInt = 0;
	bool startOfLine = true;
	int VerticeId = 0;
	int lineWidth = 0;
	while (c != EOF)
	{
		if (c == '\n')
		{
			if (lineWidth > width) width = lineWidth;
			readInt = 0;
			VerticeId = 0;
			lineWidth = 0;
			startOfLine = true;
		}
		else if (c == '\t')
		{
			if (startOfLine)
			{
				VerticeId = readInt - 1;
				std::vector<int> line;
				result.push_back(line);
			}
			else
			{
				result[VerticeId].push_back(readInt - 1);
				lineWidth++;
			}
			readInt = 0;
			startOfLine = false;
		}
		else
		{
			readInt *= 10;
			readInt += c - '0';
		}
		c = fgetc(file);
	}

	fclose(file);
	file = NULL;

	for (auto i = result.begin(); i < result.end(); i++)
	{
		while (i->size() < width) i->push_back(-1);
	}

	return result;
}

long long GetTimeMs64()
{
	/* Windows */
	FILETIME ft;
	LARGE_INTEGER li;

	/* Get the amount of 100 nano seconds intervals elapsed since January 1, 1601 (UTC) and copy it
	* to a LARGE_INTEGER structure. */
	GetSystemTimeAsFileTime(&ft);
	li.LowPart = ft.dwLowDateTime;
	li.HighPart = ft.dwHighDateTime;

	unsigned long long ret = li.QuadPart;
	//ret -= 116444736000000000LL; /* Convert from file time to UNIX epoch time. */
	//ret /= 10000; /* From 100 nano seconds (10^-7) to 1 millisecond (10^-3) intervals */

	return ret;
}

int * FlattenGraph(std::vector<std::vector<int>> graph, size_t width)
{
	size_t amountOfNodes = graph.size();

	auto result = new int[amountOfNodes * width];
	for (size_t i = 0; i < amountOfNodes; i++)
	{
		size_t j = 0;
		std::for_each(graph[i].begin(), graph[i].end(), [=, &result, &j](int elem)
		{
			result[i * width + j] = elem;
			j++;
		});
	}

	return result;
}
