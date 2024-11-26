using CatsAndPies.Domain.Enums;
using CatsAndPies.Domain.Exceptions.Items;
using CatsAndPies.Domain.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Exceptions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            BaseResponse<object> response;
            try
            {
                await _next(context);
            }
            catch(RequestHandlingException ex)
            {
                response = new()
                {
                    StatusCode = ex.StatusCode,
                    MessageForUser = ex.MessageForUser,
                };
                if (ex.AdditionalData != null)
                    response.Data = ex.AdditionalData;

                context.Response.StatusCode = (int)ex.StatusCode;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Exception caught in middleware. Path: {Path}", context.Request.Path);
                _logger.LogError(ex.Message);

                response = new()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Data = null,
                    MessageForUser = "Что-то пошло не так. Попробуйте позже"
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
