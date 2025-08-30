using Application.Interfaces;
using Domain.DTO.Employee;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Post([FromBody] EmployeeDto employeeDto)
    {
        await _employeeService.CreateAsync(employeeDto);
        return Ok();
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var employees = await _employeeService.GetAllAsync();
        return Ok(employees);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] EmployeeDto employeeDto)
    {
        await _employeeService.UpdateAsync(employeeDto);
        return Ok();
    }
}
