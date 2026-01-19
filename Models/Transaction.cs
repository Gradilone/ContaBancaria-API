namespace ContaBancaria_API.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; private set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;
    }
}
