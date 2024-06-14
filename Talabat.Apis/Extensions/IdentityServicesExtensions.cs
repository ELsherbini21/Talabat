using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;
using Talabat.Repository.Identity;
using Talabat.Service;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Talabat.Apis.Extensions
{
    public static class IdentityServicesExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IAuthService), typeof(AuthService));

            // add service to IOC_Container [UserManager , SignInManager , RoleManager]
            // Second_overload make Configure
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {

            }).AddEntityFrameworkStores<ApplicationIdentityDbContext>();

            services.AddAuthentication(options =>// Here the default scheme .
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;// instead of bearer
            })  

                .AddJwtBearer(options => // the default scheme for this toke is bearer .
                {
                    // Configure Auth Handler . 

                    // Select Things that i want make validation at it . 
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true, // make validate at auudeince 
                        ValidAudience = configuration["JWT:ValidAudience"],

                        ValidateIssuer = true, // make validate at issuer 
                        ValidIssuer = configuration["JWT:ValidIssuer"],

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])),

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromDays(double.Parse(configuration["JWT:DurationInDays"])),


                    };

                });

            return services;
        }
    }

}
