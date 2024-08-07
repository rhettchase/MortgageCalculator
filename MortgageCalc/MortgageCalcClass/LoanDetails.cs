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
        public decimal PurchasePrice { get; private set; }
        public decimal DownPayment { get; private set; }

        // constructor using loan amount
        public LoanDetails(decimal loanAmount, int loanTermInMonths, decimal annualInterestRate)
        {
            ValidateLoanAmount(loanAmount);
            ValidateLoanTerm(loanTermInMonths);
            ValidateInterestRate(annualInterestRate);

            LoanAmount = loanAmount;
            LoanTermInMonths = loanTermInMonths;
            AnnualInterestRate = annualInterestRate;
        }

        // constructor using purchase price and down payment
        public LoanDetails(decimal purchasePrice, decimal downPayment, int loanTermInMonths, decimal annualInterestRate)
        {
            ValidatePurchasePrice(purchasePrice);
            ValidateDownPayment(downPayment, purchasePrice);
            ValidateLoanTerm(loanTermInMonths);
            ValidateInterestRate(annualInterestRate);

            PurchasePrice = purchasePrice;
            DownPayment = downPayment;
            LoanAmount = CalculateLoanAmount(purchasePrice, downPayment);
            LoanTermInMonths = loanTermInMonths;
            AnnualInterestRate = annualInterestRate;
        }

        // New constructor using purchase price and down payment as a percentage
        public LoanDetails(decimal purchasePrice, int loanTermInMonths, decimal annualInterestRate, decimal downPaymentPercentage)
        {
            ValidatePurchasePrice(purchasePrice);
            ValidateLoanTerm(loanTermInMonths);
            ValidateInterestRate(annualInterestRate);
            ValidateDownPaymentPercentage(downPaymentPercentage);

            PurchasePrice = purchasePrice;
            DownPayment = CalculateDownPaymentFromPercentage(purchasePrice, downPaymentPercentage);
            LoanAmount = CalculateLoanAmount(purchasePrice, DownPayment);
            LoanTermInMonths = loanTermInMonths;
            AnnualInterestRate = annualInterestRate;
        }

        // Method to calculate loan amount based on purchase price and down payment
        private decimal CalculateLoanAmount(decimal purchasePrice, decimal downPayment)
        {
            return purchasePrice - downPayment;
        }

        // Method to calculate down payment from a percentage
        private decimal CalculateDownPaymentFromPercentage(decimal purchasePrice, decimal percentage)
        {
            return purchasePrice * (percentage / 100);
        }

        private void ValidateLoanAmount(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Loan amount must be greater than zero.");
        }

        private void ValidatePurchasePrice(decimal price)
        {
            if (price <= 0)
                throw new ArgumentException("Purchase price must be greater than zero.");
        }

        private void ValidateDownPayment(decimal downPayment, decimal purchasePrice)
        {
            if (downPayment < 0 || downPayment >= purchasePrice)
                throw new ArgumentException("Down payment must be non-negative and less than the purchase price.");
        }

        private void ValidateDownPaymentPercentage(decimal percentage)
        {
            if (percentage < 0 || percentage > 100)
                throw new ArgumentException("Down payment percentage must be between 0 and 100.");
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
