namespace MortgageCalculator.SharedLogic.Calculators
{
    public class MonthlyMortgage : IMonthlyMortgage
    {
		double _term;
        double _interestRatePercent;
        double _mortgageAmount;
        double _housePrice;
        double _depositAmount;

        /// <inheritdoc />
        public double Term
        {
			get => _term;
            set => _term = value;
        }

        /// <inheritdoc />
        public double TermMonths => _term * 12;

        /// <inheritdoc />
		public double InterestRatePercent
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
        public double InterestRate => _interestRatePercent / 1200;

        /// <inheritdoc />
        public double MortgageAmount
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
        public double MonthlyRepayment
        {
            get
            {
                var ratio = (InterestRate * Math.Pow((1 + InterestRate), TermMonths)) /
                            (Math.Pow((1 + InterestRate), TermMonths) - 1);

                return MortgageAmount * ratio;
            }
        }

        /// <inheritdoc />
        public double InterestOnlyMonthlyRepayment => MortgageAmount * InterestRate;

        /// <inheritdoc />
        public double HousePrice
        {
            get => _housePrice;
            set
            {
                if (value < 0)
                    return;

                if (DepositAmount >= 0)
                {
                    MortgageAmount = HousePrice - DepositAmount;
                }

                _housePrice = value;
            }
        }

        /// <inheritdoc />
        public double DepositAmount
        {
            get => _depositAmount;
            set
            {
                if (value < 0)
                    return;

                if (HousePrice > 0)
                    MortgageAmount = HousePrice - DepositAmount;

                _depositAmount = value;
            }
        }

        /// <inheritdoc />
        public Dictionary<double, double> OutstandingAmountPerYear
        {
            get
            {
                var amountOutstanding = new Dictionary<double, double>();

                for (int i = 0; i <= Term; i++)
                    amountOutstanding.Add(i, CalculateRemainingAmount(i));

                return amountOutstanding;
            }
        }

        /// <inheritdoc />
        public Dictionary<double, double> OutstandingAmountPerYearWhenOverpayingMonthly
        {
            get
            {
                var amountOutstanding = new Dictionary<double, double>();

                for (int i = 0; i <= Term; i++)
                    amountOutstanding.Add(i, CalculateRemainingAmount(i, MonthlyOverPayment));

                return amountOutstanding;
            }
        }

        /// <inheritdoc />
        public double MonthlyOverPayment { get; set; }

        /// <inheritdoc />
        public double TotalCostOfMortgage => MonthlyRepayment * TermMonths;

        /// <inheritdoc />
        public double TotalCostOfMortgageWhenOverpaying =>
            CalculatePayoffDate() * (MonthlyRepayment + MonthlyOverPayment);

        /// <inheritdoc />
        public DateTime PayOffDateTime => DateTime.Now.AddMonths(CalculatePayoffDate());

        public DateTime PayOffDateTimeWhenOverpaying => DateTime.Now.AddMonths(CalculatePayoffDate(MonthlyOverPayment));

        /// <summary>
        /// Helper method that calculates the remaining number of months as an integer. Overpayment can be passed to give different dates.
        /// </summary>
        /// <returns>integer representing the months remaining until the mortgage amount reaches 0</returns>
        int CalculatePayoffDate(double overpayment = 0)
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
        /// <param name="currentYear">the year as a double</param>
        /// <param name="overpayment">optional overpayment as a double</param>
        /// <returns></returns>
        double CalculateRemainingAmount(double currentYear, double overpayment = 0)
        {
            var monthlyPayment = MonthlyRepayment + overpayment;
            var remainingAmount = MortgageAmount;

            for (int i = 1; i <= currentYear * 12; i++)
            {
                var interestPayment = remainingAmount * InterestRate;
                var principalPayment = monthlyPayment - interestPayment;
                remainingAmount -= principalPayment;
            }

            return remainingAmount;
        }
    }
}