using AutoMapper;
using EmployeeManagement.BusinessObject.DTOs;
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

    public IQueryable<EmployeeDto> GetQueryable()
    {
        return _mapper.ProjectTo<EmployeeDto>(_employeeRepository.GetQueryable());
    }

    public async Task<EmployeeDto> GetByIdAsync(Guid id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null)
            throw new KeyNotFoundException($"Không tìm thấy nhân viên với ID: {id}");

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
            throw new KeyNotFoundException($"Không tìm thấy nhân viên với ID: {dto.Id} để cập nhật.");

        _mapper.Map(dto, employee);
        _employeeRepository.Update(employee);
        await _employeeRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null)
            throw new KeyNotFoundException($"Không tìm thấy nhân viên với ID: {id} để xóa.");

        _employeeRepository.Delete(employee);
        await _employeeRepository.SaveChangesAsync();
    }
}