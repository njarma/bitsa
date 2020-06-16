using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace API.Exceptions
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                _logger.LogInformation(String.Format("BEGIN request to: {0}", httpContext.Request.Path));
                await _next(httpContext);
                _logger.LogInformation("END request.");
            }
            catch (HttpStatusCodeException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, HttpStatusCodeException exception)
        {
            ErrorDetail result;
            context.Response.Clear();
            context.Response.ContentType = "application/json";

            if (exception is HttpStatusCodeException)
            {
                result = new ErrorDetail() { Message = exception.Message, StatusCode = (int)exception.StatusCode };
                context.Response.StatusCode = (int)exception.StatusCode;
                _logger.LogError(String.Format("1-Middleware Exception: {0} - {1}", context.Response.StatusCode, result.Message));
            }
            else
            {
                result = new ErrorDetail() { Message = "Runtime Error", StatusCode = (int)HttpStatusCode.BadRequest };
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                _logger.LogError(String.Format("2-Middleware Exception: {0} - {1}", context.Response.StatusCode, result.Message));
            }

            await context.Response.WriteAsync(result.ToString());
            return;
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ErrorDetail result;
            context.Response.Clear();
            context.Response.ContentType = "application/json";

            result = new ErrorDetail() { Message = exception.Message, StatusCode = (int)HttpStatusCode.InternalServerError };
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            _logger.LogInformation(String.Format("3-Middleware Exception: {0} - {1}", context.Response.StatusCode, result.Message));

            await context.Response.WriteAsync(result.ToString());
            return;
        }

    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlerMiddlewareExtensions
    {

        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }

}
