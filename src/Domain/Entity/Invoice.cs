namespace Domain.Entity
{
    public class Invoice:BaseEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string InvoiceNumber { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
