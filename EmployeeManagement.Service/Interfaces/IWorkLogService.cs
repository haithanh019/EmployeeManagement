using EmployeeManagement.BusinessObject.DTOs.WorkLogDTO;

namespace EmployeeManagement.Service.Interfaces;

public interface IWorkLogService
{
    Task LogWorkAsync(CreateWorkLogDto dto);
}