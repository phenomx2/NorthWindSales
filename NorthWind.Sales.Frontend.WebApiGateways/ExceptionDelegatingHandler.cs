using System.Text.Json;
using NorthWind.Sales.Entities.ValueObjects;

namespace NorthWind.Sales.Frontend.WebApiGateways;

public class ExceptionDelegatingHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            string errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
            string source = null;
            string message = null;
            IEnumerable<ValidationError> errors  = null;
            bool isValidProblemDetails = false;

            try
            {
                var contentType = response.Content.Headers.ContentType!.MediaType;
                var jsonResponse = JsonSerializer.Deserialize<JsonElement>(errorMessage);
                if (contentType == "application/problem+json" && TryGetProperty(jsonResponse, "instance", out JsonElement instanceValue))
                {
                    string value = instanceValue.ToString();
                    if (value.ToLower().StartsWith("problemdetails/"))
                    {
                        source = value;
                        if (TryGetProperty(jsonResponse, "title", out var titleValue))
                        {
                            message = titleValue.ToString();
                        }

                        if (TryGetProperty(jsonResponse, "detail", out var detailValue))
                        {
                            message = $"{message} {detailValue}";
                        }

                        if (TryGetProperty(jsonResponse, "errors", out var errorsValue))
                        {
                            errors = JsonSerializer.Deserialize<IEnumerable<ValidationError>>(errorsValue.ToString());
                        }

                        isValidProblemDetails = true;
                    }
                }
            }
            catch (Exception e)
            {
                
            }

            if (!isValidProblemDetails)
            {
                message = errorMessage;
                source = null;
                errors = null;
            }

            var ex = new HttpRequestException(message, null, response.StatusCode);
            ex.Source = source;
            if(errors != null)ex.Data.Add("Errors", errors);
            throw ex;
        }
        return await base.SendAsync(request, cancellationToken);
    }

    private bool TryGetProperty(JsonElement element, string propertyName, out JsonElement value)
    {
        bool found = false;
        value = default;
        var property = element
            .EnumerateObject()
            .FirstOrDefault(e => string.Compare(e.Name, propertyName, StringComparison.OrdinalIgnoreCase) == 0);

        if (property.Value.ValueKind != JsonValueKind.Undefined)
        {
            value = property.Value;
            found = true;
        }
        return found;
    }
}