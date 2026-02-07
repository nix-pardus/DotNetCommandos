using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Application.DTO.Shared;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Application.Services;

namespace ServiceCenter.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssignmentController(IAssignmentService assignmentService, IScheduleService scheduleService, IOrderService orderService) : ControllerBase
{
    [HttpPost("create")]
    [Authorize(Policy = "Operator")]
    public async Task<IActionResult> Post([FromQuery] AssignmentCreateRequest request)
    {
        var order=await orderService.GetAsync(request.OrderId);//читаем данные заявки
        if (order != null)
        {
            if ((order.StartDateTime != null) && (order.EndDateTime != null)) //в заявке указано время
            {
                var d = await scheduleService.GetScheduleByEmployee(request.EmployeeId, DateOnly.FromDateTime((DateTime)order.StartDateTime), DateOnly.FromDateTime(((DateTime)order.EndDateTime)));

                if (d != null)
                {
                    var targetItem = d.FirstOrDefault(item => item.Date == DateOnly.FromDateTime((DateTime)order.StartDateTime));

                    // Получаем DayType, если элемент найден
                    string dayType = targetItem?.DayType; // Будет null, если элемент не найден

                    if (dayType != null)
                    {
                        if (dayType.ToLower() == "рабочий")
                        {
                            await assignmentService.CreateAsync(request);
                            return Ok();
                        }
                        else return BadRequest("У мастера выходной");
                    }
                    else
                    {
                        return BadRequest("Не найдено расписание мастера");
                    }                   
                }
                else return BadRequest("Не найдено расписание мастера");
            }
            else return BadRequest("Не указано время визита мастера в заявке");
        }
        else { return BadRequest("Заявка не найдена"); }
    }

    [HttpDelete("delete")]
    [Authorize(Policy = "Operator")]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        await assignmentService.DeleteAsync(id);
        return Ok();
    }

    [HttpPost("get-by-filters")]
    [Authorize(Policy = "Operator")]
    [ProducesResponseType(typeof(PagedResponse<AssignmentResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByFilters([FromBody] GetByFiltersRequest request)
    {
        var response = await assignmentService.GetByFiltersAsync(request);
        return Ok(response);
    }
}
