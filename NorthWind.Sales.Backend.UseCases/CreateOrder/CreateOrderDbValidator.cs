using NorthWind.Sales.Backed.BusinessObjects.Interfaces.Repositories;
using NorthWind.Sales.Backed.BusinessObjects.ValueObjects;
using NorthWind.Sales.Backend.UseCases.Resources;
using NorthWind.Sales.Entities.Dtos;
using NorthWind.Sales.Entities.Interfaces.Common;
using NorthWind.Sales.Entities.ValueObjects;

namespace NorthWind.Sales.Backend.UseCases.CreateOrder;

public class CreateOrderDbValidator : IModelValidator<CreateOrderDto>
{
    private readonly IQueriesRepository _queriesRepository;
    private readonly IList<ValidationError> _errors = new List<ValidationError>();
    public IEnumerable<ValidationError> Errors => _errors;

    public CreateOrderDbValidator(IQueriesRepository queriesRepository)
    {
        _queriesRepository = queriesRepository;
        
    }

    
    public async Task<bool> Validate(CreateOrderDto model) =>
        await ValidateCustomer(model) &&
        await ValidateProducts(model);

    private async Task<bool> ValidateProducts(CreateOrderDto model)
    {
        IList<ProductUnitsInStock> requiredQuantities = model.OrderDetails.GroupBy(x => x.ProductId)
            .Select(d => 
                new ProductUnitsInStock(d.Key, (short)d.Sum(d => d.Quantity)))
            .ToList();

        var productIds = requiredQuantities.Select(x => x.ProductId);

        var inStockQuantities = await _queriesRepository.GetProductsUnitsInStock(productIds);

        var requiredVsInStock = requiredQuantities
            .GroupJoin(inStockQuantities, required => required.ProductId, inStock => inStock.ProductId,
                (oneRequired, manyInStock) =>  new {oneRequired, manyInStock})
            .SelectMany(groupResult => groupResult.manyInStock.DefaultIfEmpty(), 
                (groupResult, singleInStock) => new
                {
                    groupResult.oneRequired.ProductId,
                    Required = groupResult.oneRequired.UnitsInStock,
                    InStock = singleInStock?.UnitsInStock
                });

        foreach (var item in requiredVsInStock)
        {
            if (!item.InStock.HasValue)
            {
                var orderDetail = model.OrderDetails.First(x => x.ProductId == item.ProductId);
                var index = model.OrderDetails.ToList().IndexOf(orderDetail);
                var propertyName = $"{nameof(model.OrderDetails)}[{index}].{nameof(orderDetail.ProductId)}";
                _errors.Add(new ValidationError(propertyName, 
                    string.Format(CreateOrderMessages.ProductIdNotFoundErrorTemplate, item.ProductId)));
            }
            else
            {
                if (item.InStock < item.Required)
                {
                    var orderDetail = model.OrderDetails.Last(x => x.ProductId == item.ProductId);
                    var index = model.OrderDetails.ToList().IndexOf(orderDetail);
                    var propertyName = $"{nameof(model.OrderDetails)}[{index}].{nameof(orderDetail.ProductId)}";
                    _errors.Add(new ValidationError(propertyName,
                        string.Format(CreateOrderMessages.UnitInStockLessThanQuantityErrorTemplate, 
                            item.Required, item.InStock, item.ProductId)));
                }
            }
        }

        return !_errors.Any();

    }

    private async Task<bool> ValidateCustomer(CreateOrderDto model)
    {
        var currentBalance = await _queriesRepository
            .GetCustomerCurrentBalance(model.CustomerId);
        if (currentBalance == null)
        {
            _errors.Add(new ValidationError(nameof(model.CustomerId), CreateOrderMessages.CustomerIdNotFound));
        }
        else if (currentBalance > 0)
        {
            _errors.Add(new ValidationError(nameof(model.CustomerId), 
                string.Format(CreateOrderMessages.CustomerWIthBalanceErrorTemplate, model.CustomerId, currentBalance)));
        }

        return !_errors.Any();
    }
}
