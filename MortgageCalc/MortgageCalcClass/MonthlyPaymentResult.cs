using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MortgageCalcClass
{
    public class MonthlyPaymentResult
    {
        public int Month { get; set; }
        public decimal TotalMonthlyPayment { get; set; }
        public decimal InterestPayment { get; set; }
        public decimal PrincipalPayment { get; set; }
        public decimal RunningTotalInterest { get; set; }
        public decimal RemainingBalance { get; set; }

        public MonthlyPaymentResult(int month, decimal totalMonthlyPayment, decimal interestPayment, decimal principalPayment, decimal runningTotalInterest, decimal remainingBalance)
        {
            Month = month;
            TotalMonthlyPayment = totalMonthlyPayment;
            InterestPayment = interestPayment;
            PrincipalPayment = principalPayment;
            RunningTotalInterest = runningTotalInterest;
            RemainingBalance = remainingBalance;
        }
    }
}
