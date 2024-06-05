using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Errors;
using Talabat.Apis.Helpers;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;

namespace Talabat.Apis.Extensions
{
    public static class ApplicationServicesExtensions
    {
        // IServiceCollection == IOC Container . 
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // when some ask to create object IGenericRepostiory of type . 
            // ==> create object from type genericRepo of the same type .
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //builder.Services.AddScoped(typeof(ISpecifications<>), typeof(BaseSpecifications <>));

            services.AddAutoMapper(typeof(MappingProfile));




            // Handle Validation Error in 
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext =>
                {
                    // here the error of model state 
                    // The Errors of Parameter list .
                    // => here i want to get error of context that implemented , or action That executed .
                    var errors = actionContext.ModelState


                    // here i make Filter for All obj base on vlaues 
                   .Where(parameter => parameter.Value.Errors.Count() > 0)

                   // i want to select value.Errors , so that i will make Selectmany 
                   //  make iterator at Each Parameter . 
                   // Each Error is object .
                   .SelectMany(p => p.Value.Errors)

                   // here from this error , i want to select error message .
                   .Select(error => error.ErrorMessage)
                   .ToArray();

                    // here i creaet object from validation error resopnse with Value (400)
                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(validationErrorResponse);
                });
            });

            return services;
        }
    }

}
