using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Wepsys.DianaHr.WebApi.ExceptionHandler
{
    [ExcludeFromCodeCoverage]
    internal static class ExceptionMiddlewareExtensions
    {
        private const string DefaultErrorMessage = "Error processing your request, please contact the administrator.";
        private const string ContentType = "application/json";
        internal static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = ContentType;
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature is null)
                    {
                        await BuildResponse(context, (int)HttpStatusCode.InternalServerError,
                            DefaultErrorMessage);
                        return;
                    }

                    int statusCode = (int)HttpStatusCode.BadRequest;

                    string message = contextFeature.Error is
                        ArgumentException or
                        ArgumentOutOfRangeException or
                        InvalidOperationException or
                        FormatException ? contextFeature.Error.Message : SetStatusCodeAndReturnError(out statusCode);

                    await BuildResponse(context, statusCode, message);
                });
            });
        }

        private static string SetStatusCodeAndReturnError(out int statusCode)
        {
            statusCode = (int)HttpStatusCode.InternalServerError;
            return DefaultErrorMessage;
        }

        private static async Task BuildResponse(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(new { Message = message });
        }
    }
}
