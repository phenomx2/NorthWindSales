using Microsoft.Extensions.Logging;
using NorthWind.Sales.Backed.BusinessObjects.Interfaces.Common;

namespace NorthWind.Sales.Backend.Presenters.ExceptionHandlers;

public class ExceptionHandlerOrchestrator
{
    private readonly Dictionary<Type, object> _handlers;
    private readonly ILogger<ExceptionHandlerOrchestrator> _logger;

    public ExceptionHandlerOrchestrator(
        //[FromKeyedServices(typeof(IExceptionHandler<>))]
        IEnumerable<object> handlers, ILogger<ExceptionHandlerOrchestrator> logger)
    {
        _logger = logger;
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
}