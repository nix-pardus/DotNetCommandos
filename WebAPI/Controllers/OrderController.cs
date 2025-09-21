using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Domain.DTO.Order;

namespace ServiceCenter.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] OrderDto order)
    {
        await orderService.CreateAsync(order);
        return Ok();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromBody] Guid orderId)
    {
        await orderService.DeleteAsync(orderId);
        return Ok();
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var orders = await orderService.GetAllAsync();
        return Ok(orders);
    }

    [HttpPost("getById")]
    public async Task<IActionResult> GetById([FromBody] Guid orderId)
    {
        var order = await orderService.GetAsync(orderId);
        if (order == null) 
            { return NoContent(); }
        return Ok(order);
    }
}
