using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Coupons.Database
{

    public class User : IdentityUser<int>
    {
        [Required]
        [RegularExpression("[A-Z][a-z]+\\s[A-Z][a-z]+\\s[A-Z][a-z]+")]
        [StringLength(40, MinimumLength = 8)]
        public override string? UserName { get; set; }

        [Required]
        [RegularExpression("/([+]359[0-9]{8})|(02[0-9]{7})|(08(0|9|8)([0-9]{7,8}))/gm")]
        public override string? PhoneNumber { get; set; }

        [StringLength(100)]
        [RegularExpression("/[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/g")]
        public override string? Email { get; set; }

        public User(/*string password,*/ string email, string phoneNumber, string userName)
        {
            this.UserName = userName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            //this.PasswordHash = password; 
        }
    }
}

