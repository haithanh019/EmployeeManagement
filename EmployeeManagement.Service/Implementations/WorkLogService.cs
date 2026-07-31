using EmployeeManagement.BusinessObject.DTOs.WorkLogDTO;
using EmployeeManagement.BusinessObject.Entities;
using EmployeeManagement.Repository.Interfaces;
using EmployeeManagement.Service.Interfaces;

namespace EmployeeManagement.Service.Implementations
{
    public class WorkLogService : IWorkLogService
    {
        private readonly IWorkLogRepository _workLogRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public WorkLogService(IWorkLogRepository workLogRepository, IEmployeeRepository employeeRepository)
        {
            _workLogRepository = workLogRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task LogWorkAsync(CreateWorkLogDto dto)
        {
            var employee = await _employeeRepository.GetByIdAsync(dto.EmployeeId);

            if (employee == null)
                throw new KeyNotFoundException($"Nhân viên với ID: {dto.EmployeeId} không tồn tại.");

            if (!employee.IsActive)
                throw new InvalidOperationException($"Nhân viên {employee.FullName} đã nghỉ việc, không thể chấm công.");

            if (dto.HoursWorked <= 0 || dto.HoursWorked > 24)
                throw new ArgumentException("Số giờ làm việc phải lớn hơn 0 và nhỏ hơn hoặc bằng 24.");

            var workLog = new WorkLog
            {
                EmployeeId = dto.EmployeeId,
                WorkDate = dto.WorkDate.Date,
                HoursWorked = dto.HoursWorked
            };

            await _workLogRepository.AddAsync(workLog);
            await _workLogRepository.SaveChangesAsync();
        }
    }
}
