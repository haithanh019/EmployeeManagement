using EmployeeManagement.BusinessObject.DTOs.EmployeeDTO;

namespace EmployeeManagement.Service.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllAsync();
    Task<EmployeeDto> GetByIdAsync(Guid id);
    Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto);
    Task UpdateAsync(UpdateEmployeeDto dto);
    Task DeleteAsync(Guid id);
}
