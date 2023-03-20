namespace Ecommerce_api.Models
{
    public class Transactions
    {
        public int Id { get; set; }
        public string? AccountNumber { get; set; }
        public string? Mode { get; set; }
        public decimal? Amount { get; set; }
        public string? TransactionTo { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? Reason { get; set;}
    }
}
