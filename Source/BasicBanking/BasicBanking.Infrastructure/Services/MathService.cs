using BasicBanking.Application.Common.Interfaces;
using System;

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
