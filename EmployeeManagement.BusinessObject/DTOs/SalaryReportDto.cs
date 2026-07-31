namespace EmployeeManagement.BusinessObject.DTOs
{
    public class SalaryReportDto
    {
        public Guid EmployeeId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public decimal TotalHours { get; set; }
        public decimal TotalSalary { get; set; }
    }
}
