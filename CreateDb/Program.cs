using CreateDb.Model;
using Microsoft.EntityFrameworkCore;

namespace CreateDb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()  // Permite qualquer origem
                          .AllowAnyMethod()  // Permite qualquer método HTTP (GET, POST, etc.)
                          .AllowAnyHeader(); // Permite qualquer cabeçalho
                });
            });
            builder.Services.AddDbContext<SchoolContext>(options => 
            { 
                options.UseSqlServer("Server=DESKTOP-0M45M7S\\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;TrustServerCertificate=True;");
            
            });
            builder.Services.AddScoped<StudentStore>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.MapControllerRoute(
                name:"default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("AllowAllOrigins");
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
