# Mortgage Calculator

## Description

Mortgage calculator is a simple tool that allows you to calculate the monthly payment for a mortgage based on the loan amount (based on purchase price and down payment), interest rate, and loan term.

## Classes

### `LoanDetails` class holds loan data

- This class holds the user inputs and provide methods for validation and manipulation of the data.

### `MortgageCalculator` handles calculations

- performs calculations to determine the total monthly payment, interest payment, principal payment, and remaining balance for each month.
- For each month in the loan term, `MortgageCalculator` creates an instance of `MonthlyPaymentResult`, passing the calculated values to its constructor.
- returns a list of `MonthlyPaymentResult` objects, each representing the payment details for a single month. This list constitutes the amortization schedule, showing how the loan balance decreases over time.

### `MonthlyPaymentResult` encapsulates the result of calculations

- data container for the results of a single month's mortgage payment calculation

## Formula and Components

The formula used to calculate the monthly mortgage payment is:

```
Monthly Payment = Loan Amount * (rate/1200) / (1 - (1 + rate/1200)^-Number of Months)
```

### Explanation of Formula Components

1. **Monthly Interest Rate**:
    - rate/1200: Rate​ converts the annual percentage rate (APR) into a monthly decimal rate.
2. **Discount Factor**:
    - The expression (1-(1+ rate/1200)^(-number of months)) calculates the discount factor, which considers the effect of compound interest over time. This factor ensures that the payment schedule accounts for interest accruing on the remaining balance each month.
3. **Monthly Payment Calculation**:
    - The formula calculates the fixed monthly payment required to pay off the loan over the specified term at the given interest rate.

### Additional Components

1. **Remaining Balance Before the First Month**:
    - Initially, the remaining balance is equal to the loan amount because no payments have been made yet.
2. **Interest Payment**
	- Interest Payment = Previous Remaining Balance * (Rate/1200)
    - This formula calculates the interest component of the monthly payment based on the remaining balance at the start of the month.
4. **Principal Payment**
	- Principal Payment = Total Monthly Payment−Interest Payment
    - This determines how much of the monthly payment goes towards reducing the loan principal.
6. **Remaining Balance**
	- Remaining Balance = Previous Remaining Balance − Principal Payment
    - This updates the remaining balance after each payment, decreasing it by the amount of the principal payment.

    ## Usage

    Run the app by setting the `MortgageCalculatorRazorApp` as the startup project and pressing `F5` in Visual Studio.

    User enters the loan details, and the app calculates the monthly payment and displays the amortization schedule.