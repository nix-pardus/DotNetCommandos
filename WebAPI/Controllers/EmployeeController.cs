using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Application.DTO.Shared;
using ServiceCenter.Application.Interfaces;

namespace ServiceCenter.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController(IEmployeeService employeeService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Post([FromBody] EmployeeCreateRequest employee)
    {
        await employeeService.CreateAsync(employee);
        return Ok();
    }

    [HttpPost("get-by-filters")]
    [ProducesResponseType(typeof(PagedResponse<EmployeeFullResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByFilters([FromBody] GetByFiltersRequest request)
    {
        var response = await employeeService.GetByFiltersAsync(request);
        return Ok(response);
    }

    [HttpPost("get-by-filters-with-orders")]
    [ProducesResponseType(typeof(PagedResponse<EmployeeFullResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByFiltersWithOrders([FromBody] GetByFiltersRequest request)
    {
        var response = await employeeService.GetByFiltersWithOrdersAsync(request);
        return Ok(response);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] EmployeeUpdateRequest employee)
    {
        await employeeService.UpdateAsync(employee);
        return Ok();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromBody] Guid id)
    {
        await employeeService.DeleteAsync(id);
        return Ok();
    }
}
