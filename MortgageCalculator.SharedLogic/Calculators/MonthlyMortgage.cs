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
                {
                    MortgageAmount = HousePrice - DepositAmount;
                }

                _depositAmount = value;
            }
        }
    }
}