using BasicBanking.Domain.Common;
using System.Collections.Generic;

namespace BasicBanking.Domain.Entities
{
    public class User : AuditableEntity
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IDNumber { get; set; }
        public List<BankAccount> BankAccounts { get; set; }
    }
}
