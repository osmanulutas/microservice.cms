using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace Microservice.Content.API.SeedWork.ProblemDetails
{
    public class FluentValidatorExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<FluentValidatorExceptionHandler> _logger;

        public FluentValidatorExceptionHandler(ILogger<FluentValidatorExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is ValidationException validationException)
            {
                _logger.LogError(exception, "Validation error occurred");

                var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation Error",
                    Detail = "One or more validation errors occurred.",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                };

                var errors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                problemDetails.Extensions["errors"] = errors;

                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

                return true;
            }

            return false;
        }
    }
}
