//// =============TASK1==============
class Program
{
    public static void Main()
    {
        int[] numbers = { 1, 2, 11, 7, 3 };
        SwapMinMax(numbers);

        foreach (int number in numbers)
        {
            Console.Write(number + " ");
        }
        static void SwapMinMax(int[] arr)
        {
            if (arr.Length < 2)
            {
                return;
            }
            int minIndex = 0;
            int maxIndex = 0;

            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] < arr[minIndex])
                {
                    minIndex = i;
                }
                else if (arr[i] > arr[maxIndex])
                {
                    maxIndex = i;
                }
            }
            int temp = arr[minIndex];
            arr[minIndex] = arr[maxIndex];
            arr[maxIndex] = temp;
        }
    }
}


//================TASK2===============

class Program
{
    static void Main()
    {
        int[] numbers = { 1, 3, 7 };
        Console.Write("Original Array: ");
        PrintArray(numbers);

        Console.WriteLine();// setirler arasi bosluq ucun

        int[] modifiedArray = InsertArray(ref numbers, 4, 5, 6);
        Console.Write("Modified Array: ");
        PrintArray(modifiedArray);
    }

    static void PrintArray(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write(array[i]);
            if (i < array.Length - 1)
            {
                Console.Write(", ");
            }
        }
    }

    static int[] InsertArray( ref int[] originalArray, params int[] Add)
        {
            int originalLength = originalArray.Length;
            int newLength = originalLength + Add.Length;

            int[] newArray = new int[newLength];

            for (int i = 0; i < originalLength; i++)
            {
                newArray[i] = originalArray[i];
            }

            for (int i = 0; i < Add.Length; i++)
            {
                newArray[originalLength + i] = Add[i];
            }

            return newArray;

        }
    }
