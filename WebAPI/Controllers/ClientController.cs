using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.DTO.Client;
using ServiceCenter.Application.DTO.Shared;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Domain.Entities;

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
    
    [HttpPost("get-by-filters")]
    [ProducesResponseType(typeof(PagedResponse<Client>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByFilters([FromBody] GetByFiltersRequest request)
    {
        var response = await clientService.GetByFiltersAsync(request);
        return Ok(response);
    }
}
