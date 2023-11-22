using FluentValidation;
using NorthWind.Sales.Entities.Dtos;
using NorthWind.Sales.Entities.Validators.Common;

namespace NorthWind.Sales.Entities.Validators.CreateOrder;

internal class CreateOrderDetailDtoValidator : ValidatorBase<CreateOrderDetailsDto>
{
    public CreateOrderDetailDtoValidator()
    {
        RuleFor(d => d.ProductId)
            .GreaterThan(0)
            .WithMessage(CreateOrderMessages.ProductIdGreatherThanZero);

        RuleFor<int>(d => d.Quantity)
            .GreaterThan(0)
            .WithMessage(CreateOrderMessages.QuantityGreatherThanZero);

        RuleFor(d => d.UnitPrice)
            .GreaterThan(0)
            .WithMessage(CreateOrderMessages.UnitPriceGreatherThanZero);
    }
}
