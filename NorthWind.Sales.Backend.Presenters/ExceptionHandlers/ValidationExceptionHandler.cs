using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthWind.Sales.Backed.BusinessObjects.Exceptions;
using NorthWind.Sales.Backed.BusinessObjects.Interfaces.Common;
using NorthWind.Sales.Backend.Presenters.ExceptionHandlers.Resources;

namespace NorthWind.Sales.Backend.Presenters.ExceptionHandlers;

internal class ValidationExceptionHandler : IExceptionHandler<ValidationException>
{
    public ProblemDetails Handle(ValidationException exception)
    {
        var details = new ProblemDetails();
        details.Status = StatusCodes.Status400BadRequest;
        details.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
        details.Title = ExceptionMessages.ValidationExceptionTitle;
        details.Detail = ExceptionMessages.ValidationExceptionDetail;
        details.Instance = $"{nameof(ProblemDetails)}/{nameof(ValidationException)}";
        details.Extensions.Add("errors", exception.Message);
        return details;
    }
}