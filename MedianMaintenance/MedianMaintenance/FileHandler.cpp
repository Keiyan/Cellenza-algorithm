#include "stdafx.h"
#include "MedianMaintenance.h"

#define BUFFER_SIZE 100000

int* ReadFile(const char * fileName, int size)
{
    FILE * file = NULL;
    fopen_s(&file, fileName, "r");
    if (!file) return NULL;

    int* result = new int[size];

    char buffer[BUFFER_SIZE];
    auto readChars = fread_s(buffer, BUFFER_SIZE, sizeof(char) , BUFFER_SIZE, file);
    int readInt = 0;
    unsigned int i = 0, j = 0;
    char c = buffer[i];

    while (c != EOF && readChars != 0)
    {
        if (c == '\n')
        {
            result[j++] = readInt;
            readInt = 0;
        }
        else
        {
            readInt *= 10;
            readInt += c - '0';
        }
        if (++i >= readChars)
        {
            readChars = fread_s(buffer, BUFFER_SIZE, sizeof(char) , BUFFER_SIZE, file);
            i = 0;
        }

        c = buffer[i];
    }

    fclose(file);
    file = NULL;

    return result;
}