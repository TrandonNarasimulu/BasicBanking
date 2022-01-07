using System.Collections.Generic;

namespace BasicBanking.Application.Banking.Commands.BalanceByIDNumber
{
    public class BalanceByIDViewModel
    {
        public List<AccountDetailsModel> accountDetails { get; set; }
    }

    public class AccountDetailsModel
    {
        public string AccountNumber { get; set; }
        public double AccountBalance { get; set; }
        public string AccountHolderFirstName { get; set; }
        public string AccountHolderLastName { get; set; }
        public string AccountHolderIDNumber { get; set; }
    }
}
