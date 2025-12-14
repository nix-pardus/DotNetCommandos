using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceCenter.Application.DTO.Employee;
using ServiceCenter.Application.DTO.Shared;
using ServiceCenter.Application.Interfaces;

namespace ServiceCenter.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController(IEmployeeService employeeService) : ControllerBase
{
    [HttpPost("create")]
    [Authorize(Policy = "Operator")]
    public async Task<IActionResult> Post([FromQuery] CreateEmployeeDto createDto)
    {
        await employeeService.CreateAsync(createDto);
        return Ok();
    }

    [HttpPost("get-by-filters")]
    [Authorize(Policy = "Operator")]
    [ProducesResponseType(typeof(PagedResponse<EmployeeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByFilters([FromBody] GetByFiltersRequest request)
    {
        var response = await employeeService.GetByFiltersAsync(request);
        return Ok(response);
    }

    [HttpPost("get-by-filters-with-orders")]
    [Authorize(Policy = "Operator")]
    [ProducesResponseType(typeof(PagedResponse<EmployeeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByFiltersWithOrders([FromBody] GetByFiltersRequest request)
    {
        var response = await employeeService.GetByFiltersWithOrdersAsync(request);
        return Ok(response);
    }

    [HttpPut("update")]
    [Authorize(Policy = "Operator")]
    public async Task<IActionResult> Update([FromBody] EmployeeDto employeeDto)
    {
        await employeeService.UpdateAsync(employeeDto);
        return Ok();
    }

    [HttpDelete("delete")]
    [Authorize(Policy = "Operator")]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        await employeeService.DeleteAsync(id);
        return Ok();
    }
}
