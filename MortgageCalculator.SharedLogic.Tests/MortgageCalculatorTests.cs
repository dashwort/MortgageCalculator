using MortgageCalculator.SharedLogic.Calculators;


namespace MortgageCalculator.SharedLogic.Tests
{
    [TestClass]
    public class MortgageCalculatorTests
    {
        [TestMethod]
        [DataRow(320000, 40.00, 6.00, 1600.00)]
        public void Should_Return_Correct_MonthlyPayment_When_InterestOnlyMortgage(double mortgageAmount, int termInYears, double interestRateInPercent, double expectedResult)
        {
            // Arrange
            var calculator = new MonthlyMortgage
            {
                InterestRatePercent = interestRateInPercent,
                MortgageAmount = mortgageAmount,
                Term = termInYears
            };

            // Act
            var sut = Math.Round(calculator.InterestOnlyMonthlyRepayment, 2);

            // Assert
            Assert.AreEqual(sut, expectedResult);
        }

        [TestMethod]
        [DataRow(320000, 40.00, 6.00, 1760.68)]
        public void Should_Return_Correct_MonthlyPayment_When_RepaymentMortgage(double mortgageAmount, int termInYears, double interestRateInPercent, double expectedResult)
        {
            // Arrange
            var calculator = new MonthlyMortgage
            {
                InterestRatePercent = interestRateInPercent,
                MortgageAmount = mortgageAmount,
                Term = termInYears
            };

            // Act
            var sut = Math.Round(calculator.MonthlyRepayment, 2);

            // Assert
            Assert.AreEqual(sut, expectedResult);
        }
    }
}