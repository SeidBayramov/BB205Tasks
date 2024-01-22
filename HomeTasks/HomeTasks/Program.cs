//==============TASK1=====================
namespace HomeTasks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = { 5, 2, 9, 1, 5, 6 };
            int minValue = MinNumber(numbers);

            Console.WriteLine("The smallest element in the array is: " + minValue);

        public static int MinNumber(int[] arr)//We can use also params method for arrays here:)

        {
            int min = arr[0];

            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                }
            }

            return min;
        }
    }
}


//================TASK2=================
using System;

class Program
{
    static void Main(string[] args)
    {
        int num1 = 16;
        int num2 = 2;

        bool isPower = IsPowerOf(num1, num2);

        if (isPower)
        {
            Console.WriteLine(num1 + " is a power of  " + num2);
        }
        else
        {
            Console.WriteLine(num1 + " is not a power of  " + num2);
        }
    }

    static bool IsPowerOf(int x, int y)
    {
        if (x == 1)
        {
            return true;
        }
        while (x > 1)
        {
            if (x % y != 0)
            {
                return false;
            }
            x /= y;
        }

        return x == 1;
    }
}


//==============TASK3==============

using System;

class Program
{
    static void Main(string[] args)
    {
        int[] array = { 1, 2, 3, 4, 5 };
        bool isArrayOrdered = is_Ordered(array);

        Console.WriteLine(isArrayOrdered);
    }

    public static bool is_Ordered(int[] arr)
    {
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] < arr[i - 1])
            {
                return false;
            }
        }

        return true;
    }
}


///// ==============TASK4==========
using System;

class Program
{
    static void Main(string[] args)
    {
        string inputString = "test";
        char searchChar = 'a';

        int charIndex = FindCharIndex(inputString, searchChar);

        if (charIndex != -1)
        {
            Console.WriteLine(searchChar + " is found at index : " + charIndex);
        }
        else
        {
            Console.WriteLine(searchChar + " is not found in the string  " + "-1");
        }
    }

    static int FindCharIndex(string input, char targetChar)
    {
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == targetChar)
            {
                return i;
            }
        }
        return -1;
    }
}

