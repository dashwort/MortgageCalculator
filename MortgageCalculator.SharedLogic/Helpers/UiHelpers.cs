using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculator.SharedLogic.Helpers
{
    public static class UiHelpers
    {
        public static string ToBritishDateFormat(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("en-GB"));
        }

        public static string RepaymentDateFormat(this DateTime dateTime)
        {
            return dateTime.ToString("MMMM yyyy", CultureInfo.GetCultureInfo("en-GB"));
        }

        public static string ConvertDecimalToStandardFormat(this decimal cost)
        {
            return $"£{String.Format("{0:N2}", cost)}";
        }
    }
}
