using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace NorthWind.Sales.Backend.Presenters.Extensions;

public static class HttpContextExtensions
{
    public static async ValueTask WriteProblemDetails(this HttpContext context, ProblemDetails details)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = details.Status.Value;

        var stream = context.Response.Body;
        await JsonSerializer.SerializeAsync(stream, details);
    }
}