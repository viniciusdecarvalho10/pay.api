using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using Pay.Api.Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Pay.Api.Core.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = context.Request.ContentType;

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        var errorDetails = GetErrorDetails(contextFeature);
                        context.Response.StatusCode = (int)errorDetails.statusCode;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(errorDetails.ToString());
                    }
                });
            });
        }
        private static ErrorDetails GetErrorDetails(IExceptionHandlerFeature contextFeature)
        {
            var error = contextFeature.Error;
            var errorDetails = ErrorDetails.Create();
            errorDetails.statusCode = (int)GetErrorCode(error);

            if (error is FluentValidation.ValidationException validationException)
            {
                if (validationException.Errors.Any())
                    errorDetails.messages.AddRange(validationException.Errors.Select(err => err.ErrorMessage));
                else
                    errorDetails.messages.Add(validationException.Message);
            }
            else if (error is Exception exception)
            {
                errorDetails.messages.Add(exception.Message);
                var messageException = error.InnerException?.Message;
                if (messageException != null && messageException != String.Empty)
                {
                    errorDetails.messages.Add(error.InnerException?.Message);
                }
            }
            else
            {
                errorDetails.errorCode = "INTERNAL_SERVER_ERROR";
                errorDetails.messages.Add(contextFeature.Error.Message);
            }
            return errorDetails;
        }
        private static HttpStatusCode GetErrorCode(Exception e)
        {
            switch (e)
            {
                case FluentValidation.ValidationException _:
                    return HttpStatusCode.PreconditionFailed;
                case ValidationException _:
                    return HttpStatusCode.BadRequest;
                case FormatException _:
                    return HttpStatusCode.BadRequest;
                case AuthenticationException _:
                    return HttpStatusCode.Forbidden;
                case NotImplementedException _:
                    return HttpStatusCode.NotImplemented;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}