using FluentValidation;
using NorthWind.Sales.Entities.Interfaces.Common;
using NorthWind.Sales.Entities.ValueObjects;

namespace NorthWind.Sales.Entities.Validators.Common;

internal abstract class ValidatorBase<TModel> : AbstractValidator<TModel>, IModelValidator<TModel>
{
    public IEnumerable<ValidationError> Errors { get; private set; }

    async Task<bool> IModelValidator<TModel>.Validate(TModel model)
    {
        var result = await ValidateAsync(model);
        if (!result.IsValid)
        {
            Errors = result.Errors
                .Select(e => new ValidationError(e.PropertyName, e.ErrorMessage));
        }
        return result.IsValid;
    }
}
