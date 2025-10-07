using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.DTO.Shared;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Application.Services;
using ServiceCenter.Domain.DTO.Order;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto order)
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


    [HttpPost("getByFilters")]
    public async Task<IActionResult> GetByFilters([FromBody] GetByFiltersRequest request)
    {
        var response = await orderService.GetByFiltersAsync(request);
        return Ok(response);
    }

    [HttpPost("getByFilters")]
    public async Task<IActionResult> GetByFilters([FromBody] GetByFiltersRequest request)
    {
        var response = await orderService.GetByFiltersAsync(request);
        return Ok(response);
    }

}
