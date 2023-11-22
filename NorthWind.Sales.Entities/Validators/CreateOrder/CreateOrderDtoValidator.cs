using FluentValidation;
using NorthWind.Sales.Entities.Dtos;
using NorthWind.Sales.Entities.Validators.Common;

namespace NorthWind.Sales.Entities.Validators.CreateOrder;

internal class CreateOrderDtoValidator : ValidatorBase<CreateOrderDto>
{
    public CreateOrderDtoValidator()
    {
        RuleFor(c => c.CustomerId)
            .NotEmpty()
            .WithMessage(CreateOrderMessages.CustomerIdRequired)
            .Length(5)
            .WithMessage(CreateOrderMessages.CustumerIdRequiredLength);

        RuleFor(c => c.ShipAddress)
            .NotEmpty()
            .WithMessage(CreateOrderMessages.ShipAddressRequire)
            .MaximumLength(60)
            .WithMessage(CreateOrderMessages.ShipAddressMaximumLength);

        RuleFor(c => c.ShipCity)
            .NotEmpty()
            .WithMessage(CreateOrderMessages.ShipCityRequired)
            .MaximumLength(15)
            .WithMessage(CreateOrderMessages.ShipCityMaximumLength)
            .MinimumLength(3)
            .WithMessage(CreateOrderMessages.ShipCityMinimumLength);

        RuleFor(c => c.ShipCountry)
            .NotEmpty()
            .WithMessage(CreateOrderMessages.ShipCountryRequired)
            .MaximumLength(15)
            .WithMessage(CreateOrderMessages.ShipCountryMaximumLength)
            .MinimumLength(3)
            .WithMessage(CreateOrderMessages.ShipCountryMinimumLength);

        RuleFor(c => c.ShipPostalCode)
            .MaximumLength(10)
            .WithMessage(CreateOrderMessages.ShipPostalCodeMaximumLength);

        RuleFor(c => c.OrderDetails)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage(CreateOrderMessages.OrderDetailsRequired)
            .NotEmpty()
            .WithMessage(CreateOrderMessages.OrderDetailsNotEmpty);

        RuleForEach(c => c.OrderDetails)
            .SetValidator(new CreateOrderDetailDtoValidator());

    }
}