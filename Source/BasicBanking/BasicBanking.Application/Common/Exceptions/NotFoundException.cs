using System;
using System.Collections.Generic;
using System.Text;

namespace BasicBanking.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, string key)
            :base($"Entity \'{name}\' ({key}) was not found")
        {

        }
    }
}
