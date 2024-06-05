using System.Net;
using System.Text.Json;
using Talabat.Apis.Errors;

namespace Talabat.Apis.MiddleWares
{
    // to make middleWare by conventsion : I must follow these steps .
    // 1. Class must End With middleWare . 
    // 2. inject in Constructor object From class RequestDelegate ,ILogger<ExceptionMiddleware>,IHostEnvironment.
    // 3. make method with this signature :  public async Task InvokeAsync(HttpContext httpContext)
    // 4. will be the first middleware in pipeline

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionMiddleware> _logger;

        private readonly IHostEnvironment _hostEnvironment;

        public ExceptionMiddleware(RequestDelegate next, // pointer to the next middleware . 
            ILogger<ExceptionMiddleware> logger, // to log error at the level of this class .
            IHostEnvironment hostEnvironment // to make check at Environment
            )
        {
            _next = next;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            // At The Request 
            //1 make the request go to the next middle ware . 
            try
            {
                // here i mkae invoke to the next middleware.
                await _next(httpContext);

            }
            // if he can't move to the next middleware
            catch (Exception ex)
            {
                //1.log Execption .

                _logger.LogError(ex, ex.Message); // In case i'm in the development 
                // Log Exception in (Database | Files) \\ in The Production .

                // response Header
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // response body [3 Property = Statucode , message ,Exceptiondetails (i need class for this.)]
                ApiExceptionResponse response;

                // i must make Check At Environment  
                if (_hostEnvironment.IsDevelopment())
                    response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString());

                // if i am in another exception 
                else
                    response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

                // i must convert property in json => camelCase 
                // to Enable Front To interact with json file .
                var jsonOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, jsonOptions);

                // body
                await httpContext.Response.WriteAsync(json);
            }
        }

    }
}
