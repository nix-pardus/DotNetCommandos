using Application.Interfaces;
using Domain.DTO.Order;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        // POST api/<ClientController>
        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] OrderDto order)
        {
            await orderService.CreateAsync(order);
            return Ok();
        }
    }
}
