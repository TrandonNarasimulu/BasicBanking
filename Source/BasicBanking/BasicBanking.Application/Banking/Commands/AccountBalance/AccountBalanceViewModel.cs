using System;
using System.Collections.Generic;
using System.Text;

namespace BasicBanking.Application.Banking.Commands.AccountBalance
{
    public class AccountBalanceViewModel
    {
        public string AccountNumber { get; set; }
        public double AccountBalance { get; set; }
        public string AccountHolderFirstName { get; set; }
        public string AccountHolderLastName { get; set; }
    }
}
