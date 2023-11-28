using System.Net.Http.Json;
using NorthWind.Sales.Entities.Dtos;
using NorthWind.Sales.Entities.ValueObjects;
using NorthWind.Sales.Frontend.BusinessObjects.Interfaces;

namespace NorthWind.Sales.Frontend.WebApiGateways;

public class CreateOrderGateway : ICreateOrderGateway
{
    private readonly HttpClient _client;

    public CreateOrderGateway(HttpClient client)
    {
        _client = client;
    }

    public async Task<int> CreateOrderAsync(CreateOrderDto order)
    {
        int orderId = 0;
        var response = await _client.PostAsJsonAsync(Endpoints.CreateOrder, order);
        if (response.IsSuccessStatusCode)
        {
            orderId = await response.Content.ReadFromJsonAsync<int>();
        }
        else
        {
            throw new HttpRequestException(await response.Content.ReadAsStringAsync());
        }
        return orderId;
    }
}
