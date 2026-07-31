using EmployeeManagement.BusinessObject.DTOs.EmployeeDTO;
using EmployeeManagement.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace EmployeeManagement.API.Controllers;

public class EmployeesController : ODataController
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(_employeeService.GetQueryable());
    }

    [HttpGet("{id}")]
    [EnableQuery]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var employee = await _employeeService.GetByIdAsync(id);
        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateEmployeeDto dto)
    {
        var created = await _employeeService.CreateAsync(dto);
        return Created(created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateEmployeeDto dto)
    {
        dto.Id = id;
        await _employeeService.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _employeeService.DeleteAsync(id);
        return NoContent();
    }
}