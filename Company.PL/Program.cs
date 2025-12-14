using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Data.Context;
using Company.PL.Mapping;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;

namespace Company.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepsitory>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();


            builder.Services.AddDbContext<DbContextSql>(options => 
            { options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
                
                ); 
            });

            //builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddAutoMapper(m => m.AddProfile(new EmployeeProfile()));

            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
