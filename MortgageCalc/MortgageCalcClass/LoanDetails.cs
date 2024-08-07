using System;
using System.Collections.Generic;

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
            var errors = new List<string>();
            ValidateLoanAmount(loanAmount, errors);
            ValidateLoanTerm(loanTermInMonths, errors);
            ValidateInterestRate(annualInterestRate, errors);

            ThrowIfErrors(errors);

            LoanAmount = loanAmount;
            LoanTermInMonths = loanTermInMonths;
            AnnualInterestRate = annualInterestRate;
        }

        // constructor using purchase price and down payment
        public LoanDetails(decimal purchasePrice, decimal downPayment, int loanTermInMonths, decimal annualInterestRate)
        {
            Console.WriteLine($"Debug: PurchasePrice={purchasePrice}, DownPayment={downPayment}, LoanTermInMonths={loanTermInMonths}, AnnualInterestRate={annualInterestRate}");

            var errors = new List<string>();
            ValidatePurchasePrice(purchasePrice, errors);
            ValidateDownPayment(downPayment, purchasePrice, errors);
            ValidateLoanTerm(loanTermInMonths, errors);
            ValidateInterestRate(annualInterestRate, errors);

            ThrowIfErrors(errors);

            PurchasePrice = purchasePrice;
            DownPayment = downPayment;
            LoanAmount = CalculateLoanAmount(purchasePrice, downPayment);
            LoanTermInMonths = loanTermInMonths;
            AnnualInterestRate = annualInterestRate;
        }

        // New constructor using purchase price and down payment as a percentage
        public LoanDetails(decimal purchasePrice, int loanTermInMonths, decimal annualInterestRate, decimal downPaymentPercentage)
        {
            var errors = new List<string>();
            ValidatePurchasePrice(purchasePrice, errors);
            ValidateLoanTerm(loanTermInMonths, errors);
            ValidateInterestRate(annualInterestRate, errors);
            ValidateDownPaymentPercentage(downPaymentPercentage, errors);

            ThrowIfErrors(errors);

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

        private void ValidateLoanAmount(decimal amount, List<string> errors)
        {
            if (amount <= 0)
                errors.Add("Loan amount must be greater than zero.");
        }

        private void ValidatePurchasePrice(decimal price, List<string> errors)
        {
            if (price <= 0)
                errors.Add("Purchase price must be greater than zero.");
        }

        private void ValidateDownPayment(decimal downPayment, decimal purchasePrice, List<string> errors)
        {
            if (downPayment < 0 || downPayment >= purchasePrice)
                errors.Add("Down payment must be non-negative and less than the purchase price.");
        }

        private void ValidateDownPaymentPercentage(decimal percentage, List<string> errors)
        {
            if (percentage < 0 || percentage > 100)
                errors.Add("Down payment percentage must be between 0 and 100.");
        }

        private void ValidateLoanTerm(int term, List<string> errors)
        {
            if (term <= 0)
                errors.Add("Loan term must be greater than zero.");
        }

        private void ValidateInterestRate(decimal rate, List<string> errors)
        {
            if (rate < 0 || rate > 100)
                errors.Add("Interest rate must be between 0 and 100.");
        }

        private void ThrowIfErrors(List<string> errors)
        {
            if (errors.Count > 0)
                throw new ArgumentException(string.Join("\n", errors));
        }
    }
}
