using System.ComponentModel;

namespace BasicBanking.Application.Common.Enumerations
{
    public enum TransactionDetails
    {
        [Description("Deposit")]
        Deposit,

        [Description("Money In")]
        Money_In,

        [Description("Money Out")]
        Money_Out
    }
}
