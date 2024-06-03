using Bigon.Data;
using Bigon.Infrastructure.Commons;
using Bigon.Infrastructure.Services.Abstracts;
using Bigon.Infrastructure.Services.Concretes;
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
                cfg.UseSqlServer(cstring,opt =>
                {
                    opt.MigrationsHistoryTable("Migrations");
                });

            });
            builder.Services.Configure<EmailOptions>(
                cfg =>
                {
                    builder.Configuration.GetSection("emailAccount").Bind(cfg);
                });

            builder.Services.AddSingleton<IEmailService,EmailService>();

            var app = builder.Build();

           app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );
            app.UseStaticFiles();
            app.MapControllerRoute("default", "{controller=home}/{action=index}/{id?}");

           

            

            app.Run();
        }
    }
}