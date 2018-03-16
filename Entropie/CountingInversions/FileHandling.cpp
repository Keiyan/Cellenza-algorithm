#include "stdafx.h"
#include "CountingInversions.h"

int GetArraySize(int fileSize)
{
	fileSize += 3; // artificially add '0\r\n'
	int amount = 9, size = 3; // 10 single digit of size 3 (digit + CR + LF)
	int arraySize = 0;
	while (fileSize > amount * size)
	{
		arraySize += amount;
		fileSize -= amount * size;
		amount *= 10; // 10 more number
		size+=1; // with one more digit
	}

	return arraySize + fileSize / size;
}

long GetFileSize(const char *fileName)
{
	BOOL                        fOk;
	WIN32_FILE_ATTRIBUTE_DATA   fileInfo;

	if (NULL == fileName)
		return -1;

	fOk = GetFileAttributesExA(fileName, GetFileExInfoStandard, (void*) &fileInfo);
	if (!fOk)
		return -1;
	return (long) fileInfo.nFileSizeLow;
}

void ReadArray(const char * fileName)
{
	FILE * file = NULL;
	fopen_s(&file, fileName, "r");
	if (!file) return;

	long fileSize = GetFileSize(fileName);

	Size = GetArraySize(fileSize);
	Data = new ListItem[Size];

	ListItem *current = Data;
	int index = 0;

	char c = fgetc(file);
	int readInt = 0;
	while (c != EOF)
	{
		if (c == '\n')
		{
			++index;
			current->data = readInt;
			readInt = 0;
			current->next = &Data[index];
			current = current->next;
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

	current = NULL;
}
