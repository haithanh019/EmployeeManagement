using EmployeeManagement.API.Middlewares;
using EmployeeManagement.Repository;
using EmployeeManagement.Repository.Implementations;
using EmployeeManagement.Repository.Interfaces;
using EmployeeManagement.Service.Implementations;
using EmployeeManagement.Service.Interfaces;
using EmployeeManagement.Service.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace EmployeeManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            builder.Services.AddOpenApi();

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.LicenseKey = builder.Configuration["AutoMapper:LicenseKey"];
                cfg.AddProfile<MappingProfile>();
            });

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IWorkLogRepository, WorkLogRepository>();

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IWorkLogService, WorkLogService>();
            builder.Services.AddScoped<ISalaryService, SalaryService>();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                    policy.WithOrigins("http://localhost:5173",
                                       "https://employee-management-silk-omega.vercel.app")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
            });
            var app = builder.Build();
            app.UseCors();
            app.UseMiddleware<GlobalExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
