using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.DTO.Requests;
using ServiceCenter.Application.DTO.Responses;
using ServiceCenter.Application.Interfaces;

namespace ServiceCenter.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController(IEmployeeService employeeService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Post([FromBody] EmployeeCreateRequest employeeDto)
    {
        await employeeService.CreateAsync(employeeDto);
        return Ok();
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var employees = await employeeService.GetAllAsync();
        return Ok(employees);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] EmployeeUpdateRequest employeeDto)
    {
        await employeeService.UpdateAsync(employeeDto);
        return Ok();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromBody] Guid id)
    {
        await employeeService.DeleteAsync(id);
        return Ok();
    }
}
