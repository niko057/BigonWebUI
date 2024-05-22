using BigonWebUI.Models;
using Microsoft.EntityFrameworkCore;

namespace BigonWebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            var cstring = builder.Configuration.GetConnectionString("cString");

            builder.Services.AddDbContext<DataContext>(cfg =>
            {
                cfg.UseSqlServer(cstring);

            });
            var app = builder.Build();

            app.MapControllerRoute("default", "{controller=home}/{action=index}/{id?}");

            app.UseStaticFiles();

            

            app.Run();
        }
    }
}