using ServiceCenter.Application.DTO.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTO.Responses
{
    public class ScheduleEmployeeListResponse
    {
        public EmployeeMinimalResponse Employee { get; set; }
        public List<ScheduleDayDto> ScheduleDays { get; set; }
    }
}
