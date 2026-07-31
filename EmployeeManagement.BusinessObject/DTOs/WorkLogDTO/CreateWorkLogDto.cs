namespace EmployeeManagement.BusinessObject.DTOs.WorkLogDTO
{
    public class CreateWorkLogDto
    {
        public Guid EmployeeId { get; set; }
        public DateTime WorkDate { get; set; }
        public decimal HoursWorked { get; set; }
    }
}
