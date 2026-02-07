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
        await orderService.CreateAsync(order);
        return Ok();
    }

    [HttpDelete("delete")]
    [Authorize(Policy = "Operator")]
    public async Task<IActionResult> Delete([FromQuery] Guid orderId)
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
        await orderService.UpdateAsync(order);
        return Ok();
    }
}
