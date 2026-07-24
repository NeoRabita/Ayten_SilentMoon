using System;
using OnionArchitecture.Application.Interfaces.Services;

namespace OnionArchitecture.Infrastructure.Persistence.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTimeOffset localTime => TimeZoneInfo.ConvertTime(DateTimeOffset.Now, TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time"));
        public DateTime NowUtc => localTime.DateTime;
    }
}