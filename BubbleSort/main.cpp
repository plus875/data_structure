#include "iostream"
#include "stdio.h"

void bubble_sort(int* arr, int len) {
	for (int i = 0; i < len - 1; i++)
	{
		for (int j = 0; j < len - 1 - i; j++)
		{
			if (arr[j + 1] < arr[j]) {
				int temp = arr[j + 1];
				arr[j + 1] = arr[j];
				arr[j] = temp;
			}
		}
	}

	for (int i = 0; i < len; i++)
	{
		printf("i:%d, value:%d \n", i, arr[i]);
	}
}

int main() {
	int arr[] = {5, 10, 22, 33, 4, 11, 9, 80};

	int len = sizeof(arr) / (sizeof(*arr));
	bubble_sort(arr, len);

	system("pause");

	return 0;
}

