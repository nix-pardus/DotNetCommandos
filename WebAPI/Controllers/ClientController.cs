using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
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
    [Authorize(Policy = "Operator")]
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
    [Authorize(Policy = "Operator")]
    public async Task<IActionResult> DeleteById([FromQuery] Guid id)
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
    [Authorize(Policy = "Operator")]
    [ProducesResponseType(typeof(PagedResponse<Client>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByFilters([FromBody] GetByFiltersRequest request)
    {
        var response = await clientService.GetByFiltersAsync(request);
        return Ok(response);
    }
}
