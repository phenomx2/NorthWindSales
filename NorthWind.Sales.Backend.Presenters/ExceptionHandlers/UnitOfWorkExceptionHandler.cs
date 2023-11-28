using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthWind.Sales.Backed.BusinessObjects.Exceptions;
using NorthWind.Sales.Backed.BusinessObjects.Interfaces.Common;
using NorthWind.Sales.Backend.Presenters.ExceptionHandlers.Resources;

namespace NorthWind.Sales.Backend.Presenters.ExceptionHandlers;

public class UnitOfWorkExceptionHandler : IExceptionHandler<UnitOfWorkException>
{
    private readonly ILogger<UnitOfWorkExceptionHandler> _logger;

    public UnitOfWorkExceptionHandler(ILogger<UnitOfWorkExceptionHandler> logger)
    {
        _logger = logger;
    }

    public ProblemDetails Handle(UnitOfWorkException exception)
    {
        var details = new ProblemDetails();
        details.Status = StatusCodes.Status500InternalServerError;
        details.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1";
        details.Title = ExceptionMessages.UnitOfWorkExceptionTitle;
        details.Detail = ExceptionMessages.UnitOfWorkExceptionDetail;
        details.Instance = $"{nameof(ProblemDetails)}/{nameof(UnitOfWorkException)}";
        _logger.LogError(exception, ExceptionMessages.UnitOfWorkExceptionTitle + 
                                    ": " + string.Join(" ", exception.Entities));
        return details;
    }
}
