using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalcClass
{
    public class MonthlyPaymentResult
    {
        public decimal TotalMonthlyPayment { get; }
        public decimal InterestPayment { get; }
        public decimal PrincipalPayment { get; }
        public decimal RemainingBalance { get; }

        public MonthlyPaymentResult(decimal totalMonthlyPayment, decimal interestPayment, decimal principalPayment, decimal remainingBalance)
        {
            TotalMonthlyPayment = totalMonthlyPayment;
            InterestPayment = interestPayment;
            PrincipalPayment = principalPayment;
            RemainingBalance = remainingBalance;
        }
    }
}
