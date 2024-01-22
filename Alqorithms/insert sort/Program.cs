
//================ Insertion Sort ===============

int[] array = { 50, 10, 21, 7, 9, 78, 36, 123 };
for (int i = 1; i < array.Length; i++)
{
    for (int j = i; j > 0; j--)
    {
        if (array[j - 1] > array[j])
        {
            int temp = array[j];
            array[j] = array[j - 1];
            array[j - 1] = temp;
        }
    }
}
Console.WriteLine(string.Join(", ", array));

// ============== Interpolation Search ===============


int[] array = { 10, 21, 27, 77, 99, 108, 136, 143 };
int n = int.Parse(Console.ReadLine();)
int l = 0;
int h = array.Length - 1;
int findNumber = 0;

while (l <= h && n >= array[l] && n <= array[h])
{
    findNumber = l + ((n - array[l]) * (h - l)) / (array[h] - array[l]);

    if (array[findNumber] == n)
    {
        Console.WriteLine("Element " + n + " found at position " + findNumber);
        break;
    }

    if (array[findNumber] < n)
    {
        l = findNumber + 1;
    }
    else
    {
        h = findNumber - 1;
    }
}

if (l > h || n < array[l] || n > array[h])
{
    Console.WriteLine("Element " + n + " not found in the array.");
}
