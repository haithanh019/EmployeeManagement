using EmployeeManagement.BusinessObject.DTOs.WorkLogDTO;
using EmployeeManagement.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkLogsController : ControllerBase
{
    private readonly IWorkLogService _workLogService;

    public WorkLogsController(IWorkLogService workLogService)
    {
        _workLogService = workLogService;
    }

    [HttpPost]
    public async Task<IActionResult> LogWork([FromBody] CreateWorkLogDto dto)
    {
        await _workLogService.LogWorkAsync(dto);
        return Ok(new { Message = "Work logged successfully!" });
    }
}