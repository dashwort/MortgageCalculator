using MortgageCalculator.SharedLogic.Calculators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalculator.SharedLogic.Tests
{
    [TestClass]
    public class MortgageCalculatorTests
    {
        private Fixture fixture;
        private decimal _mortgageAmount;
        private decimal _interestRateInPercent;
        private int _termInYears;

        [TestInitialize]
        public void Initialize()
        {
            fixture = new Fixture();

            _mortgageAmount = 320000m;
            _interestRateInPercent = 6m;
            _termInYears = 40;
        }

        [TestMethod]
        public void Should_Return_Correct_MonthlyPayment_When_InterestOnlyMortgage()
        {
            // Arrange
            var calculator = new MonthlyMortgage
            {
                InterestRatePercent = _interestRateInPercent,
                MortgageAmount = _mortgageAmount,
                Term = _termInYears
            };

            // Act
            var sut = Math.Round(calculator.InterestOnlyMonthlyRepayment, 0);

            // Assert
            Assert.AreEqual(sut, 1600m);
        }

        [TestMethod]
        public void Should_Return_Correct_MonthlyPayment_When_RepaymentMortgage()
        {
            // Arrange
            var calculator = new MonthlyMortgage
            {
                InterestRatePercent = _interestRateInPercent,
                MortgageAmount = _mortgageAmount,
                Term = _termInYears
            };

            // Act
            var sut = Math.Round(calculator.MonthlyRepayment, 0);

            // Assert
            Assert.AreEqual(sut, 1761m);
        }

        [TestMethod]
        public void Should_Return_Correct_InterestRate()
        {
            // Arrange
            var calculator = new MonthlyMortgage
            {
                InterestRatePercent = _interestRateInPercent,
                MortgageAmount = _mortgageAmount,
                Term = _termInYears
            };

            // Act
            var sut = Math.Round(calculator.InterestRate, 4);

            // Assert
            Assert.AreEqual(sut, 0.005m);
        }

        [TestMethod]
        public void Should_Update_MortgageAmount_WhenHousePriceAndDepositSet()
        {
            // Arrange
            var calculator = new MonthlyMortgage
            {
                InterestRatePercent = _interestRateInPercent,
                MortgageAmount = _mortgageAmount,
                Term = _termInYears
            };

            calculator.HousePrice = 300000m;
            calculator.DepositAmount = 20000m;

            // Act
            var sut = Math.Round(calculator.MortgageAmount, 0);

            // Assert
            Assert.AreEqual(sut, 280000m);
        }

        [TestMethod]
        public void Should_Return_Correct_OutstandingMortgageAmount()
        {
            // Arrange
            var calculator = new MonthlyMortgage
            {
                InterestRatePercent = _interestRateInPercent,
                MortgageAmount = _mortgageAmount,
                Term = _termInYears
            };

            // Act
            var year5 = Math.Round(calculator.OutstandingAmountPerYear[5], 0);
            var year15 = Math.Round(calculator.OutstandingAmountPerYear[15], 0);
            var year25 = Math.Round(calculator.OutstandingAmountPerYear[25], 0);
            var year36 = Math.Round(calculator.OutstandingAmountPerYear[36], 0);
            var year40 = Math.Round(calculator.OutstandingAmountPerYear[40], 0);

            // Assert
            Assert.AreEqual(year5, 308767);
            Assert.AreEqual(year15, 273178);
            Assert.AreEqual(year25, 208428);
            Assert.AreEqual(year36, 74488);
            Assert.AreEqual(year40, 0);
        }
    }

}
