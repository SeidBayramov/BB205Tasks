namespace StaticTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string text = "Said Bayramov";
            Console.WriteLine("Word is inside in string:");
            text.ToLower().CustomContains("said");
            Console.WriteLine("Character is inside in string:  ");
            text.ToLower().CustomContains("t");
            Console.WriteLine("=====================================================");

            Console.WriteLine("Enter checking Prime number:");
            int number = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter checking Power number:");
            int number2 = int.Parse(Console.ReadLine());
            bool isPrime=number.IsPrime();
            bool isPowOfTwo = number2.IsPowOfTwo();

            Console.WriteLine($"Number is Prime:  {isPrime}");
            Console.WriteLine($"Number is Power of two:   {isPowOfTwo}");

            }
    }
}