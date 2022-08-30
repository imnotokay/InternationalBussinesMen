using System;
using System.Collections.Generic;
using System.Text;

namespace InternationalBusinessMen.ApplicationCore.Extensions
{
    public static class DecimalExtension
    {
        public static decimal BankersRounding(this decimal value)
        {
            decimal output = (int)value;
            Boolean isNegative = value < 0;
            decimal decimalPart = Convert.ToDecimal(Math.Truncate(value).ToString()[0].ToString());

            if (decimalPart < 5)
            {
                output = (int)value;
            }
            else if (decimalPart > 5)
            {
                if (isNegative)
                    output = ((int)value) - 1;
                else
                    output = ((int)value) + 1;
            }
            else
            {
                if(2 % value == 0)
                {
                    if (isNegative)
                        output = ((int)value) + 1;
                    else
                        output = ((int)value) - 1;
                }
                else
                {
                    if (isNegative)
                        output = ((int)value) - 1;
                    else
                        output = ((int)value) + 1;
                }
            }

            return output;
        }
    }
}
