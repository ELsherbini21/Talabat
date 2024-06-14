using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Diagnostics;
using Talabat.Apis.Errors;
using Talabat.Apis.Extensions;
using Talabat.Apis.Helpers;
using Talabat.Apis.MiddleWares;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Specifications;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Service;

namespace Talabat.Apis
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //StoreContext dbContext;
            //await dbContext.Database.MigrateAsync();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure Service .
            // Register Web Api Services in IOC Container. 
            builder.Services.AddControllers();

            builder.Services.AddSwaggerServices();

            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));

            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(serviceprovider =>
            {
                var ceonnection = builder.Configuration.GetConnectionString("Redis");

                return ConnectionMultiplexer.Connect(ceonnection);
            });

            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            builder.Services.AddApplicationServices();
            // this is Extension method that From  ApplicationServicesExtensions.AddApplicationServices()
            #endregion

            var app = builder.Build();
            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            #region Configure Kestrel MiddleWares .
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWares();
            }

            // make redirect for another end point 
            app.UseStatusCodePagesWithReExecute("/Errors/{0}"); // => {0} refer to position one . 

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.MapControllers();

            app.UseAuthentication();

            app.UseAuthorization();

            #endregion

            // Scope Mean => Per request.
            using var scope = app.Services.CreateScope(); // lifeTime .

            var services = scope.ServiceProvider;//IOC Container .

            // here i ask Clr to create object from class dbconte Explicitly . 
            var _dbContext = services.GetRequiredService<StoreContext>();

            // here i ask Clr to create object from class Explicitly . 
            var _identityDbContext = services.GetRequiredService<ApplicationIdentityDbContext>();

            // here i must add service Identity to the container .
            var _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            try
            {
                await _dbContext.Database.MigrateAsync();

                // after applying migration make seeding for data .
                await StoreContextSeed.SeedAsync(_dbContext);

                await _identityDbContext.Database.MigrateAsync();

                await ApplicationIdentityDbContextSeed.SeedUsersAsync(_userManager);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger<Program>();

                logger.LogError(ex, "an error has been occured during apply the migration .");
            }

            app.Run();
        }
    }
}