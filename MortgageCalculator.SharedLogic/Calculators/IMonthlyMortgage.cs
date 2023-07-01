using MortgageCalculator.SharedLogic.Models;
using MortgageCalculator.SharedLogic.Models.Chart;

namespace MortgageCalculator.SharedLogic.Calculators;

public interface IMonthlyMortgage
{
    /// <summary>
    /// Mortgage term when expressed in years
    /// </summary>
    int Term { get; set; }

    /// <summary>
    /// Mortgage term when expressed in months
    /// </summary>
    int TermMonths { get; }

    /// <summary>
    /// The mortgage interest rate expressed as a percentage
    /// </summary>
    decimal InterestRatePercent { get; set; }

    /// <summary>
    /// The mortgage interest rate expressed as a decimal and divided by 12
    /// </summary>
    decimal InterestRate { get; }

    /// <summary>
    /// The mortgage amount outstanding
    /// </summary>
    decimal MortgageAmount { get; set; }

    /// <summary>
    /// calculates the monthly repayment for a repayment style mortgage
    /// P * [i(1 + i)^n] / [(1 + i)^n - 1]
    ///
    /// Where:
    /// M = Monthly mortgage payment
    /// P = Principal loan amount(the amount you borrow)
    /// i = Monthly interest rate(annual interest rate divided by 12 and expressed as a decimal)
    /// n = Number of monthly payments(loan term in months)
    /// </summary>
    /// <returns>the monthly repayment as a decimal</returns>
    decimal MonthlyRepayment { get; }

    /// <summary>
    /// calculates the monthly repayment for a repayment style mortgage
    /// M = P * i
    ///
    /// Where:
    /// M = Monthly mortgage payment
    /// P = Principal loan amount(the amount you borrow)
    /// i = Monthly interest rate(annual interest rate divided by 12 and expressed as a decimal)
    /// </summary>
    /// <returns>the monthly repayment as a decimal</returns>
    decimal InterestOnlyMonthlyRepayment { get; }

    /// <summary>
    /// The purchase price of the house in the selected currency
    /// </summary>
    decimal HousePrice { get; set; }

    /// <summary>
    /// The amount of deposit/down-payment available for the mortgage
    /// </summary>
    decimal DepositAmount { get; set; }

    /// <summary>
    /// 
    /// </summary>
    decimal MonthlyOverPayment { get; set; }

    /// <summary>
    /// 
    /// </summary>
    IMortgageData Repayment { get; }

    /// <summary>
    /// 
    /// </summary>
    IMortgageData Overpayment { get; }

    /// <summary>
    /// 
    /// </summary>
    IMortgageData InterestOnly { get; }

    /// <summary>
    /// 
    /// </summary>
    string[] XAxisLabels { get; }

    /// <summary>
    /// Calculates the remaining mortgage amount for a given year
    /// </summary>
    /// <param name="currentYear">the year as a decimal</param>
    /// <param name="overpayment">optional overpayment as a decimal</param>
    /// <returns></returns>
    public decimal CalculateRemainingAmount(decimal currentYear, decimal overpayment = 0);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool ParametersAreNotSet();

    /// <summary>
    /// Helper method that calculates the remaining number of months as an integer. Overpayment can be passed to give different dates.
    /// </summary>
    /// <returns>integer representing the months remaining until the mortgage amount reaches 0</returns>
    public int CalculatePayoffDate(decimal overpayment = 0);

}