// ReSharper disable IdentifierTypo

using NorthWind.Sales.Backed.BusinessObjects.Enums;
using NorthWind.Sales.Backend.UseCases.CreateOrder;
using NorthWind.Sales.Backend.UseCases.Tests.Fakes;
using NorthWind.Sales.Entities.Dtos;

namespace NorthWind.Sales.Backend.UseCases.Tests;

public class CreateOrderInteractorTests
{
    [Fact]
    public async Task CreateOrder_ReturnsIdGreatherThanZero()
    {
        //Arrange
        var stubRepository = new RepositoryFake();
        var mockPresenter = new PresenterFake();
        var createOrderDetailsDto = new List<CreateOrderDetailsDto>
        { 
            new(1,1,20)
        };
        var order = new CreateOrderDto("CUSTOMER_ID",
            "address",
            "city",
            "Mexico",
            "08000",
            createOrderDetailsDto);
        var createOrderInteractor = new CreateOrderInteractor(mockPresenter, stubRepository, null!);
        //Act
        await createOrderInteractor.Handle(order);
        //Test
        Assert.True(mockPresenter.OrderId > 0);
        Assert.True(mockPresenter.Order.ShippingType == ShippingType.Road);
    }
}
