using BasicBanking.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicBanking.Domain.Entities
{
    public class BankAccount : AuditableEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
    }
}
