using System;

namespace EnumTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the amount:");
            decimal amount = decimal.Parse(Console.ReadLine());
      
            Console.WriteLine("Please select the target currency:");
            Console.WriteLine("1. USD");
            Console.WriteLine("2. EUR");
            Console.WriteLine("3. TRY");
            Console.WriteLine("4. RUB");

            string targetCurrencyChoice = Console.ReadLine();
            Currency targetCurrency;
            switch (targetCurrencyChoice)
            {
                case "1":
                    targetCurrency = Currency.USD;
                    break;
                case "2":
                    targetCurrency = Currency.EUR;
                    break;
                case "3":
                    targetCurrency = Currency.TRY;
                    break;
                case "4":
                    targetCurrency = Currency.RUB;
                    break;
                default:
                    Console.WriteLine("Unknown target currency.");
                    return;
            }

            decimal convertedAmount = CurrencyConvert.ConvertCurrency(targetCurrency, amount);

            Console.WriteLine($"{amount} AZN is equal to {convertedAmount} {targetCurrency}");
        }
    }
}
