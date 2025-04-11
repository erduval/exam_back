using Examen.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Text.Json;

namespace Examen.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IStringLocalizer _localizer;

        public ErrorHandlingMiddleware (RequestDelegate next, IStringLocalizerFactory localizerFactory)
        {
            _next = next;
            _localizer = localizerFactory.Create("Errors", typeof(Program).Assembly.FullName);
        }

        public async Task InvokeAsync (HttpContext context)
        {
            var culture = context.Request.Headers["Accept-Language"].ToString();
            if (!string.IsNullOrEmpty(culture))
            {
                try { CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = new CultureInfo(culture); }
                catch { /* culture invalide = fallback */ }
            }

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync (HttpContext context, Exception ex)
        {
            ProblemDetails problem;
            int statusCode;

            switch (ex)
            {
                case UnauthorizeException ue:
                    statusCode = StatusCodes.Status400BadRequest;
                    problem = new ProblemDetails
                    {
                        Type = $"https://example.com/errors/{ue.Code}",
                        Title = _localizer[ue.Code],
                        Status = statusCode,
                        Instance = context.Request.Path
                    };
                    break;

                case CustomErrorMessageException ce:
                    statusCode = StatusCodes.Status400BadRequest;
                    problem = new ProblemDetails
                    {
                        Type = $"https://example.com/errors/{ce.Code}",
                        Title = _localizer[ce.Code],
                        Status = statusCode,
                        Instance = context.Request.Path
                    };
                    break;
                case ElementNotFoundExcetion elementNotFoundExcetion:
                    statusCode = StatusCodes.Status404NotFound;
                    problem = new ProblemDetails
                    {
                        Type = $"https://example.com/errors/{elementNotFoundExcetion.Code}",
                        Title = _localizer[elementNotFoundExcetion.Code],
                        Status = statusCode,
                        Instance = context.Request.Path
                    };
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    problem = new ProblemDetails
                    {
                        Type = "https://example.com/errors/unexpected",
                        Title = _localizer["generic.error"],
                        Status = statusCode,
                        Detail = ex.Message,
                        Instance = context.Request.Path
                    };
                    break;
            }

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/problem+json";

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            await context.Response.WriteAsync(JsonSerializer.Serialize(problem, options));
        }
    }
}
