using System;

namespace OnionArchitecture.Application.Interfaces.Services
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
        DateTimeOffset localTime { get; }
    }
}