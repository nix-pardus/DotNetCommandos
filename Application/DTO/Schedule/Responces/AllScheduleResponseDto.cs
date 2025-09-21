using ServiceCenter.Application.DTO.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTO.Schedule.Responces
{
    public class AllScheduleResponseDto
    {
        public EmployeeMinimalDto Employee { get; set; }
        public List<ScheduleDayDto> ScheduleDays { get; set; }
    }
}
