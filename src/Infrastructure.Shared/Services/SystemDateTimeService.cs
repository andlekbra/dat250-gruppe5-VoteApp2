using VoteApp.Application.Interfaces.Services;
using System;

namespace VoteApp.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}