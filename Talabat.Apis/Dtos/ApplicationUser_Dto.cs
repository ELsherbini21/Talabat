using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Talabat.Apis.Dtos
{
    public class ApplicationUser_Dto
    {
        public string DisplayName { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }
    }

    public class Login_Dto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        //[DataType(DataType.Password)]  // => there is no view .
        public string Password { get; set; }
    }
}
