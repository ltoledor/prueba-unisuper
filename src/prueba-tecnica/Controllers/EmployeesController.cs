using Microsoft.AspNetCore.Mvc;
using prueba_tecnica.Models;
using prueba_tecnica.Services;

namespace prueba_tecnica.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController(IEmployeeService employeeService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
    {
        var employees = await employeeService.GetAllAsync();
        return Ok(employees);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Employee>> GetById(int id)
    {
        var response = await employeeService.GetByIdAsync(id);
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> Create([FromBody] Employee employee)
    {
        var createdEmployee = await employeeService.CreateAsync(employee);

        return CreatedAtAction(nameof(GetById), new { id = createdEmployee.Id }, createdEmployee);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Employee employee)
    {
        await employeeService.UpdateAsync(id, employee);

        return NoContent();
    }

    [HttpGet("{id:int}/bonus")]
    public async Task<ActionResult<object>> GetAnnualBonus(int id)
    {
        var bonusResponse = await employeeService.GetAnnualBonusAsync(id);

        return Ok(bonusResponse);
    }
}
