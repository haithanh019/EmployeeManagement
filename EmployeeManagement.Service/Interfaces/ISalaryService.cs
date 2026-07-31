using EmployeeManagement.BusinessObject.DTOs;

namespace EmployeeManagement.Service.Interfaces;

public interface ISalaryService
{
    Task<IEnumerable<SalaryReportDto>> CalculateSalaryAsync(DateTime fromDate, DateTime toDate);
    Task<byte[]> ExportSalaryReportToExcelAsync(DateTime fromDate, DateTime toDate);
}