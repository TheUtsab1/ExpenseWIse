public class TransactionModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; }
    public double Amount { get; set; }
    public string? Note { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public string? Source { get; set; }
    public bool IsCleared { get; set; }

    public TransactionType Type { get; set; }

    public string? Tag { get; set; } // Added nullable tag property

    //public DebtStatus status { get; set; }

    public TransactionModel(
        string title,
        double amount,
        string? note,
        DateTime date,
        DateTime dueDate,
        string? source = null,
        TransactionType type = TransactionType.Income,
        string tag = "",
        bool isCleared = false
        )
    {
        Title = title;
        Amount = amount;
        Note = note;
        Date = date;
        DueDate = dueDate;
        Source = source;
        Type = type;
        Tag = tag; // Assigned the tag parameter to the Tag property
        isCleared = isCleared;
    }
}

public enum TransactionType
{
    Income,
    Expense,
    Debt
}

//public enum DebtStatus
//{
//    Completed,
//    Pending
//}
