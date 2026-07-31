using EmployeeManagement.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalariesController : ControllerBase
{
    private readonly ISalaryService _salaryService;

    public SalariesController(ISalaryService salaryService)
    {
        _salaryService = salaryService;
    }

    [HttpGet("export")]
    public async Task<IActionResult> ExportToExcel([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
    {
        var fileContent = await _salaryService.ExportSalaryReportToExcelAsync(fromDate, toDate);
        var fileName = $"SalaryReport_{fromDate:yyyyMMdd}_{toDate:yyyyMMdd}.xlsx";

        return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
    }
}