using BasicBanking.Domain.Common;

namespace BasicBanking.Domain.Entities
{
    public class Transaction : AuditableEntity
    {
        public long Id { get; set; }
        public string AccountNumber { get; set; }
        public string TransactionDetails { get; set; }
        public double Amount { get; set; }
    }
}
