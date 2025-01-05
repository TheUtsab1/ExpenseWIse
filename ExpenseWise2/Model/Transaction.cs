namespace ExpenseWise2.Models
{

    public class Transaction1
    {
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Tags { get; set; }
        public string Note { get; set; }
    }