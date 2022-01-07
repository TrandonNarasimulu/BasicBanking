using BasicBanking.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicBanking.Infrastructure.Services
{
    public class MathService : IMath
    {
        public int GetRandomNumber(int max)
        {
            return new Random().Next(max);
        }
    }
}
