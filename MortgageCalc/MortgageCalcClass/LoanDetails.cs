using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalcClass
{
    public class LoanDetails
    {
        // Properties
        public decimal LoanAmount { get; private set; }
        public decimal AnnualInterestRate { get; private set; }
        public int LoanTermInMonths { get; private set; }

        // constructor

        public LoanDetails(decimal loanAmount, int loanTermInMonths, decimal annualInterestRate)
        {
            ValidateLoanAmount(loanAmount);
            ValidateLoanTerm(loanTermInMonths);
            ValidateInterestRate(annualInterestRate);

            LoanAmount = loanAmount;
            LoanTermInMonths = loanTermInMonths;
            AnnualInterestRate = annualInterestRate;
        }

        private void ValidateLoanAmount(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Loan amount must be greater than zero.");
        }

        private void ValidateLoanTerm(int term)
        {
            if (term <= 0)
                throw new ArgumentException("Loan term must be greater than zero.");
        }

        private void ValidateInterestRate(decimal rate)
        {
            if (rate < 0 || rate > 100)
                throw new ArgumentException("Interest rate must be between 0 and 100.");
        }
    }
}
