using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Domain.DTO.Order;

namespace ServiceCenter.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IOrderService orderService) : ControllerBase
{
    // POST api/<ClientController>
    [HttpPost("create")]
    public async Task<IActionResult> Post([FromBody] OrderDto order)
    {
        await orderService.CreateAsync(order);
        return Ok();
    }
}
