using ClosedXML.Excel;
using EmployeeManagement.BusinessObject.DTOs;
using EmployeeManagement.Repository.Interfaces;
using EmployeeManagement.Service.Interfaces;

namespace EmployeeManagement.Service.Implementations;

public class SalaryService : ISalaryService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IWorkLogRepository _workLogRepository;

    public SalaryService(IEmployeeRepository employeeRepository, IWorkLogRepository workLogRepository)
    {
        _employeeRepository = employeeRepository;
        _workLogRepository = workLogRepository;
    }

    public async Task<IEnumerable<SalaryReportDto>> CalculateSalaryAsync(DateTime fromDate, DateTime toDate)
    {
        var employees = await _employeeRepository.GetAllAsync();
        var workLogs = await _workLogRepository.FindAsync(w => w.WorkDate >= fromDate.Date && w.WorkDate <= toDate.Date);

        var report = employees.Select(emp =>
        {
            var empLogs = workLogs.Where(w => w.EmployeeId == emp.Id).ToList();
            var totalHours = empLogs.Sum(w => w.HoursWorked);

            return new SalaryReportDto
            {
                EmployeeId = emp.Id,
                FullName = emp.FullName,
                TotalHours = totalHours,
                TotalSalary = totalHours * emp.HourlyRate
            };
        }).Where(r => r.TotalHours > 0).ToList();

        return report;
    }

    public async Task<byte[]> ExportSalaryReportToExcelAsync(DateTime fromDate, DateTime toDate)
    {
        var reportData = await CalculateSalaryAsync(fromDate, toDate);

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Salary Report");

        worksheet.Cell(1, 1).Value = "Employee ID";
        worksheet.Cell(1, 2).Value = "Full Name";
        worksheet.Cell(1, 3).Value = "Total Hours";
        worksheet.Cell(1, 4).Value = "Total Salary";

        var headerRow = worksheet.Row(1);
        headerRow.Style.Font.Bold = true;
        headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;

        int row = 2;
        foreach (var item in reportData)
        {
            worksheet.Cell(row, 1).Value = item.EmployeeId.ToString();
            worksheet.Cell(row, 2).Value = item.FullName;
            worksheet.Cell(row, 3).Value = item.TotalHours;
            worksheet.Cell(row, 4).Value = item.TotalSalary;
            row++;
        }

        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}