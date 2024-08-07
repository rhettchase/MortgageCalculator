using MortgageCalcClass;

namespace NUnitMortgageCalcTests
{
    public class MortgageCalculatorTests
    {
        private LoanDetails _loanDetails;
        private MortgageCalculator _calculator;

        [SetUp]
        public void Setup()
        {
            // Initialize the LoanDetails and MortgageCalculator for testing
            _loanDetails = new LoanDetails(300000m, 360, 3.5m); // $300,000 loan, 30 years, 3.5% interest
            _calculator = new MortgageCalculator(_loanDetails);
        }

        [Test]
        public void CalculateAmortizationSchedule_ShouldReturnCorrectRemainingBalanceAfterFirstMonth()
        {
            // Arrange
            var expectedRemainingBalanceAfterFirstMonth = 299527.87m; // Known correct balance after the first payment

            // Act
            var schedule = _calculator.CalculateAmortizationSchedule();
            var actualRemainingBalance = schedule[0].RemainingBalance;

            // Assert
 
            Assert.That(Math.Round(actualRemainingBalance, 2), Is.EqualTo(expectedRemainingBalanceAfterFirstMonth), "The remaining balance after the first month is incorrect.");
        }

        [Test]
        public void LoanDetails_InvalidLoanAmount_ShouldThrowArgumentException()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() => new LoanDetails(-5000m, 360, 3.5m), "Negative loan amount should throw an exception.");
            Assert.Throws<ArgumentException>(() => new LoanDetails(0m, 360, 3.5m), "Zero loan amount should throw an exception.");

        }

        [Test]
        public void LoanDetails_InvalidInterestRate_ShouldThrowArgumentException()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() => new LoanDetails(300000m, 360, -1m), "Negative interest rate should throw an exception.");
        }

       
    }
}