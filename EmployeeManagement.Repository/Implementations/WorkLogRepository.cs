using EmployeeManagement.BusinessObject.Entities;
using EmployeeManagement.Repository.Interfaces;

namespace EmployeeManagement.Repository.Implementations;

public class WorkLogRepository : GenericRepository<WorkLog>, IWorkLogRepository
{
    public WorkLogRepository(ApplicationDbContext context) : base(context)
    {
    }
}