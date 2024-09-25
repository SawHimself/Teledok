using Entities;
using Microsoft.AspNetCore.Diagnostics;
using Services.LoggerService;
using System.Net;

namespace TeledokWebAPI.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");

                        if (contextFeature.Error is ArgumentException argumentException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            await context.Response.WriteAsync(new ErrorDetails()
                            {
                                StatusCode = (int)HttpStatusCode.BadRequest,
                                Message = argumentException.Message,
                            }.ToString());
                        }
                        else
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            await context.Response.WriteAsync(new ErrorDetails()
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = "Internal Server Error.",
                            }.ToString());
                        }
                    }
                });
            });
        }
    }
}
