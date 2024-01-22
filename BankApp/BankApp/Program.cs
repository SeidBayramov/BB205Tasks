using BankApp.Enum;
using BankApp.Interfeys;
using System;

class Program
{
    static void Main(string[] args)
    {
        Bank bank = new Bank();

        Console.WriteLine("Welcome to the Bank!");

        while (true)
        {
            try
            {
                Console.WriteLine("1. Create an Account");
                Console.WriteLine("2. Deposit Money");
                Console.WriteLine("3. Withdraw Money");
                Console.WriteLine("4. List Transactions");
                Console.WriteLine("5. List All Accounts");
                Console.WriteLine("6. Transfer between accounts");
                Console.WriteLine("7. Currency Conversion");
                Console.WriteLine("8. EXIT");

                Console.Write("Enter the operation number: ");
                int choice = int.Parse(Console.ReadLine());

                switch ((Operation)choice)
                {
                    case Operation.CreateAccount:
                        while (true)
                        {
                            Console.Write("Select account type (1 - Checking, 2 - Savings, 3 - Business): ");
                            if (int.TryParse(Console.ReadLine(), out int accountTypeChoice))
                            {
                                if (Enum.IsDefined(typeof(AccountType), accountTypeChoice))
                                {
                                    AccountType accountType = (AccountType)accountTypeChoice;

                                    Console.Write("Select currency type (1 - USD, 2 - AZN, 3 - EUR): ");
                                    CurrencyType currencyType = (CurrencyType)int.Parse(Console.ReadLine());

                                    if (accountType == AccountType.Savings && currencyType != CurrencyType.USD)
                                    {
                                        Console.WriteLine("Savings accounts are only available in USD. Please select a different currency.");
                                    }
                                    else
                                    {
                                        IAccount newAccount = bank.CreateAccount(accountType, currencyType);
                                        Console.WriteLine($"New account created. Account ID: {newAccount.AccountId}");
                                        break; // Exit the loop when a valid account is created
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid account type. Please select a valid account type.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a valid number for account type.");
                            }
                        }
                        break;



                    case Operation.DepositMoney:
                        Console.Write("Enter the account ID: ");
                        int depositAccountId = int.Parse(Console.ReadLine());

                        Console.Write("Enter the amount to deposit: ");
                        decimal depositAmount = decimal.Parse(Console.ReadLine());

                        bank.DepositMoney(depositAccountId, depositAmount);

                        Console.WriteLine("Money successfully deposited.");
                        break;

                    case Operation.WithdrawMoney:
                        Console.Write("Enter the account ID: ");
                        int withdrawAccountId = int.Parse(Console.ReadLine());

                        Console.Write("Enter the amount to withdraw: ");
                        decimal withdrawAmount = decimal.Parse(Console.ReadLine());

                        bank.WithdrawMoney(withdrawAccountId, withdrawAmount);

                        break;

                    case Operation.TransferMoney:
                        Console.Write("Enter the source account ID: ");
                        int fromAccountId = int.Parse(Console.ReadLine());

                        Console.Write("Enter the target account ID: ");
                        int toAccountId = int.Parse(Console.ReadLine());

                        Console.Write("Enter the amount to transfer: ");
                        decimal transferAmount = decimal.Parse(Console.ReadLine());

                        bank.TransferMoney(fromAccountId, toAccountId, transferAmount);
                        break;

                    case Operation.ListTransactions:
                        Console.Write("Enter the account ID: ");
                        int listTransactionsAccountId = int.Parse(Console.ReadLine());

                        bank.ListTransactions(listTransactionsAccountId);
                        break;

                    case Operation.ListAccounts:
                        var allAccounts = bank.GetAllAccounts();
                        Console.WriteLine("List of all accounts:");
                        foreach (IAccount account in allAccounts)
                        {
                            Console.WriteLine($"Account ID: {account.AccountId}, Balance: {account.Balance} {account.CurrencyType}");
                        }
                        break;

                    case Operation.CurrencyConversion:
                        Console.Write("Enter the source account ID: ");
                        int sourceAccountId = int.Parse(Console.ReadLine());


                        Console.Write("Enter the target currency (1 - USD, 2 - AZN, 3 - EUR): ");
                        CurrencyType targetCurrency = (CurrencyType)int.Parse(Console.ReadLine());

                        decimal convertedBalance = bank.CurrencyConversion(sourceAccountId, targetCurrency);
                        Console.WriteLine($"Converted balance: {convertedBalance} {targetCurrency}");
                        break;

                    case Operation.Exit:
                        Console.WriteLine("Exiting the program.");
                        return;

                    default:
                        Console.WriteLine("You entered an incorrect operation number.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
