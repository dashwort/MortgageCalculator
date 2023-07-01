using MortgageCalculator.SharedLogic.Models;
using MortgageCalculator.SharedLogic.Models.Chart;

namespace MortgageCalculator.SharedLogic.Calculators
{
    public class MonthlyMortgage : IMonthlyMortgage
    {
		int _term;
        decimal _interestRatePercent;
        decimal _mortgageAmount;
        decimal _housePrice;
        decimal _depositAmount;

        /// <inheritdoc />
        public int Term
        {
			get => _term;
            set => _term = value;
        }

        /// <inheritdoc />
        public int TermMonths => _term * 12;

        /// <inheritdoc />
		public decimal InterestRatePercent
        {
			get => _interestRatePercent;
            set
            {
                if (value is > 0 and < 100)
                {
                    _interestRatePercent = value;
                }
            }
		}

        /// <inheritdoc />
        public decimal InterestRate => _interestRatePercent / 1200;

        /// <inheritdoc />
        public decimal MortgageAmount
        {
            get => _mortgageAmount;
            set
            {
                if (value < 0)
                    return;

                _mortgageAmount = value;
            }
        }

        /// <inheritdoc />
        public decimal MonthlyRepayment
        {
            get
            {
                if (ParametersAreNotSet())
                    return 0;

                var ratio = ((double)InterestRate * Math.Pow(1 + (double)InterestRate, TermMonths)) /
                                (Math.Pow((1 + (double)InterestRate), TermMonths) - 1);

                return Math.Round(MortgageAmount * (decimal)ratio);
            }
        }

        /// <inheritdoc />
        public decimal InterestOnlyMonthlyRepayment => MortgageAmount * InterestRate;

        /// <inheritdoc />
        public decimal HousePrice
        {
            get => _housePrice;
            set
            {
                if (value <= 0)
                    return;

                _housePrice = value;

                if (DepositAmount > 0 && HousePrice > 0)
                    MortgageAmount = HousePrice - DepositAmount;
            }
        }

        /// <inheritdoc />
        public decimal DepositAmount
        {
            get => _depositAmount;
            set
            {
                if (value <= 0)
                    return;

                _depositAmount = value;

                if (HousePrice > 0 && DepositAmount > 0) 
                    MortgageAmount = HousePrice - DepositAmount;
            }
        }

        /// <inheritdoc />
        public string[] XAxisLabels
        {
            get
            {
                var xAxisLabels = new List<string>();

                for (int i = 0; i <= Term; i++)
                    xAxisLabels.Add(i.ToString());

                return xAxisLabels.ToArray();
            }
        }

        /// <inheritdoc />
        public decimal MonthlyOverPayment { get; set; }

        /// <inheritdoc />
        public IMortgageData Repayment
        {
            get
            {
                return new MortgageData(this, Models.enums.MortgageType.Repayment);
            }
        }

        /// <inheritdoc />
        public IMortgageData Overpayment
        {
            get
            {
                return new MortgageData(this, Models.enums.MortgageType.Overpayment);
            }
        }

        /// <inheritdoc />
        public IMortgageData InterestOnly
        {
            get
            {
                return new MortgageData(this, Models.enums.MortgageType.InterestOnly);
            }
        }

        /// <inheritdoc />
        public int CalculatePayoffDate(decimal overpayment = 0)
        {
            var remainingAmount = MortgageAmount;
            var totalMonths = 0;

            while (remainingAmount > 0)
            {
                var interestPayment = remainingAmount * InterestRate;
                var principalPayment = MonthlyRepayment - interestPayment + overpayment;
                remainingAmount -= principalPayment;
                totalMonths++;

                if (totalMonths > TermMonths)
                    break;
            }

            return totalMonths;
        }

        /// <inheritdoc />
        public decimal CalculateRemainingAmount(decimal currentYear, decimal overpayment = 0)
        {
            var monthlyPayment = MonthlyRepayment + overpayment;
            var remainingAmount = MortgageAmount;

            for (int i = 1; i <= currentYear * 12; i++)
            {
                var interestPayment = remainingAmount * InterestRate;
                var principalPayment = monthlyPayment - interestPayment;
                remainingAmount -= principalPayment;
            }

            return remainingAmount < 0 ? 0 : remainingAmount;
        }

        public bool ParametersAreNotSet()
        {
            return InterestRatePercent <= 0 || Term <= 0 || MortgageAmount <= 0;
        }
    }
}