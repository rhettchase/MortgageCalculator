using Spectre.Console;
using MortgageCalcClass;
using System.Text.Json;

public static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            switch (args[0])
            {
                case "--interactive":
                    RunInteractiveMode();
                    break;
                case "--batch":
                    if (args.Length > 1)
                    {
                        RunBatchMode(args[1]); // Pass the JSON string as an argument
                    }
                    else
                    {
                        Console.WriteLine("Please provide batch data as a JSON string.");
                    }
                    break;
                default:
                    RunStandardMode();
                    break;
            }
        }
        else
        {
            RunStandardMode();
        }
    }
    private static void RunStandardMode()
    {
        AnsiConsole.Write(new FigletText("Mortgage Calculator").Centered().Color(Color.Aqua));

        try
        {
            // Get and validate user inputs using the LoanDetails validation methods
            decimal purchasePrice = GetValidatedInput<decimal>("Enter the [green]purchase price[/] of the house:", (input, errors) => LoanDetails.ValidatePurchasePrice(input, errors));
            decimal downPayment = GetValidatedInput<decimal>("Enter the [green]down payment[/] amount:", (input, errors) => LoanDetails.ValidateDownPayment(input, purchasePrice, errors));
            int loanTermYears = GetValidatedInput<int>("Enter the [green]loan term[/] in years:", (input, errors) => LoanDetails.ValidateLoanTerm(input, errors));
            decimal interestRate = GetValidatedInput<decimal>("Enter the [green]interest rate[/] (%):", (input, errors) => LoanDetails.ValidateInterestRate(input, errors));

            // Convert years to months
            int loanTermInMonths = loanTermYears * 12; 

            // Create LoanDetails and Calculator
            var loanDetails = new LoanDetails(purchasePrice, downPayment, loanTermInMonths, interestRate);
            var calculator = new Calculator(loanDetails);

            // Calculate monthly payment and amortization schedule
            var monthlyPayment = calculator.CalculateTotalMonthlyPayment();
            var schedule = calculator.CalculateAmortizationSchedule();

            // Display results
            AnsiConsole.MarkupLine($"[bold yellow]Total Monthly Payment:[/] [bold green]{monthlyPayment:C}[/]");
            AnsiConsole.MarkupLine($"[bold yellow]Total Interest Paid:[/] [bold green]{schedule.Sum(payment => payment.InterestPayment):C}[/]");

            // Create and display the amortization table
            var table = new Table()
               .Border(TableBorder.Rounded)
               .BorderColor(Color.SpringGreen4);

            table.AddColumn(new TableColumn("Month").Centered());
            table.AddColumn(new TableColumn("Payment Amount").RightAligned());
            table.AddColumn(new TableColumn("Principal Payment").RightAligned());
            table.AddColumn(new TableColumn("Interest Payment").RightAligned());
            table.AddColumn(new TableColumn("Total Interest").RightAligned());
            table.AddColumn(new TableColumn("Remaining Balance").RightAligned());


            foreach (var payment in schedule)
            {
                table.AddRow(
                   payment.Month.ToString(),
                   Markup.Escape(payment.TotalMonthlyPayment.ToString("C")),
                   Markup.Escape(payment.PrincipalPayment.ToString("C")),
                   Markup.Escape(payment.InterestPayment.ToString("C")),
                   Markup.Escape(payment.RunningTotalInterest.ToString("C")),
                   Markup.Escape(payment.RemainingBalance.ToString("C"))
               );
            }

            AnsiConsole.Write(table);
        }
        catch (ArgumentException ex)
        {
            AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]An unexpected error occurred: {ex.Message}[/]");
        }
    }

    private static void RunInteractiveMode()
    {
        AnsiConsole.Write(new FigletText("Interactive Mortgage Calculator").Centered().Color(Color.Aqua));
        AnsiConsole.MarkupLine("[bold yellow]Interactive mode activated.[/]");

        bool continueCalculating = true;

        while (continueCalculating)
        {
            try
            {
                // Get and validate user inputs using the LoanDetails validation methods
                decimal purchasePrice = GetValidatedInput<decimal>("Enter the [green]purchase price[/] of the house:", (input, errors) => LoanDetails.ValidatePurchasePrice(input, errors));
                decimal downPayment = GetValidatedInput<decimal>("Enter the [green]down payment[/] amount:", (input, errors) => LoanDetails.ValidateDownPayment(input, purchasePrice, errors));
                int loanTermYears = GetValidatedInput<int>("Enter the [green]loan term[/] in years:", (input, errors) => LoanDetails.ValidateLoanTerm(input, errors));
                decimal interestRate = GetValidatedInput<decimal>("Enter the [green]interest rate[/] (%):", (input, errors) => LoanDetails.ValidateInterestRate(input, errors));

                // Convert years to months
                int loanTermInMonths = loanTermYears * 12;

                // Create LoanDetails and Calculator
                var loanDetails = new LoanDetails(purchasePrice, downPayment, loanTermInMonths, interestRate);
                var calculator = new Calculator(loanDetails);

                // Calculate monthly payment and amortization schedule
                var monthlyPayment = calculator.CalculateTotalMonthlyPayment();
                var schedule = calculator.CalculateAmortizationSchedule();

                // Display results
                AnsiConsole.MarkupLine($"[bold yellow]Total Monthly Payment:[/] [bold green]{monthlyPayment:C}[/]");
                AnsiConsole.MarkupLine($"[bold yellow]Total Interest Paid:[/] [bold green]{schedule.Sum(payment => payment.InterestPayment):C}[/]");

                // Create and display the amortization table
                var table = new Table()
                   .Border(TableBorder.Rounded)
                   .BorderColor(Color.SpringGreen4);

                table.AddColumn(new TableColumn("Month").Centered());
                table.AddColumn(new TableColumn("Payment Amount").RightAligned());
                table.AddColumn(new TableColumn("Principal Payment").RightAligned());
                table.AddColumn(new TableColumn("Interest Payment").RightAligned());
                table.AddColumn(new TableColumn("Total Interest").RightAligned());
                table.AddColumn(new TableColumn("Remaining Balance").RightAligned());

                foreach (var payment in schedule)
                {
                    table.AddRow(
                       payment.Month.ToString(),
                       Markup.Escape(payment.TotalMonthlyPayment.ToString("C")),
                       Markup.Escape(payment.PrincipalPayment.ToString("C")),
                       Markup.Escape(payment.InterestPayment.ToString("C")),
                       Markup.Escape(payment.RunningTotalInterest.ToString("C")),
                       Markup.Escape(payment.RemainingBalance.ToString("C"))
                   );
                }

                AnsiConsole.Write(table);
            }
            catch (ArgumentException ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]An unexpected error occurred: {ex.Message}[/]");
            }

            // Ask if the user wants to calculate again
            continueCalculating = AnsiConsole.Confirm("[green]Do you want to calculate another mortgage?[/]");
        }
    }

    private static void RunBatchMode(string jsonData)
    {
        AnsiConsole.Write(new FigletText("Batch Mortgage Calculator").Centered().Color(Color.Aqua));
        AnsiConsole.MarkupLine("[bold yellow]Batch mode activated.[/]");

        try
        {
            var loanDetailsList = JsonSerializer.Deserialize<List<LoanDetails>>(jsonData);

            foreach (var loanDetails in loanDetailsList)
            {
                // Debugging output to check values before processing
                Console.WriteLine($"Processing loan: PurchasePrice={loanDetails.PurchasePrice}, DownPayment={loanDetails.DownPayment}, LoanTermInMonths={loanDetails.LoanTermInMonths}, InterestRate={loanDetails.AnnualInterestRate}");
                var calculator = new Calculator(loanDetails);

                var monthlyPayment = calculator.CalculateTotalMonthlyPayment();
                var schedule = calculator.CalculateAmortizationSchedule();

                var loanInfo = $"Loan for {loanDetails.PurchasePrice:C} with {loanDetails.DownPayment:C} down, {loanDetails.LoanTermInMonths / 12} years at {loanDetails.AnnualInterestRate}% interest:";
                AnsiConsole.MarkupLine($"[bold yellow]{Markup.Escape(loanInfo)}[/]");
                AnsiConsole.MarkupLine($"[bold yellow]Total Monthly Payment:[/] [bold green]{monthlyPayment:C}[/]");
                AnsiConsole.MarkupLine($"[bold yellow]Total Interest Paid:[/] [bold green]{schedule.Sum(payment => payment.InterestPayment):C}[/]");

                var table = new Table().Border(TableBorder.Rounded).BorderColor(Color.SpringGreen4);

                table.AddColumn(new TableColumn("Month").Centered());
                table.AddColumn(new TableColumn("Payment Amount").RightAligned());
                table.AddColumn(new TableColumn("Principal Payment").RightAligned());
                table.AddColumn(new TableColumn("Interest Payment").RightAligned());
                table.AddColumn(new TableColumn("Total Interest").RightAligned());
                table.AddColumn(new TableColumn("Remaining Balance").RightAligned());

                foreach (var payment in schedule)
                {
                    table.AddRow(
                        Markup.Escape(payment.Month.ToString()),
                        Markup.Escape(payment.TotalMonthlyPayment.ToString("C")),
                        Markup.Escape(payment.PrincipalPayment.ToString("C")),
                        Markup.Escape(payment.InterestPayment.ToString("C")),
                        Markup.Escape(payment.RunningTotalInterest.ToString("C")),
                        Markup.Escape(payment.RemainingBalance.ToString("C"))
                    );
                }

                AnsiConsole.Write(table);
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]An unexpected error occurred: {Markup.Escape(ex.Message)}[/]");
        }
    }



    private static T GetValidatedInput<T>(string prompt, Action<T, List<string>> validate)
    {
        while (true)
        {
            try
            {
                var input = AnsiConsole.Ask<T>(prompt);
                var errors = new List<string>();
                validate(input, errors);
                if (errors.Count == 0)
                {
                    return input;
                }
                AnsiConsole.MarkupLine($"[red]{string.Join("\n", errors)}[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Invalid input: {ex.Message}[/]");
            }
        }
    }

    private static void ValidatePurchasePrice(decimal price, List<string> errors)
    {
        LoanDetails.ValidatePurchasePrice(price, errors);
    }

    private static void ValidateDownPayment(decimal downPayment, List<string> errors, decimal purchasePrice)
    {
        LoanDetails.ValidateDownPayment(downPayment, purchasePrice, errors);
    }

    private static void ValidateLoanTerm(int term, List<string> errors)
    {
        LoanDetails.ValidateLoanTerm(term, errors);
    }

    private static void ValidateInterestRate(decimal rate, List<string> errors)
    {
        LoanDetails.ValidateInterestRate(rate, errors);
    }
}
        

    

    
