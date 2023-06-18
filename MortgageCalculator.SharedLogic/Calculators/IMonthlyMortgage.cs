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
    /// This series show the mortgage amount remaining per year of the mortgage
    /// </summary>
    Dictionary<int, decimal> OutstandingAmountPerYear { get; }

    /// <summary>
    /// 
    /// </summary>
    Dictionary<int, decimal> OutstandingAmountPerYearWhenOverpayingMonthly { get; }

    /// <summary>
    /// 
    /// </summary>
    decimal TotalCostOfMortgage { get; }
    
    /// <summary>
    /// 
    /// </summary>
    decimal TotalCostOfMortgageWhenOverpaying { get; }

    /// <summary>
    /// 
    /// </summary>
    DateTime PayOffDateTime { get; }

    /// <summary>
    /// 
    /// </summary>
    DateTime PayOffDateTimeWhenOverpaying { get; }
}