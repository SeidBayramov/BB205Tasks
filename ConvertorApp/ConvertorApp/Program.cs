namespace EnumTask
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("==================Welcome Convert APP=============");
            Console.WriteLine("Please enter amount");
            decimal amount = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Please selecet target currency");
            foreach (CurrencyEnum currency in Enum.GetValues(typeof(CurrencyEnum)))
            {
                Console.WriteLine($"{(int)currency}. {currency}");
            }

            int targetCurrencyInput;
            if (int.TryParse(Console.ReadLine(), out targetCurrencyInput) &&
                Enum.IsDefined(typeof(CurrencyEnum), targetCurrencyInput))
            {
                CurrencyEnum targetCurrency = (CurrencyEnum)(targetCurrencyInput);

                decimal convertedAmount = CurrencyConvert.ConvertCurrency(targetCurrency, amount);
                Console.WriteLine($"{amount} USD is equal to {convertedAmount} {targetCurrency}");
            }
            else
            {
                Console.WriteLine("Invalid target currency selection.");
            }
        }
    }
}

