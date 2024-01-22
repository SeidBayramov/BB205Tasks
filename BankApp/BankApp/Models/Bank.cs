using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BankApp.Enum;
using BankApp.Exceptionlar;
using BankApp.Interfeys;

public class Bank
{
  
    private List<IAccount> accounts;
    private List<List<Transaction>> accountTransactions;

    public Bank()
    {
        accounts = new List<IAccount>();
        accountTransactions = new List<List<Transaction>>();
    }

    public IAccount CreateAccount(AccountType accountType, CurrencyType currencyType)
    {
        IAccount newAccount = new BankAccount(accountType, currencyType);
        accounts.Add(newAccount);
        accountTransactions.Add(new List<Transaction>());
        return newAccount;
    }


    public void DepositMoney(int accountId, decimal amount)
    {
        IAccount account = FindAccountById(accountId);
        if (account == null)
        {
            throw new AccountNotFoundException("Account not found");
        }

        account.Deposit(amount);

        RecordTransaction(account, TransactionType.Deposit, amount);
    }

    public void WithdrawMoney(int accountId, decimal amount)
    {
        IAccount account = FindAccountById(accountId);
        if (account == null)
        {
            throw new AccountNotFoundException("Account not found");
        }

        try
        {
            account.Withdraw(amount);

            RecordTransaction(account, TransactionType.Withdraw, amount);
        }
        catch (InvalidAmountException)
        {
            Console.WriteLine("The amount you entered should be greater than zero and less than the balance.");
        }
    }
    public void TransferMoney(int fromAccountId, int toAccountId, decimal amount)
    {
        IAccount fromAccount = FindAccountById(fromAccountId);
        IAccount toAccount = FindAccountById(toAccountId);

        if (fromAccount == null || toAccount == null)
        {
            throw new AccountNotFoundException("One or both accounts not found");
        }

        if (fromAccount.CurrencyType == toAccount.CurrencyType)
        {
            try
            {
                fromAccount.Withdraw(amount);
                toAccount.Deposit(amount);

                RecordTransaction(fromAccount, TransactionType.Transfer, -amount);
                RecordTransaction(toAccount, TransactionType.Transfer, amount);

                Console.WriteLine("Money successfully transferred.");
                Console.WriteLine($"Remaining balance in source account: {fromAccount.Balance} {fromAccount.CurrencyType}");
                Console.WriteLine($"Balance in target account: {toAccount.Balance} {toAccount.CurrencyType}");
            }
            catch (InvalidAmountException)
            {
                Console.WriteLine("The amount you entered should be greater than zero and less than the balance.");
            }
        }
        else
        {
            decimal exchangeRate = GetExchangeRate(fromAccount.CurrencyType, toAccount.CurrencyType);
            if (exchangeRate > 0)
            {
                decimal convertedAmount = amount * exchangeRate;

                try
                {
                    fromAccount.Withdraw(amount);
                    toAccount.Deposit(convertedAmount);

                    RecordTransaction(fromAccount, TransactionType.Transfer, -amount);
                    RecordTransaction(toAccount, TransactionType.Transfer, convertedAmount);

                    Console.WriteLine("Money successfully transferred with currency conversion.");
                    Console.WriteLine($"Remaining balance in source account: {fromAccount.Balance} {fromAccount.CurrencyType}");
                    Console.WriteLine($"Balance in target account: {toAccount.Balance} {toAccount.CurrencyType}");
                }
                catch (InvalidAmountException)
                {
                    Console.WriteLine("The amount you entered should be greater than zero and less than the balance.");
                }
            }
            else
            {
                Console.WriteLine("Currency conversion rate not available for this currency pair.");
            }
        }
    }


    public List<IAccount> GetAllAccounts()
    {
        return accounts;
    }

    public void ListTransactions(int accountId)
    {
        IAccount account = FindAccountById(accountId);

        if (account == null)
        {
            throw new AccountNotFoundException("Account not found");
        }

        int index = accounts.IndexOf(account);

        if (index >= 0)
        {
            List<Transaction> transactions = accountTransactions[index];
            Console.WriteLine("List of transactions:");
            foreach (Transaction transaction in transactions)
            {
                Console.WriteLine($"{transaction.TransactionId}: {transaction.TransactionType} - {transaction.Amount} {account.CurrencyType} - {transaction.TransactionDate}");
            }
        }
    }

    public decimal CurrencyConversion(int accountId, CurrencyType targetCurrency)
    {
        IAccount account = FindAccountById(accountId);

        if (account == null)
        {
            throw new AccountNotFoundException("Account not found");
        }

        decimal conversionRate = GetExchangeRate(account.CurrencyType, targetCurrency);

        if (conversionRate > 0)
        {
            decimal convertedBalance = account.Balance * conversionRate;

            Console.WriteLine($"Current balance: {account.Balance} {account.CurrencyType}");
            Console.WriteLine($"Converted balance to {targetCurrency}: {convertedBalance} {targetCurrency}");
            return convertedBalance;
        }
        else
        {
            Console.WriteLine("Currency conversion is not supported for the selected currencies.");
            return account.Balance;
        }
    }



    public static decimal GetExchangeRate(CurrencyType sourceCurrency, CurrencyType targetCurrency)
    {


        if (sourceCurrency == CurrencyType.USD && targetCurrency == CurrencyType.AZN)
        {
            return 1.7m;
        }
        else if (sourceCurrency == CurrencyType.USD && targetCurrency == CurrencyType.EUR)
        {
            return 0.85m;
        }
        else if (sourceCurrency == CurrencyType.AZN && targetCurrency == CurrencyType.USD)
        {
            return 0.59m;
        }
        else if (sourceCurrency == CurrencyType.AZN && targetCurrency == CurrencyType.EUR)
        {
            return 0.5m;
        }
        else if (sourceCurrency == CurrencyType.EUR && targetCurrency == CurrencyType.USD)
        {
            return 1.18m;
        }
        else if (sourceCurrency == CurrencyType.EUR && targetCurrency == CurrencyType.AZN)
        {
            return 2.0m;
        }
        else
        {
            return -1;
        }
    }

    private IAccount FindAccountById(int accountId)
    {
        return accounts.Find(a => a.AccountId == accountId);
    }


    private void RecordTransaction(IAccount account, TransactionType transactionType, decimal amount)
    {
        int index = accounts.IndexOf(account);
        if (index >= 0)
        {
            List<Transaction> transactions = accountTransactions[index];
            transactions.Add(new Transaction(transactionType, amount, DateTime.Now));
        }
    }
}

