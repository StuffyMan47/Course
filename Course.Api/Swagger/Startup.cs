using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Course.Infrastructure.Swagger;

public static class Startup
{
    public const string ApplicationTitle = "Course";
    public const string AnonymousGroupName = "anonymous";
    public const string AuthorizedGroupName = "authorized";

    public static IServiceCollection AddSwaggerBuilder(this IServiceCollection services)
    {

        services.AddFluentValidationRulesToSwagger();

        services.AddSwaggerGen(setup =>
        {

            setup.SwaggerDoc(
                AnonymousGroupName,
                new()
                {
                    Title = ApplicationTitle,
                    Version = AnonymousGroupName
                }
            );
            setup.SwaggerDoc(
                AuthorizedGroupName,
                new()
                {
                    Title = ApplicationTitle,
                    Version = AuthorizedGroupName
                }
            );
            setup.AddSecurityDefinition("Bearer", new()
            {
                Description =
                    "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Cookie,
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            setup.AddSecurityRequirement(new()
            {
                {
                    new()
                    {
                        Reference = new()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            // setup.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Course.Api.xml"));
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerBuilder(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/swagger/{AnonymousGroupName}/swagger.json", AnonymousGroupName);
            c.SwaggerEndpoint($"/swagger/{AuthorizedGroupName}/swagger.json", AuthorizedGroupName);
        });

        return app;
    }
}