using EmployeeManagement.BusinessObject.DTOs.EmployeeDTO;

namespace EmployeeManagement.Service.Interfaces;

public interface IEmployeeService
{
    IQueryable<EmployeeDto> GetQueryable(); // Dùng cho OData
    Task<EmployeeDto> GetByIdAsync(Guid id);
    Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto);
    Task UpdateAsync(UpdateEmployeeDto dto);
    Task DeleteAsync(Guid id);
}