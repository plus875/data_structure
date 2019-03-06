using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        int[] arr = GenerateArray(9);

        Console.WriteLine("\ninput num:");
        int read = int.Parse(Console.ReadLine());
        Console.Write("get num:" + read);

        int left = 0;
        int right = arr.Length - 1;
        int c = 0;
        while (left <= right)
        {
            var half = left + (right - left) / 2;

            if (arr[half] < read)
            {
                left = half + 1;
            }
            else if(arr[half] > read)
            {
                right = half - 1;
            }
            else
            {
                int startIdx = half;
                int endIdx = half;
                int i = 1;
                while (true)
                {
                    if (half - i >= 0 && arr[half] == arr[half - i])
                    {
                        startIdx = half - i;
                        i++;
                    }
                    else
                        break;
                }

                i = 1;
                while (true)
                {
                    if (half + i < arr.Length && arr[half] == arr[half + i])
                    {
                        endIdx = half + i;
                        i++;
                    }
                    else
                        break;
                }

                c = (endIdx - startIdx + 1);
                break;
            }
        }
        Console.WriteLine("\ncount:" + c);

        Console.ReadKey();
    }

    private static int[] GenerateArray(int len)
    {
        int[] arr1 = new int[]{1,1};

        return arr1;


        int num = 0;
        int[] arr = new int[len];
        Random r = new Random();
        for (var i = 0; i < arr.Length; i++)
        {
            int a = r.Next(0, 3);
            num += a;
            arr[i] = num;
            Console.Write(" " + arr[i]);
        }

        return arr;
    }
}

