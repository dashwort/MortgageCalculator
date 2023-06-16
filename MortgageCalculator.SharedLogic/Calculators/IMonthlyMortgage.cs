namespace MortgageCalculator.SharedLogic.Calculators;

public interface IMonthlyMortgage
{
    /// <summary>
    /// Mortgage term when expressed in years
    /// </summary>
    double Term { get; set; }

    /// <summary>
    /// Mortgage term when expressed in months
    /// </summary>
    double TermMonths { get; }

    /// <summary>
    /// The mortgage interest rate expressed as a percentage
    /// </summary>
    double InterestRatePercent { get; set; }

    /// <summary>
    /// The mortgage interest rate expressed as a double and divided by 12
    /// </summary>
    double InterestRate { get; }

    /// <summary>
    /// The mortgage amount outstanding
    /// </summary>
    double MortgageAmount { get; set; }

    /// <summary>
    /// calculates the monthly repayment for a repayment style mortgage
    /// P * [i(1 + i)^n] / [(1 + i)^n - 1]
    ///
    /// Where:
    /// M = Monthly mortgage payment
    /// P = Principal loan amount(the amount you borrow)
    /// i = Monthly interest rate(annual interest rate divided by 12 and expressed as a double)
    /// n = Number of monthly payments(loan term in months)
    /// </summary>
    /// <returns>the monthly repayment as a double</returns>
    double MonthlyRepayment { get; }

    /// <summary>
    /// calculates the monthly repayment for a repayment style mortgage
    /// M = P * i
    ///
    /// Where:
    /// M = Monthly mortgage payment
    /// P = Principal loan amount(the amount you borrow)
    /// i = Monthly interest rate(annual interest rate divided by 12 and expressed as a double)
    /// </summary>
    /// <returns>the monthly repayment as a double</returns>
    double InterestOnlyMonthlyRepayment { get; }

    /// <summary>
    /// The purchase price of the house in the selected currency
    /// </summary>
    double HousePrice { get; set; }

    /// <summary>
    /// The amount of deposit/down-payment available for the mortgage
    /// </summary>
    double DepositAmount { get; set; }
}