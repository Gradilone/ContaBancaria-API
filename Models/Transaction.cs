namespace ContaBancaria_API.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; } 
        public DateTime Date { get; set; }
        public int SenderAccountId { get; set; } 
        public int ReceiverAccountId { get; set; } 
        public string Description { get; set; } = string.Empty;
    }
}
