using AutoMapper;
using EmployeeManagement.BusinessObject.DTOs.EmployeeDTO;
using EmployeeManagement.BusinessObject.Entities;
using EmployeeManagement.Repository.Interfaces;
using EmployeeManagement.Service.Interfaces;

namespace EmployeeManagement.Service.Implementations;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
    }

    public async Task<EmployeeDto> GetByIdAsync(Guid id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null)
            throw new KeyNotFoundException($"Employee not found with ID: {id}.");

        return _mapper.Map<EmployeeDto>(employee);
    }

    public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto)
    {
        var employee = _mapper.Map<Employee>(dto);
        await _employeeRepository.AddAsync(employee);
        await _employeeRepository.SaveChangesAsync();
        return _mapper.Map<EmployeeDto>(employee);
    }

    public async Task UpdateAsync(UpdateEmployeeDto dto)
    {
        var employee = await _employeeRepository.GetByIdAsync(dto.Id);
        if (employee == null)
            throw new KeyNotFoundException($"Employee not found with ID: {dto.Id} for update.");

        _mapper.Map(dto, employee);
        await _employeeRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null)
            throw new KeyNotFoundException($"Employee not found with ID: {id} for deletion.");

        _employeeRepository.Delete(employee);
        await _employeeRepository.SaveChangesAsync();
    }
}