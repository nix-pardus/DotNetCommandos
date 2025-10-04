using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Application.Interfaces;

namespace ServiceCenter.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController(IClientService clientService) : ControllerBase
{
    // POST api/<ClientController>
    [HttpPost("create")]
    public async Task<IActionResult> Post([FromBody] ClientCreateRequest client)
    {
        await clientService.CreateAsync(client);
        return Ok();
    }
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] ClientUpdateRequest client)
    {
        await clientService.UpdateAsync(client);
        return Ok();
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteById([FromBody] Guid id)
    {
        try
        {
            await clientService.DeleteAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            //TODO: сделать отдельный Exception, пока - так
            return BadRequest(ex);
        }

    }
}
