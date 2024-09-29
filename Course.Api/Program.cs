using System.Text.Json.Serialization;
using Course.Api.Controllers.Utils;
using Course.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Course.Application;
using Course.Infrastructure;
using Course.Infrastructure.Swagger;

Log.Information("Application start...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // builder.AddConfigurations().RegisterSerilog();

    builder.Services
        .AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
        .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

    builder.Services
        .AddMvc(options =>
        {
            options.Conventions.Add(new ControllerNameConvention());
            options.Filters.Add(new ModelValidationFilter());
        });
    builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddSwaggerBuilder();

    builder.Services.AddWareouseApplicationLayer();
    builder.Services.AddWareouseInfrastructureLayer(builder.Configuration, builder.Environment);
    builder.Services.AddSignalR();



    var app = builder.Build();
    app.UseSwaggerBuilder(app.Environment);

    app.UseWareouseInfrastructureLayer(app.Configuration, app.Environment);

    app.MapControllers();
    
    await app.RunAsync();
}
catch (Exception ex) when (!ex.GetType().Name.Equals("HostAbortedException", StringComparison.Ordinal))
{
    Log.Fatal(ex, "Приложение не может быть запущено из-за критической ошибки");
}
finally
{
    Log.Information("Application shutdown...");
    await Log.CloseAndFlushAsync();
}