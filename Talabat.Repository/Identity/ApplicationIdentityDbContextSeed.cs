using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class ApplicationIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<ApplicationUser> _userManager)
        {
            if (_userManager.Users.Count() == 0)
            {
                var appUser = new ApplicationUser()
                {
                    DisplayName = "Elsherbini Mahmoud",
                    Email = "ElsherbiniMahmoud1999@Gmail.com",
                    UserName = "Elsherbini.Khalifa",
                    PhoneNumber = "01024041766"
                };

                await _userManager.CreateAsync(appUser, "ElsherbiniMahmoud1999");
            }
        }
    }
}
