using NorthWind.Sales.Entities.ValueObjects;

namespace NorthWind.Sales.Backed.BusinessObjects.Exceptions;

public class ValidationException : Exception
{
    public IEnumerable<ValidationError> Errors { get; set; }

    public ValidationException()
    {
        
    }

    public ValidationException(string message): base(message)
    {
        
    }

    public ValidationException(string message, Exception innerException): base (message, innerException)
    {
        
    }

    public ValidationException(IEnumerable<ValidationError> errors) => Errors = errors;
}
