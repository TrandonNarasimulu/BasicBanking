using BasicBanking.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicBanking.Domain.Entities
{
    public class User : AuditableEntity
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<BankAccount> BankAccounts { get; set; }
    }
}
