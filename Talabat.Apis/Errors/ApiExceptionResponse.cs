﻿namespace Talabat.Apis.Errors
{
    public class ApiExceptionResponse : ApiResponse
    {
        public string? Details { get; set; }
        public ApiExceptionResponse(int statusCode, string? message = null, string? details = null)
            : base(statusCode = 500, message)
        {
            Details = details;
        }
    }

}
