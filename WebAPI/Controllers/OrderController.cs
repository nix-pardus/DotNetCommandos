using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Shared;
using ServiceCenter.Application.Interfaces;

namespace ServiceCenter.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpPost("create")]
    [Authorize(Policy = "Operator")]
    public async Task<IActionResult> Create([FromBody] OrderCreateRequest order)
    {
        if (order.StartDateTime != null)
        {
            if (order.EndDateTime == null)
                return BadRequest("Не указана время окончания визита");
            else
            {
                //проверяем, что дата начала и окончания визита совпадают и что начало визита раньше его окончания
                if ((order.StartDateTime.Value.Date!=order.EndDateTime.Value.Date)||(order.StartDateTime.Value > order.EndDateTime.Value))
                    return BadRequest("Неверно указана дата визита");
            }
        }
        await orderService.CreateAsync(order);
        return Ok();
    }

    [HttpDelete("delete")]
    [Authorize(Policy = "Operator")]
    public async Task<IActionResult> Delete([FromBody] Guid orderId)
    {
        await orderService.DeleteAsync(orderId);
        return Ok();
    }

    [HttpPost("getById")]
    [Authorize(Policy = "All")]
    public async Task<IActionResult> GetById([FromBody] Guid orderId)
    {
        var order = await orderService.GetAsync(orderId);
        if (order == null) 
            { return NoContent(); }
        return Ok(order);
    }

    [HttpPost("getByFilters")]
    [Authorize(Policy = "All")]
    public async Task<IActionResult> GetByFilters([FromBody] GetByFiltersRequest request)
    {
        var response = await orderService.GetByFiltersAsync(request);
        return Ok(response);
    }

    [HttpPut("update")]
    [Authorize(Policy = "All")]
    public async Task<IActionResult> Update([FromBody] OrderUpdateRequest order)
    {
        if (order.StartDateTime != null)  
        {
            if (order.EndDateTime == null)
                return BadRequest("Не указана время окончания визита");
            else
            {
                //проверяем, что дата начала и окончания визита совпадают и что начало визита раньше его окончания
                if ((order.StartDateTime.Value.Date != order.EndDateTime.Value.Date) || (order.StartDateTime.Value > order.EndDateTime.Value))
                    return BadRequest("Неверно указана дата визита");
            }
        }
        await orderService.UpdateAsync(order);
        return Ok();
    }
}
