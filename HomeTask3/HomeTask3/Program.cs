//   tap1                  1 ilə 25 arasında kök altısı olan ədədləri tapın.

int count = 0;
for (int i = 1; i <= 25; i++)
{
    if (i * i >= 1 && i * i <= 25)
    {

        Console.WriteLine("Kokaltisi olan ededler " + i);
        count++;
    }
}




// tap2            Arraydaki yalnız tək ədədlərin cəmini çap edən alqoritm qurun.


int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
int sum = 0;
for (int i = 0; i < numbers.Length; i++)
{
    if (numbers[i] % 2 != 0)
    {
        sum += numbers[i];
    }
}
Console.WriteLine("Sum  " + sum);



//tap 3   Verilmiş massivin tək indeksində duran elementlərin cəmini tapan alqoritm yazın. Nəticəni konsola çıxarın. Məs. {1, 2, 1, 4, 3} => 6


int[] array = { 1, 2, 1, 4, 3 };
int sum = 0;
for (int i = 1; i < array.Length; i += 2)
{
    sum += array[i];
}

Console.WriteLine("Sum " + sum);


//.Verilmiş sozun polindrom olub olmadigini göstərən alqoritm yazın (məs: "ata" sondan və baslanğıcdan eyni oxunur cavab true cixacaq)

string arr = Console.ReadLine();
int l = 0;
int h = arr.Length - 1;
bool palindrom = true;

while (l < h)
{
    if (arr[l] != arr[h])
    {
        palindrom = false;

        break;
    }
        l++;
        h--;
    }

    if (palindrom)
    {
        Console.WriteLine("palindromdu ");
    }
    else
    {
        Console.WriteLine("palindrom deyil ");
    }


