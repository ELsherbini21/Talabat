using AutoMapper;
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
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Repository;
using Talabat.Repository.Data;

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

            builder.Services.AddSingleton<IConnectionMultiplexer>();

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

            app.UseAuthorization();

            app.MapControllers();

            #endregion

            // Scope Mean => Per request.
            using var scope = app.Services.CreateScope(); // lifeTime .

            var services = scope.ServiceProvider;//IOC Container .

            var _dbContext = services.GetRequiredService<StoreContext>();
            // here i ask Clr to create object from class dbconte Explicitly . 

            try
            {
                await _dbContext.Database.MigrateAsync();

                // after applying migration make seeding for data .
                await StoreContextSeed.SeedAsync(_dbContext);
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