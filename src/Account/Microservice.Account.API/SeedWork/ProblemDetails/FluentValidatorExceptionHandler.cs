using FluentValidation;
using FluentValidation.Results;
using Microservice.Account.SharedKernel.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;

namespace Microservice.Account.API.SeedWork.ProblemDetails
{
    public class FluentValidatorExceptionHandler(ILogger<FluentValidatorExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not ValidationException fluentValidationException)
            {
                return false;
            }
            logger.LogError(fluentValidationException, "Exception  ocurred: {Message}", fluentValidationException.Message);

            var problemDetails = new ApiResponse<object>()
            {
                Status = StatusCodes.Status422UnprocessableEntity,
                Title = "Validation Exception: ",
                Detail = fluentValidationException.Message,
                Data = null,
                Error = new ApiProblemDetails
                {
                    Detail = fluentValidationException.Message,
                    Type = httpContext.Request.GetDisplayUrl(),
                    Instance = httpContext.Request.Path,
                    Extensions = GetErrors(fluentValidationException.Errors),
                    ErrorType = "Validation Exception"
                }
            };
            httpContext.Response.StatusCode = problemDetails.Status;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
        private static Dictionary<string, object?> GetErrors(IEnumerable<ValidationFailure> failures)
        {
            var detectedFailures = new Dictionary<string, object?>();
            var errors = new Dictionary<string, string[]>();
            var failureGroups = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage);
            foreach (var failureGroup in failureGroups)
            {
                var propertyName = failureGroup.Key;
                var propertyFailures = failureGroup.ToArray();
                errors.Add(propertyName, propertyFailures);
            }
            detectedFailures.Add("errors", errors);
            return detectedFailures;
        }
    }
}
