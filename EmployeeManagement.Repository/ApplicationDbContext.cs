using EmployeeManagement.BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repository;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<WorkLog> WorkLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>()
            .Property(e => e.HourlyRate)
            .HasPrecision(18, 2);

        modelBuilder.Entity<WorkLog>()
            .Property(w => w.HoursWorked)
            .HasPrecision(18, 2);
    }
}