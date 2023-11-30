using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthWind.Sales.Backend.Presenters.ExceptionHandlers.Resources;
using NorthWind.Sales.Backend.Presenters.Extensions;

namespace NorthWind.Sales.Backend.Presenters.ExceptionHandlers;

public class UnhandledExceptionHandler : IExceptionHandler
{
    private readonly ILogger<UnhandledExceptionHandler> _logger;

    public UnhandledExceptionHandler(ILogger<UnhandledExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ProblemDetails details = new ProblemDetails();
        details.Status = StatusCodes.Status500InternalServerError;
        details.Type = "https://datatracker.ietf.org/doc/hrml/rfc7231#section-6.6.1";
        details.Title = ExceptionMessages.UnhandledExceptionTitle;
        details.Detail = ExceptionMessages.UnhandledExceptionDetails;
        details.Instance = $"{nameof(ProblemDetails)}/{exception.GetType().ToString()}";
        _logger.LogError(exception, ExceptionMessages.UnhandledExceptionTitle);
        await httpContext.WriteProblemDetails(details);
        return true;
    }
}