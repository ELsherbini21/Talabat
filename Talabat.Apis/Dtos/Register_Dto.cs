using System.ComponentModel.DataAnnotations;

namespace Talabat.Apis.Dtos
{
    public class Register_Dto
    {
        [Required]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,20}$",
        ErrorMessage = "Password must be between 8 and 20 characters long and include at least one uppercase letter, one lowercase letter, one digit, and one special character (#?!@$ %^&*-).")]
        public string Password { get; set; }

        //[Required]
        //[Compare(nameof(Password), ErrorMessage = "This Password don't match with that you Enter sir .")]
        //public string ReWritePassword { get; set; }

    }
}
