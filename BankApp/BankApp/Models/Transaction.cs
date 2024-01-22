using BankApp.Enum;

public class Transaction
{
    private int TransactionIdCounter=0;
    public int TransactionId { get; }
    public decimal Amount { get; }
    public DateTime TransactionDate { get; }
    public TransactionType TransactionType { get; }

    private DateTime now;

    public Transaction( TransactionType transactionType, decimal amount)
    {
        TransactionIdCounter++;
        TransactionId = TransactionIdCounter;
        TransactionType = transactionType;
        Amount = amount;
        TransactionDate = DateTime.Now;

    }

    public Transaction(TransactionType transactionType, decimal amount, DateTime now) : this(transactionType, amount)
    {
        TransactionDate = now;
    }
}