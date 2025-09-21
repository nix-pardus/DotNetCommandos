using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.DTO.Employee;
using ServiceCenter.Application.DTO.Schedule;
using ServiceCenter.Application.DTO.Schedule.Responces;
using ServiceCenter.Application.Interfaces;

namespace ServiceCenter.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScheduleController(IScheduleService service) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Post([FromBody] ScheduleDto schedule)
    {
        await service.CreateAsync(schedule);
        return Ok();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteById([FromBody] Guid id)
    {
        try
        {
            await service.DeleteAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            //TODO: сделать отдельный Exception, пока - так
            return BadRequest(ex);
        }

    }

    //TODO:кмк он должен быть в Employee
    [HttpGet("get-employee-work-days-by-interval")]
    public async Task<IActionResult> GetEmployeeDaysByInterval(Guid employeeId, DateOnly startDate, DateOnly endDate)
    {
        var schedule = await service.GetScheduleByEmployee(employeeId, startDate, endDate);
        return Ok(schedule);
    }
    //TODO:кмк он должен быть в Employee
    [HttpGet("get-all-work-days-by-interval")]
    [ProducesResponseType(typeof(AllScheduleResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllDaysByInterval([FromQuery]DateOnly startDate, [FromQuery] DateOnly endDate)
    {
        var schedule = await service.GetSchedule(startDate, endDate);
        var response = schedule.Select(kvp => new AllScheduleResponseDto
        {
            Employee = kvp.Key,
            ScheduleDays = kvp.Value.ToList()
        }).ToList();
        return Ok(response);
    }
}
