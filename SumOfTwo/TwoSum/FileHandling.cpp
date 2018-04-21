#include "stdafx.h"
#include "TwoSum.h"

#define BUFFER_SIZE 100000

std::hash_set<long long> * ReadFile(const char * fileName)
{
    FILE * file = NULL;
    fopen_s(&file, fileName, "r");
    if (!file) return NULL;

    std::hash_set<long long> * result = new std::hash_set<long long>();

    char buffer[BUFFER_SIZE];
    auto readChars = fread_s(buffer, BUFFER_SIZE, sizeof(char) , BUFFER_SIZE, file);
    long long readInt = 0;
    unsigned int i = 0;
    char c = buffer[i];
    bool minus = false;

    while (c != EOF && readChars != 0)
    {
        if (c == '\n')
        {
            if (minus) readInt *= -1;
            result->insert(readInt);
            readInt = 0;
            minus = false;
        }
        else if (c == '-')
        {
            minus = true;
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

long long * ReadFileAsArray(const char * fileName, int size, long long offset)
{
    FILE * file = NULL;
    fopen_s(&file, fileName, "r");
    if (!file) return NULL;

    long long* result = new long long[size];

    char buffer[BUFFER_SIZE];
    auto readChars = fread_s(buffer, BUFFER_SIZE, sizeof(char) , BUFFER_SIZE, file);
    long long readInt = 0;
    unsigned int i = 0, j = 0;
    char c = buffer[i];
    bool minus = false;

    while (c != EOF && readChars != 0)
    {
        if (c == '\n')
        {
            if (minus) readInt *= -1;
            result[j++] = readInt - offset;
            readInt = 0;
            minus = false;
        }
        else if (c == '-')
        {
            minus = true;
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