using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Application.DTO.Shared;
using ServiceCenter.Application.Interfaces;

namespace ServiceCenter.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssignmentController(IAssignmentService assignmentService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Post([FromQuery] AssignmentCreateRequest request)
    {
        await assignmentService.CreateAsync(request);
        return Ok();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        await assignmentService.DeleteAsync(id);
        return Ok();
    }

    [HttpPost("get-by-filters")]
    [ProducesResponseType(typeof(PagedResponse<AssignmentResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByFilters([FromBody] GetByFiltersRequest request)
    {
        var response = await assignmentService.GetByFiltersAsync(request);
        return Ok(response);
    }
}
