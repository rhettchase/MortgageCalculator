using MortgageCalcClass;

namespace NUnitMortgageCalcTests
{
    public class MortgageCalculatorTests
    {
        private LoanDetails _loanDetailsSimple;
        private Calculator _calculatorSimple;

        [SetUp]
        public void Setup()
        {
            // Initialize the LoanDetails and MortgageCalculator for testing
            _loanDetailsSimple = new LoanDetails(300000m, 360, 3.5m); // $300,000 loan, 30 years, 3.5% interest
            _calculatorSimple = new Calculator(_loanDetailsSimple);
        }

        [Test]
        public void CalculateAmortizationSchedule_ShouldReturnCorrectRemainingBalanceAfterFirstMonth()
        {
            // Arrange
            var expectedRemainingBalanceAfterFirstMonth = 299527.87m; // Known correct balance after the first payment

            // Act
            var schedule = _calculatorSimple.CalculateAmortizationSchedule();
            var actualRemainingBalance = schedule[0].RemainingBalance;

            // Assert
 
            Assert.That(Math.Round(actualRemainingBalance, 2), Is.EqualTo(expectedRemainingBalanceAfterFirstMonth), "The remaining balance after the first month is incorrect.");
        }

        [Test]
        public void CalculateAmortizationSchedule_CorrectTotalInterestAmount()
        {
            // Arrange
            var expectedTotalInterestAmount = 184968.26m; // Known correct total interest paid

            // Act
            var schedule = _calculatorSimple.CalculateAmortizationSchedule();
            var actualTotalInterest = schedule.Sum(payment => payment.InterestPayment);

            // Assert

            Assert.That(Math.Round(actualTotalInterest, 2), Is.EqualTo(expectedTotalInterestAmount), "The remaining balance after the first month is incorrect.");
        }

        [Test]
        public void CalculateAmortizationSchedule_CorrectMonthlyPayment()
        {
            // Arrange
            var expectedMonthlyPayment = 1347.13m; // Known correct monthly payment

            // Act
            var schedule = _calculatorSimple.CalculateAmortizationSchedule();
            var actualMonthlyPayment = schedule[0].TotalMonthlyPayment;

            // Assert

            Assert.That(Math.Round(actualMonthlyPayment, 2), Is.EqualTo(expectedMonthlyPayment), "The remaining balance after the first month is incorrect.");
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

    public class MortgageCalculatorWithDownPaymentTests
    {
        private LoanDetails _loanDetailsWithDownPayment;
        private Calculator _calculator;

        [SetUp]
        public void Setup()
        {
            // Initialize the LoanDetails and MortgageCalculator for testing
            _loanDetailsWithDownPayment = new LoanDetails(300000m, 60000m, 360, 3.5m); // $300,000 loan, 60,000 down payment, 30 years, 3.5% interest
            _calculator = new Calculator(_loanDetailsWithDownPayment);
        }

        [Test]
        public void CalculateAmortizationSchedule_ShouldReturnCorrectRemainingBalanceAfterFirstMonth()
        {
            // Arrange
            var expectedRemainingBalanceAfterFirstMonth = 239622.29m; // Known correct balance after the first payment

            // Act
            var schedule = _calculator.CalculateAmortizationSchedule();
            var actualRemainingBalance = schedule[0].RemainingBalance;

            // Assert

            Assert.That(Math.Round(actualRemainingBalance, 2), Is.EqualTo(expectedRemainingBalanceAfterFirstMonth), "The remaining balance after the first month is incorrect.");
        }

        [Test]
        public void CalculateAmortizationSchedule_CorrectTotalInterestAmount()
        {
            // Arrange
            var expectedTotalInterestAmount = 147974.61m; // Known correct total interest paid

            // Act
            var schedule = _calculator.CalculateAmortizationSchedule();
            var actualTotalInterest = schedule.Sum(payment => payment.InterestPayment);

            // Assert

            Assert.That(Math.Round(actualTotalInterest, 2), Is.EqualTo(expectedTotalInterestAmount), "The remaining balance after the first month is incorrect.");
        }

        [Test]
        public void CalculateAmortizationSchedule_CorrectMonthlyPayment()
        {
            // Arrange
            var expectedMonthlyPayment = 1077.71m; // Known correct monthly payment

            // Act
            var schedule = _calculator.CalculateAmortizationSchedule();
            var actualMonthlyPayment = schedule[0].TotalMonthlyPayment;

            // Assert

            Assert.That(Math.Round(actualMonthlyPayment, 2), Is.EqualTo(expectedMonthlyPayment), "The remaining balance after the first month is incorrect.");
        }

        [Test]
        public void LoanDetails_InvalidDownpaymentAmount_ShouldThrowArgumentException()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() => new LoanDetails(5000000m, -5000m, 360, 3.5m), "Negative down payment should throw an exception.");
            Assert.Throws<ArgumentException>(() => new LoanDetails(20000m, 30000m, 360, 3.5m), "Down payment amount > Purchase price should throw an exception.");

        }

    }
    public class LoanDetailsTests
    {
        [Test]
        public void Constructor_WithFixedDownPayment_ShouldCalculateCorrectLoanAmount()
        {
            // Arrange
            decimal purchasePrice = 500000m;
            decimal downPayment = 100000m; // Fixed down payment
            int loanTerm = 360; // 30 years
            decimal interestRate = 3.5m;

            // Act
            var loanDetails = new LoanDetails(purchasePrice, downPayment, loanTerm, interestRate);

            // Assert
            Assert.AreEqual(400000m, loanDetails.LoanAmount, "Loan amount calculated with fixed down payment is incorrect.");
        }

