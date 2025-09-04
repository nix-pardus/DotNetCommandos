using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.DTO.Client;
using ServiceCenter.Application.Interfaces;

namespace ServiceCenter.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController(IClientService clientService) : ControllerBase
{
    // POST api/<ClientController>
    [HttpPost("create")]
    public async Task<IActionResult> Post([FromBody] ClientDto client)
    {
        await clientService.CreateAsync(client);
        return Ok();
    }
}
