using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.Interfaces;

namespace ServiceCenter.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScheduleExceptionController(IScheduleExceptionService service) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Post([FromBody] ScheduleExceptionCreateRequest scheduleException)
    {
        await service.CreateAsync(scheduleException);
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
    [HttpGet("get-schedule-exceptions-by-interval")]
    public async Task<IActionResult> Get(Guid employeeId, int page, int pageSize)
    {
        var schedule = await service.GetAllByEmployeePaged(employeeId, page, pageSize);
        return Ok(schedule);
    }
}
