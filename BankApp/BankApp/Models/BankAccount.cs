using BankApp.Enum;
using BankApp.Exceptionlar;
using BankApp.Interfeys;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class BankAccount : IAccount
{
    private static int accountIdCounter = 0;
    private List<Transaction> transactions;

    public int AccountId { get; }
    public decimal Balance { get; private set; }
    public AccountType AccountType { get; }
    public CurrencyType CurrencyType { get; }

    public BankAccount(AccountType accountType, CurrencyType currencyType)
    {
        AccountId = ++accountIdCounter;
        Balance = 0;
        AccountType = accountType;
        CurrencyType = currencyType;
        transactions = new List<Transaction>();

    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new InvalidAmountException("Invalid deposit amount");
        }

        Balance += amount;
        transactions.Add(new Transaction(TransactionType.Deposit, amount));
    }

    public void Withdraw(decimal amount)
    {
        Console.WriteLine($"Your current balance: {Balance}");

        if (amount <= 0 || amount > Balance)
        {
            throw new InvalidAmountException("Invalid withdrawal amount");
        }

        Balance -= amount;
        transactions.Add(new Transaction(TransactionType.Withdraw, amount));
        Console.WriteLine("Money successfully withdrawn.");
        Console.WriteLine($"Remaining balance: {Balance}");
    }

    public List<Transaction> GetTransactions()
    {
        return transactions;
    }
}