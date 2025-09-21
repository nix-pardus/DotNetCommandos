using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Helpers
{
    internal static class ScheduleDaysCalculator
    {
        public static bool IsWorkDay(DateOnly date, DateOnly scheduleStartDate, int workDays, int restDays)
        {
            // Если стандартная 5/2 неделя - используем оптимизированный расчет
            if (workDays == 5 && restDays == 2)
            {
                return date.DayOfWeek is >= DayOfWeek.Monday and <= DayOfWeek.Friday;
            }

            // Для всех остальных графиков используем циклический расчет
            var totalCycleDays = workDays + restDays;
            var daysFromStart = date.DayNumber - scheduleStartDate.DayNumber;

            // Определяем позицию в цикле
            var positionInCycle = daysFromStart % totalCycleDays;

            // Если позиция в пределах рабочих дней - рабочий день
            return positionInCycle < workDays;
        }

        public static string GetDayType(DateOnly date, DateOnly scheduleStartDate, int workDays, int restDays)
        {
            return IsWorkDay(date, scheduleStartDate, workDays, restDays) ? "Рабочий" : "Выходной";
        }
    }

}
