#pragma once

#include "stdafx.h"
#include "utils.h"

using namespace std;

int Contract(std::vector<std::vector<int>> & data, int height, int width) 
{
    static random_device dev;
    static default_random_engine e{ dev() };

    std::vector<int> redirections(height);
    std::vector<int> validDestinations(height);
    for (int i = 0; i < height; i++)
    {
        redirections[i] = i;
        validDestinations[i] = -1;
    }


    for (int loop = 0; loop < height - 2; loop++)
    {
        unsigned int source, destination;

        uniform_int_distribution<int> d{ 0, height - loop - 1 };
        int sourceIndex = d(e);
        for (int i = 0; i < height; i++)
        {
            if (sourceIndex == 0 && redirections[i] == i)
            {
                source = i;
                break;
            }
            if (redirections[i] == i) --sourceIndex;
        }

        int rank = 0;
        for (int i = 0; i < height; i++)
        {
            if (redirections[i] == source)
            {
                for (int j = 0; j < width; j++)
                {
                    auto local = data[i][j];
                    if (local == -1) break;

                    auto mappedLocal = redirections[local];
                    if (mappedLocal != source
                        && validDestinations[mappedLocal] == -1)
                    {
                        validDestinations[mappedLocal] = 1;
                        rank++;
                    }
                }
            }
        }

        uniform_int_distribution<int> d2{ 0, rank - 1 };
        int destIndex = d2(e);
        for (int i = 0; i < height; i++)
        {
            if (validDestinations[i] > -1)
            {
                if (destIndex == 0) destination = i;
                --destIndex;
                validDestinations[i] = -1;
            }
        }

        for (int i = 0; i < height; i++)
        {
            if (redirections[i] == source)
            {
                redirections[i] = destination;
            }
        }
    }

    int result = 0;
    int nodeValue = redirections[0];
    for (int i = 0; i < height; i++)
    {
        if (redirections[i] == nodeValue)
            for (int j = 0; j < width; j++)
            {
                auto local = data[i][j];
                if (local != -1 && redirections[local] != nodeValue) ++result;
            }
    }
    return result;
}

void CpuMinCut(std::vector<std::vector<int>> graph, size_t iterationCount, int height, int width, std::vector<int> & results)
{
    parallel_for(0, (int)iterationCount, 1, [=,&results, &graph](int i) {
        results[i] = Contract(graph, height, width);
    });
}
