using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.DTO.Client;
using ServiceCenter.Application.DTO.Schedule;
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
    [HttpGet("get-work-days-by-interval")]
    public async Task<IActionResult> Get(Guid employeeId, DateOnly startDate, DateOnly endDate)
    {
        var schedule = await service.GetScheduleByEmployee(employeeId, startDate, endDate);
        return Ok(schedule);
    }
}
