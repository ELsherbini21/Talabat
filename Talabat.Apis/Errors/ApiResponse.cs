using Microsoft.AspNetCore.Mvc.Formatters;

namespace Talabat.Apis.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statusCode, string? message = null)
        {
            this.StatusCode = statusCode;
            this.Message = message ?? GetDefaultMessageDependOnStatusCode(statusCode);
        }

        private string? GetDefaultMessageDependOnStatusCode(int statusCode)
        {
            return statusCode switch
            {

                400 => "A bad request , you have made ",
                401 => "You are not authorized",
                404 => "Resource was not found",
                500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate. Hate to career change",
                _ => null
            };
        }
    }

    
}
