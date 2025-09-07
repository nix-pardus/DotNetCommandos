using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Domain.DTO.Order;

namespace ServiceCenter.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IOrderService orderService) : ControllerBase
{
    // POST api/<OrderController>
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] OrderDto order)
    {
        await orderService.CreateAsync(order);
        return Ok();
    }

    // POST api/<OrderController>
    [HttpPost("delete")]
    public async Task<IActionResult> Delete([FromBody] Guid orderId)
    {
        await orderService.DeleteAsync(orderId);
        return Ok();
    }
}
