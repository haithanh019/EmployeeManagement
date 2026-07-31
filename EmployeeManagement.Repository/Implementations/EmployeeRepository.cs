using EmployeeManagement.BusinessObject.Entities;
using EmployeeManagement.Repository.Interfaces;

namespace EmployeeManagement.Repository.Implementations;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
    }
}