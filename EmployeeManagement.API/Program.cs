using EmployeeManagement.API.Middlewares;
using EmployeeManagement.BusinessObject.DTOs.EmployeeDTO;
using EmployeeManagement.Repository;
using EmployeeManagement.Repository.Implementations;
using EmployeeManagement.Repository.Interfaces;
using EmployeeManagement.Service.Implementations;
using EmployeeManagement.Service.Interfaces;
using EmployeeManagement.Service.Mappers;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System.Text.Json.Serialization;

namespace EmployeeManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.LicenseKey = builder.Configuration["AutoMapper:LicenseKey"];
                cfg.AddProfile<MappingProfile>();
            });
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            static IEdmModel GetEdmModel()
            {
                var odataBuilder = new ODataConventionModelBuilder();
                odataBuilder.EntitySet<EmployeeDto>("Employees");

                return odataBuilder.GetEdmModel();
            }

            builder.Services.AddControllers()
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
            .AddOData(options => options
                .Select()
                .Filter()
                .OrderBy()
                .Expand()
                .Count()
                .SetMaxTop(100)
                .AddRouteComponents("Odata", GetEdmModel()));
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IWorkLogRepository, WorkLogRepository>();

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IWorkLogService, WorkLogService>();
            builder.Services.AddScoped<ISalaryService, SalaryService>();

            var app = builder.Build();
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