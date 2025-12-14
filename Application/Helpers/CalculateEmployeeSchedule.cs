using ServiceCenter.Application.DTO.Shared;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Helpers
{
    internal class CalculateEmployeeSchedule
    {
        public static IEnumerable<ScheduleDayDto> Calculate(
            List<Schedule> schedules,
            List<ScheduleException> exceptions,
            DateOnly startDate,
            DateOnly endDate)
        {
            if (!schedules.Any())
                return Enumerable.Empty<ScheduleDayDto>();

            var intervalStart = startDate < schedules.FirstOrDefault()?.EffectiveFrom ? schedules.FirstOrDefault()?.EffectiveFrom ?? startDate : startDate;
            var exceptionDates = exceptions
            .SelectMany(ex =>
            {
                if (ex.EffectiveTo != default && ex.EffectiveTo >= ex.EffectiveFrom)
                {
                    var days = ex.EffectiveTo.DayNumber - ex.EffectiveFrom.DayNumber + 1;
                    return Enumerable.Range(0, days).Select(offset => ex.EffectiveFrom.AddDays(offset));
                }
                return new[] { ex.EffectiveFrom };
            })
            .ToHashSet();
            return Enumerable.Range(0, (endDate.DayNumber - startDate.DayNumber) + 1)
                .Select(x => intervalStart.AddDays(x))
                .Where(date => date <= endDate)
            .Select(date =>
            {
                var schedule = schedules.LastOrDefault(x => x.EffectiveFrom <= date && (x.EffectiveTo >= date || x.EffectiveTo == null));
                var workDays = schedule.Pattern.Split('/').Select(x => int.Parse(x)).ToArray();
                return new ScheduleDayDto
                (
                    exceptionDates.Contains(date) ? "Исключение" : ScheduleDaysCalculator.GetDayType(
                    date,
                    schedule.EffectiveFrom,
                    workDays[0],
                    workDays[1]
                ),
                    schedule?.StartTime ?? TimeOnly.MinValue,
                    schedule?.EndTime ?? TimeOnly.MinValue,
                    date
                );
            }).ToList();
        }
    }
}
