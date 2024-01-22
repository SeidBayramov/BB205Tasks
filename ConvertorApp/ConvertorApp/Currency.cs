using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumTask
{
    public class CurrencyConvert
    {

        public static decimal ConvertCurrency(CurrencyEnum targetCurrency, decimal amount)
        {
            decimal convertedAmount = 0;

            switch (targetCurrency)
            {
                case CurrencyEnum.USD:

                    convertedAmount = amount * 0.59m;
                    break;
                case CurrencyEnum.EUR:
                    convertedAmount = amount * 0.56m;
                    break;
                case CurrencyEnum.TRY:
                    convertedAmount = amount * 17.0m;
                    break;
                case CurrencyEnum.RUB:
                    convertedAmount = amount * 0.017m;
                    break;
                default:
                    throw new ArgumentException("Unsupported target currency.");
            }

            return convertedAmount;
        }
    }
}
