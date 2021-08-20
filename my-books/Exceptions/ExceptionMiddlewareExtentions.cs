﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using my_books.Data.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace my_books.Exceptions
{
    public static class ExceptionMiddlewareExtentions
    {
        public static void ConfigBuilderInExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context => 
                {
                    var logger = loggerFactory.CreateLogger("ConfigBuilderInExceptionHandler");
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var contextRequest = context.Features.Get<IHttpRequestFeature>();

                    if(contextFeature != null)
                    {
                        var errorVMString = new ErrorVM()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            Path = contextRequest.Path
                        }.ToString();
                        logger.LogError(errorVMString);

                await context.Response.WriteAsync(errorVMString);
                    }
                });
            });
        }

        public static void ConfigCustomInExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
