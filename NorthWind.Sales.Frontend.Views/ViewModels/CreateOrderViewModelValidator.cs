using NorthWind.Sales.Entities.Dtos;
using NorthWind.Sales.Entities.Interfaces.Common;
using NorthWind.Sales.Entities.ValueObjects;

namespace NorthWind.Sales.Frontend.Views.ViewModels;

internal class CreateOrderViewModelValidator : IModelValidator<CreateOrderViewModel>
{
    public IEnumerable<ValidationError> Errors => _validator.Errors;
    private readonly IModelValidator<CreateOrderDto> _validator;

    public CreateOrderViewModelValidator(IModelValidator<CreateOrderDto> validator)
    {
        _validator = validator;
    }

    public Task<bool> Validate(CreateOrderViewModel model)
    {
        return _validator.Validate((CreateOrderDto)model);
    }
}