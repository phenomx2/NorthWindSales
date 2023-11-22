namespace NorthWind.Sales.Entities.ValueObjects;

public class ValidationError
{
    public string Message { get; set; }
    public string PropertyName { get; set; }

    public ValidationError(string propertyName, string message )
    {
        PropertyName = propertyName;
        Message = message;
    }
}
