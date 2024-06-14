using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;

namespace Talabat.Apis.Extensions
{
    public static class UserManagerExtensions
    {

        public static async Task<ApplicationUser> FindUserIncludingAddressAsync
            (this UserManager<ApplicationUser> userManager, ClaimsPrincipal User)
        {

            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var appUser = await userManager.Users.Include(user => user.Address)
                .SingleOrDefaultAsync(user => user.Email == userEmail);

            return appUser;

        }
    }
}
