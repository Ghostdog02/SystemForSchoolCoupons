using Coupons.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SystemForSchoolCoupons
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("CouponsContextConnection");
            builder.Services.AddDbContext<CouponsContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<User, IdentityRole<int>>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole<int>>()
                .AddEntityFrameworkStores<CouponsContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.Lockout.AllowedForNewUsers = false;
            });

            builder.Services.AddAuthorization();

            //builder.Services.AddMvc();
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddRazorPages();
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

            //app.MapControllerRoute(
            //    name: "Areas",
            //    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            //using var scope = app.Services.CreateScope();
            var provider = app.Services;
            var seeder = new SeedData();
            seeder.InitializeAsync(provider);

            app.MapRazorPages();
            await ApplyMigrations(app);
            app.Run();
        }

        static async Task ApplyMigrations(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var cancelationTokenSource = new CancellationTokenSource();
            cancelationTokenSource.CancelAfter(TimeSpan.FromMinutes(5));

            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<Program>>();
            var dbContext = services.GetRequiredService<CouponsContext>();

            try
            {
                logger.LogInformation("Starting to apply database migrations...");

                // Apply any pending migrations
                await dbContext.Database.MigrateAsync(cancelationTokenSource.Token);

                logger.LogInformation("Database migrations applied successfully.");
            }

            catch (Exception ex)
            {
                // Log the exception if migrations fail
                logger.LogError(ex, "An error occurred while applying database migrations.");

                throw;
            }
        }
    }
}
