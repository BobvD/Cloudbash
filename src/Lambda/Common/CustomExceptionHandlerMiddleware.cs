using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Cloudbash.Application.Common.Exceptions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using ValidationException = Cloudbash.Application.Common.Exceptions.ValidationException;
using Amazon.Lambda.Core;

namespace Cloudbash.Lambda.Common
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            LambdaLogger.Log("Middleware Invoked");

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var lambdaContext = context.Items["LambdaContext"] as ILambdaContext;
                lambdaContext?.Logger.LogLine("Validation Exception thrown");                
                await HandleExceptionAsync(context, ex);
            }
        }


        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
          
            var code = HttpStatusCode.InternalServerError;

            var result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(validationException.Failures);
                    break;
                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (string.IsNullOrEmpty(result))
            {
                result = JsonConvert.SerializeObject(new { error = exception.Message });
            }

            return context.Response.WriteAsync(result);

        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}