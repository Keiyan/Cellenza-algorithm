#pragma once
#pragma comment(lib,"d3d11.lib")

#include "stdafx.h"
#include "utils.h"
#include "rnd\amp_tinymt_rng.h"
#include <d3d11.h>

using namespace concurrency;

const int amountOfNodes = 500;

int Contract(const array_view<int, 2> & data, int height, int  width, tinymt random) restrict(amp)
{
    int redirections[amountOfNodes];
    int validDestinations[amountOfNodes];
    for (int  i = 0; i < height; i++)
    {
        redirections[i] = i;
        validDestinations[i] = -1;
    }


    for (int  loop = 0; loop < height - 2; loop++)
    {
        unsigned int source, destination;

        int sourceIndex = (random.next_uint() - 1) % (height-loop);
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
        for (int  i = 0; i < height; i++)
        {
            if (redirections[i] == source)
            {
                for (int  j = 0; j < width; j++)
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

        int destIndex = (random.next_uint() - 1) % rank;
        for (int i = 0; i < height; i++)
        {
            if (validDestinations[i] > -1)
            {
                if (destIndex == 0) destination = i;
                --destIndex;
                validDestinations[i] = -1;
            }
        }

        for (int  i = 0; i < height; i++)
        {
            if (redirections[i] == source)
            {
                redirections[i] = destination;
            }
        }
    }

    int result = 0;
    int nodeValue = redirections[0];
    for (int  i = 0; i < height; i++)
    {
        if (redirections[i] == nodeValue)
            for (int  j = 0; j < width; j++)
            {
                auto local = data[i][j];
                if (local != -1 && redirections[local] != nodeValue) ++result;
            }
    }
    return result;
}

accelerator_view CreateNoTDRAccellerator()
{
    unsigned int createDeviceFlags = D3D11_CREATE_DEVICE_DISABLE_GPU_TIMEOUT;
    ID3D11DeviceContext *pContext;
    D3D_FEATURE_LEVEL featureLevel;
    ID3D11Device *pDevice;
    D3D_FEATURE_LEVEL featureLevels[] =
    {
        D3D_FEATURE_LEVEL_11_1,
        D3D_FEATURE_LEVEL_11_0,
        D3D_FEATURE_LEVEL_10_1,
        D3D_FEATURE_LEVEL_10_0,
        D3D_FEATURE_LEVEL_9_3,
        D3D_FEATURE_LEVEL_9_1
    };
    HRESULT hr = D3D11CreateDevice(nullptr,
        D3D_DRIVER_TYPE_HARDWARE,
        NULL,
        createDeviceFlags,
        featureLevels,
        ARRAYSIZE(featureLevels),
        D3D11_SDK_VERSION,
        &pDevice,
        &featureLevel,
        &pContext);

    if (FAILED(hr) ||
        ((featureLevel != D3D_FEATURE_LEVEL_11_1) &&
        (featureLevel != D3D_FEATURE_LEVEL_11_0)))
    {
        std::cerr << "Failed to create Direct3D 11 device with HRESULT " << hr << std::endl;
        throw "Failed to create Direct3D device";
    }


    return concurrency::direct3d::create_accelerator_view(pDevice);
}

void GpuMinCut(std::vector<std::vector<int>> graph, int iterationCount, int height, int width, std::vector<int> & results)
{
    auto data = FlattenGraph(graph, width);

    accelerator_view accelerator = CreateNoTDRAccellerator();

    array_view<int, 2> gpuData(height, width, data);

    results.resize(iterationCount);
    array_view<int> gpuResults(iterationCount, results);

    concurrency::extent<1> e_size(65535);
    tinymt_collection<1> random(e_size);
    concurrency::extent<1> seedSize(1);
    tinymt_collection<1> seed(seedSize, 0);
    index<1> seedIndex(0);

    parallel_for_each(//accelerator,
        gpuResults.extent,
        [=](index<1> i) restrict(amp) {
        auto t = random[i % e_size.size()];

        t.initialize(seed[seedIndex].next_uint());

        gpuResults[i] = Contract(gpuData, (int)height, (int)width, t);
    });

    try
    {
        gpuResults.synchronize();
    }
    catch (accelerator_view_removed& ex)
    {
        std::cout << ex.what() << std::endl;
        std::cout << ex.get_view_removed_reason() << std::endl;
    }
}
