using BasicBanking.Domain.Common;

namespace BasicBanking.Domain.Entities
{
    public class Transaction : AuditableEntity
    {
        public long Id { get; set; }
        public string MainAccountNumber { get; set; }
        public string OtherAccountNumber { get; set; }
        public string TransactionDetails { get; set; }
        public double Amount { get; set; }
    }
}
