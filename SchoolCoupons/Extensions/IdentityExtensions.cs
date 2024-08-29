//using AspnetIdentity20.Models;
using Coupons.Database;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
//using System.Threading.Tasks;

namespace SchoolCoupons.Extensions
{
    public static class IdentityExtensions
    {
        public static async Task<User> FindByNameOrEmailAsync
            (this UserManager<User> userManager, string usernameOrEmail, string password)
        {
            var username = usernameOrEmail;
            if (usernameOrEmail.Contains("@"))
            {
                var userForEmail = await userManager.FindByEmailAsync(usernameOrEmail);
                if (userForEmail != null)
                {
                    username = userForEmail.UserName;
                }
            }
            return await userManager.FindByNameAsync(username);
        }
    }
}