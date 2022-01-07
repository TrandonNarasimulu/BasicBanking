using System.Collections.Generic;

namespace BasicBanking.Application.Banking.Queires.GetBalanceByIDNumber
{
    public class BalanceByIDViewModel
    {
        public List<AccountDetailsModel> AccountDetails { get; set; }
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
