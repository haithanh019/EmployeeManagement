using AutoMapper;
using EmployeeManagement.BusinessObject.DTOs.EmployeeDTO;
using EmployeeManagement.BusinessObject.Entities;

namespace EmployeeManagement.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Employee, EmployeeDto>().ReverseMap();
        CreateMap<CreateEmployeeDto, Employee>();
        CreateMap<UpdateEmployeeDto, Employee>();
    }
}