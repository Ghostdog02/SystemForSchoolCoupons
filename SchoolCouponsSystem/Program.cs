using Coupons.Database;
using Microsoft.EntityFrameworkCore;

namespace SystemForSchoolCoupons
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<CouponsContext>(options => options.UseSqlServer(connectionString));
            //builder.Services.AddDbContextFactory<CouponsContext>(options => options.UseSqlServer(connectionString));

            //builder.Services.AddIdentity < User, "Admin" > (options => options.SignIn.RequireConfirmedAccount = true);
            //    < User > (
            //    options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<CouponsContext>();


            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //This section below is for connection string ConfigureServices
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
