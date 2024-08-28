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
            string email = "admin@admin.com";
            string password = "V3s3ly#042";
            string userName = "Admin";
            string phoneNumber = "0882452245";
            //IdentityOptions identityOptions = new IdentityOptions();
            //identityOptions.User.RequireUniqueEmail = true;
                        
            if (await userManager.FindByEmailAsync(email) == null)
            {
                
                //string userName = "Admin";
                ////var user = new User(email, phoneNumber, userName);
                //var normalizedEmail = userManager.NormalizeEmail(email);
                //var normalizedUsername = userManager.NormalizeName(userName);

                
                //await userManager.CreateAsync(user, password);
                //await userManager.AddToRoleAsync(user, "Admin");
                //var user = new User();

                var user = new User
                {
                    UserName = userName,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false,
                };

                user.PhoneNumber = phoneNumber;

                //var userStore = new CustomUserStore(context);
                //var emailStore = GetEmailStore(userManager, userStore);

                //await userStore.SetUserNameAsync(user, userName, CancellationToken.None);
                //await emailStore.SetEmailAsync(user, email, CancellationToken.None);
                await userManager.CreateAsync(user, password);
                await userManager.AddToRoleAsync(user, "Admin");
            }

            //AssignRoles(serviceProvider, user.Email, roles);

            await context.SaveChangesAsync();
        }

        //private IUserEmailStore<User> GetEmailStore(UserManager<User> userManager, CustomUserStore userStore)
        //{
        //    if (!userManager.SupportsUserEmail)
        //    {
        //        throw new NotSupportedException("The default UI requires a user store with email support.");
        //    }
        //    return (IUserEmailStore<User>)userStore;
        //}

        //public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
        //{
        //    UserManager<User> _userManager = services.GetService<UserManager<User>>();
        //    User user = await _userManager.FindByEmailAsync(email);
        //    var result = await _userManager.AddToRolesAsync(user, roles);

        //    return result;
        //}
    }
}
