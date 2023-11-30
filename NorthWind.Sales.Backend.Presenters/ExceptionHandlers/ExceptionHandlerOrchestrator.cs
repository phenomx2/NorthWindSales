using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NorthWind.Sales.Backed.BusinessObjects.Interfaces.Common;
using NorthWind.Sales.Backend.Presenters.ExceptionHandlers.Resources;
using NorthWind.Sales.Backend.Presenters.Extensions;

namespace NorthWind.Sales.Backend.Presenters.ExceptionHandlers;

public class ExceptionHandlerOrchestrator : IExceptionHandler
{
    private readonly Dictionary<Type, object> _handlers;

    public ExceptionHandlerOrchestrator(
        [FromKeyedServices(typeof(IExceptionHandler<>))]
        IEnumerable<object> handlers)
    {
        _handlers = new();
        foreach (var handler in handlers)
        {
            var exceptionType = handler
                .GetType()
                .GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IExceptionHandler<>))?
                .GetGenericArguments()[0];
            _handlers.TryAdd(exceptionType, handler);
        }
    }

    private ProblemDetails ToProblemDetails(Exception exception)
    {
        ProblemDetails details;
        if (_handlers.TryGetValue(exception.GetType(), out object handler))
        {
            Type handlerType = handler.GetType();
            details = (ProblemDetails)handlerType.
                GetMethod(nameof(IExceptionHandler<Exception>.Handle))
                .Invoke(handler, new object[]{exception});

        }
        else
        {
            details = new ProblemDetails();
            details.Status = StatusCodes.Status500InternalServerError;
            details.Type = "https://datatracker.ietf.org/doc/hrml/rfc7231#section-6.6.1";
            details.Title = ExceptionMessages.UnhandledExceptionTitle;
            details.Detail = ExceptionMessages.UnhandledExceptionDetails;
            details.Instance = $"{nameof(ProblemDetails)}/{exception.GetType().ToString()}";
        }
        
        return details;
    }

    public async Task HandleException(HttpContext context)
    {
        IExceptionHandlerFeature exceptionDetails = context.Features.Get<IExceptionHandlerFeature>();
        Exception exception = exceptionDetails.Error;

        if (exception != null)
        {
            var problemDetails = ToProblemDetails(exception);
            await context.WriteProblemDetails(problemDetails);
        }
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        bool handled = false;

        if (_handlers.TryGetValue(exception.GetType(), out object handler))
        {
            Type handlerType = handler.GetType();
            ProblemDetails details = (ProblemDetails)handlerType.
                GetMethod(nameof(IExceptionHandler<Exception>.Handle))
                .Invoke(handler, new object[] { exception });
            await httpContext.WriteProblemDetails(details);
            handled = true;
        }

        return handled;
    }
}