        [Test]
        public void Constructor_WithPercentageDownPayment_ShouldCalculateCorrectLoanAmount()
        {
            // Arrange
            decimal purchasePrice = 500000m;
            decimal downPaymentPercentage = 20m; // 20% down payment
            int loanTerm = 360; // 30 years
            decimal interestRate = 3.5m;

            // Act
            var loanDetails = new LoanDetails(purchasePrice, loanTerm, interestRate, downPaymentPercentage);

            // Assert
            Assert.AreEqual(400000m, loanDetails.LoanAmount, "Loan amount calculated with percentage down payment is incorrect.");
        }

        [Test]
        public void Constructor_WithInvalidDownPaymentAmount_ShouldThrowArgumentException()
        {
            // Arrange
            decimal purchasePrice = 500000m;
            decimal downPayment = 600000m; // Invalid: greater than purchase price
            int loanTerm = 360; // 30 years
            decimal interestRate = 3.5m;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new LoanDetails(purchasePrice, downPayment, loanTerm, interestRate), "Down payment greater than purchase price should throw an exception.");
        }

        [Test]
        public void Constructor_WithInvalidDownPaymentPercentage_ShouldThrowArgumentException()
        {
            // Arrange
            decimal purchasePrice = 500000m;
            decimal downPaymentPercentage = 120m; // Invalid: greater than 100%
            int loanTerm = 360; // 30 years
            decimal interestRate = 3.5m;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new LoanDetails(purchasePrice, loanTerm, interestRate, downPaymentPercentage), "Down payment percentage greater than 100% should throw an exception.");
        }

        [Test]
        public void Constructor_WithZeroDownPayment_ShouldCalculateCorrectLoanAmount()
        {
            // Arrange
            decimal purchasePrice = 500000m;
            decimal downPayment = 0m; // No down payment
            int loanTerm = 360; // 30 years
            decimal interestRate = 3.5m;

            // Act
            var loanDetails = new LoanDetails(purchasePrice, downPayment, loanTerm, interestRate);

            // Assert
            Assert.AreEqual(500000m, loanDetails.LoanAmount, "Loan amount calculated with zero down payment is incorrect.");
        }
    }
}