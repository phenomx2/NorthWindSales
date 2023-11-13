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
        var order = new CreateOrderDto()
        {
            CustomerId = "CUSTOMER_ID",
            ShipAddress = "address",
            ShipCity = "city",
            ShipCountry = "Mexico",
            ShipPostalCode = "08000",
            OrderDetails = new List<CreateOrderDetailsDto>()
            {
                new CreateOrderDetailsDto()
                {
                    ProductId = 1,
                    Quantity = 1,
                    UnitPrice = 10
                }
            }
        };
        var createOrderInteractor = new CreateOrderInteractor(mockPresenter, stubRepository);
        //Act
        await createOrderInteractor.Handle(order);
        //Test
        Assert.True(mockPresenter.OrderId > 0);
        Assert.True(mockPresenter.Order.ShippingType == ShippingType.Road);
    }
}
