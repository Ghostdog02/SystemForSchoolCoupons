using System.ComponentModel.DataAnnotations;

namespace SystemForSchoolCoupons.Models
{
    public class Transaction
    {
        [Required]
        public DateTime timeOfTransaction { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
