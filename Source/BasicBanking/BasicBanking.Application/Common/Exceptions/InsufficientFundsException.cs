using System;
using System.Collections.Generic;
using System.Text;

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
