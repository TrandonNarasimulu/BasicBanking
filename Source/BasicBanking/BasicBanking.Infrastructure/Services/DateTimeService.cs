using BasicBanking.Application.Common.Interfaces;
using System;

namespace BasicBanking.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
