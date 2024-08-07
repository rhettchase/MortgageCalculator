namespace MortgageCalcClass
{
    public class Calculator
    {
        public LoanDetails LoanDetails { get; }

        // constructor
        public Calculator(LoanDetails loanDetails)
        {
            LoanDetails = loanDetails;
        }

        public decimal CalculateTotalMonthlyPayment()
        {
            // converts annual percentage rate into monthly interest rate as decimal
            decimal monthlyInterestRate = LoanDetails.AnnualInterestRate / 1200;

            // discount factor
            decimal factor = (decimal)Math.Pow(1 + (double)monthlyInterestRate, -LoanDetails.LoanTermInMonths);

            return LoanDetails.LoanAmount * monthlyInterestRate / (1 - factor);
        }

        public List<MonthlyPaymentResult> CalculateAmortizationSchedule()
        {
            // creates empty list to hold MonthlyPaymentResults
            var schedule = new List<MonthlyPaymentResult>();
            decimal remainingBalance = LoanDetails.LoanAmount; // initial balance is the loan amount
            decimal totalMonthlyPayment = CalculateTotalMonthlyPayment();
            decimal monthlyInterestRate = LoanDetails.AnnualInterestRate / 1200;
            decimal runningTotalInterest = 0;

            // for each month through loan term:
            // calculate interestPayment: Interest Payment = Previous Remaining Balance * Monthly Interest Rate
            // calculate principalPayment: Principal Payment  = Total Monthly Payment − Interest Payment
            // update remainingBalance by subtracting principalPayment
            // add to schedule
            for (int month = 1; month <= LoanDetails.LoanTermInMonths; month++)
            {
                decimal interestPayment = remainingBalance * monthlyInterestRate;
                decimal principalPayment = totalMonthlyPayment - interestPayment;
                runningTotalInterest += interestPayment;
                remainingBalance -= principalPayment;
                schedule.Add(new MonthlyPaymentResult(month, totalMonthlyPayment, interestPayment, principalPayment, runningTotalInterest, remainingBalance));
            }
            return schedule;
        }
    }
}
