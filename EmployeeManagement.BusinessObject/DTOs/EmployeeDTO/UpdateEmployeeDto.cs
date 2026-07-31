namespace EmployeeManagement.BusinessObject.DTOs.EmployeeDTO
{
    public class UpdateEmployeeDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public decimal HourlyRate { get; set; }
        public bool IsActive { get; set; }
    }
}
