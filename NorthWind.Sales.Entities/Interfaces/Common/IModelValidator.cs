using NorthWind.Sales.Entities.ValueObjects;

namespace NorthWind.Sales.Entities.Interfaces.Common;

public interface IModelValidator <TModel>
{
    Task<bool> Validate(TModel  model);
    IEnumerable<ValidationError> Errors { get; }
}
