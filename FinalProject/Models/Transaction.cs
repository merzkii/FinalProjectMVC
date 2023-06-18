namespace FinalProject.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public DateTime? TransactionDate { get; set; }
    }
}
