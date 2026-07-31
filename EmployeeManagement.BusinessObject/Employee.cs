using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.BusinessObject.Entities;

public class Employee
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    public string Position { get; set; } = string.Empty;

    public decimal HourlyRate { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<WorkLog> WorkLogs { get; set; } = new List<WorkLog>();
}