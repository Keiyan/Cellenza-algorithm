// KargerMinCut.Cpp.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "utils.h"
#include "gpu.h"
#include "cpu.h"

int main(int argc, char* argv[])
{
	if (argc != 3)
	{
		std::cout << "Usage : " << std::endl
			<< "	KargerMinCut.Cpp.exe -cpu graphFileName for the CPU version" << std::endl
			<< "	KargerMinCut.Cpp.exe -gpu graphFileName for the GPGPU version" << std::endl;

		return -1;
	}

	bool cpu = strcmp(argv[1], "-cpu") == 0;
	std::string fileName = argv[2];

	auto start = GetTimeMs64();

	int width = 0;
	auto graph = ReadFile(fileName, width);
	int amountOfNodes = (int)graph.size();
	int iterationCount = amountOfNodes * amountOfNodes * 3;

	std::vector<int> results(iterationCount);

	if (cpu)
	{
		CpuMinCut(graph, iterationCount, amountOfNodes, width, results);
	}
	else
	{
		GpuMinCut(graph, iterationCount, amountOfNodes, width, results);
	}

	int result = INT_MAX;
	for (auto i = results.begin(); i < results.end(); i++)
	{
		if (*i < result) result = *i;
	}

	auto stop = GetTimeMs64();

	std::cout << "Performed " << iterationCount << " iterations" << std::endl;
	std::cout << "Edge count is " << result << std::endl;
	std::cout << "Time is " << (stop - start) / 10000 << " milliseconds" << std::endl;

	return result;
}

