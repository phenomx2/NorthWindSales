using NorthWind.Sales.Backend.EFCore.Options;
using NorthWind.Sales.Backend.IoC;
using NorthWind.Sales.Backend.Presenters.Extensions;

namespace NorthWind.Sales.WebApi;

public static class Startup
{
    public static WebApplication CreateWebApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddNorthWindSalesServices(dbOptions => 
            builder.Configuration.GetSection(DbOptions.SectionKey).Bind(dbOptions));
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(config =>
            {
                config.AllowAnyHeader();
                config.AllowAnyMethod();
                config.AllowAnyOrigin();
            });
        });
        return builder.Build();
    }

    public static WebApplication ConfigureWebApplication(this WebApplication app)
    {
        app.UseExceptionHandler(builder => {});
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapNorthWindSalesEndpoints();
        app.UseCors();
        return app;
    }
}
