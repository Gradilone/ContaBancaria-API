namespace ContaBancaria_API.DTO
{
    public class TransferDTO
    {
        public string ToAccountNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
