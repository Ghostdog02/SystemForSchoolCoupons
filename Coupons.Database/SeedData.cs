using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Coupons.Database
{
    public class SeedData
    {
        //private readonly UserManager<User> userManager;
        //private readonly IUserStore<User> userStore;
        //private readonly IUserEmailStore<User> emailStore;

        //public SeedData(UserManager<User> userManager,
        //    IUserStore<User> userStore)
        //{
        //    this.userManager = userManager
        //    this.userStore = userStore;
        //    this.emailStore = GetEmailStore();
        //}

        public async void InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            //var context = serviceProvider.GetRequiredService<CouponsContext>();
            //var roleManager = scope.ServiceProvider.GetRequiredService<CustomRoleManager>();
            var context = scope.ServiceProvider.GetRequiredService<CouponsContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            //var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = ["Admin", "Parent", "Cook"];

            //foreach (var role in roles)
            //{
            //    if (!await roleManager.RoleExistsAsync(role))
            //        await roleManager.CreateAsync(new IdentityRole<int>(role));

            //}

            foreach (string role in roles)
            {
                var roleStore = new CustomRoleStore(context);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    var identityRole = new IdentityRole<int>(role);
                    var normalizedRoleName = userManager.NormalizeName(role);
                    identityRole.NormalizedName = normalizedRoleName;
                    //identityRole.NormalizedName = role.Normalize();
                    identityRole.ConcurrencyStamp = Guid.NewGuid().ToString("D");
                    await roleStore.CreateAsync(identityRole);
                }
            }

            //if (!context.Users.Any(u => u.UserName == user.UserName))
            //{
            //    var password = new PasswordHasher<User>();
            //    var hashed = password.HashPassword(user, "0dcPx2TFY1rYKls");
            //    user.PasswordHash = hashed;

            //    var userStore = new CustomUserStore(context);
            //    var result = userStore.CreateAsync(user);

            //}
            //var newScope = serviceProvider.CreateScope();
            //var userManager = newScope.ServiceProvider.GetRequiredService<UserManager<User>>();

            //IdentityOptions identityOptions = new IdentityOptions();
            //identityOptions.User.RequireUniqueEmail = true;
            var users = new List<User>();
            string password = "oXMR4TgdfQAEqEN";
            //string roleForUser = "Admin";
            var user = new User
            {
                UserName = "Admin",
                Email = "admin@admin.com",
                PhoneNumber = "0882452245",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = false,
            };

            var user2 = new User
            {
                UserName = "Parent",
                Email = "parent@parent.com",
                PhoneNumber = "0882432245",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = false,
            };

            users.Add(user);
            users.Add(user2);

            foreach (var normalUser in users)
            {
                if (await userManager.FindByEmailAsync(normalUser.Email) == null)
                {
                    await userManager.CreateAsync(normalUser, password);
                    //Change role Parameter if you want to change you username later
                    await userManager.AddToRoleAsync(normalUser, normalUser.UserName);
                }
            }

            

            await context.SaveChangesAsync();
        }
    }
}
