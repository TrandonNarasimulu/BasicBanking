using System.Collections.Generic;

namespace BasicBanking.Application.Banking.Queires.GetTransferHistory
{
    public class TransferHistoryViewModel
    {
        public List<TransferHistoryItem> TransferHistory { get; set; }
    }

    public class TransferHistoryItem
    {
        public string TransactionDetails { get; set; }
        public double Amount { get; set; }
    }
}
