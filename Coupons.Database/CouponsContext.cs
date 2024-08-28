using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Database
{
    public class CouponsContext : IdentityDbContext<User, IdentityRole<int>, int>
    {


        public CouponsContext(DbContextOptions<CouponsContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<IdentityRole<int>>(entity =>
            {
                entity.ToTable(name: "Roles");
            });

            modelBuilder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            modelBuilder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            modelBuilder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            modelBuilder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
        }

    }

    public class CustomRoleStore : RoleStore<IdentityRole<int>, CouponsContext, int>
    {
        public CustomRoleStore(CouponsContext context) : base(context)
        {
        }
    }

    public class CustomUserStore : UserStore<User, IdentityRole<int>, CouponsContext, int>
    {
        public CustomUserStore(CouponsContext context) : base(context)
        {
        }
    }

    //public class CustomRoleManager : RoleManager<IdentityRole<int>>
    //{
    //    public CustomRoleManager(IRoleStore<IdentityRole<int>> store,
    //        IEnumerable<IRoleValidator<IdentityRole<int>>> roleValidators,
    //        ILookupNormalizer keyNormalizer,
    //        IdentityErrorDescriber errors,
    //        ILogger<RoleManager<IdentityRole<int>>> logger) :
    //        base(store, roleValidators, keyNormalizer, errors, logger)
    //    {

    //    }
    //}

    //public class CustomEmailStore : IUserEmailStore<User>
    //{

    //}
}
