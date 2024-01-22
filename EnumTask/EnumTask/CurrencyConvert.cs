using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumTask
{
    public class CurrencyConvert
    {

        public static decimal ConvertCurrency(Currency targetCurrency, decimal amount)
        {
            decimal convertedAmount = 0;

            switch (targetCurrency)
            {
                case Currency.USD:
                   
                    convertedAmount = amount * 0.59m;
                    break;
                case Currency.EUR:
                    convertedAmount = amount * 0.56m;
                    break;
                case Currency.TRY:
                    convertedAmount = amount * 17.0m;
                    break;
                case Currency.RUB:
                    convertedAmount = amount * 0.017m;
                    break;
                default:
                    Console.WriteLine(("Unsupported target currency."));
                    return 0;
            }

            return convertedAmount;
        }
    }
}
