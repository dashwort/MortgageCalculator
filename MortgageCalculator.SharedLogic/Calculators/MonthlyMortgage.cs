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
                    return 1;

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
        public Dictionary<int, decimal> OutstandingAmountPerYear
        {
            get
            {
                var amountOutstanding = new Dictionary<int, decimal>();

                if (ParametersAreNotSet())
                    return amountOutstanding;

                for (int i = 0; i <= Term; i++)
                    amountOutstanding.Add(i, CalculateRemainingAmount(i));

                return amountOutstanding;
            }
        }

        /// <inheritdoc />
        public Dictionary<int, decimal> OutstandingAmountPerYearWhenOverpayingMonthly
        {
            get
            {
                var amountOutstanding = new Dictionary<int, decimal>();

                if (ParametersAreNotSet())
                    return amountOutstanding;

                for (int i = 0; i <= Term; i++)
                    amountOutstanding.Add(i, CalculateRemainingAmount(i, MonthlyOverPayment));

                return amountOutstanding;
            }
        }

        /// <inheritdoc />
        public decimal MonthlyOverPayment { get; set; }

        /// <inheritdoc />
        public decimal TotalCostOfMortgage => MonthlyRepayment * TermMonths;

        /// <inheritdoc />
        public decimal TotalCostOfMortgageWhenOverpaying =>
            CalculatePayoffDate(MonthlyOverPayment) * (MonthlyRepayment + MonthlyOverPayment);

        /// <inheritdoc />
        public DateTime PayOffDateTime => DateTime.Now.AddMonths(CalculatePayoffDate());

        public DateTime PayOffDateTimeWhenOverpaying => DateTime.Now.AddMonths(CalculatePayoffDate(MonthlyOverPayment));

        /// <summary>
        /// Helper method that calculates the remaining number of months as an integer. Overpayment can be passed to give different dates.
        /// </summary>
        /// <returns>integer representing the months remaining until the mortgage amount reaches 0</returns>
        int CalculatePayoffDate(decimal overpayment = 0)
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

        /// <summary>
        /// Calculates the remaining mortgage amount for a given year
        /// </summary>
        /// <param name="currentYear">the year as a decimal</param>
        /// <param name="overpayment">optional overpayment as a decimal</param>
        /// <returns></returns>
        decimal CalculateRemainingAmount(decimal currentYear, decimal overpayment = 0)
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