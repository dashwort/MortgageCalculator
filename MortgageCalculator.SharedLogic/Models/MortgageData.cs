using MortgageCalculator.SharedLogic.Calculators;
using MortgageCalculator.SharedLogic.Models.Chart;
using MortgageCalculator.SharedLogic.Models.enums;

namespace MortgageCalculator.SharedLogic.Models
{
    public class MortgageData : IMortgageData
    {
        IMonthlyMortgage _calculator;
        MortgageType _type;

        public MortgageData(IMonthlyMortgage calculator, MortgageType type)
        {
            _calculator = calculator;
            _type = type;
        }

        /// <inheritdoc />
        public Dataset OutstandingAmountPerYear
        {
            get
            {
                var amountOutstanding = new List<decimal>();
                var dataset = new Dataset();

                if (_calculator.ParametersAreNotSet())
                    return dataset;

                switch (_type)
                {
                    case MortgageType.Overpayment:
                        dataset.label = "Outstanding Amount (£) with Overpayment";

                        for (int i = 0; i <= _calculator.Term; i++)
                            amountOutstanding.Add(_calculator.CalculateRemainingAmount(i, _calculator.MonthlyOverPayment));
                        break;
                    case MortgageType.InterestOnly:
                        dataset.label = "Outstanding Amount (£) Interest Only";

                        for (int i = 0; i <= _calculator.Term; i++)
                            amountOutstanding.Add(_calculator.MortgageAmount);
                        break;
                    default:
                        dataset.label = "Outstanding Amount (£)";

                        for (int i = 0; i <= _calculator.Term; i++)
                            amountOutstanding.Add(_calculator.CalculateRemainingAmount(i));
                        break;
                }

                dataset.data = amountOutstanding.ToArray();

                return dataset;
            }
        }

        /// <inheritdoc />
        public decimal TotalCostOfMortgage
        {
            get
            {
                switch (_type)
                {
                    case MortgageType.Overpayment:
                        return _calculator.CalculatePayoffDate(_calculator.MonthlyOverPayment) * (_calculator.MonthlyRepayment + _calculator.MonthlyOverPayment);
                    case MortgageType.InterestOnly:
                        return _calculator.InterestOnlyMonthlyRepayment * _calculator.TermMonths;
                    default:
                        return _calculator.MonthlyRepayment * _calculator.TermMonths;
                }
            }
        }

        /// <inheritdoc />
        public DateTime PayOffDateTime
        {
            get
            {
                switch (_type)
                {
                    case MortgageType.Overpayment:
                        return DateTime.Now.AddMonths(_calculator.CalculatePayoffDate(_calculator.MonthlyOverPayment));
                    case MortgageType.InterestOnly:
                        return DateTime.Now.AddMonths(_calculator.TermMonths);
                    default:
                        return DateTime.Now.AddMonths(_calculator.CalculatePayoffDate());
                }
            }
        }

        /// <inheritdoc />
        public Data ChartData
        {
            get
            {
                var data = new Data();
                data.labels = _calculator.XAxisLabels;
                data.AddDataSet(OutstandingAmountPerYear);
                return data;
            }
        }
    }
}
