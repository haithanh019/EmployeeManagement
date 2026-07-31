namespace EmployeeManagement.BusinessObject.DTOs.EmployeeDTO
{
    public class CreateEmployeeDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public decimal HourlyRate { get; set; }
    }
}
