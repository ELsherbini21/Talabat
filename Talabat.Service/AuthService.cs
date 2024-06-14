using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;

namespace Talabat.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            this._configuration = configuration;
        }

        public async Task<string> CreateTokenAsync(ApplicationUser appUser)
        {

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

            var SingInCred = new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature);

            var authClaims = await CreatePrivateClaims(appUser);

            var tokenObject = await CreateJwtSecurityToken(authClaims, SingInCred);

            return new JwtSecurityTokenHandler().WriteToken(tokenObject);

        }


        // Create Private Claims related to user 
        private async Task<List<Claim>> CreatePrivateClaims(ApplicationUser appUser)
        {
            var claimsList = new List<Claim>()
            {
            new Claim(ClaimTypes.GivenName , appUser.UserName),
            new Claim(ClaimTypes.Email, appUser.Email)
            };

            var appUserRoles = await _userManager.GetRolesAsync(appUser);

            if (appUserRoles?.Count() > 0)
                foreach (var role in appUserRoles)
                    claimsList.Add(new Claim(ClaimTypes.Role, role));

            return claimsList;

        }

        private async Task<JwtSecurityToken> CreateJwtSecurityToken(List<Claim> authClaims, SigningCredentials SingInCred)
        {
            return new JwtSecurityToken(
                 audience: _configuration["JWT:ValidAudience"],
                 issuer: _configuration["JWT:ValidIssuer"],
                 expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
                 claims: authClaims,
                 signingCredentials: SingInCred

                 );// Used to built token . [This is Token Object .]
        }

        // Create Registed Claims 






    }


}

// Header 

// Payload 
// private Claims [User-Defined => change from user to another.]
// Registed Claims .
// Signautre 

