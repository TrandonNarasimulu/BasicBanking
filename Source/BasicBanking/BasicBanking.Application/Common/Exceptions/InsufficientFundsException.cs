using System;

namespace BasicBanking.Application.Common.Exceptions
{
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException(string message)
            :base(message)
        {

        }
    }
}
