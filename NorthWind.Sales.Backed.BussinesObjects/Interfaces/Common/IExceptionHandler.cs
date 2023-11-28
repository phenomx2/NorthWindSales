using Microsoft.AspNetCore.Mvc;

namespace NorthWind.Sales.Backed.BusinessObjects.Interfaces.Common;

public interface IExceptionHandler<TException> where TException : Exception
{
    ProblemDetails Handle(TException exception);
}
